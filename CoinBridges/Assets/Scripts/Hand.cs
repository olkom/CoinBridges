using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public int NbrOfCoinsInHand = 3;
    public Coin[] handCoins = new Coin[3];
    
    
    
    public float yoffsetHand = 0.4f;

    public void addCoinToHandArray(float CoinNbr, Coin handCoin)
    {
        handCoins[(int)CoinNbr] = handCoin;
    }

    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
	void Update () {
		
	}
}
