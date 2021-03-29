using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject gunflashPrefab;
    public float shotSpeed;
    public int shotCount = 28;
    private float shotInterval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            shotInterval += 1;
            
            if(shotInterval%30==0&&shotCount>0)
            {
                GameObject gunflash = (GameObject)Instantiate(gunflashPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y - 90, 0));
                Destroy(gunflash, 3f);
                Debug.Log(this.name);

                shotCount -= 1;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
                Destroy(bullet, 3.0f);
            }
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 28;
        }
    }
}
