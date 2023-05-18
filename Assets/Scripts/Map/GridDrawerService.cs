using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrawerService : MonoBehaviour
{
    [SerializeField] Tile prefab;

    // Gizmosdraw settings
    [SerializeField] private Color gizmosColor = Color.cyan;
    [SerializeField] private float gizmosHeight = 1f;
    [SerializeField] GridDrawerService baseGridDrawerService;

    // Start is called before the first frame update


    public Mesh DrawGridMesh(Vector2Int tileSize, Vector3 centerOfMap)
    {
        // Create a new mesh
        Mesh mesh = new Mesh();

        // Calculate the dimensions of the grid
        int width = tileSize.x;
        int height = tileSize.y;

        // Calculate the total number of vertices and triangles
        int numVertices = (width + 1) * (height + 1);
        int numTriangles = width * height * 6;

        // Create arrays to store the vertices, triangles, and UV coordinates
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numTriangles];
        Vector2[] uv = new Vector2[numVertices];

        // Calculate the half size of the grid
        float halfWidth = width * 0.5f;
        float halfHeight = height * 0.5f;

        // Calculate the step size between each vertex
        float stepX = tileSize.x / (float)width;
        float stepY = tileSize.y / (float)height;

        // Loop through each vertex and assign its position and UV coordinates
        int vertexIndex = 0;
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                // Calculate the position of the vertex
                float xPos = (x * stepX) - halfWidth;
                float yPos = (y * stepY) - halfHeight;
                Vector3 vertexPosition = new Vector3(xPos, yPos, 0f) + centerOfMap;

                // Set the vertex position
                vertices[vertexIndex] = vertexPosition;

                // Set the UV coordinates based on the normalized position within the grid
                uv[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                vertexIndex++;
            }
        }

        // Loop through each cell and assign the triangles
        int triangleIndex = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Calculate the indices of the vertices for the current cell
                int topLeft = (y * (width + 1)) + x;
                int topRight = topLeft + 1;
                int bottomLeft = ((y + 1) * (width + 1)) + x;
                int bottomRight = bottomLeft + 1;

                // Assign the triangles for the current cell
                triangles[triangleIndex] = topLeft;
                triangles[triangleIndex + 1] = topRight;
                triangles[triangleIndex + 2] = bottomLeft;

                triangles[triangleIndex + 3] = bottomLeft;
                triangles[triangleIndex + 4] = topRight;
                triangles[triangleIndex + 5] = bottomRight;

                triangleIndex += 6;
            }
        }

        // Assign the vertices, triangles, and UV coordinates to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Calculate the normals and tangents for the mesh
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        // Set the mesh filter's mesh to the generated mesh
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // Return the generated mesh
        Debug.Log("Mesh was generated");

        return mesh;
    }
    public void DrawGizmosGrid(Vector2Int gridSize, Vector3 centerOfMap)
    {
        Vector3 gizmosOffset = new Vector3(0f, gizmosHeight, 0f);

        for (int y = 0; y <= gridSize.y; y++)
        {
            for (int x = 0; x <= gridSize.x; x++)
            {
                Vector3 tilePosition = new Vector3(x, 0f, y) + centerOfMap;

                if (y > gridSize.y / 2)
                {
                    Gizmos.color = gizmosColor;
                    Gizmos.DrawWireCube(tilePosition + gizmosOffset, Vector3.one);
                }
            }
        }
    }

}
