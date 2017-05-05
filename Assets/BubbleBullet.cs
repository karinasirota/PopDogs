using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : MonoBehaviour
{

    /*Runs Flood Fill algoritm to determine if bubbles pop */


    // Use this for initialization
    void Start()
    {
        
    }

    void Update()
    {

    }

    //when bullet collides with something
    void OnCollisionEnter(Collision collision)
    {
        //if the parent isn't null, which is pretty much everything. This just stops the annouying error
        if (collision.gameObject.transform.parent != null)
        {
            //if the bubble collided with an object on the grid
            //this is comparing the parent tag
            if (collision.gameObject.transform.parent.tag == "grid")
            {
                //stop the bubble more or less 
                //todo fix proximity
                //set all velocity equal to 0 and set it to sleep
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().Sleep();
                //make it a member of the grid 
                gameObject.transform.parent = collision.gameObject.transform.parent;
                //check neighbors to see if they are the same color. 
                FloodFillAlgoritm(gameObject);

            }
        }
    }

    //recursive DFS to 
    void FloodFillAlgoritm(GameObject bubble)
    {
        //floodfill algorithm from wikipedia

        /* Flood-fill (node, target-color, replacement-color):
            1. If target-color is equal to replacement-color, return.
            2. If the color of node is not equal to target-color, return.
            3. Set the color of node to replacement-color.
            4. Perform Flood-fill (one step to the south of node, target-color, replacement-color).
               Perform Flood-fill (one step to the north of node, target-color, replacement-color).
               Perform Flood-fill (one step to the west of node, target-color, replacement-color).
               Perform Flood-fill (one step to the east of node, target-color, replacement-color).
            5. Return.

         */

        //this is modified from the above. 

        //find everything that is within one uniyt of the sphere
        Collider[] neighbors = Physics.OverlapSphere(bubble.transform.position, 1.0f);
        //constuct list to put same game objects in 
        List<Collider> sameColors = new List<Collider>();
        int counter = 0;
        //say that you visited this node. 
        bubble.GetComponent<AllBubbleAttributes>().visited = true;
        //for each node that the bullet is touching
        foreach (Collider x in neighbors)
        {
            //if the colors are the same via tag comparison
            if (bubble.tag == x.tag)
            {
                //increase teh counter
                counter++;
                //if it's not already in the list of the same color touching bubbles
                if (!sameColors.Contains(x))
                {
                    //add it to that list
                    sameColors.Add(x);
                    //then do the same for all its unvisited neighbors, recursively. 
                    if (!x.GetComponent<AllBubbleAttributes>().visited)
                        FloodFillAlgoritm(x.gameObject);
                }

            }

        }
        //after you have idenitdied all the matching color neighbors, destory each gameobejct in that list
        //if there counter is more than one, since sameColors includes the bubble you shot 
        foreach (Collider y in sameColors)
        {
            if (counter != 1)
                Destroy(y.gameObject);
        }

        

    }
}
