using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マウスカーソルの状態
public class CursorState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //カーソルを透明にする
        Cursor.lockState = CursorLockMode.Locked; //カーソルを中央に固定
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
