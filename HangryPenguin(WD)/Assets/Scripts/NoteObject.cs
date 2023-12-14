using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;     //grants particle effect to hit UI

    public AudioSource biteSource;                                  // sets up audio

    // Start is called before the first frame update
    void Start()
    {
        biteSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();                   // tells GameManger when a note is hit (SAVE FOR LATER)

                if (Mathf.Abs (transform.position.y) > 0.25)                     // calculates location hit for Hit note
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                    
                }
                
                else if(Mathf.Abs(transform.position.y) > 0.05f)            // calculates location hit for Good note
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                    
                }

                else                                                            // calculates location hit for Perfect note
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                    
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)                 // detects if arrow is over a button
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)                 // detects if arrow isn't over a button
    {
        if(gameObject.activeInHierarchy)
        {
            if (collision.tag == "Activator")
            {
                canBePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }

            if (collision.tag == "CountdownZone")
            {
                biteSource.Play();
            }
        }
    }
}
