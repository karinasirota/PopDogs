using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBubbleAttributes : MonoBehaviour
{

    public bool visited;
    public bool connected;

    // Use this for initialization
    // this is on all bubble prefabs and gameobjects
    void Start () {
        //just here for the floodfill algorithm for DFS to stop infinite recursion
        visited = false;
        connected = false;
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load("Dogs Bark #5") as AudioClip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
