using UnityEngine;


public class ChessBoard : MonoBehaviour
{
    public int size = 8;
    public float cellSize = 1f;
    public Color lightColor = new Color(0.9f, 0.9f, 0.8f);
    public Color darkColor = new Color(0.3f, 0.3f, 0.25f);

    
    public Vector3 GetWorldPosition(int col, int row)
    {
        Vector3 origin = transform.position;
        return origin + new Vector3(col * cellSize, 0f, row * cellSize);
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

       
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Vector3 center = origin + new Vector3(
                    col * cellSize + cellSize * 0.5f,
                    0f,
                    row * cellSize + cellSize * 0.5f);

           
                bool isLight = (row + col) % 2 == 0;
                Gizmos.color = isLight ? lightColor : darkColor;

                Vector3 cubeSize = new Vector3(cellSize, 0.02f, cellSize);
                Gizmos.DrawCube(center, cubeSize);

               
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(center, cubeSize);
            }
        }
    }
}