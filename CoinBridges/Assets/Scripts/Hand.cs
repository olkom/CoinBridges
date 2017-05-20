using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    private int nbrOfCoins;
    private int nbrOfBridges;
    public float yoffsetHand = 0.4f;

    public int NbrOfCoins
    {
        set
        {
            nbrOfCoins = value;
        }
        get
        {
            return nbrOfCoins;
        }
    }
    public int NbrOfBridges
    {
        set
        {
            nbrOfBridges = value;
        }
        get
        {
            return nbrOfBridges;
        }
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
