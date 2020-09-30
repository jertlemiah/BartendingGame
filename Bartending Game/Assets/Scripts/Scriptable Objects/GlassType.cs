using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Glass", menuName = "Glass Type")]
public class GlassType : ScriptableObject
{
    public int maxVolume;
    public int optimalVolume;
    public string glassName;
}
