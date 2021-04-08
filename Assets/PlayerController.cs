using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public GameObject Player;
    public GameObject Camera;
    private Transform PlayerTransform;
    private Transform CameraTransform;
    private float yLimit;
    private CharacterController controller;
    private Vector3 velocity;
    bool runFlag = false;
    private float zSpeed = 6.0f;
    private float runSpeed = 13.0f;
    private float slidingSpeed = 17.0f;
    private float xSpeed = 5.0f;
    private float jump = 12f;
    private float gravity = -20f;
    private float crouchHeight=0.5f;
    private float normalHeight=2.0f;
    private Shooting Shooting;

    public int hp = 200;
    private Slider Slider;
    public GameObject slider;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        CameraTransform = transform.Find("Main Camera");
        this.myRigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        Shooting = GameObject.Find("Shooting").GetComponent<Shooting>();
        Slider = slider.GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        PlayerTransform.transform.Rotate(0, X_Rotation, 0);

        yLimit = Camera.transform.localEulerAngles.x;
        if (yLimit > 290 && yLimit < 360 || yLimit > 0 && yLimit < 70)
        {
            CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
        }
        else
        {
            if (yLimit > 280)
            {
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
            else
            {
                if (Input.GetAxis("Mouse Y") > 0)
                {
                    CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
        }

        //移動
        velocity = new Vector3(Input.GetAxis("Horizontal") * xSpeed, velocity.y, Input.GetAxis("Vertical") * zSpeed);
        velocity = transform.TransformDirection(velocity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //走る
        if (runFlag)
        {
            zSpeed = runSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(runFlag == true)
            {
                runFlag = false;
                zSpeed = 6.0f;
            }
            else if (runFlag == false)
            {
                runFlag = true;
            }
        }

        //しゃがみ、スライディング
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if (controller.height >= crouchHeight)
            {
                controller.height -= 0.02f;
            }
            else if (runFlag == true)
            {
                zSpeed = slidingSpeed;
            }
        }
        else
        {
            slidingSpeed = 17f;
            if(controller.height<=normalHeight)
            {
                controller.height += 0.02f;
            }
        }

        //接地判定、ジャンプ
        if (controller.isGrounded)
        {
            if(slidingSpeed>0)
            {
                slidingSpeed -= 0.005f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = jump;
            }
        }

        //射撃
        Shooting.ShotUpdate();

        //体力
        Slider.value = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            hp -= 5;
        }
    }
}
