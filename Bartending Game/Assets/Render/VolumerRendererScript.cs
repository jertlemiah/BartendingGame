using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumerRendererScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float Offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.material.SetFloat("_Offset", Offset);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.material.SetFloat("_LiquidLevel",.2f);
        //_Color
        //_Thickness
        //_Offset
    }
}
