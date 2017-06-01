using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        GameObject grid = GameObject.FindWithTag("grid");
        foreach (Transform child in grid.transform)
        {
            Collider[] neighbors = Physics.OverlapSphere(child.position, 1.0f);
            foreach (Collider neighbor in neighbors)
            {
                if (neighbor.tag == "bottom")
                {
//					child.gameObject.GetComponent<Rigidbody> ().useGravity = true;
					SceneManager.LoadScene(2);
                }

            }


        }
    }


}
