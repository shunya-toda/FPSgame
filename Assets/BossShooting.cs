using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bossの射撃
public class BossShooting : MonoBehaviour
{
    public GameObject bossBullet; //Bossの弾のPrefab
    public float shotSpeed; //弾速
    private float shotInterval; //射撃間隔

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Bossの射撃
    public void BShotUpdate()
    {
        shotInterval += 1;

        if (shotInterval % 60 == 0)
        {
            GameObject bBullet = (GameObject)Instantiate(bossBullet, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bBullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);
        }
    }
}
