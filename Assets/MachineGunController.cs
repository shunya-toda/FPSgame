using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マシンガンの制御
public class MachineGunController : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //照準を覗く
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            myAnimator.SetTrigger("Look");
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            myAnimator.SetTrigger("Look");
        }
    }
}
