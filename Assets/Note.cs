using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    bool called;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        called = false;
	}
	
	// Update is called once per frame
	void Update () {
        // Makes sure that notes don't start falling before song starts
	    if (PlayerPrefs.GetInt("Start") == 1 && !called)
        {
            rb.velocity = new Vector2(0, -speed);
            called = true;
        }
	}
}
