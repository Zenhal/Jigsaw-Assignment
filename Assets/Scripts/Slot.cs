using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private int slotIndex;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public int GetSlotIndex() => slotIndex;

    public RectTransform GetRectTransform() => rectTransform;
}

