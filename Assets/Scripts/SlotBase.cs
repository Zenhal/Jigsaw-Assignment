using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBase : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;

    public List<Slot> GetSlots() => slots ?? new List<Slot>();
}
