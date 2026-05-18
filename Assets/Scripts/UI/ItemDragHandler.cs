using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    public GameObject gameC;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        gameC = GameObject.Find("GameController");
    }   

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // save og parent
        transform.SetParent(transform.root); //Above other canvas
        canvasGroup.blocksRaycasts = false; 
        canvasGroup.alpha = 0.6f; // semi- transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follow the mouse 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //enable raycasts
        canvasGroup.alpha = 1f; // no longer transparent

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // slot where item dropped 
        GameObject Dobject =  eventData.pointerEnter?.gameObject;
        if(dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
                Debug.Log(dropSlot);
            }
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();
        Debug.Log(originalSlot);

        if(dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                //Is a slot under drop point
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                
            }
            else
            {
                originalSlot.currentItem = null;
                gameC.GetComponent<InventoryController>().EmptySlots.Add(originalParent.gameObject);
                Debug.Log("Added");
            }

            //move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
            Debug.Log("Dropped");
            gameC.GetComponent<InventoryController>().EmptySlots.Remove(Dobject);
        }
        else
        {
            //no slot under drop point 
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //center


    }

}
