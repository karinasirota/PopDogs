using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {
    public int score;

	// Use this for initialization
	void Start () {
        score = 0;
        gameObject.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }
}
