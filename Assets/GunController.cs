using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
