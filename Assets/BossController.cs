using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody myRigidbody;
    private float distance;
    public int hp;
    private Transform bossTransform;

    // Start is called before the first frame update
    void Start()
    {
        bossTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        this.myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 30)
        {
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(playerPos);
            agent.destination = player.transform.position;
            agent.destination = transform.position;
        }
        
    }
}
