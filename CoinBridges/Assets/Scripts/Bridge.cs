using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

    public bool isDragable;
    public Coin[] bridgeCoins = new Coin[2];

    public void addCoinTobridge(float coinNbr, Coin bridgeCoin)
    {
        bridgeCoins[(int)coinNbr] = bridgeCoin;
    } 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
