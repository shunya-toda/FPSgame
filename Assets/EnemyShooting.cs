using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyの射撃
public class EnemyShooting : MonoBehaviour
{
    public GameObject enemyBullet; //Enemyの弾のPrefab
    public float shotSpeed; //弾速
    private float shotInterval; //射撃間隔

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    //Enemyの射撃
    public void EShotUpdate()
    {
        shotInterval += 1;

        if (shotInterval % 60 == 0)
        {
            GameObject eBullet = (GameObject)Instantiate(enemyBullet, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = eBullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);
        }
    }
}
