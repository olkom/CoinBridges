using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropCoin : MonoBehaviour
{
    private bool isOver;
    private bool up;
    private Vector3 startPosition;
    public Coin coin;
    private float dragY;
    private float dropY;
    private float CameraHeight;

    void Awake()
    {
        startPosition = coin.transform.position;
        CameraHeight = 5.5f; //depending on the main camera height over the board.
    }

    void OnMouseEnter()
    {
        isOver = true;
    }

    void OnMouseExit()
    {
        isOver = false;
    }

    IEnumerator OnMouseDown()
    {
        if (coin.isDragable)
        {
            up = false;
            dragY = coin.transform.position.y;
            while (up == false)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 pos = ray.origin + (ray.direction * CameraHeight);
                coin.transform.position = pos;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void OnMouseUp()
    {
        if (coin.isDragable)
        {
            up = true;
            dropY = dragY;
            Vector3 pos = new Vector3(coin.transform.position.x, dropY, coin.transform.position.z);
            coin.transform.position = pos;
        }
    }

    public void Reset()
    {
        coin.transform.position = startPosition;
    }

    void OnGUI()
    {
        if (isOver)
        {
            GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 200, 20), "Left Click and drag to move");
        }
    }
}