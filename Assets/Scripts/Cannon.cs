﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    /*Instantiates bubble and chooses a place for it to go */

    public GameObject redprefab;
    public GameObject blueprefab;
    public GameObject yellowprefab;
    public GameObject orangeprefab;
    public GameObject greenprefab;
    public GameObject purpleprefab;
    public GameObject cannon;

    public GameObject[] bubbles;
    public GameObject[] clusterBubbles;

	public GameObject nextBubble;

    public GridFill gf;

    // Use this for initialization
    void Start()
    {
        bubbles = new GameObject[6];
        bubbles[0] = redprefab;
        bubbles[1] = blueprefab;
        bubbles[2] = yellowprefab;
        bubbles[3] = orangeprefab;
        bubbles[4] = greenprefab;
        bubbles[5] = purpleprefab;
        //instantiate a bubble in the correct location
        SpawnBubble ();
    }


    // Update is called once per frame
    void Update()
    {
        //fire bubble when a is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
			nextBubble.GetComponent<Rigidbody>().WakeUp();
			//add a force to move it in the up vector of the cannon
			nextBubble.GetComponent<Rigidbody>().AddForce(cannon.transform.up, ForceMode.Impulse);
            //other ways to maybe move stuff-- keep for now?
            //hex.transform.position = Vector3.MoveTowards(hex.transform.position, cannon.transform.up * 5.0f, 10.0f * Time.deltaTime);
            //Physics.SphereCast(hex.transform.position, 1,cannon.transform.up,);

			//schedule this to happen after 2 seconds
			Invoke ("SpawnBubble", 1f);
        }

        //rotate cannon
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cannon.transform.Rotate(cannon.transform.forward, 20 * Time.deltaTime);
			// move bubble with the cannon
            
			nextBubble.transform.position=cannon.transform.position + cannon.transform.up;
        }

        //rotate cannon
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cannon.transform.Rotate(-cannon.transform.forward, 20 * Time.deltaTime);
			// move bubble with the cannon
			nextBubble.transform.position=cannon.transform.position + cannon.transform.up;
        }

        //makes that pink line that goes all the way to the top
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.numPositions = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.up * 20 + transform.position);
    }

	void SpawnBubble()
	{
		//instantiate next bubble
		int ran = Random.Range(0, 6);
		nextBubble= Instantiate(bubbles[ran],cannon.transform.position + cannon.transform.up, Quaternion.identity) as GameObject;
		//attach the bubble bullet script to the new bubble
		nextBubble.AddComponent<BubbleBullet>();
		nextBubble.GetComponent<Rigidbody>().Sleep();
		//want rigid body to be asleep so that bubble is attached to cannon till shot
	}
}
