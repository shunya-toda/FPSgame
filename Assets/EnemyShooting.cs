using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject enemyBullet;
    public float shotSpeed;
    public int shotCount = 3;
    private float shotInterval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void EShotUpdate()
    {
        shotInterval += 1;

        if (shotInterval % 60 == 0 && shotCount > 0)
        {
            shotCount -= 1;

            GameObject ebullet = (GameObject)Instantiate(enemyBullet, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = ebullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);
        }
        shotCount = 3;
    }
}
