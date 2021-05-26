using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float Speed = 7f;

    // Update is called once per frame 
    void Update()
    {
        Move();
    }

    // movement 
    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow)) // ก่ 
        {
            // "Translate" changes the value as much as the value entered in () from the current position.
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
            // Time.deltaTime is intended to make all devices (regardless of computer and OS) move at the same speed. 
        }

        if (Input.GetKey(KeyCode.DownArrow)) // ก้  
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow)) // กๆ 
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) // ก็  
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
    }
}