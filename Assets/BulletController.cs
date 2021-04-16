using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの弾
public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //2秒後に破棄
        Destroy(this.gameObject, 2);
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
