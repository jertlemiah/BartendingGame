using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Liquid// : MonoBehaviour
{
    public LiquidType liquidType;
    public double volume;

    public Liquid()
    {

    }

    public Liquid(LiquidType liquidType, double volume)
    {
        this.liquidType = liquidType;
        this.volume = volume;
    }


}
