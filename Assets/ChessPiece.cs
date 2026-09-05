using System.Collections.Generic;
using UnityEngine;

public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}


[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class ChessPiece : MonoBehaviour
{
    [Header("Piece Settings")]
    public ChessPieceType pieceType = ChessPieceType.Pawn;
    public Color colorTint = Color.white;

    [Header("Sprites (drag the matching PNG for each type)")]
    public Sprite pawnSprite;
    public Sprite rookSprite;
    public Sprite knightSprite;
    public Sprite bishopSprite;
    public Sprite queenSprite;
    public Sprite kingSprite;

    [Header("Board Reference")]
    [Tooltip("Should match the squareSize on your ChessBoard script.")]
    public float squareSize = 1f;

    private SpriteRenderer sr;

    private void OnValidate()
    {
        
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
        UpdateTint();
    }

    private void UpdateSprite()
    {
        if (sr == null) return;

        switch (pieceType)
        {
            case ChessPieceType.Pawn: sr.sprite = pawnSprite; break;
            case ChessPieceType.Rook: sr.sprite = rookSprite; break;
            case ChessPieceType.Knight: sr.sprite = knightSprite; break;
            case ChessPieceType.Bishop: sr.sprite = bishopSprite; break;
            case ChessPieceType.Queen: sr.sprite = queenSprite; break;
            case ChessPieceType.King: sr.sprite = kingSprite; break;
        }
    }

    private void UpdateTint()
    {
        if (sr == null) return;
        sr.color = colorTint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.4f);
        Vector3 origin = transform.position;

        foreach (Vector2Int offset in GetMoveOffsets())
        {
            Vector3 target = origin + new Vector3(offset.x * squareSize, offset.y * squareSize, 0f);
            Gizmos.DrawCube(target, new Vector3(squareSize * 0.85f, squareSize * 0.85f, 0.01f));
        }
    }


    private List<Vector2Int> GetMoveOffsets()
    {
        var moves = new List<Vector2Int>();

        switch (pieceType)
        {
            case ChessPieceType.Pawn:
                moves.Add(new Vector2Int(0, 1));
                moves.Add(new Vector2Int(0, 2));
                break;

            case ChessPieceType.Rook:
                for (int i = 1; i <= 7; i++)
                {
                    moves.Add(new Vector2Int(i, 0));
                    moves.Add(new Vector2Int(-i, 0));
                    moves.Add(new Vector2Int(0, i));
                    moves.Add(new Vector2Int(0, -i));
                }
                break;

            case ChessPieceType.Bishop:
                for (int i = 1; i <= 7; i++)
                {
                    moves.Add(new Vector2Int(i, i));
                    moves.Add(new Vector2Int(-i, i));
                    moves.Add(new Vector2Int(i, -i));
                    moves.Add(new Vector2Int(-i, -i));
                }
                break;

            case ChessPieceType.Queen:
                for (int i = 1; i <= 7; i++)
                {
                    moves.Add(new Vector2Int(i, 0));
                    moves.Add(new Vector2Int(-i, 0));
                    moves.Add(new Vector2Int(0, i));
                    moves.Add(new Vector2Int(0, -i));
                    moves.Add(new Vector2Int(i, i));
                    moves.Add(new Vector2Int(-i, i));
                    moves.Add(new Vector2Int(i, -i));
                    moves.Add(new Vector2Int(-i, -i));
                }
                break;

            case ChessPieceType.King:
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx != 0 || dy != 0)
                            moves.Add(new Vector2Int(dx, dy));
                    }
                }
                break;

            case ChessPieceType.Knight:
                int[,] knightOffsets =
                {
                    { 1, 2 }, { 2, 1 }, { -1, 2 }, { -2, 1 },
                    { 1, -2 }, { 2, -1 }, { -1, -2 }, { -2, -1 }
                };
                for (int i = 0; i < knightOffsets.GetLength(0); i++)
                    moves.Add(new Vector2Int(knightOffsets[i, 0], knightOffsets[i, 1]));
                break;
        }

        return moves;
    }
}