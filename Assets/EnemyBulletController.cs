using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyの弾
public class EnemyBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //3秒後に破棄
        Destroy(this.gameObject, 3.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        //他のオブジェクトに当たったら破棄
        if (other.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
