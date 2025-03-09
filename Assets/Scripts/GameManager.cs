using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private JigsawData[] jigsawDatas;
    [SerializeField] private Transform pieceContainer;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button nextLevelButton;
    
    
    private List<Piece> jigsawPieces = new List<Piece>();
    private List<Vector2> gridPositions = new List<Vector2>();
    private SlotBase slotBase;
    private JigsawData currentJigsawData;

    private int piecePlacedCount = 0;
    private int currentLevelCount = 0;
    
    private void Start()
    {
        LoadLevel(currentLevelCount); // Load first puzzle by default
    }

    private void OnEnable()
    {
        EventManager.StartListening("PiecePlaced", OnPiecePlaced);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PiecePlaced", OnPiecePlaced);
    }

    private void LoadLevel(int index)
    {
        if (IsValidLevel(index) == false) return;

        currentJigsawData = jigsawDatas[index];
        
        InitialiseSlots(currentJigsawData.slotObject);
        InitialisePieceContainer();
        
        GenerateJigsawPieces();
    }

    private void InitialiseSlots(GameObject slotObject)
    {
        slotBase = Instantiate(slotObject, slotContainer).GetComponent<SlotBase>();
    }

    private void InitialisePieceContainer()
    {
        ClearPieces();
        gridPositions.Clear();
        jigsawPieces.Clear();
        
        //Currently supports 3x3 grid (9 pieces) only
        int rows = 3;
        int cols = 3;
        RectTransform containerRect = pieceContainer.GetComponent<RectTransform>();
        float widthStep = containerRect.rect.width / cols;
        float heightStep = containerRect.rect.height / rows;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float x = (j * widthStep) - (containerRect.rect.width / 2) + (widthStep / 2);
                float y = (i * heightStep) - (containerRect.rect.height / 2) + (heightStep / 2);
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }

    private void GenerateJigsawPieces()
    {
        ShuffleList(gridPositions);
        for (int i = 0; i < currentJigsawData.pieces.Count; i++)
        {
            var pieceData = currentJigsawData.pieces[i];
            GameObject newPiece = Instantiate(piecePrefab, pieceContainer);
            Piece piece = newPiece.GetComponent<Piece>();
            piece.SetPiece(pieceData.pieceImage, gridPositions[i], GetSlotFromIndex(pieceData.index));
            piece.GetComponent<RectTransform>().sizeDelta = pieceData.pieceImage.rect.size * 3 / 5;
            piece.GetComponent<RectTransform>().anchoredPosition = gridPositions[i];
            jigsawPieces.Add(piece);
        }
    }
    
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
    private Slot GetSlotFromIndex(int index)
    {
        var slots = slotBase.GetSlots();
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            if (slot.GetSlotIndex() == index)
                return slot;
        }
        return null;
    }

    private bool IsValidLevel(int index) => index < jigsawDatas.Length;

    private void ClearPieces()
    {
        foreach (var piece in jigsawPieces)
        {
            Destroy(piece.gameObject);
        }
    }

    private void OnPiecePlaced(Dictionary<string, object> message)
    {
        piecePlacedCount++;
        if (piecePlacedCount == slotBase.GetSlots().Count)
            ShowVictory();
    }

    private void Reset()
    {
        piecePlacedCount = 0;
        ClearPieces();
        gridPositions.Clear();
        jigsawPieces.Clear();
        Destroy(slotBase.gameObject);
    }


    private void ShowVictory()
    {
        AudioManager.Instance.PlaySound("win");
        victoryPanel.SetActive(true);
        currentLevelCount++;
        nextLevelButton.gameObject.SetActive(IsValidLevel(currentLevelCount));
    }

    public void OnNextLevelButtonClicked()
    {
        victoryPanel.SetActive(false);
        Reset();
        LoadLevel(currentLevelCount);
    }

}
