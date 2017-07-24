using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    int multiplier = 1;
    int streak = 0;
    int score = 0;
    int lifepoints;
    public Text scoreText;
    public Text multText;
    public Text streakText;
    public Image healthBar;
    Color color;
    string songName;
    AudioSource drum;
    //GameObject notes;

	// Use this for initialization
	void Start () {
        scoreText.text = "0";
        multText.text = "1x";
        streakText.text = "0";
        lifepoints = 100;
        color = healthBar.color;
        // Starts song
        AudioSource song = gameObject.AddComponent<AudioSource>();
        song.clip = Resources.Load(PlayerPrefs.GetString("SongFileName")) as AudioClip;
        print(PlayerPrefs.GetString("SongFileName"));
        //notes = Resources.Load(PlayerPrefs.GetString("NotesName")) as GameObject;
        PlayerPrefs.SetInt("Start", 1);
        song.Play();
        // initialize drum sound
        drum = gameObject.AddComponent<AudioSource>();
        drum.clip = Resources.Load("soft-hitclap2") as AudioClip;
        drum.volume = 0.50f;
        // set song name
        songName = PlayerPrefs.GetString("SongName");
        // resets stats
        PlayerPrefs.SetInt("NotesHit", 0);
        PlayerPrefs.SetInt("TotalNotes", 0);
        PlayerPrefs.SetInt("HighStreak", 0);
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Sets the notes-hit streak back to 0
    public void ResetStreak()
    {
        if (streak > PlayerPrefs.GetInt(songName + " highstreak"))
            PlayerPrefs.SetInt(songName + " highstreak", streak);
        if (streak > PlayerPrefs.GetInt("HighStreak"))
            PlayerPrefs.SetInt("HighStreak", streak);
        streak = 0;
        multiplier = 1;
        // lowers your life for missing a note
        lifepoints -= 10;
        if (lifepoints <= 0)
        {
            lifepoints = 0;
            Lose();
        }
        UpdateHealthBar();
        SetGameTexts();
    }

    // Adds to total score
    public void AddScore()
    {
        // multiplier goes up by 1 every 10 notes
        multiplier = (int)(1 + streak / 10);
        // 100 points per note hit times multiplier
        score += multiplier * 100;
        streak++;
        // gain lifepoints for hitting a note
        if (lifepoints < 100)
        {
            lifepoints += 2 * multiplier;
            if (lifepoints > 100)
                lifepoints = 100;
            UpdateHealthBar();
        }
        PlayerPrefs.SetInt("NotesHit", PlayerPrefs.GetInt("NotesHit") + 1);
        // sets game texts
        SetGameTexts();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMultiplier()
    {
        return multiplier;
    }

    public int GetStreak()
    {
        return streak;
    }

    public int GetLifePoints()
    {
        return lifepoints;
    }

    void SetGameTexts()
    {
        scoreText.text = "" + score;
        multText.text = multiplier + "x";
        streakText.text = "" + streak;
    }

    public void UpdateHealthBar()
    {
        float ratio = ((float) lifepoints) / 100f;
        healthBar.rectTransform.localScale = new Vector3(1, ratio, 1);
        // gradually reddens health bar
        healthBar.color = new Color(color.r, color.g * ratio, color.b);
    }

    public void PlayKeySound()
    {
        // AudioSource.PlayClipAtPoint(drum.clip, transform.position, 0.75f);
        drum.Play();
    }

    public void Lose()
    {
        PlayerPrefs.SetInt("Start", 0);
        SceneManager.LoadScene(4);
    }

    public void Win()
    {
        PlayerPrefs.SetInt("Start", 0);
        int highscore = PlayerPrefs.GetInt(songName + " highscore");
        // checks high streak
        ResetStreak();
        // store the score for victory screen
        PlayerPrefs.SetInt("Score", score);
        // sets high score
        if (score > highscore)
            PlayerPrefs.SetInt(songName + " highscore", score);
        // for victory high score display
        PlayerPrefs.SetInt("SongHighScore", PlayerPrefs.GetInt(songName + " highscore"));
        // for victory high streak display
        PlayerPrefs.SetInt("SongHighStreak", PlayerPrefs.GetInt(songName + " highstreak"));
        // finds accuracy
        int notesHit = PlayerPrefs.GetInt("NotesHit");
        int totalNotes = PlayerPrefs.GetInt("TotalNotes");
        float accuracyFloat = (float) (notesHit) / (float) totalNotes * 100f;
        string accuracy = accuracyFloat.ToString("n2") + "%";
        PlayerPrefs.SetString("Accuracy", accuracy);
        SceneManager.LoadScene(3);
    }
}
