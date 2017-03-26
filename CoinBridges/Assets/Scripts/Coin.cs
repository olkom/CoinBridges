using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour {

    // use this enum to keep track if the coin is on the board or in hand
    /*public enum Placement {
     board,
     hand 
     }
    */

    //public Color[] coinColors = { Color.green, Color.red, Color.blue, Color.yellow };
    public bool dragable; //true = drag and drop able
    private Color topColor;
    public Color bottomColor;
    public Vector3 position;


    public void newCoin(bool _dragable, Vector3 pos)
    {
        dragable = _dragable;
        position = pos;
        //topColor = coinColors[Random.Range(0, coinColors.Length)];
        //bottomColor = coinColors[Random.Range(0, coinColors.Length)];

    }

	
}
