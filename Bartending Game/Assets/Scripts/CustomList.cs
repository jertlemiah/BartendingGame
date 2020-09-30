//Script name : CustomList.cs
using UnityEngine;
using System;
using System.Collections.Generic; // Import the System.Collections.Generic class to give us access to List<>

public class CustomList : MonoBehaviour
{

    //This is our custom class with our variables
    [System.Serializable]
    //public class MyClass
    public class Liquid2
    {
        //public GameObject AnGO;
        //public int AnInt;
        //public float AnFloat;
        //public Vector3 AnVector3;
        //public int[] AnIntArray = new int[0];
        public LiquidType liquidType;
        public double volume;
    }

    //This is our list we want to use to represent our class as an array.
    //public List<MyClass> MyList = new List<MyClass>(1);
    public List<Liquid2> LiquidsList = new List<Liquid2>(1);

    void AddNew()
    {
        //Add a new index position to the end of our list
        //MyList.Add(new MyClass());
        LiquidsList.Add(new Liquid2());
    }

    void Remove(int index)
    {
        //Remove an index position from our list at a point in our list array
        //MyList.RemoveAt(index);
        LiquidsList.RemoveAt(index);
    }
}