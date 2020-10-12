using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class Tooltip : MonoBehaviour {

    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float padding;

    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake() {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<TextMeshProUGUI>();

        //ShowTooltip("Random tooltip text");
        HideTooltip();
        /*
        ShowTooltip("Random tooltip text");

        FunctionPeriodic.Create(() => {
            string abc = "abcoiwo iehgosdnk afnfp+\ngpjgidfbasjbdwqwrioj";
            string showText = "";
            for (int i = 0; i < Random.Range(30, 150); i++) {
                showText += abc[Random.Range(0, abc.Length)];
            }
            ShowTooltip(showText);
        }, .5f);
        */
    }

    private void Update() {
        // Get mouse position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), 
            Input.mousePosition, uiCamera, out localPoint);

        // Set tooltip location to mouse position
        transform.localPosition = localPoint;


    }

    private void FollowCursor()
    {
        //if (!transform.activeSelf) { return; }

        //Vector3 newPos = Input.mousePosition + offset;
        //newPos.z = 0f;
        //float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
        //if (rightEdgeToScreenEdgeDistance < 0)
        //{
        //    newPos.x += rightEdgeToScreenEdgeDistance;
        //}
        //float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - rect.width * popupCanvas.scaleFactor / 2) + padding;
        //if (leftEdgeToScreenEdgeDistance > 0)
        //{
        //    newPos.x += leftEdgeToScreenEdgeDistance;
        //}
        //float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
        //if (topEdgeToScreenEdgeDistance < 0)
        //{
        //    newPos.y += topEdgeToScreenEdgeDistance;
        //}
        //transform.position = newPos;
    }

    private void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
        Update();
    }

    private void HideTooltip() {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString) {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static() {
        instance.HideTooltip();
    }

}
