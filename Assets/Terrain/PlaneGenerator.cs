using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    private MeshFilter meshFilter;
    public Vector2 size = new Vector2(10f, 10f);
    [SerializeField] private int resolution = 5;
    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Generate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        }
    }

    private void Generate()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.uv = GenerateUVs();
        mesh.RecalculateNormals();
        
        meshFilter.mesh = mesh;
    }

    private Vector3[] GenerateVertices()
    {
        List<Vector3> vertices = new List<Vector3>();
        
        float xStep = size.x / resolution;
        float yStep = size.y / resolution;
        
        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                vertices.Add(new Vector3(x*xStep, 0f, y*yStep));
            }
        }
        
        return vertices.ToArray();
    }

    private int[] GenerateTriangles()
    {
        List<int> triangles = new List<int>();

        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;
                
                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);
                
                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);
            }   
        }
        return triangles.ToArray();
    }

    private Vector2[] GenerateUVs()
    {
        List<Vector2> uvs = new List<Vector2>();
        
        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                uvs.Add(new Vector2((float)x / resolution, (float)y / resolution));
            }
        }
        
        return uvs.ToArray();
    }
}
