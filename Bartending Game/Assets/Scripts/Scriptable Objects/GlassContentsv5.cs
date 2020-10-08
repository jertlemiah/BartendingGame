using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassContentsv5 : MonoBehaviour
{
    public class LiquidMix
    {
        public Dictionary<LiquidType, double> LiquidProportions = new Dictionary<LiquidType, double>();
        public Color ColorMixed = Color.white;
        public double Volume = 0;

        public LiquidMix() { }

        public LiquidMix(Liquid liquid)
        {
            LiquidProportions.Add(liquid.liquidType, 1.0);
            Volume = liquid.volume;
        }

        public void EmptyContents()
        {
            LiquidProportions.Clear();
            Volume = 0;
        }

        public bool SameLiquidMix(LiquidMix newLiquidMix)
        {
            bool same = true;

            // Check if new mix has all types from original mix
            foreach (LiquidType type in LiquidProportions.Keys)
                if(!(newLiquidMix.LiquidProportions.ContainsKey(type)))
                    same = false;

            // Check if original mix has all types from new mix
            foreach (LiquidType type in newLiquidMix.LiquidProportions.Keys)
                if (!(LiquidProportions.ContainsKey(type)))
                    same = false;

            return same;
        }

        public double UpdateVolume(double newTotalVolume)
        {
            return Volume = newTotalVolume;
        }

        public void Combine(LiquidMix newLiquidMix, double remainingSpace)
        {
            // If there's no space, don't do anything
            if (remainingSpace == 0)
                return;

            // Ignore volume requirements
            else if (remainingSpace == -1) { }

            if (newLiquidMix.Volume > remainingSpace)
                newLiquidMix.Volume = remainingSpace;

            Dictionary<LiquidType, double> LiquidVolumes = new Dictionary<LiquidType, double>();
            foreach (KeyValuePair<LiquidType, double> entry in LiquidProportions)
                LiquidVolumes.Add(entry.Key, entry.Value * Volume);

            Dictionary<LiquidType, double> newLiquidProportions = newLiquidMix.LiquidProportions;
            foreach (KeyValuePair<LiquidType, double> entry in newLiquidProportions)
            {
                double entryVolume = entry.Value * newLiquidMix.Volume;
                if (LiquidVolumes.ContainsKey(entry.Key))
                    LiquidVolumes[entry.Key] += entryVolume;
                else
                    LiquidVolumes.Add(entry.Key, entryVolume);
            }

            ConvertVolumeToPorportions(LiquidVolumes);
        }

        private void ConvertVolumeToPorportions(Dictionary<LiquidType, double> LiquidVolumes)
        {
            Volume = 0;
            foreach (KeyValuePair<LiquidType, double> entry in LiquidVolumes)
                Volume += entry.Value;

            LiquidProportions.Clear();
            double totalProportion = 1.0;
            int count = 0;
            foreach (KeyValuePair<LiquidType, double> entry in LiquidVolumes)
            {
                count++;
                double proportion = System.Math.Round(entry.Value / Volume, 2);

                // If its the last term, use all the remaining proportion so that it always sums to 1.00
                if (count == LiquidVolumes.Count)
                    proportion = totalProportion;
                else
                    totalProportion -= proportion;

                LiquidProportions.Add(entry.Key, proportion);
            }
        }
        ///<summary>
        ///Adds the newLiquid to the LiquidsDict with regards to the remainingSpace. 
        ///If remainingSpace = -1, this condition is ignored.
        ///</summary>
        public void AddLiquidToMix(Liquid newLiquid, double remainingSpace)
        {
            LiquidMix newLiquidMix = new LiquidMix(newLiquid);
            Combine(newLiquidMix, remainingSpace);

            // Update stats according to the new levels
            UpdateInfo();
        }

        ///<summary>
        ///Updates the stats for this particular liquid collection, such as ColorMixed
        ///</summary>
        public void UpdateInfo()
        {
            float red = 0, green = 0, blue = 0, alpha = 0;

            foreach (KeyValuePair<LiquidType, double> entry in LiquidProportions)
            {
                red += entry.Key.liquidColor.r * (float)entry.Value;
                blue += entry.Key.liquidColor.b * (float)entry.Value;
                green += entry.Key.liquidColor.g * (float)entry.Value;
                alpha += entry.Key.liquidColor.a * (float)entry.Value;
            }
            ColorMixed = new Color(red, green, blue, alpha);
        }
    }

    public GlassType glassType;
    public SpriteRenderer m_SpriteRenderer;

    public bool OverrideLevel = false;
    public Sprite[] spriteList;

    public int maxVolume = 1;           // in ounces
    public double currentVolume = 0;     // in ounces
    public int currentVolumeLayer = 0;  // used for animations

    public float alphaValue = 0.5f;

    public List<LiquidMix> LiquidMixList = new List<LiquidMix>();//Liquid[] liquids;

    public Color Color_Mixed = Color.white; // The color from mixing liquids
    public bool OverrideColor = false;
    public Color Color_Override = Color.white;        // The override color
    float m_Red, m_Blue, m_Green;       // The values for the sliders for controlling the override color

    // Start is called before the first frame update
    void Start()
    {
        maxVolume = glassType.maxVolume;

    }



    // this is only for testing. clicking on the glass will empty it
    private void OnMouseDown()
    {
        LiquidMixList.Clear();
    }

    public void EmptyContents()
    {
        LiquidMixList.Clear();
    }


    public void AddLiquid(LiquidType liquidType, double volumeAdded)
    {
        if (currentVolume >= maxVolume)
            return;
        else if (currentVolume + volumeAdded > maxVolume)
            volumeAdded = maxVolume - currentVolume; 

        LiquidMix newLiquidMix = new LiquidMix(new Liquid(liquidType, volumeAdded));
        if(LiquidMixList[0] != null)
        {
            if (LiquidMixList[LiquidMixList.Count - 1].SameLiquidMix(newLiquidMix))
            {

            }
        }


    }

    public void RemoveLiquid(int index)
    {
        LiquidMixList.RemoveAt(index);
    }

    // Update is called once per frame
    void Update()
    {
        //https://answers.unity.com/questions/181903/jump-to-a-specific-frame-in-an-animation.html
        //https://forum.unity.com/threads/changing-sprite-during-run-time.211817/

        // Determine liquid level
        //if (!OverrideLevel)
        //{
        //    currentVolumeLayer = (int)((currentVolume / maxVolume) * spriteList.Length);
        //}

        //// Set the sprite in the animation, if array is exceeded use last frame
        //if (currentVolumeLayer < spriteList.Length)
        //{
        //    m_SpriteRenderer.sprite = spriteList[currentVolumeLayer];
        //}
        //else
        //{
        //    m_SpriteRenderer.sprite = spriteList[spriteList.Length - 1];
        //}


        
    }
}
