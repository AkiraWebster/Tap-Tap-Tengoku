using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour {

    // Handles loading of different data

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetSongPath(string filename)
    {
        PlayerPrefs.SetString("SongFileName", filename);
    }

    public void SetSongName(string songName)
    {
        PlayerPrefs.SetString("SongName", songName);
    }

    public void SetNotesPath(string notesName)
    {
        PlayerPrefs.SetString("NotesName", notesName);
    }
}
