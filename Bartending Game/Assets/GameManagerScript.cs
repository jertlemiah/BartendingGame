using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public InputMaster controls;
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
        }
    }

    public void UseObject()
    {
        if (heldObject == null)
        {
            return;
        }

        Debug.Log("UseObject called");
        heldObject.GetComponent<DrinkBottle>().PourBottle();
    }
    
    void PickUpObject()
    {
        if(heldObject != null)
        {
            
            heldObject.GetComponent<SpriteRenderer>().sortingOrder -= 10;
            heldObject.GetComponent<OutlineScript>().overrideOutline = false;
            heldObject.GetComponent<OutlineScript>().EnableHighlight();
            heldObject = null;
            return;
        }

        Debug.Log("PickUpObject called");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(controls.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name + " at pos " + mousePos);
            if (hit.transform.gameObject.tag == "Item")
            {
                //transform.position = Input.mousePosition;
                //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("Target is item");
                //hit.transform.gameObject.
                heldObject = hit.transform.gameObject;
                heldObject.GetComponent<SpriteRenderer>().sortingOrder += 10;
                heldObject.GetComponent<OutlineScript>().overrideOutline = true;
                heldObject.GetComponent<OutlineScript>().DisableHighlight();
            }

        }
    }
}
