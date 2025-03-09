using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Image image;
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Slot slot;

    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void SetPiece(Sprite sprite, Vector2 position, Slot slot)
    {
        image.sprite = sprite;
        originalPosition = position;
        if(slot != null)
            this.slot = slot;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsCloseToSlot())
        {
            Debug.Log("Found Slot");
            rectTransform.position = slot.GetRectTransform().position;
            EventManager.TriggerEvent("PiecePlaced", null);
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
    
    private bool IsCloseToSlot()
    {
        if (slot == null) return false;
        var minDist = float.MaxValue;
        var piecePosition = GetScreenPosition(rectTransform);
        var slotPosition = GetScreenPosition(slot.GetRectTransform());
        var dist = Vector2.Distance(piecePosition, slotPosition);
        Debug.Log(dist);
        if (dist < minDist)
        {
            minDist = dist;
        }
        return minDist < 50f ;
    }

    private Vector2 GetScreenPosition(RectTransform objectRectTransform) => RectTransformUtility.WorldToScreenPoint(null, objectRectTransform.position);
}
