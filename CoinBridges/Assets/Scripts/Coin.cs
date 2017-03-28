using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour {

    private Color topColor;
    private Color bottomColor;
    public bool dragable; //true = drag and drop able
    public bool isTopCoin; //true = the top coin of a stack, means other coins can be dropped on it.
    private Vector3 position;

    public Transform CoinPrefab;

    public Coin(Transform coinPF, Color topcolor, Color botcolor, bool drag, bool istopcoin, Vector3 pos)
    {
        CoinPrefab = coinPF;
        topColor = topcolor;
        bottomColor = botcolor;
        dragable = drag;
        isTopCoin = istopcoin;
        position = pos;
        Debug.Log(pos);
        Transform instance = Instantiate(CoinPrefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);

        //dragable = true;

        //topColor = coinColors[Random.Range(0, coinColors.Length)];
        //bottomColor = coinColors[Random.Range(0, coinColors.Length)];

    }
    
	
}
