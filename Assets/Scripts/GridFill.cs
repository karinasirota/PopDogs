using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridFill : MonoBehaviour
{

    /*creates bubble grid */

        //found a website; will cite once I refind the link
    public Transform redprefab;
    public Transform blueprefab;
    public Transform yellowprefab;
    public Transform treatprefab;
    public Transform orangeprefab;
    public Transform greenprefab;
    public Transform purpleprefab;

    public Transform[] bubbles;

    public GameObject wall;

    public int gridWidth = 11;
    public int gridHeight = 11;

    public int totalGridWidth = 11;
    public int totalGridHeight = 11;

    public float hexWidth = 1.732f;
    public float hexHeight = 1.0f;
    public float gap = 0.0f;



    Vector3 startPos;
    

    void Start()
    {
        //makes an array of bubble gameobejcts to fill out the grid
        bubbles = new Transform[7];
        bubbles[0]= redprefab;
        bubbles[1]= blueprefab;
        bubbles[2] = yellowprefab;
        bubbles[3]=treatprefab;
        bubbles[4] = orangeprefab;
        bubbles[5] = greenprefab;
        bubbles[6] = purpleprefab;
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    //adds the gap between bubbles
    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    //finds the position of the first bubble
    void CalcStartPos()
    {
        float offset = 0;
        float wallZ = wall.transform.position.y ;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, wallZ - z + 1.0f);
    }

    //finds position of all other bubbles
    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, z, 0);
    }

    //prints grid out
    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                //pics a random bubble game object and instantiates it as a transform (which is a subcategory of GameObject)
                //I'm trying to change it to game object but something is weird. Will check with Ethan.
                int ran = Random.Range(0, 7);
                int ran_treat=Random.Range(0,9);
                Transform hex;
                if(ran_treat==1){
                  hex=Instantiate(bubbles[3]) as Transform;
                }
                else{
                  hex=Instantiate(bubbles[ran]) as Transform;
                }
                Vector2 gridPos = new Vector2(x, y);
                //find the position of the bubble
                hex.transform.position = CalcWorldPos(gridPos);
                //set it as a child of the grid class
                hex.transform.parent = this.transform;
                //freeze the position so it doesn't move when hit
                hex.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

            }
        }
    }
}
