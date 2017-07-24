using UnityEngine;
using System.Collections;

public class HitRegion : MonoBehaviour {

    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note;
    GameObject gmObject;
    GameManager gm;
    Color original;
    public bool createMode;
    public GameObject n;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
        // load the instance of game manager
        gmObject = GameObject.Find("GameManager");
        gm = gmObject.GetComponent<GameManager>();
        // keeping track of region's original color
        original = sr.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (createMode && Input.GetKeyDown(key))
        {
            Instantiate(n, transform.position, Quaternion.identity);
        }
        else
        {
            if (Input.GetKeyDown(key))
            {
                // play drum sound every time use presses down a note
                gm.PlayKeySound();
                // change color of hit region when key is pressed
                sr.color = new Color(original.r, original.g, original.b, 1.4f);
                // handles destroying note after it's been hit at the proper time
                if (active)
                {
                    Destroy(note);
                    active = false;
                    gm.AddScore();
                } else
                {
                    // note was missed
                    gm.ResetStreak();
                }
            }
            else if (Input.GetKeyUp(key))
            {
                // sets the hit region's color back to normal when key is released
                sr.color = original;
            }
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        // if a note enters the hit region, make it active
        if (col.gameObject.tag == "Note")
        {
            active = true;
            note = col.gameObject;
            // tracks number of notes in song
            PlayerPrefs.SetInt("TotalNotes", PlayerPrefs.GetInt("TotalNotes") + 1);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Note" && !createMode)
        {
            // note passed through hit region without being hit
            active = false;
            gm.ResetStreak();
        }
    }
}
