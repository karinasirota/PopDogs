using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosingScene : MonoBehaviour {

    public int score;
    public string finalwords;
	// Use this for initialization
	void Start () {
        score = PlayerPrefs.GetInt("Player Score");
        finalwords = "Your Score is: ";
        gameObject.GetComponent<UnityEngine.UI.Text>().text = finalwords + score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        gameObject.GetComponent<UnityEngine.UI.Text>().text = finalwords + score.ToString();
    }
}
