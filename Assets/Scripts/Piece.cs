using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Image image;
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Slot slot;
    private PieceData pieceData;

    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void SetPiece(Sprite sprite, Vector2 position, Slot slot, ref PieceData pieceData)
    {
        image.sprite = sprite;
        originalPosition = position;
        this.pieceData = pieceData;
        SetPosition(position);
        SetSlot(slot);
        SetImage();
    }

    private void SetPosition(Vector2 position)
    {
        if(pieceData.isPlaced)
            rectTransform.position = position;
        else
            rectTransform.anchoredPosition = originalPosition;
    }

    private void SetSlot(Slot slot)
    {
        if(slot != null)
            this.slot = slot;
    }

    private void SetImage()
    {
        rectTransform.sizeDelta = image.sprite.rect.size * 3 / 5;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (pieceData.isPlaced) return;
        transform.SetAsLastSibling();
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsCloseToSlot())
        {
            Debug.Log("Found Slot");
            rectTransform.position = slot.GetRectTransform().position;
            pieceData.isPlaced = true;
            var pieceState = new PieceState(pieceData.index, true);
            AudioManager.Instance.PlaySound("placed");
            EventManager.TriggerEvent("PiecePlaced", new Dictionary<string, object>() {{ "Piece", pieceState }});
        }
        else
        {
            AudioManager.Instance.PlaySound("failed");
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
