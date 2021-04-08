using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 3.0f);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject)
            Destroy(this.gameObject);
    }
}
