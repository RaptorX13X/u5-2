using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private uint seed = 1;
    [SerializeField] private bool regenerateOnUpdate;
    [SerializeField] private Vector2 roomSize = Vector2.one;
    [SerializeField] private Vector2 segmentSize;

    [SerializeField] private Mesh meshA;
    [SerializeField] private Mesh meshB;
    [SerializeField] private Mesh meshC;
    [SerializeField] private Material material;
    
    private List<Matrix4x4> wallsA;
    private List<Matrix4x4> wallsB;
    private List<Matrix4x4> wallsC;
    private Random random;

    private void Start()
    {
        random = new Random(seed);
        wallsA = new List<Matrix4x4>();
        wallsB = new List<Matrix4x4>();
        wallsC = new List<Matrix4x4>();
        GenerateWalls();
    }

    private void GenerateWalls()
    {
        wallsA.Clear();
        wallsB.Clear();
        wallsC.Clear();
        random.InitState(seed);
        
        int wallCount = Mathf.Max(1,(int)(roomSize.x / segmentSize.x));
        float scale = (roomSize.x / wallCount) / segmentSize.x;
        for (int i = 0; i < wallCount; i++)
        {
            Vector3 t = transform.position + new Vector3((-roomSize.x / 2f) +  (i * segmentSize.x * scale) + (segmentSize.x * scale / 2f), 0f, roomSize.y / 2f);
            Quaternion r = Quaternion.Euler(0f, 90f, 0f);
            Vector3 s = new Vector3(1f, 1f, scale);
            
            Matrix4x4 matrix = Matrix4x4.TRS(t, r, s);
            int randomNumber = random.NextInt(0, 3);
            
            if      (randomNumber == 0) wallsA.Add(matrix);
            else if (randomNumber == 1) wallsB.Add(matrix);
            else if (randomNumber == 2) wallsC.Add(matrix);
        }
        
        for (int i = 0; i < wallCount; i++)
        {
            Vector3 t = transform.position + new Vector3((-roomSize.x / 2f) +  (i * segmentSize.x * scale) + (segmentSize.x * scale / 2f), 0f, -roomSize.y / 2f);
            Quaternion r = Quaternion.Euler(0f, 90f, 0f);
            Vector3 s = new Vector3(1f, 1f, scale);
            
            Matrix4x4 matrix = Matrix4x4.TRS(t, r, s);
            int randomNumber = random.NextInt(0, 3);
            
            if      (randomNumber == 0) wallsA.Add(matrix);
            else if (randomNumber == 1) wallsB.Add(matrix);
            else if (randomNumber == 2) wallsC.Add(matrix);
        }
        wallCount = Mathf.Max(1, (int)(roomSize.y / segmentSize.y));
        scale = (roomSize.y / wallCount) / segmentSize.y;
        for (int i = 0; i < wallCount; i++)
        {
            Vector3 t = transform.position + new Vector3(roomSize.x / 2f, 0f, (-roomSize.y / 2f) +  (i * segmentSize.y * scale) + (segmentSize.y * scale / 2f));
            Quaternion r = Quaternion.Euler(0f, 0f, 0f);
            Vector3 s = new Vector3(1f, 1f, scale);
            
            Matrix4x4 matrix = Matrix4x4.TRS(t, r, s);
            int randomNumber = random.NextInt(0, 3);
            
            if      (randomNumber == 0) wallsA.Add(matrix);
            else if (randomNumber == 1) wallsB.Add(matrix);
            else if (randomNumber == 2) wallsC.Add(matrix);
        }
        
        for (int i = 0; i < wallCount; i++)
        {
            Vector3 t = transform.position + new Vector3(-roomSize.x / 2f, 0f, (-roomSize.y / 2f) +  (i * segmentSize.y * scale) + (segmentSize.y * scale / 2f));
            Quaternion r = Quaternion.Euler(0f, 0f, 0f);
            Vector3 s = new Vector3(1f, 1f, scale);
            
            Matrix4x4 matrix = Matrix4x4.TRS(t, r, s);
            int randomNumber = random.NextInt(0, 3);
            
            if      (randomNumber == 0) wallsA.Add(matrix);
            else if (randomNumber == 1) wallsB.Add(matrix);
            else if (randomNumber == 2) wallsC.Add(matrix);
        }
    }

    private void Update()
    {
        if(regenerateOnUpdate || Input.GetKeyDown(KeyCode.Space))
        {
            GenerateWalls();
        }
        Graphics.DrawMeshInstanced(meshA, 0, material, wallsA.ToArray(), wallsA.Count);
        Graphics.DrawMeshInstanced(meshB, 0, material, wallsB.ToArray(), wallsB.Count);
        Graphics.DrawMeshInstanced(meshC, 0, material, wallsC.ToArray(), wallsC.Count);
    }
}