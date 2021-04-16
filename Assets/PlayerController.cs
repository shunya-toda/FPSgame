using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//プレイヤーの制御
public class PlayerController : MonoBehaviour
{
    public GameObject player; //プレイヤー
    public GameObject Camera; //カメラ
    private Transform playerTransform; 
    private Transform cameraTransform; 
    private Rigidbody myRigidbody; 
    private CharacterController controller; 
    private float yLimit; //視点の上下の制限
    private Vector3 velocity; //プレイヤーの速度
    bool runFlag = false; //走る
    private float zSpeed = 6.0f; //歩いている時のスピード
    private float runSpeed = 13.0f;　//走っている時のスピード
    private float slidingSpeed = 17.0f; //スライディング時のスピード
    private float xSpeed = 5.0f;　//横移動時のスピード
    private float jump = 12f;　//ジャンプ力
    private float gravity = -20f;　//重力
    private float crouchHeight = 0.5f;　//しゃがんだ時の高さ
    private float normalHeight = 2.0f;　//通常時の高さ
    private Shooting shooting; //射撃
    private int hp = 200;　//プレイヤーの体力値
    public GameObject hpGauge; //プレイヤーの体力ゲージ
    private Slider slider; 
    private float deadLine = -20; //ゲームオーバーになる位置
    private GameObject gameOver; //ゲームオーバーの表示
    private GameObject clear; //ゲームクリアの表示
    private BossController boss; //ボス

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>(); 
        cameraTransform = transform.Find("Main Camera"); 
        myRigidbody = GetComponent<Rigidbody>(); 
        controller = GetComponent<CharacterController>(); 
        shooting = GameObject.Find("Shooting").GetComponent<Shooting>(); //射撃のスクリプトを取得
        slider = hpGauge.GetComponent<Slider>(); //プレイヤーの体力
        gameOver = GameObject.Find("GameOverText"); //ゲームオーバーを表示するテキストを取得
        clear = GameObject.Find("ClearText"); //ゲームクリアを表示するテキストを取得
        boss = GameObject.Find("Boss").GetComponent<BossController>(); //ボスを取得
    }

    // Update is called once per frame
    void Update()
    {
        //視点移動
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        playerTransform.transform.Rotate(0, X_Rotation, 0);
        yLimit = Camera.transform.localEulerAngles.x;
        if (yLimit > 290 && yLimit < 360 || yLimit > 0 && yLimit < 70)
        {
            cameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
        }
        else
        {
            if (yLimit > 280)
            {
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    cameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
            else
            {
                if (Input.GetAxis("Mouse Y") > 0)
                {
                    cameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
        }

        //プレイヤー移動
        velocity = new Vector3(Input.GetAxis("Horizontal") * xSpeed, velocity.y, Input.GetAxis("Vertical") * zSpeed);
        velocity = transform.TransformDirection(velocity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //走る
        if (runFlag)
        {
            zSpeed = runSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(runFlag == true)
            {
                runFlag = false;
                zSpeed = 6.0f;
            }
            else if (runFlag == false)
            {
                runFlag = true;
            }
        }

        //しゃがみ、スライディング
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if (controller.height >= crouchHeight)
            {
                controller.height -= 0.02f;
            }
            else if (runFlag == true)
            {
                zSpeed = slidingSpeed;
            }
        }
        else
        {
            slidingSpeed = 17f;
            if(controller.height<=normalHeight)
            {
                controller.height += 0.02f;
            }
        }

        //接地判定、ジャンプ
        if (controller.isGrounded)
        {
            if(slidingSpeed>0)
            {
                slidingSpeed -= 0.005f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = jump;
            }
        }

        //射撃
        shooting.ShotUpdate();

        //体力
        slider.value = hp;

        //ゲームオーバー・クリア判定
        if(transform.position.y<deadLine||hp<=0)
        {
            gameOver.GetComponent<Text>().text = "GAME OVER";
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if(boss.bossHp<=0)
        {
            clear.GetComponent<Text>().text = "CLEAR!!";
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //敵からのダメージ・回復
        if (other.gameObject.tag == "EnemyBullet")
        {
            hp -= 5;
        }
        if(other.gameObject.tag=="BossBullet")
        {
            hp -= 8;
        }
        if(other.gameObject.tag=="Heal")
        {
            GetComponent<ParticleSystem>().Play();
            hp = 200;
        }
    }
}
