using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    //THESE VALUES ARE SET TWICE AT THE MOMENT, FIX!! (ALSO IN GAMEMANAGER SCRIPT)

    public int columns = 4;
    public int rows = 4;
    // height offset so that the coins lay flat on the ground.
    public float yoffsetBoard = 0.2f;
    public Coin[,] BoardTopCoins = new Coin[4,4];

    //private Transform coinHolder;
    public void AddTopCoin(Vector3 position, Coin topCoin)
    {
        BoardTopCoins[(int)position.x, (int)position.z] = topCoin;
    }
    public Color GetTopCoinTopColor(float x, float z)
    {
        if (((int)x > rows-1 || (int)z > columns-1) || ((int)x < 0 || (int)z < 0)) // this statement make sure to return black when asking for a position outside the coins on the board.
        {
            return Color.black;
        }
        else
        {
            return BoardTopCoins[(int)x, (int)z].TopColor;
        }
    }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
