using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class DrinkGradePanel : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public GlassContentsv5 currentGlass;


    //private void UpdateField(string fieldName, string)
    //{
    //    textComponent = transform.Find("text").GetComponent<TextMeshProUGUI>();
    //    textComponent.text = tooltipString;
    //    Update();
    //}

    // Start is called before the first frame update
    void PopulateCurrentFields(GlassContentsv5 glassContent)
    {
        //if (currentGlass.LiquidMixList.Count == 0)
        //    return;

        transform.Find("currentGlass").GetComponent<TextMeshProUGUI>().text = glassContent.glassType.glassName;
        int j = 0;
        StringBuilder sb = new StringBuilder("");
        //foreach (GlassContentsv5.LiquidMix liquidMix in currentGlass.LiquidMixList)
        for (int i = currentGlass.LiquidMixList.Count-1; i >= 0; i--)
        {
            //sb.Clear();
            sb.AppendFormat("Liquid Layer {0}", j+1);
            //foreach (KeyValuePair<LiquidType, double> entry in liquidMix.LiquidProportions)
            foreach (KeyValuePair<LiquidType, double> entry in currentGlass.LiquidMixList[i].LiquidProportions)
            {
                //double entryVolume = entry.Value * liquidMix.TotalVolume;
                double entryVolume = entry.Value * currentGlass.LiquidMixList[i].TotalVolume;
                entryVolume = System.Math.Round(entryVolume, 2);
                sb.AppendFormat("\n* {0} oz {1}", entryVolume, entry.Key.name);
            }
            sb.AppendFormat("\n");
            j++;
        }

        transform.Find("currentContents").GetComponent<TextMeshProUGUI>().text = sb.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        PopulateCurrentFields(currentGlass);
    }
}
