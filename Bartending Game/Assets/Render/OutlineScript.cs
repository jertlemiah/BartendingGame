using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineScript : MonoBehaviour
{
    Color tempColor;
    public InputMaster controls;
    public bool overrideOutline = false;

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Start()
    {
        
        tempColor = GetComponent<Renderer>().material.GetColor("_Color");
        tempColor.a = 0;
        GetComponent<Renderer>().material.SetColor("_Color", tempColor);
    }

    private void OnMouseOver()
    {
        if(overrideOutline == false)
        {
            Debug.Log("Mouse hovering over bottle");
            EnableHighlight();
        }
    }

    public void EnableHighlight()
    {
        tempColor = GetComponent<Renderer>().material.GetColor("_Color");
        tempColor.a = 1;
        GetComponent<Renderer>().material.SetColor("_Color", tempColor);
    }

    private void OnMouseExit()
    {
        if(overrideOutline == false)
        {
            Debug.Log("Mouse stop hovering over bottle");
            DisableHighlight();
        }
    }

    public void DisableHighlight()
    {
        tempColor = GetComponent<Renderer>().material.GetColor("_Color");
        tempColor.a = 0;
        GetComponent<Renderer>().material.SetColor("_Color", tempColor);
    }
}
