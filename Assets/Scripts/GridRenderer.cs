using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public sealed class GridRenderer : MonoBehaviour
{
    [SerializeField]
    private int _size = 40;
    public int Size { get => _size; }

    private void Start()
    {
        DrawGrid(40);
    }

    public void DrawGrid(int size)
    {
        _size = size;

        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        List<int> indicies = new List<int>();
        List<Vector3> verticies = new List<Vector3>();

        float gridStart = -_size / 2f;
        float gridEnd = _size / 2f;

        DrawCube(verticies, indicies, gridStart, gridEnd);
        DrawCube(verticies, indicies, gridStart, gridStart + 1);

        mesh.vertices = verticies.ToArray();
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;

        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.white;
    }

    private void DrawCube(List<Vector3> vertexBuffer, List<int> indexBuffer, float gridStart, float gridEnd)
    {
        int indexOffset = vertexBuffer.Count;

        //Vertex 1 (-0.5,-0.5,-0.5)
        vertexBuffer.Add(new Vector3(gridStart, gridStart, gridStart));

        //Vertex 2 (-0.5,0.5,-0.5)
        vertexBuffer.Add(new Vector3(gridStart, gridEnd, gridStart));

        //Vertex 3 (0.5,-0.5,-0.5)
        vertexBuffer.Add(new Vector3(gridEnd, gridStart, gridStart));

        //Vertex 4 (0.5,0.5,-0.5)
        vertexBuffer.Add(new Vector3(gridEnd, gridEnd, gridStart));

        //Vertex 5 (-0.5,-0.5,0.5)
        vertexBuffer.Add(new Vector3(gridStart, gridStart, gridEnd));

        //Vertex 6 (-0.5,0.5,0.5)
        vertexBuffer.Add(new Vector3(gridStart, gridEnd, gridEnd));

        //Vertex 7 (0.5,-0.5,0.5)
        vertexBuffer.Add(new Vector3(gridEnd, gridStart, gridEnd));

        //Vertex 8 (0.5,0.5,0.5)
        vertexBuffer.Add(new Vector3(gridEnd, gridEnd, gridEnd));

        //Line 1 (Vertex 1 -> Vertex 2)
        indexBuffer.Add(indexOffset);
        indexBuffer.Add(indexOffset + 1);

        //Line 2 (Vertex 1 -> Vertex 3)
        indexBuffer.Add(indexOffset);
        indexBuffer.Add(indexOffset + 2);

        //Line 3 (Vertex 1 -> Vertex 5)
        indexBuffer.Add(indexOffset);
        indexBuffer.Add(indexOffset + 4);

        //Line 4 (Vertex 2 -> Vertex 4)
        indexBuffer.Add(indexOffset + 1);
        indexBuffer.Add(indexOffset + 3);

        //Line 5 (Vertex 2 -> Vertex 6)
        indexBuffer.Add(indexOffset + 1);
        indexBuffer.Add(indexOffset + 5);

        //Line 6 (Vertex 3 -> Vertex 4)
        indexBuffer.Add(indexOffset + 2);
        indexBuffer.Add(indexOffset + 3);

        //Line 7 (Vertex 3 -> Vertex 7)
        indexBuffer.Add(indexOffset + 2);
        indexBuffer.Add(indexOffset + 6);

        //Line 8 (Vertex 4 -> Vertex 8)
        indexBuffer.Add(indexOffset + 3);
        indexBuffer.Add(indexOffset + 7);

        //Line 9 (Vertex 5 -> Vertex 6)
        indexBuffer.Add(indexOffset + 4);
        indexBuffer.Add(indexOffset + 5);

        //Line 10 (Vertex 5 -> 7)
        indexBuffer.Add(indexOffset + 4);
        indexBuffer.Add(indexOffset + 6);

        //Line 11 (Vertex 6 -> Vertex 8)
        indexBuffer.Add(indexOffset + 5);
        indexBuffer.Add(indexOffset + 7);

        //Line 12 (Vertex 7 -> Vertex 8)
        indexBuffer.Add(indexOffset + 6);
        indexBuffer.Add(indexOffset + 7);
    }


}
