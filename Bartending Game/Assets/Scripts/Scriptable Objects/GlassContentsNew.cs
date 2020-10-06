using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassContentsNew : MonoBehaviour
{
    public GlassType glassType;
    //public SpriteRenderer m_SpriteRenderer;

    public bool OverrideLevel = false;
    public Sprite[] spriteList;
    public SpriteRenderer[] rendererList;

    public int maxVolume = 1;           // in ounces
    public double currentVolume = 0;     // in ounces
    public int currentVolumeLayer = 0;  // used for animations

    public float alphaValue = 0.5f;

    public List<Liquid> LiquidsList = new List<Liquid>();//Liquid[] liquids;
    public List<LiquidsCollection> LiquidsCollList = new List<LiquidsCollection>();


    public Color Color_Mixed = Color.white; // The color from mixing liquids
    public bool OverrideColor = false;
    public Color Color_Override = Color.white;        // The override color
    float m_Red, m_Blue, m_Green;       // The values for the sliders for controlling the override color

    public class LiquidsCollection
    {
        public List<Liquid> LiquidsList = new List<Liquid>();
        public Color ColorMixed = Color.white;
        public List<LiquidType> LiquidTypes = new List<LiquidType>();
        public double currentVolume = 0;

        public void EmptyContents()
        {
            LiquidsList.Clear();
            LiquidTypes.Clear();
            currentVolume = 0;
        }
        public void AddLiquid(LiquidType liquidType, double volumeAdded)
        {
            if (currentVolume >= maxVolume)
                return;

            //
            if (LiquidsList != null)
            {
                for (int i = 0; i < LiquidsList.Count; i++)
                {
                    if (LiquidsList[i].liquidType == liquidType)
                    {
                        LiquidsList[i].volume += volumeAdded;
                        return;
                    }
                }
            }
            // else // liquids is null, or liquidType is not in list
            {
                Liquid addedLiquid = new Liquid(liquidType, volumeAdded);
                LiquidsList.Add(addedLiquid);
            }
        }

        public void UpdateCall()
        {
            double newVolume = 0;
            float red = 0, green = 0, blue = 0;

            if (LiquidsList != null)// && liquids[0] != null)
            {

                //Debug.Log("Made it to if statement");
                for (int i = 0; i < LiquidsList.Count; i++)
                {
                    if (LiquidsList[i].liquidType != null)
                    {
                        newVolume += LiquidsList[i].volume;
                        red += LiquidsList[i].liquidType.liquidColor.r * (float)LiquidsList[i].volume;
                        blue += LiquidsList[i].liquidType.liquidColor.b * (float)LiquidsList[i].volume;
                        green += LiquidsList[i].liquidType.liquidColor.g * (float)LiquidsList[i].volume;
                    }
                }
                currentVolume = newVolume;
                Color_Mixed = new Color(red / (float)currentVolume,
                    green / (float)currentVolume,
                    blue / (float)currentVolume, alphaValue);
            }
        }
    }

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
        foreach (LiquidsCollection liquidsCollection in LiquidsCollList)
        {
            liquidsCollection.EmptyContents();
        }      
    }

    public void AddLiquid(LiquidType liquidType, double volumeAdded)
    {
        if (currentVolume >= maxVolume)
            return;

        //
        if (LiquidsList != null)
        {
            for (int i = 0; i < LiquidsList.Count; i++)
            {
                if (LiquidsList[i].liquidType == liquidType)
                {
                    LiquidsList[i].volume += volumeAdded;
                    return;
                }
            }
        }
        // else // liquids is null, or liquidType is not in list
        {
            Liquid addedLiquid = new Liquid(liquidType, volumeAdded);
            LiquidsList.Add(addedLiquid);
        }
    }

    public void RemoveLiquid(int index)
    {
        LiquidsList.RemoveAt(index);
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
            m_SpriteRenderer.sprite = spriteList[currentVolumeLayer];
        }
        else
        {
            m_SpriteRenderer.sprite = spriteList[spriteList.Length - 1];
        }



        // Set color from override or from mixed
        if (OverrideColor)
        {
            m_SpriteRenderer.color = Color_Override;
        }
        else
        {
            //currentVolume = 0;
            double newVolume = 0;
            float red = 0, green = 0, blue = 0;

            if (LiquidsList != null)// && liquids[0] != null)
            {

                //Debug.Log("Made it to if statement");
                for (int i = 0; i < LiquidsList.Count; i++)
                {
                    if (LiquidsList[i].liquidType != null)
                    {
                        newVolume += LiquidsList[i].volume;
                        red += LiquidsList[i].liquidType.liquidColor.r * (float)LiquidsList[i].volume;
                        blue += LiquidsList[i].liquidType.liquidColor.b * (float)LiquidsList[i].volume;
                        green += LiquidsList[i].liquidType.liquidColor.g * (float)LiquidsList[i].volume;
                    }
                }
                currentVolume = newVolume;
                Color_Mixed = new Color(red / (float)currentVolume,
                    green / (float)currentVolume,
                    blue / (float)currentVolume, alphaValue);
            }

            m_SpriteRenderer.color = Color_Mixed;
        }
    }
}
