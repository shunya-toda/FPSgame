using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Player; //プレイヤー
    public GameObject Camera; //カメラ
    private Transform PlayerTransform; //プレイヤーの位置
    private Transform CameraTransform; //カメラの位置
    private Rigidbody myRigidbody; //プレイヤーのRigidbody
    private CharacterController controller; //プレイヤーのCharacterController
    private float yLimit; //視点の上下の制限
    private Vector3 velocity; //プレイヤーの速度
    bool runFlag = false; //走る判定
    private float zSpeed = 6.0f; //歩いている時のスピード
    private float runSpeed = 13.0f;　//走っている時のスピード
    private float slidingSpeed = 17.0f; //スライディング時のスピード
    private float xSpeed = 5.0f;　//横移動時のスピード
    private float jump = 12f;　//ジャンプ力
    private float gravity = -20f;　//重力
    private float crouchHeight = 0.5f;　//しゃがんだ時の高さ
    private float normalHeight = 2.0f;　//通常時の高さ
    private Shooting Shooting; //射撃
    public int hp = 200;　//プレイヤーの体力値
    public GameObject slider; //プレイヤーの体力ゲージ
    private Slider Slider; //体力ゲージのSlider
    private float deadLine = -10; //ゲームオーバーになる位置

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>(); //プレイヤーの位置を取得
        CameraTransform = transform.Find("Main Camera"); //カメラの位置を取得
        myRigidbody = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得
        controller = GetComponent<CharacterController>(); //プレイヤーのCharacterControllerを取得
        Shooting = GameObject.Find("Shooting").GetComponent<Shooting>(); //Shootingを取得
        Slider = slider.GetComponent<Slider>(); //Sliderを取得

    }

    // Update is called once per frame
    void Update()
    {
        //視点移動
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        PlayerTransform.transform.Rotate(0, X_Rotation, 0);
        yLimit = Camera.transform.localEulerAngles.x;
        if (yLimit > 290 && yLimit < 360 || yLimit > 0 && yLimit < 70)
        {
            CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
        }
        else
        {
            if (yLimit > 280)
            {
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
            else
            {
                if (Input.GetAxis("Mouse Y") > 0)
                {
                    CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
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
        Shooting.ShotUpdate();

        //体力
        Slider.value = hp;

        /*//ゲームオーバー判定
        if(transform.position.y<deadLine||hp<=0)
        {
            GameObject.Find("Canvas").GetComponent<GameOverController>().GameOver();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        //敵からのダメージ
        if (other.gameObject.tag == "EnemyBullet")
        {
            hp -= 5;
        }
    }
}
