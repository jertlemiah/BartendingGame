//This example outputs Sliders that control the red green and blue elements of a sprite's color
//Attach this to a GameObject and attach a SpriteRenderer component

using UnityEngine;

public class drinkDisplay : MonoBehaviour
{
    public Drink drink;
    public SpriteRenderer m_SpriteRenderer;
    public GlassContents glassContents; // obsolete probably

    public Sprite[] spriteList;

    public int maxVolume;           // in ounces
    public float currentVolume;     // in ounces
    public int currentVolumeLayer;  // used for animations
    public string glassType;

    public Color Color_Mixed = Color.white; // The color from mixing liquids
    public bool OverrideColor = false;
    public Color Color_Override;        // The override color
    float m_Red, m_Blue, m_Green;       // The values for the sliders for controlling the override color

    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        //m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
        //m_SpriteRenderer.color = Color.blue;
        //m_SpriteRenderer.color = Color.white;
    }

    void OnGUI()
    {
        //https://answers.unity.com/questions/181903/jump-to-a-specific-frame-in-an-animation.html
        //https://forum.unity.com/threads/changing-sprite-during-run-time.211817/

        // Set the sprite in the animation, if array is exceeded use last frame
        if (glassContents.currentVolume <= spriteList.Length)
        {
            m_SpriteRenderer.sprite = spriteList[glassContents.currentVolumeLayer];
        } else
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
            m_SpriteRenderer.color = Color_Mixed;
        }
        

    }

}
