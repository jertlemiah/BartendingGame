using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManagerScript : MonoBehaviour//Singleton<GameManagerScript>
{
    public InputMaster controls;
    public float bottleRotationDegrees = 45;
    public float rotationDuration = 1;
    private bool useItem = false;
    public GlassContentsv5 currentGlass;

    GameObject heldObject;

    // Start is called before the first frame update
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.PickUpObject.performed += context => PickUpObject();
        controls.Player.UseObject.performed += context => UseObject();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public void Update()
    {
        if(heldObject != null)
        {
            Vector2 cameraPos = Camera.main.ScreenToWorldPoint(controls.Player.MousePosition.ReadValue<Vector2>());
            float xOffset = heldObject.GetComponent<DrinkBottle>().xOffset;
            float yOffset = heldObject.GetComponent<DrinkBottle>().yOffset;
            heldObject.transform.position = new Vector3(cameraPos.x + xOffset, cameraPos.y + yOffset, 0);

            //Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D>();
            //heldObject.GetComponent<Rigidbody2D>().MovePosition(new Vector3(cameraPos.x, cameraPos.y, 0));
            if (controls.Player.UseObject.ReadValue<float>() > 0)
            {
                if (heldObject.tag == "Bottle")
                {
                    heldObject.GetComponent<DrinkBottle>().PourBottle(0.1);
                }
            }
        }

    }

    public void OnMixButtonClick()
    {
        Debug.Log("OnMixButtonClick has been clicked");
        currentGlass.MixAll();
    }

    public void SetTotalLiquid(double newVolume)
    {
        if (heldObject != null && heldObject.tag == "Bottle")
        {
            heldObject.GetComponent<DrinkBottle>().SetTotalLiquid(newVolume);

        }
    }

    public void UseObject()
    {
        Debug.Log("UseObject called");
        if (heldObject == null)
        {
            return;
        }

        
        //if (heldObject.tag == "Bottle")
        //{
        //    heldObject.GetComponent<DrinkBottle>().PourBottle();
        //}
    }

    void PickUpObject()
    {
        Debug.Log("PickUpObject called");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(controls.Player.MousePosition.ReadValue<Vector2>());

        if (heldObject != null)
        {
            Debug.Log("Dropping " + heldObject.name + " at pos " + mousePos);
            if (heldObject.tag == "Bottle")
            {
                DropBottle();
                return;
            }
        }   
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Picking up " + hit.collider.gameObject.name + " at pos " + mousePos);
            if (hit.transform.gameObject.tag == "Bottle")
            {
                //Debug.Log("Target is item");
                PickUpBottle(hit);
            }
        }
    }

    void DropBottle()
    {
        // Drop item if one is already held
        heldObject.GetComponent<SpriteRenderer>().sortingOrder -= 10;       // return sorting order to normal
        heldObject.GetComponent<OutlineScript>().overrideOutline = false;   // turn override back on
        heldObject.GetComponent<OutlineScript>().EnableHighlight();
        // un-tilt bottle
        //heldObject.transform.eulerAngles = Vector3.forward * 0;
        heldObject.transform.DORotate(Vector3.forward * 0, rotationDuration);
        heldObject = null;
    }

    void PickUpBottle(RaycastHit2D hit)
    {
        heldObject = hit.transform.gameObject;
        heldObject.GetComponent<SpriteRenderer>().sortingOrder += 10;       // bring bottle to higher sorting layer
        heldObject.GetComponent<OutlineScript>().overrideOutline = true;    // override the outline
        heldObject.GetComponent<OutlineScript>().DisableHighlight();
        // Tilt bottle
        //heldObject.transform.eulerAngles = Vector3.forward * bottleRotationDegrees;
        heldObject.transform.DORotate(Vector3.forward * bottleRotationDegrees, rotationDuration);
    }
}
