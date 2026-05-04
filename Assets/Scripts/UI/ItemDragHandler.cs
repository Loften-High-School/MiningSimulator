using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
        if(dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

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
            }

            //move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            //no slot under drop point 
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //center


    }

}
