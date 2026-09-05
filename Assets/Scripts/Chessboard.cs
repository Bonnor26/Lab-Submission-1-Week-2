using UnityEngine;


public class ChessBoard : MonoBehaviour
{
    [Header("Board Settings")]
    public int boardSize = 8;
    public float squareSize = 1f;

    [Header("Colors")]
    public Color lightColor = Color.white;
    public Color darkColor = Color.gray;

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        // Nested loop -> 8x8 grid
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                bool isLightSquare = (x + y) % 2 == 0;
                Gizmos.color = isLightSquare ? lightColor : darkColor;

                Vector3 center = origin + new Vector3(
                    x * squareSize + squareSize * 0.5f,
                    y * squareSize + squareSize * 0.5f,
                    0f
                );

                Gizmos.DrawWireCube(center, new Vector3(squareSize, squareSize, 0.01f));
            }
        }
    }
}