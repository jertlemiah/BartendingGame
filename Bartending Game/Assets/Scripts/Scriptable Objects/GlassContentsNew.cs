using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassContentsNew : MonoBehaviour
{

    public class LiquidsCollection
    {
        //public List<Liquid> LiquidsList = new List<Liquid>();
        public Dictionary<LiquidType,Liquid> LiquidsDict = new Dictionary<LiquidType, Liquid>();
        public Color ColorMixed = Color.white;
        public List<LiquidType> LiquidTypes = new List<LiquidType>();
        public double CombinedVolume = 0;

        public LiquidsCollection(){ }

        public LiquidsCollection(Liquid liquid)
        {
            AddLiquid(liquid, -1);
        }

        public void EmptyContents()
        {
            LiquidsDict.Clear();
            LiquidTypes.Clear();
            CombinedVolume = 0;
        }

        public bool SameLiquidMix(LiquidsCollection liquidsCollection)
        {
            return SameLiquidMix(liquidsCollection.LiquidTypes);
        }

        public bool SameLiquidMix(List<LiquidType> liquidTypes)
        {
            return LiquidTypes.TrueForAll(itm => liquidTypes.Contains(itm));
        }

        ///<summary>
        /// This function is going to cause crazy rounding problems
        ///</summary>
        public void ResizeTotalVolume(double newTotalVolume)
        {
            //ahhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh

            foreach (KeyValuePair<LiquidType, Liquid> entry in LiquidsDict)
            {
                double percentVol = entry.Value.volume / CombinedVolume;
                entry.Value.volume = percentVol * newTotalVolume;
            }
            UpdateInfo();
        }

        public void Combine(LiquidsCollection newLiquidsCollection, double remainingSpace)
        {
            if(newLiquidsCollection.CombinedVolume > remainingSpace)
            {
                ResizeTotalVolume(remainingSpace);
            }
            Dictionary<LiquidType, Liquid> newLiquidsDict = newLiquidsCollection.LiquidsDict;

            foreach (KeyValuePair<LiquidType,Liquid> entry in newLiquidsDict)
            {
                AddLiquid(entry.Value, remainingSpace);
                remainingSpace -= entry.Value.volume;
            }            
        }

        ///<summary>
        ///Adds the newLiquid to the LiquidsDict with regards to the remainingSpace. 
        ///If remainingSpace = -1, this condition is ignored.
        ///</summary>
        public void AddLiquid(Liquid newLiquid, double remainingSpace)
        {
            // If there's no space, don't do anything
            if (remainingSpace == 0)
                return;

            // Ignore volume requirements
            else if (remainingSpace == -1) { }
            
            // If the incoming volume is greater than remaining space, change vol to remaining vol
            else
            {
                if (newLiquid.volume > remainingSpace)
                    newLiquid.volume = remainingSpace;
            }

            // Handle if liquid type already exists or not
            if (LiquidsDict.ContainsKey(newLiquid.liquidType))
                LiquidsDict[newLiquid.liquidType].volume += newLiquid.volume;
            else
                LiquidsDict.Add(newLiquid.liquidType, newLiquid);

            // Update stats according to the new levels
            UpdateInfo();
        }

        ///<summary>
        ///Updates the stats for this particular liquid collection, such as CombinedVolume, ColorMixed, and LiquidTypes
        ///</summary>
        public void UpdateInfo()
        {
            CombinedVolume = 0;
            float red = 0, green = 0, blue = 0, alpha = 0;

            foreach (KeyValuePair<LiquidType, Liquid> entry in LiquidsDict)
            {
                Liquid liquid = entry.Value;

                CombinedVolume += liquid.volume;
                red += liquid.liquidType.liquidColor.r * (float)liquid.volume;
                blue += liquid.liquidType.liquidColor.b * (float)liquid.volume;
                green += liquid.liquidType.liquidColor.g * (float)liquid.volume;
                alpha += liquid.liquidType.liquidColor.a * (float)liquid.volume;
            }
            ColorMixed = new Color(
                red / (float)CombinedVolume,
                green / (float)CombinedVolume,
                blue / (float)CombinedVolume, 
                alpha / (float)CombinedVolume);

            LiquidTypes = new List<LiquidType>(LiquidsDict.Keys);
        }
    }


    public GlassType glassType;
    //public SpriteRenderer m_SpriteRenderer;

    public bool OverrideLevel = false;
    public Sprite[] spriteList;
    public SpriteRenderer[] rendererList;

    public int maxVolume = 1;           // in ounces
    public double currentVolume = 0;     // in ounces
    public int currentVolumeLayer = 0;  // used for animations

    public float alphaValue = 0.5f;

    public List<LiquidsCollection> LiquidsCollList = new List<LiquidsCollection>();

    public bool OverrideColor = false;
    public Color Color_Override = Color.white;        // The override color
    float m_Red, m_Blue, m_Green;       // The values for the sliders for controlling the override color

    // Start is called before the first frame update
    void Start()
    {
        maxVolume = glassType.maxVolume;
        EmptyContents();
    }

    // this is only for testing. clicking on the glass will empty it
    private void OnMouseDown()
    {
        EmptyContents();
    }

    public void EmptyContents()
    {
        //foreach (LiquidsCollection liquidsCollection in LiquidsCollList)
        //{
        //    liquidsCollection.EmptyContents();
        //}      
        LiquidsCollList.Clear();
        currentVolume = 0;
    }

    private void UpdateInfo()
    {
        currentVolume = 0;
        foreach (LiquidsCollection liquidsCollection in LiquidsCollList)
        {
            currentVolume += liquidsCollection.CombinedVolume;
        }
    }

    public void MixAll()
    {
        if (LiquidsCollList.Count > 1)
        {
            double remainingVolume = maxVolume - currentVolume;
            currentVolume = 0;
            foreach (LiquidsCollection liquidsCollection in LiquidsCollList)
            {
                if (liquidsCollection != LiquidsCollList[1])
                    LiquidsCollList[1].Combine(liquidsCollection, remainingVolume);
                currentVolume += liquidsCollection.CombinedVolume;
            }
            LiquidsCollList.RemoveRange(1, LiquidsCollList.Count - 1);
        }
    }

    public void AddLiquid(Liquid newLiquid)
    {
        AddLiquid(new LiquidsCollection(newLiquid));
    }

    public void AddLiquid(LiquidsCollection newLiquidsCollection)
    {
        // If there's no room, don't add anything
        if (currentVolume >= maxVolume)
            return;

        // If the incoming liquid is the same as the top liquid, add to the volume of it
        double remainingVolume = maxVolume - currentVolume;
        if (LiquidsCollList[LiquidsCollList.Count - 1].SameLiquidMix(newLiquidsCollection))
        {
            LiquidsCollList[LiquidsCollList.Count - 1].Combine(newLiquidsCollection, remainingVolume);
        }

        // else the incoming liquid is different and will be added
        else
        {
            if (newLiquidsCollection.CombinedVolume > remainingVolume)
                newLiquidsCollection.ResizeTotalVolume(remainingVolume);
            LiquidsCollList.Add(newLiquidsCollection);
        }
    }

    public void RemoveLiquid(int index)
    {
        LiquidsCollList.RemoveAt(index);
    }

    // Update is called once per frame
    void Update()
    {
        //https://answers.unity.com/questions/181903/jump-to-a-specific-frame-in-an-animation.html
        //https://forum.unity.com/threads/changing-sprite-during-run-time.211817/

        // Determine liquid level
        if (!OverrideLevel)
        {
            currentVolumeLayer = (int)((currentVolume / maxVolume) * spriteList.Length);
        }

        // Set the sprite in the animation, if array is exceeded use last frame
        if (currentVolumeLayer < spriteList.Length)
        {
            rendererList[0].sprite = spriteList[currentVolumeLayer];
        }
        else
        {
            rendererList[0].sprite = spriteList[spriteList.Length - 1];
        }

    }
}
