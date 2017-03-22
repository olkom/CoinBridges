using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public bool dragable;
    public Color topColor;
    public Color bottomColor;
    public Vector3 position;

    void newCoin(bool _dragable, Color _topColor, Color _bottomColor, Vector3 pos)
    {
        dragable = _dragable;
        topColor = _topColor;
        bottomColor = _bottomColor;
        position = pos;
    }

	
}
