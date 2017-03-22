using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    public int columns = 2;
    public int rows = 2;
    public GameObject coin;

    private Transform boardHolder;

    void BoardSetup()
    {
        boardHolder = new GameObject("BoardCoins").transform;
        for (int x= 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(coin, new Vector3(x, y, 0.1f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

	public void LayStartingCoins()
    {
        BoardSetup();
    }
}
