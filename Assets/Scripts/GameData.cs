using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int currentLevelIndex;
    public List<PieceState> pieceStates;

    public GameData()
    {
        currentLevelIndex = 0;
        pieceStates = new List<PieceState>();
    }

    public void AddPieceStates(PieceState pieceState)
    {
        pieceStates.Add(pieceState);
    }

    public List<PieceState> GetPieceSates() => pieceStates;

    public void ResetData()
    {
        currentLevelIndex = 0;
        pieceStates = new List<PieceState>();
    }

    public void SetCurrentLevelIndex(int index)
    {
        currentLevelIndex = index;
    }
    
    public int GetCurrentLevelIndex() => currentLevelIndex;
}
