using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassContentsv5 : MonoBehaviour
{
    public class LiquidMix
    {
        public Dictionary<LiquidType, double> LiquidProportions = new Dictionary<LiquidType, double>();
        public Color ColorMixed = Color.white;
        public double TotalVolume = 0;
        //public GameObject LayerObj;

        public LiquidMix() { }

        public LiquidMix(Liquid liquid)
        {
            LiquidProportions.Add(liquid.liquidType, 1.0);
            TotalVolume = liquid.volume;
            UpdateInfo();
            //LayerObj = Instantiate(LiquidLayerPrefab, new Vector3(0, 0, 0), Quaternion.identity)
        }

        public void EmptyContents()
        {
            LiquidProportions.Clear();
            TotalVolume = 0;
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
            return TotalVolume = newTotalVolume;
        }

        public void Combine(LiquidMix newLiquidMix, double remainingSpace)
        {
            // If there's no space, don't do anything
            if (remainingSpace == 0)
                return;

            // Ignore volume requirements
            else if (remainingSpace == -1) { }

            // If the incoming volume is greater than remaining space, change incoming volume amount
            else if (newLiquidMix.TotalVolume > remainingSpace)
                newLiquidMix.TotalVolume = remainingSpace;

            // Using the total volume and the proportions of each liquidType, get the volume for each liquidType
            Dictionary<LiquidType, double> LiquidVolumes = new Dictionary<LiquidType, double>();
            foreach (KeyValuePair<LiquidType, double> entry in LiquidProportions)
                LiquidVolumes.Add(entry.Key, entry.Value * TotalVolume);

            // Create new dictionary, loop through new list, add to current stuff
            Dictionary<LiquidType, double> newLiquidProportions = newLiquidMix.LiquidProportions;
            foreach (KeyValuePair<LiquidType, double> entry in newLiquidProportions)
            {
                double entryVolume = entry.Value * newLiquidMix.TotalVolume;
                if (LiquidVolumes.ContainsKey(entry.Key))
                    LiquidVolumes[entry.Key] += entryVolume;
                else
                    LiquidVolumes.Add(entry.Key, entryVolume);
            }

            UpdateProportionsGivenVolumes(LiquidVolumes);
            UpdateInfo();
        }

        private void UpdateProportionsGivenVolumes(Dictionary<LiquidType, double> LiquidVolumes)
        {
            TotalVolume = 0;
            foreach (KeyValuePair<LiquidType, double> entry in LiquidVolumes)
                TotalVolume += entry.Value;

            LiquidProportions.Clear();
            double totalProportion = 1.0;
            int count = 0;
            foreach (KeyValuePair<LiquidType, double> entry in LiquidVolumes)
            {
                count++;
                double proportion = System.Math.Round(entry.Value / TotalVolume, 2);

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
    //public SpriteRenderer m_SpriteRenderer;

    //public bool OverrideLevel = false;
    //public Sprite[] spriteList;
    public Sprite FullSprite;
    public GameObject LiquidLayerPrefab;
    public List<GameObject> LiquidLayerReferences;

    public int maxVolume = 1;           // in ounces
    public double currentVolume = 0;     // in ounces
    //public int currentVolumeLayer = 0;  // used for animations

    //public float alphaValue = 0.5f;

    public List<LiquidMix> LiquidMixList = new List<LiquidMix>();//Liquid[] liquids;

    //public Color Color_Mixed = Color.white; // The color from mixing liquids
    //public bool OverrideColor = false;
    //public Color Color_Override = Color.white;        // The override color
    //float m_Red, m_Blue, m_Green;       // The values for the sliders for controlling the override color

    // Start is called before the first frame update
    void Start()
    {
        maxVolume = glassType.maxVolume;

    }

    void EvaluateDrink(GlassContentsv5 recipe)
    {

    }

    // this is only for testing. clicking on the glass will empty it
    private void OnMouseDown()
    {
        EmptyContents();
    }

    public void EmptyContents()
    {
        LiquidMixList.Clear();
        DestroyLayerObjects();
        currentVolume = 0;
    }

    public void DestroyLayerObjects()
    {
        foreach (GameObject obj in LiquidLayerReferences)
        {
            Destroy(obj);
        }
        LiquidLayerReferences.Clear();
    }

    public void MixAll()
    {
        if (LiquidMixList.Count > 1)
        {
            double remainingVolume = maxVolume - currentVolume;
            //currentVolume = 0;
            foreach (LiquidMix liquidMix in LiquidMixList)
            {
                // For each liquidMix that is not the first, combine with the first
                if (liquidMix != LiquidMixList[0])
                    LiquidMixList[0].Combine(liquidMix, -1);
                //LiquidMixList[1].Combine(liquidMix, remainingVolume);
                //currentVolume += liquidMix.Volume;

            }
            LiquidMixList.RemoveRange(1, LiquidMixList.Count - 1);

            DestroyLayerObjects();

            //foreach (GameObject obj in LiquidLayerReferences)
            //{
            //    if (obj != LiquidLayerReferences[0])
            //        Destroy(obj);
            //}
            //LiquidLayerReferences.RemoveRange(1, LiquidLayerReferences.Count - 1);
        }
    }

    //public void MixAll()
    //{
    //    if (LiquidsCollList.Count > 1)
    //    {
    //        double remainingVolume = maxVolume - currentVolume;
    //        currentVolume = 0;
    //        foreach (LiquidsCollection liquidsCollection in LiquidsCollList)
    //        {
    //            if (liquidsCollection != LiquidsCollList[1])
    //                LiquidsCollList[1].Combine(liquidsCollection, remainingVolume);
    //            currentVolume += liquidsCollection.CombinedVolume;
    //        }
    //        LiquidsCollList.RemoveRange(1, LiquidsCollList.Count - 1);
    //    }
    //}

    public void AddLiquid(LiquidType liquidType, double volumeAdded)
    {
        double remainingVolume = maxVolume - currentVolume;

        if (currentVolume >= maxVolume)
            return;
        else if (currentVolume + volumeAdded > maxVolume)
            volumeAdded = remainingVolume;

        currentVolume += volumeAdded;
        LiquidMix newLiquidMix = new LiquidMix(new Liquid(liquidType, volumeAdded));

        // If the top mix is the same recipe as the incoming, just update the proportions & volume
        if(LiquidMixList.Count > 0)
        {
            if (LiquidMixList[LiquidMixList.Count - 1].SameLiquidMix(newLiquidMix))
            {
                LiquidMixList[LiquidMixList.Count - 1].Combine(newLiquidMix, remainingVolume);
            }
            else
                LiquidMixList.Add(newLiquidMix);
        }
        // Else add a new layer to the thing
        else
        {
            LiquidMixList.Add(newLiquidMix);

            //LiquidLayerReferences.Add(Instantiate(LiquidLayerPrefab , new Vector3(0, 0, 0), Quaternion.identity));
        }


    }

    //private void

    //public void RemoveLiquid(int index)
    //{
    //    LiquidMixList.RemoveAt(index);
    //}
    public float SpriteOffset = 0;
    public float ScalingFactor = 1;
    // Update is called once per frame
    void Update()
    {
        int i = 0;
        float liquidLevel = 0f;
        foreach (LiquidMix liquidMix in LiquidMixList)
        {
            if (LiquidLayerReferences.Count < i + 1)//(LiquidLayerReferences[i] == null)
            {
                LiquidLayerReferences.Add(Instantiate(LiquidLayerPrefab, new Vector3(0, 0, 0), Quaternion.identity));
                LiquidLayerReferences[i].transform.parent = gameObject.transform;
                LiquidLayerReferences[i].transform.position = gameObject.transform.position;
            }
            SpriteRenderer spriteRenderer = LiquidLayerReferences[i].GetComponent<SpriteRenderer>();
            spriteRenderer.material.SetColor("_Color", liquidMix.ColorMixed);
            spriteRenderer.material.SetFloat("_LiquidLevel", (float)liquidLevel);
            liquidLevel += (float)(liquidMix.TotalVolume / maxVolume);
            spriteRenderer.material.SetFloat("_Thickness", (float)(liquidMix.TotalVolume / maxVolume));
            spriteRenderer.material.SetFloat("_Offset", (float)SpriteOffset);
            spriteRenderer.material.SetFloat("_ScalingFactor", (float)ScalingFactor);
            //_ScalingFactor

            //_Offset
            //_Thickness
            //_Color
            //_LiquidLevel
            i++;
        }
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //tempColor = spriteRenderer.material.GetColor("_Color");
        //tempColor.a = 0;
        //spriteRenderer.material.SetColor("_Color", tempColor);

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
