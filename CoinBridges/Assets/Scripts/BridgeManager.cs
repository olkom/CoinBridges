using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour {

    public CoinManager coinManager;
    public Bridge bridgePrefab;

    public Bridge addBridge(Vector3 position, bool isDragable/*, int rotation*/)
    {
        Bridge bridge = Instantiate(bridgePrefab, position + new Vector3(0, -0.1f, 0), Quaternion.identity) as Bridge;
        //bridge.transform.Rotate 
        Coin coin1 = coinManager.AddCoin(position + new Vector3(0.5f,0,0), false);
        Coin coin2 = coinManager.AddCoin(position + new Vector3(-0.5f, 0, 0), false);
        coin1.transform.SetParent(bridge.transform);
        coin2.transform.SetParent(bridge.transform);
        bridge.addCoinTobridge(0, coin1);
        bridge.addCoinTobridge(1, coin2);
        bridge.isDragable = true;

        return bridge;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
