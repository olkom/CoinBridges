using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDropCoin : MonoBehaviour
{
    public CoinManager coinManager;
    public BridgeManager bridgeManager;
    public Board board;
    public Hand hand;
    private bool isOver;
    private bool up;
    private Vector3 startPosition;
    private Vector3 BridgeStartPosition;
    public Coin draggedCoin;
    public Color boardCoinTopColor;
    public Color boardCoinTopColor1;
    public Color boardCoinTopColor2;
    public Bridge draggedBridge;
    
    private float dragY;
    private float dropY;
    private float dropY1;
    private float dropY2;
    private float CameraHeight;
    private float coinHeight;
    private float bridgeHeight;
    private string coinOrBridgeType;

    void Awake()
    {
        
        startPosition = transform.position;
        coinOrBridge();
        CameraHeight = 5.5f; //depending on the main camera height over the board.
        coinHeight = 0.2f; //height of the gameobject, so that placed coins land on top of the other..
        bridgeHeight = 0.1f;
    }
    void coinOrBridge()
    {
        
        if (transform.GetComponent<Bridge>())
        {
            coinOrBridgeType = "bridge";
            
        }
        else
        {
            coinOrBridgeType = "coin";
            
        }
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
        if (coinOrBridgeType == "coin")
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
        } else if (coinOrBridgeType == "bridge") { 
            if (draggedBridge.isDragable)
            {
                up = false;
                dragY = draggedBridge.transform.position.y;
                while (up == false)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector3 pos = ray.origin + (ray.direction * CameraHeight);
                    draggedBridge.transform.position = pos;
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
    

    void OnMouseUp()
    {
        if (coinOrBridgeType == "coin")
        {
            if (draggedCoin.isDragable)
            {
                up = true;
                //the mathf.Round makes the coin snap to increments of 1 in the X and Z directions.
                Vector3 boardPos = new Vector3(Mathf.Round(draggedCoin.transform.position.x), dragY, Mathf.Round(draggedCoin.transform.position.z));
                dropY = board.GetTopCoinHeight(boardPos.x, boardPos.z);
                boardCoinTopColor = board.GetTopCoinTopColor(boardPos.x, boardPos.z);
                Vector3 dropPos = new Vector3(Mathf.Round(draggedCoin.transform.position.x), dropY + coinHeight, Mathf.Round(draggedCoin.transform.position.z));

                if (boardCoinTopColor.Equals(draggedCoin.BotColor))
                {
                    CoinPlaced(dropPos);
                }
                else
                {
                    Reset();
                }
            }
        }
        else if (coinOrBridgeType == "bridge")
        { 
            if (draggedBridge.isDragable)
            {
                up = true;
                //the mathf.Round makes the coin snap to increments of 1 in the X and Z directions.
                CheckBridgePlacement();
                Vector3 dropPos = GetBridgePosition();

                if (CheckBridgePlacement())
                {
                    BridgePlaced(dropPos);
                }
                else
                {
                    ResetBridge();
                }
            }
        }
        
    }
    Vector3 GetBridgePosition()
    {
        Vector3 boardPos1 = new Vector3(Mathf.Round(draggedBridge.getCoin1Position().x), dragY, Mathf.Round(draggedBridge.getCoin1Position().z));
        dropY1 = board.GetTopCoinHeight(boardPos1.x, boardPos1.z);
        boardPos1 = new Vector3(Mathf.Round(draggedBridge.getCoin1Position().x), dropY1 + bridgeHeight, Mathf.Round(draggedBridge.getCoin1Position().z));
        if (draggedBridge.vertical)
        {
            boardPos1 = boardPos1 + new Vector3(0, 0, 0.5f);
        } else
        {
            boardPos1 = boardPos1 + new Vector3(-0.5f, 0, 0);
        }
        
        return boardPos1;
    }
    bool CheckBridgePlacement()
    {
        Vector3 boardPos1 = new Vector3(Mathf.Round(draggedBridge.getCoin1Position().x), dragY, Mathf.Round(draggedBridge.getCoin1Position().z));
        Vector3 boardPos2 = new Vector3(Mathf.Round(draggedBridge.getCoin2Position().x), dragY, Mathf.Round(draggedBridge.getCoin2Position().z));
        
        dropY1 = Mathf.Round(board.GetTopCoinHeight(boardPos1.x, boardPos1.z)*100)/100; //round to two decimals to make sure no Y position is messed up.
        dropY2 = Mathf.Round(board.GetTopCoinHeight(boardPos2.x, boardPos2.z)*100)/100;
        boardCoinTopColor1 = board.GetTopCoinTopColor(boardPos1.x, boardPos1.z);
        boardCoinTopColor2 = board.GetTopCoinTopColor(boardPos2.x, boardPos2.z);
        
        if (boardCoinTopColor1.Equals(draggedBridge.bridgeCoins[0].BotColor) && boardCoinTopColor2.Equals(draggedBridge.bridgeCoins[1].BotColor) && dropY1 == dropY2)
        {
            return true;
        }
        else return false;
    }
    void CoinPlaced(Vector3 dropPosition)
    {
        draggedCoin.transform.position = dropPosition; //the positioning of the dropped coin.
        board.AddTopCoin(dropPosition, draggedCoin); //Updated the board topcoins, replaced the coin below with the one you place.
        draggedCoin.isDragable = false; //After coin has been placed, it can no longer be moved.
        coinManager.AddCoin(startPosition, true);
       
    }
    void BridgePlaced(Vector3 dropPosition)
    {
        draggedBridge.transform.position = dropPosition; //the positioning of the dropped board.
        board.AddTopCoin(new Vector3(Mathf.Round(draggedBridge.getCoin1Position().x), dragY, Mathf.Round(draggedBridge.getCoin1Position().z)), draggedBridge.bridgeCoins[0]); //Updated the board topcoins, replaced the coin below with the one you place.
        board.AddTopCoin(new Vector3(Mathf.Round(draggedBridge.getCoin2Position().x), dragY, Mathf.Round(draggedBridge.getCoin2Position().z)), draggedBridge.bridgeCoins[1]);

        draggedBridge.isDragable = false; //After bridge has been placed, it can no longer be moved.
        bridgeManager.addBridge(startPosition+new Vector3(0,bridgeHeight,0), true, !draggedBridge.vertical);
        board.AddBridgeCheckWinConditions(draggedBridge);
    }

    public void Reset()
    {
        draggedCoin.transform.position = startPosition;
    }
    public void ResetBridge()
    {
        draggedBridge.transform.position = startPosition;
    }

    void OnGUI()
    {
        /*if (isOver)
        {
            GUI.Button(new Rect(Screen.width / 2+200, Screen.height / 2, 200, 20), "x: "+draggedCoin.transform.position.x + " z: "+draggedCoin.transform.position.z);
            //GUI.Button(new Rect(Screen.width / 2 + 200, Screen.height / 2, 200, 20), "top: " + draggedCoin.TopColor + " bot: " + draggedCoin.BotColor);
        }*/
    }
}