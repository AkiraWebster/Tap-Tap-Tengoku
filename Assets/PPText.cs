using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PPText : MonoBehaviour {

    // PPText is used to fetch player data for displaying on screen

    public static int INT = 0, FLOAT = 1, STRING = 2;
    public int dataType;
    public string varName;


    void Update() {
        // specifies which data type needs to be fetched
        switch (dataType) {
            case 0:
                GetComponent<Text>().text = PlayerPrefs.GetInt(varName) + "";
                break;
            case 1:
                GetComponent<Text>().text = PlayerPrefs.GetFloat(varName) + "";
                break;
            case 2:
                GetComponent<Text>().text = PlayerPrefs.GetString(varName);
                break;
            default:
                Debug.Log("Invalid data type specified.");
                break;
        }
	}

}
