using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    // Destroyer destroys all notes that were missed slightly below the Main Camera

    GameObject note;
    bool active = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (active)
        {
            Destroy(note);
            active = false;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Note")
        {
            note = col.gameObject;
            active = true;
        }
        // Checks for the invisible Victory Note signifying end of the song
        else if (col.gameObject.tag == "Victory Note")
        {
            GameObject gmObject = GameObject.Find("GameManager");
            GameManager gm = gmObject.GetComponent<GameManager>();
            gm.Win();
        }
    }
}
