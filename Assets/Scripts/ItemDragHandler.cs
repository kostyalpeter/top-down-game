using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    private InventoryContoller inventoryContoller;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryContoller = InventoryContoller.Instance;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot DropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        if (DropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                DropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (DropSlot != null)
        {
            if (DropSlot.currentItem != null)
            {
                Item draggedItem = GetComponent<Item>();
                Item targetItem = DropSlot.currentItem.GetComponent<Item>();

                if (draggedItem.ID == targetItem.ID)
                {
                    targetItem.AddToStack(draggedItem.quantity);
                    originalSlot.currentItem = null;
                    Destroy(gameObject);
                }
                else
                {
                    DropSlot.currentItem.transform.SetParent(originalSlot.transform);
                    originalSlot.currentItem = DropSlot.currentItem;
                    DropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                }

            }
            else
            {
                originalSlot.currentItem = null;
            }

            transform.SetParent(DropSlot.transform);
            DropSlot.currentItem = gameObject;
        }
        else
        {
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
