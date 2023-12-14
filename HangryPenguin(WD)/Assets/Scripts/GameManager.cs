using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;                        //allows music

    public bool startPlaying;                           //starts music

    public BeatScroller theBS;                          //allows control of falling notes

    public static GameManager instance;

    public int currentScore;                            //allows point system
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;                       //tracks the next multiplier level
    public int[] multiplierThresholds;

    public Text scoreText;                              //allows displaying of text (from Canvas)
    public Text multiText;

    public float totalNotes;                            //allows displaying results end of game
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText; //displays on final results screen at end of game/song

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";                     //starts the game with 0 points.
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;    //calculates total notes for display
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {   
            if(Input.GetKeyDown("space"))
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }

        else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy) //detects when a song ends
            {
                resultsScreen.SetActive(true);                          //shows results screen (make sure to turn off before starting game!)

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";  //F1 = a "shortcut" built-in Unity to show a number as a float value, with one decimal place.

                string rankVal = "F";                                   // the beginning of the ranking system: F, D, C, B, A, S

                if(percentHit > 40)
                {
                    rankVal = "D";

                    if(percentHit > 55)
                    {
                        rankVal = "C";

                        if(percentHit > 70)
                        {
                            rankVal = "B";

                            if(percentHit > 85)
                            {
                                rankVal = "A";

                                if(percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;   //displays calculated multiplier UI.

        //currentScore += scorePerNote * currentMultiplier;     //adds points per hit note. (SAVE FOR LATER)
        scoreText.text = "Score: " + currentScore;              //displays Score UI.
    }
    
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;                                           //counts total number of normal hits for end results
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;                                           //counts total number of good hits for end results
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;                                           //counts total number of perfect hits for end results
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;                                           //counts total number of missed hits for end results
    }
}
