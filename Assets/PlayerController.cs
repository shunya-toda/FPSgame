using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float ZSpeed = 6.0f;
    private float runSpeed = 13.0f;
    private float slidingSpeed = 17.0f;
    bool slidingFlag = false;
    private float coefficient = 0.9997f;
    private float XSpeed = 5.0f;
    private float jump = 10f;
    private float gravity = -20f;
    private float crouchHeight=0.5f;
    private float normalHeight=2.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        CameraTransform = transform.Find("Main Camera");
        this.myRigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
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

       
        velocity = new Vector3(Input.GetAxis("Horizontal") * XSpeed, velocity.y, Input.GetAxis("Vertical") * ZSpeed);
        if (runFlag)
        {
            ZSpeed = runSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(runFlag == true)
            {
                runFlag = false;
                ZSpeed = 6.0f;
            }
            else if (runFlag == false)
            {
                runFlag = true;
                slidingSpeed = 17f;
                Debug.Log(slidingSpeed);
            }
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            if (controller.height >= crouchHeight)
            {
                controller.height -= 0.02f;
            }
            else if (runFlag == true)
                {
                    ZSpeed = slidingSpeed;
                    slidingSpeed *= coefficient;
                }
        }
        else
        {
            if(controller.height<=normalHeight)
            {
                controller.height += 0.02f;
            }
        }

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = jump;
            }
        }
        velocity = transform.TransformDirection(velocity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
