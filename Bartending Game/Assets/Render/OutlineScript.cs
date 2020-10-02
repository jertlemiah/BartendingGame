using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineScript : MonoBehaviour
{
    Color tempColor;
    public InputMaster controls;
    public bool overrideOutline = false;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Start()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        tempColor = spriteRenderer.material.GetColor("_Color");
        tempColor.a = 0;
        spriteRenderer.material.SetColor("_Color", tempColor);
    }

    private void OnMouseOver()
    {
        if(overrideOutline == false)
        {
            //Debug.Log("Mouse hovering over bottle");
            EnableHighlight();
        }
    }

    public void EnableHighlight()
    {
        tempColor = spriteRenderer.material.GetColor("_Color");
        tempColor.a = 1;
        spriteRenderer.material.SetColor("_Color", tempColor);
    }

    private void OnMouseExit()
    {
        if(overrideOutline == false)
        {
            //Debug.Log("Mouse stop hovering over bottle");
            DisableHighlight();
        }
    }

    public void DisableHighlight()
    {
        tempColor = spriteRenderer.material.GetColor("_Color");
        tempColor.a = 0;
        spriteRenderer.material.SetColor("_Color", tempColor);
    }
}
