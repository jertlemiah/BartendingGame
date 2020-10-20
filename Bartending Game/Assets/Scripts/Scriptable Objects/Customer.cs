using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]
public class Customer : ScriptableObject
{
    string name = "Jeffrey";
    DrinkRecipe order;

    public void EvaluateDrink(GlassContents givenDrink)
    {
        //foreach (Liquid liquid in order.LiquidsList)
        //{

        //}
    //        If(correct glass)
    //Then good, extra tip
    //Else if (correct glass family)
    //Then good
    //Else
    //Then okay
    //"Hey, isn't this supposed to be served in a [glass type]?"

    //ForEach(expected ingredient)
    //{
    //            Check if drink has it
    //If specific liquor is missing, drink is bad
    //If only modification is missing, drink is okay
    //Else continue

    //Check if content is right
    //If within good margins, then good
    //If within moderate margins, then okay
    //If proportions completely wrong, then bad
    //}
    //        If(good) liquidMissing == false && contentCorrect == 1
    //{
    //            "That was perfect!"
    //Gives high tip
    //}
    //        Else If(okay)
    //        {
    //            "Something tastes wrong"
    //        Doesn't give tip
    //        }
    //        Else(bad)
    //{
    //            "I can't accept this"
    //Must remake drink
    //Won't tip on remake
    //}
    }

}
