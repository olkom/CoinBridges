using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    
    Color[] coinColors = { Color.green, Color.red, Color.blue, Color.yellow, Color.black };
    public int columns = 2;
    public int rows = 2;
    public GameObject coin;

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
                //instantiate the coin at the x , z coordinates that we loop through
                GameObject instance = Instantiate(coin, new Vector3(x, 0.3f, z), Quaternion.identity) as GameObject;
                //accessing the renderers of the child and parent (bottom and top)
                Renderer[] renderers = instance.GetComponentsInChildren<Renderer>();
                //accessing the bottom renderer by looking at both renderers and 
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.gameObject.transform.parent != null) { //child renderer -> bottomcolor
                        renderer.material.color = coinColors[botColorNr];
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
