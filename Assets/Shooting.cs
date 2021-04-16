using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //弾のPrefab
    public GameObject gunflashPrefab; //射撃光のPrefab
    public float shotSpeed; //弾速
    public int shotCount = 28; //残弾数
    private float shotInterval; //射撃間隔
    public AudioClip shotSound; //射撃音
    AudioSource audioSource; 
    private GameObject magazineText;　//残弾数の表示

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        magazineText = GameObject.Find("MagazineText");  //残弾数を表示するテキストを取得
    }


    // Update is called once per frame
    public void Update()
    {
        //残弾数を表示
        magazineText.GetComponent<Text>().text = shotCount + "/28";
    }

    //射撃
    public void ShotUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            shotInterval += 1;
            
            if(shotInterval%30==0&&shotCount>0)
            {
                shotCount -= 1;

                //弾を生成
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);

                //射撃光を生成
                GameObject gunflash = (GameObject)Instantiate(gunflashPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y - 90, 0));
                Destroy(gunflash, 0.05f);

                //射撃音を鳴らす
                ShotSE();
            }
        }
        //リロード
        else if(Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 28;
        }
    }
    //射撃音
    public void ShotSE()
    {
        audioSource.PlayOneShot(shotSound);
    }
}
