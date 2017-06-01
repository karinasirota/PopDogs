using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosingScene : MonoBehaviour {

	public Transform redprefab;
	public Transform blueprefab;
	public Transform yellowprefab;
	public Transform orangeprefab;
	public Transform greenprefab;
//	public Transform purpleprefab;

	public Transform[] bubbles;

    public int score;
    public string finalwords;
	// Use this for initialization
	void Start () {

		bubbles = new Transform[5];
		bubbles[0]= redprefab;
		bubbles[1]= blueprefab;
		bubbles[2] = yellowprefab;
//		bubbles[3]= purpleprefab;
		bubbles[3] = orangeprefab;
		bubbles[4] = greenprefab;

		foreach (Transform bubble in bubbles) {
			int ranX = Random.Range (0, 9);
			int ranY = Random.Range (0, 9);
			Transform hex=Instantiate (bubble) as Transform;
			Vector2 gridPos = new Vector2(ranX, ranY);
			hex.transform.position = gridPos;

			bubble.gameObject.GetComponent<Rigidbody> ().useGravity = false; 
			bubble.gameObject.GetComponent<Rigidbody>().AddForce (transform.up*10);
		}	
			

        score = PlayerPrefs.GetInt("Player Score");
        finalwords = "Your Score is: ";
        gameObject.GetComponent<UnityEngine.UI.Text>().text = finalwords + score.ToString();
    }

//	Vector3 CalcWorldPos(Vector2 gridPos)
//	{
//		float offset = 0;
//		if (gridPos.y % 2 != 0)
//			offset = hexWidth / 2;
//
//		float x = startPos.x + gridPos.x * hexWidth + offset;
//		float z = startPos.z - gridPos.y * hexHeight * 0.75f;
//
//		return new Vector3(x, z, 0);
//	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
		int ran = Random.Range (0, 5);
		Transform bubble = bubbles [ran];
		int ranX = Random.Range (-9, 9);
		int ranY = Random.Range (-9, 9);
		Transform hex=Instantiate (bubble) as Transform;
		Vector2 gridPos = new Vector2(ranX, ranY);
		hex.transform.position = gridPos;

		bubble.gameObject.GetComponent<Rigidbody> ().useGravity = false; 
		bubble.gameObject.GetComponent<Rigidbody>().AddForce (transform.up*10);
//		foreach (Transform bubble in bubbles) {
//			int ranX = Random.Range (-9, 9);
//			int ranY = Random.Range (0, 9);
//			Transform hex=Instantiate (bubble) as Transform;
//			Vector2 gridPos = new Vector2(ranX, ranY);
//			hex.transform.position = gridPos;
//
//			bubble.gameObject.GetComponent<Rigidbody> ().useGravity = true; 
//			bubble.gameObject.GetComponent<Rigidbody>().AddForce (transform.up*10);
//		}
        gameObject.GetComponent<UnityEngine.UI.Text>().text = finalwords + score.ToString();
    }
}
