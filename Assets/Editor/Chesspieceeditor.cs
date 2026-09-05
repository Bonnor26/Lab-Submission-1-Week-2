using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ChessPiece))]
public class ChessPieceEditor : Editor
{
    private void OnSceneGUI()
    {
        ChessPiece piece = (ChessPiece)target;

        EditorGUI.BeginChangeCheck();

        float handleSize = HandleUtility.GetHandleSize(piece.transform.position) * 0.6f;

        // The interactive "border" handle
        Vector3 newPosition = Handles.FreeMoveHandle(
            piece.transform.position,
            handleSize,
            Vector3.zero,
            Handles.RectangleHandleCap
        );

        // --- Adjustment: snap to the nearest board square ---
        newPosition.x = Mathf.Round(newPosition.x / piece.squareSize) * piece.squareSize;
        newPosition.y = Mathf.Round(newPosition.y / piece.squareSize) * piece.squareSize;
        newPosition.z = piece.transform.position.z;

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(piece.transform, "Move Chess Piece");
            piece.transform.position = newPosition;
        }
    }
}