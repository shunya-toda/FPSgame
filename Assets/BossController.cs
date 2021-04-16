using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Bossの制御
public class BossController : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody myRigidbody;
    private float distance; //Bossとプレイヤーの間隔
    public int bossHp; //Bossの体力
    private Transform bossTransform;
    private BossShooting rightBossShooting; //Bossの右手からの射撃
    private BossShooting leftBossShooting; //Bossの左手からの射撃
    private float time;
    public float coolTime; //次の射撃までの時間
    bool bShot = false; //射撃許可

    // Start is called before the first frame update
    void Start()
    {
        bossTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        rightBossShooting = GameObject.Find("RightBossShooting").GetComponent<BossShooting>(); //右手の射撃のスクリプトを取得
        leftBossShooting = GameObject.Find("LeftBossShooting").GetComponent<BossShooting>(); //左手の射撃のスクリプトを取得
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーとの間隔
        distance = Vector3.Distance(transform.position, player.transform.position);

        //プレイヤーに近づく
        if(distance<50)
        {
            agent.destination = player.transform.position;
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(playerPos);

            //一定の距離まで近づいたら射撃
            if(distance<27)
            {
                agent.destination = player.transform.position;
                agent.destination = transform.position;

                time += Time.deltaTime;
                if (bShot)
                {
                    rightBossShooting.BShotUpdate();
                    leftBossShooting.BShotUpdate();
                }
                if (time >= coolTime && bShot == false)
                {
                    bShot = true;
                    time = 0;
                }
                if (time >= 2 && bShot == true)
                {
                    bShot = false;
                    time = 0;
                }
            }
        }
    }

    //ダメージ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            GetComponent<ParticleSystem>().Play();
            bossHp -= 10;
            if (bossHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
