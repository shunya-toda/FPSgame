using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Enemyの制御
public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private GameObject[] desPoints; //目的地
    private GameObject redWall; //シーン中のRedWall
    private int nextRoute = 0; //目的地を判別
    private NavMeshAgent agent;
    private Rigidbody myRigidbody;
    private float distance; //Enemyとプレイヤーの間隔
    public int hp; //Enemyの体力
    private EnemyShooting eShooting; //Enemyの射撃
    private float time;
    public float coolTime; //次の射撃までの時間
    bool eShot = false; //射撃許可

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        desPoints = GameObject.FindGameObjectsWithTag("Pillar"); //Pillarを目的地に設定
        agent.destination = desPoints[nextRoute].transform.position; //目的地を判別
        myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        redWall = GameObject.Find("RedWall"); //シーン中のRedWall
        eShooting = GameObject.Find("EnemyShooting").GetComponent<EnemyShooting>(); //Enemyの射撃のスクリプトを取得
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーとの間隔
        distance = Vector3.Distance(transform.position, player.transform.position);

        //プレイヤーに近づく
        if (distance<32)
        {
            agent.destination = player.transform.position;

            //一定の距離まで近づいたら射撃
            if (distance<20)
            {
                agent.destination = transform.position;
                Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(playerPos);

                time += Time.deltaTime;
                if (eShot)
                {
                    eShooting.EShotUpdate();
                }
                if(time>=coolTime&&eShot==false)
                {
                    eShot = true;
                    time = 0;
                }
                if(time>=2&&eShot==true)
                {
                    eShot = false;
                    time = 0;
                }
            }
        }
        else
        {
            agent.destination = desPoints[nextRoute].transform.position;
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        //目的地を巡回する
        if(other.gameObject.tag=="Pillar")
        {
            if(nextRoute<desPoints.Length-1)
            {
                nextRoute++;
            }
            else
            {
                nextRoute = 0;
            }
            agent.destination = desPoints[nextRoute].transform.position;
        }

        //ダメージ
        if (other.gameObject.tag=="bullet")
        {
            GetComponent<ParticleSystem>().Play();
            hp -= 10;
            if(hp<=0)
            {
                Destroy(gameObject);
                Destroy(redWall, 1.5f);
            }
        }
    }
}
