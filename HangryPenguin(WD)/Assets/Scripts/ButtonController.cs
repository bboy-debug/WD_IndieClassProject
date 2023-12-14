using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;                       //allows access to SpriteRenderer
    public Sprite defaultImage;                         //references what images are used whether the button is pressed or not.
    public Sprite pressedImage;

    public KeyCode keyToPress;                          //reacts to button presses on keyboard.

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();         //allows SpriteRenderer to be on the same object that the ButtonController is on.
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))                //checks if the key is pressed.
        {
            theSR.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))                  //checks if the key is released.
        {
            theSR.sprite = defaultImage;
        }
    }
}
