using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;                             // determines how fast the "arrows" fall

    public bool hasStarted;                             // press a button to start making thing fall down the screen

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;                    // gives us how fast the "arrows" move per second
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)                                 
        {
            if(Input.GetKeyDown("space"))                        // checks if any button has been pressed
            {
                hasStarted = true;
            }    
        }

        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);       // (x,y,z). this moves the "arrows" along a chosen axis. this one moves arrows down the y-axis.)
        }
    }
}
