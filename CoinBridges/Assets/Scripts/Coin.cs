using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public bool isDragable = false;
    
    private Color topColor;
    private Color botColor;

    public Color TopColor
    {
        get
        {
            return topColor;
        }
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

}
