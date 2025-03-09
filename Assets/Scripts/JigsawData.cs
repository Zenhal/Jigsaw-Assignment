using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JigsawData", menuName = "ScriptableObjects/JigsawData", order = 1)]
public class JigsawData : ScriptableObject
{
    public GameObject slotObject;
    public List<PieceData> pieces;
}
