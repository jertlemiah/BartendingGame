using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drink", menuName = "Drink")]
public class Drink : ScriptableObject
{
    public int maxVolume;
    public int currentVolume;
    public string glassType;

    // Update is called once per frame
    void Update()
    {

    }
}
