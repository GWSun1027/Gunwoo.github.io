using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Missile_Move : MonoBehaviour
{ 
    
    public float MoveSpeed; // Speed at which the missile flies
    public float DestroyYPos; // Where the missile disappears 
    
  
    void Start ()
    {

    }


    void Update () 
    {
        // Every frame, the missile flies in the up direction (Y-axis + direction) as much as MoveSpeed.
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

        // If the missile's position exceeds DestroyYPos, 
        if(transform.position.y >= DestroyYPos) 
        {
        GetComponent<Collider2D>().enabled = false; //eliminate missiles
        } 
    } 
}

  
