using System;
using UnityEngine;

[Serializable]
public class PieceState
{
    public int index;
    public bool isPlaced;

    public PieceState()
    {
        index = 0;
        isPlaced = false;
    }
    public PieceState(int index, bool isPlaced)
    {
        this.index = index;
        this.isPlaced = isPlaced;
    }
}
