using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drink", menuName = "DrinkRecipe")]
public class DrinkRecipe : ScriptableObject
{
    public GlassType glassType;
    public List<Liquid> LiquidsList = new List<Liquid>();
    public List<string> AccentsList = new List<string>();
    // list of accents, need to figure out how I want to handle that


    public void ChangeLiquid(Liquid liquidToRemove, Liquid liquidToAdd)
    {
        //Find liquidToRemove in drink
        //LiquidType = LiquidToAdd
        foreach (Liquid liquid in LiquidsList)
        {
            if (liquid.liquidType == liquidToRemove.liquidType)
            {
                liquid.liquidType = liquidToAdd.liquidType;
                break;
            }
        }
    }

    public void ChangeVolume(Liquid liquidToChange, double newVolume)
    {
        foreach (Liquid liquid in LiquidsList)
        {
            if (liquid.liquidType == liquidToChange.liquidType)
            {
                liquid.volume = newVolume;
                break;
            }
        }
    }

    public void AddLiquid(Liquid liquidToAdd)
    {
        LiquidsList.Add(liquidToAdd);
    }

    public void RemoveLiquid(Liquid liquidToRemove)
    {
        foreach (Liquid liquid in LiquidsList)
        {
            if (liquid.liquidType == liquidToRemove.liquidType)
            {
                LiquidsList.Remove(liquid);
                break;
            }
        }
    }
}
