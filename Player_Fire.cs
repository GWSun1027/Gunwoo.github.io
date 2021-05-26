using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{
    public GameObject PlayerMissile;
    public Transform MissileLocation;
    public float FireDelay;
    private bool FireState;

    public int MissileMaxPool; // number of missile that stored in Memory pool
    private MemoryPool MPool; // Memory pool
    private GameObject[] MissileArray; // Missile array to be used in conjunction with memory pool

    private void OnApplicationQuit() // Function that called automatically when the game ends
    {
        MPool.Dispose(); // empty the Memory pool
    }

    void Start()
    {
        FireState = true; // set the control variable to true so that the missile can be fired 

        MPool = new MemoryPool(); // reset Memory pool (Objectification)

        MPool.Create(PlayerMissile, MissileMaxPool); // Create PlayerMissile as many as MissileMaxPool has

        MissileArray = new GameObject[MissileMaxPool]; // Initialize the array as well (all values ​​will be null at this time)
    }

    // Update is called once per frame
    void Update()
    {
        playerFire(); // The missile launch function is checked every each frame.
    }

    private void playerFire() // Function that launch the missile
    {
        if (FireState) // activate only when control variable is true
        {
            if (Input.GetKey(KeyCode.A)) // A key
            {
                StartCoroutine(FireCycleControl()); // activate Coroutine

                for(int i = 0; i < MissileMaxPool; i++) // https://forum.unity.com/threads/the-script-dont-inherit-a-native-class-that-can-manage-a-script.1011988/
                {
                    if (MissileArray[i] == null)
                    {
                        MissileArray[i] = MPool.NewItem(); // take new missile from Memory pool
                        MissileArray[i].transform.position = MissileLocation.transform.position; // set position of missile to launching point
                        break; // end "for" after launch
                    }
                }
            }
        }

        for(int i = 0; i < MissileMaxPool; i ++) // Whenever a missile is fired, it checks to return the missile to the memory pool.
        {
            if (MissileArray[i] && MissileArray[i].GetComponent<Collider2D>().enabled == false) // If the missile [i] is active
            {
                MissileArray[i].GetComponent<Collider2D>().enabled = true; // Activate Collider2D again
                MPool.RemoveItem(MissileArray[i]); // Return the missile to memory
                MissileArray[i] = null; // The corresponding item in the pointed to array is also made null(no value).
            }
        }
    }

    IEnumerator FireCycleControl() //Coroutine function //https://docs.unity3d.com/Manual/Coroutines.html, https://docs.unity3d.com/ScriptReference/Coroutine.html
    {
        FireState = false;
        yield return new WaitForSeconds(FireDelay); // after time of FireDelay
        FireState = true;
    }
}
