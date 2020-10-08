using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBottle : MonoBehaviour
{
    public InputMaster controls;

    public GlassContents currentGlass;
    public GlassContentsv5 currentGlassV5;
    public LiquidType liquidType;
    public double volumeAddedPerClick = 0.1;

    public float xOffset = 0, yOffset = 0;

    private bool mouseOver = false;
    private float waitTime = 0.2f; //wait time befor reacting
    private float downTime; //internal time from when the key is pressed

    private void Awake()
    {
        controls = new InputMaster();
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }

    //private void OnMouseDown()
    //{
    //    print(this.name);
    //    downTime = Time.time;
    //    //currentGlass.addLiquid(LiquidType, double)
    //    currentGlass.AddLiquid(liquidType, volumeAddedPerClick);
    //}
    //private void OnMouseDrag()
    //{
    //    //print(this.name);
    //    //currentGlass.addLiquid(LiquidType, double)
    //    if((Time.time > downTime + waitTime) && mouseOver)
    //    {
    //        currentGlass.AddLiquid(liquidType, volumeAddedPerClick);
    //        downTime += waitTime;
    //    }
        
    //}
    public void PourBottle()
    {
        if ((Time.time > downTime + waitTime) && mouseOver)
        {
            currentGlassV5.AddLiquid(liquidType, volumeAddedPerClick);
            currentGlass.AddLiquid(liquidType, volumeAddedPerClick);
            downTime += waitTime;
        }
    }

    public void PourBottle(double volumeAddedPerClick)
    {
        if ((Time.time > downTime + waitTime) && mouseOver)
        {
            Debug.Log("Adding " + volumeAddedPerClick + " of " + liquidType);
            currentGlassV5.AddLiquid(liquidType, volumeAddedPerClick);
            currentGlass.AddLiquid(liquidType, volumeAddedPerClick);
            downTime = Time.time + waitTime;
        }
    }

    public void SetTotalLiquid(double newVolume)
    {
        currentGlass.EmptyContents();
        currentGlass.AddLiquid(liquidType, newVolume);
    }
}
