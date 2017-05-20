using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropCoin : MonoBehaviour
{
    private bool isOver;
    private bool up;
    private Vector3 startPosition;
    public Coin draggedCoin;
    public Coin dropCoin;
    private float dragY;
    private float dropY;
    private float CameraHeight;

    void Awake()
    {
        startPosition = draggedCoin.transform.position;
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
        if (draggedCoin.isDragable)
        {
            up = false;
            dragY = draggedCoin.transform.position.y;
            while (up == false)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 pos = ray.origin + (ray.direction * CameraHeight);
                draggedCoin.transform.position = pos;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    

    void OnMouseUp()
    {
        if (draggedCoin.isDragable)
        {
            up = true;
            dropY = dragY;
            //Vector3 pos = new Vector3(coin.transform.position.x, dropY, coin.transform.position.z);
            //the mathf.Round makes the coin snap to increments of 1 in the X and Z directions.
            Vector3 pos = new Vector3(Mathf.Round(draggedCoin.transform.position.x), dropY, Mathf.Round(draggedCoin.transform.position.z));
            
            //dropCoin = CoinManager.GetTopCoin(pos); <- here we will check if the coin can be placed on another coin.
            draggedCoin.transform.position = pos;
        }
    }

    public void Reset()
    {
        draggedCoin.transform.position = startPosition;
    }

    void OnGUI()
    {
        if (isOver)
        {
            GUI.Button(new Rect(Screen.width / 2+200, Screen.height / 2, 200, 20), "x: "+draggedCoin.transform.position.x + " z: "+draggedCoin.transform.position.z);
        }
    }
}