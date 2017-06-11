using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDropCoin : MonoBehaviour
{
    public CoinManager coinManager;
    public Board board;
    public Hand hand;
    private bool isOver;
    private bool up;
    private Vector3 startPosition;
    public Coin draggedCoin;
    public Color boardCoinTopColor;
    private float dragY;
    private float dropY;
    private float CameraHeight;
    private float coinHeight;

    void Awake()
    {
        startPosition = draggedCoin.transform.position;
        CameraHeight = 5.5f; //depending on the main camera height over the board.
        coinHeight = 0.2f; //height of the gameobject, so that placed coins land on top of the other..
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
            //dropY = dragY;
            
            //Vector3 pos = new Vector3(coin.transform.position.x, dropY, coin.transform.position.z);
            //the mathf.Round makes the coin snap to increments of 1 in the X and Z directions.
            Vector3 boardPos = new Vector3(Mathf.Round(draggedCoin.transform.position.x), dragY, Mathf.Round(draggedCoin.transform.position.z));
            dropY = board.GetTopCoinHeight(boardPos.x, boardPos.z);
            boardCoinTopColor = board.GetTopCoinTopColor(boardPos.x, boardPos.z);
            Vector3 dropPos = new Vector3(Mathf.Round(draggedCoin.transform.position.x), dropY+coinHeight, Mathf.Round(draggedCoin.transform.position.z));

            if (boardCoinTopColor.Equals(draggedCoin.BotColor))
            {
                
                //draggedCoin.transform.position = pos;
                CoinPlaced(dropPos);

            } else
            {
                Reset();
            }
        }
    }
    void CoinPlaced(Vector3 dropPosition)
    {
        draggedCoin.transform.position = dropPosition; //the positioning of the dropped coin.
        board.AddTopCoin(dropPosition, draggedCoin); //Updated the board topcoins, replaced the coin below with the one you place.
        draggedCoin.isDragable = false; //After coin has been placed, it can no longer be moved.
        coinManager.AddCoin(startPosition, true);
        // Signal the hand to add new coin
        // Signal board to change topCoin!
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
            //GUI.Button(new Rect(Screen.width / 2 + 200, Screen.height / 2, 200, 20), "top: " + draggedCoin.TopColor + " bot: " + draggedCoin.BotColor);
        }
    }
}