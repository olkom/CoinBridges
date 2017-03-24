using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    //the colors of the coin sides.
    Color[] coinColors = { Color.green, Color.red, Color.blue, Color.yellow };
    // the columns, rows and CoinPrefab are set in the inspector.
    public int columns; 
    public int rows;
    public Transform CoinPrefab;
    public int startingX;
    public int startingZ;
    public int endingX;
    public int endingZ;

    private Transform boardHolder;

    void LayoutCoinsOnBoard()
    {   
        //setting up a holder for the objects to be instantiated.
        boardHolder = new GameObject("BoardCoins").transform;
        //looping through the x and z of our board
        for (int x= 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                //randomizing color for the coin about to be placed.
                int botColorNr = Random.Range(0, coinColors.Length);
                int topColorNr = Random.Range(0, coinColors.Length);
                //instantiate the coin prefab at the x , z coordinates that we loop through
                Transform instance = Instantiate(CoinPrefab, new Vector3(x, 0.3f, z), Quaternion.identity);
                //accessing the renderers of the child and parent (bottom and top)
                Renderer[] renderers = instance.GetComponentsInChildren<Renderer>();
                //accessing the bottom renderer by looking at both renderers and 
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.gameObject.transform.parent != null) { //child renderer -> bottomcolor
                        //this sets the starting and ending coins bottom color to black.
                        if ((x == startingX && z==startingZ) || (x == endingX && z== endingZ))
                        {
                            renderer.material.color = Color.black;
                        } else
                        {
                            renderer.material.color = coinColors[botColorNr];
                        }
                    } else //parent renderer -> topColor
                    {
                        renderer.material.color = coinColors[topColorNr];
                    }
                }
                //putting the new spawned coin under the boardholder parent.
                instance.transform.SetParent(boardHolder);
            }
        }
    }

	public void LayStartingCoins()
    {
        LayoutCoinsOnBoard();
    }
    //public void InitBoardTexture()
    //{

    //}
}
