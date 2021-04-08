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
    public AudioClip shotSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void ShotUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            shotInterval += 1;
            
            if(shotInterval%30==0&&shotCount>0)
            {
                shotCount -= 1;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);

                GameObject gunflash = (GameObject)Instantiate(gunflashPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y - 90, 0));
                Destroy(gunflash, 0.05f);
                Debug.Log(this.name);

                ShotSE();
            }
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 28;
        }
    }
    public void ShotSE()
    {
        audioSource.PlayOneShot(shotSound);
    }
}
