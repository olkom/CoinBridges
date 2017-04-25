using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public bool isDragable = false;
    public bool canBeDroppedOn = false;
    private Color topColor;
    private Color botColor;

    public Color TopColor
    {
        set
        {
            topColor = value;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.transform.parent == null)
                { 
                    renderer.material.color = topColor;
                }
            }
        }
        get
        {
            return topColor;
        }
    }

    public Color BotColor
    {
        get
        {
            return botColor;
        }
        set
        {
            botColor = value;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.transform.parent != null)
                { 
                        renderer.material.color = botColor;
                 }
            }
        }
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    
}
