using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private GameObject[] desPoints;
    private GameObject redWall;
    private int nextRoute = 0;
    private NavMeshAgent agent;
    private Rigidbody myRigidbody;
    private float distance;
    public int hp;
    private EnemyShooting EShooting;
    private float time;
    private float intervalTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        desPoints = GameObject.FindGameObjectsWithTag("Pillar");
        agent.destination = desPoints[nextRoute].transform.position;
        this.myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        redWall = GameObject.Find("RedWall");
        EShooting = GameObject.Find("EnemyShooting").GetComponent<EnemyShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance<32)
        {
            agent.destination = player.transform.position;
            if(distance<20)
            {
                agent.destination = transform.position;
                Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(playerPos);

                time += Time.deltaTime;
                if (time>intervalTime)
                {
                    EShooting.EShotUpdate();
                    time = 0f;
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
        if(other.gameObject.tag=="bullet")
        {
            hp -= 10;
            if(hp<=0)
            {
                Destroy(gameObject);
                Destroy(redWall, 1.5f);
            }
        }
    }
}
