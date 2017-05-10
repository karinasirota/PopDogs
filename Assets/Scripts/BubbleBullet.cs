using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : MonoBehaviour
{
    public Material gray;

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
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                //check neighbors to see if they are the same color. 
                FloodFillAlgoritm(gameObject);



            }
        }
    }

    //recursive DFS to get all neighbors, mark current node as visited and pop bubbles 
    void FloodFillAlgoritm(GameObject bubble)
    {
        //floodfill algorithm from wikipedia

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

    void OnDestroy()
    {
        findSingleBubbles();
        popSingleBubbles();
    }

    void findSingleBubbles()
    {
        GameObject grid = GameObject.FindWithTag("grid");
        List<GameObject> bubblesBB = new List<GameObject>();
        

        //double check this
        foreach (Transform child in grid.transform)
        {
            
            bubblesBB.Add(child.gameObject);

        }
        
        foreach (GameObject b in bubblesBB)
        {
            
            b.GetComponent<AllBubbleAttributes>().connected = false;
        }
        //mark all nodes as unvisited
        foreach (GameObject b in bubblesBB)
        {

            Collider[] neighbors = Physics.OverlapSphere(b.transform.position, 1.0f);
            foreach (Collider neighbor in neighbors)
            {
                if (neighbor.tag == "wall")
                {

                    b.GetComponent<AllBubbleAttributes>().connected = true;
                }

            }

            foreach (Collider neighbor in neighbors)
            {
                if (neighbor.tag != "wall")
                {
                    if (b.GetComponent<AllBubbleAttributes>().connected)
                    {
                        neighbor.gameObject.GetComponent<AllBubbleAttributes>().connected = true;
                    }
                }

            }
        }

    }

    void popSingleBubbles()
    {
        GameObject grid = GameObject.FindWithTag("grid");
        foreach (Transform child in grid.transform)
        {
            if (!child.gameObject.GetComponent<AllBubbleAttributes>().connected)
                Destroy(child.gameObject);

        }
    }

}
