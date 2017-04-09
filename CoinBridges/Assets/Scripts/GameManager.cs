using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public BoardManager boardScript;
    public CoinManager coinManager;

	// Use this for initialization
	void Awake () {
        coinManager = GetComponent<CoinManager>();
        InitGame();
	}
	
    void InitGame()
    {
        //boardScript.InitBoardTexture(); //not done yet
        coinManager.LayStartingCoins();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
