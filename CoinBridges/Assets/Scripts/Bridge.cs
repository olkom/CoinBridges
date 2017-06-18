using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

    public bool isDragable;
    public Coin[] bridgeCoins = new Coin[2];
    //public enum angle {horizontal, vertical };
    public bool vertical;
   
    public void addCoinTobridge(float coinNbr, Coin bridgeCoin)
    {
        bridgeCoins[(int)coinNbr] = bridgeCoin;
    } 
    public Vector3 getCoin1Position()
    {
        return bridgeCoins[0].transform.position;
    }
    public Vector3 getCoin2Position()
    {
        return bridgeCoins[1].transform.position;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
