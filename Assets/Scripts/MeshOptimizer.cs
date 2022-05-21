using System.Collections.Generic;
using UnityEngine;

public sealed class MeshOptimizer
{
    private int _gridSize;
    private bool[,,] _array3D = null;

    private List<int> _indexBuffer = new List<int>();
    private List<Vector3> _vertexBuffer = new List<Vector3>();

    public void Initialize(int gridSize)
    {
        _gridSize = gridSize;
        _array3D = new bool[gridSize, gridSize, gridSize];
    }

    public void Clear()
    {
        _array3D = null;
        _indexBuffer.Clear();
        _vertexBuffer.Clear();
    }

    public void Set(Vector3Int coordinate, bool value)
    {
        _array3D[coordinate.x, coordinate.y, coordinate.z] = value;
    }

    public void Set(Vector3Int coordinate, Vector3Int endCoordinate, bool value)
    {
        for (int x = coordinate.x; x <= endCoordinate.x; x++)
        {
            for (int y = coordinate.y; y <= endCoordinate.y; y++)
            {
                for (int z = coordinate.z; z <= endCoordinate.z; z++)
                {
                    _array3D[x, y, z] = value;
                }
            }
        }
    }

    public bool Get(Vector3Int coordinate)
    {
        return _array3D[coordinate.x, coordinate.y, coordinate.z];
    }

    public void ProcessCubes(List<GameObject> cubes)
    {
        foreach (GameObject cube in cubes)
        {
            int halfGrid = _gridSize / 2;

            if (cube == null || cube.GetComponent<OverlapCheck>() != null || cube.activeSelf == false)
            {
                continue;
            }

            Vector3 cubePosition = cube.transform.position;
            Vector3Int arrayPos = new Vector3Int((int)(cubePosition.x + 0.1f) + halfGrid - 1, (int)(cubePosition.y + 0.1f) + halfGrid - 1, (int)(cubePosition.z + 0.1f) + halfGrid - 1);

            Set(arrayPos, true);
        }
    }

    private void ProccesCube(Vector3Int coordinate)
    {
        if (Get(coordinate) == false)
            return;

        int lastX = ProccesCubeX(coordinate);
        int lastY = ProccesCubeY(coordinate, lastX);
        int lastZ = ProccesCubeZ(coordinate, lastX, lastY);

        Vector3Int endCoordinate = new Vector3Int(lastX, lastY, lastZ);

        Set(coordinate, endCoordinate, false);
        GenerateCube(coordinate, endCoordinate);
    }
    private int ProccesCubeX(Vector3Int coordinate)
    {
        for (int x = coordinate.x; x < _gridSize; ++x)
        {
            if (Get(new Vector3Int(x, coordinate.y, coordinate.z)) == false)
            {
                return --x;
            }
        }

        return _gridSize - 1;
    }

    private int ProccesCubeY(Vector3Int coordinate, int lastX)
    {
        int endX = lastX;

        for (int y = coordinate.y + 1; y < _gridSize; ++y)
        {
            for (int x = coordinate.x; x <= endX; ++x)
            {
                if (Get(new Vector3Int(x, y, coordinate.z)) == false)
                {
                    return --y;
                }
            }
        }

        return _gridSize - 1;
    }

    private int ProccesCubeZ(Vector3Int coordinate, int lastX, int lastY)
    {
        int endX = lastX;
        int endY = lastY;

        for (int z = coordinate.z + 1; z < _gridSize; z++)
        {
            for (int y = coordinate.y; y <= endY; ++y)
            {
                for (int x = coordinate.x; x <= endX; ++x)
                {
                    if (Get(new Vector3Int(x, y, z)) == false)
                    {
                        return --z;
                    }
                }
            }
        }

        return _gridSize - 1;
    }

    private void GenerateCube(Vector3Int coordinate, Vector3Int endCoordinate)
    {
        int indexOffset = _vertexBuffer.Count;

        Vector3Int gridOffset = new Vector3Int(_gridSize / 2, _gridSize / 2, _gridSize / 2);
        coordinate -= gridOffset;
        endCoordinate -= gridOffset;
        endCoordinate.x++;
        endCoordinate.y++;
        endCoordinate.z++;

        //front
        {
            _vertexBuffer.Add(new Vector3(coordinate.x, coordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, coordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, endCoordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, endCoordinate.y, coordinate.z));

            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset + 1);
            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 3);
            _indexBuffer.Add(indexOffset + 2);

            indexOffset += 4;
        }

        //back
        {
            _vertexBuffer.Add(new Vector3(coordinate.x, coordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, coordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, endCoordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, endCoordinate.y, endCoordinate.z));

            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 1);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset + 3);

            indexOffset += 4;

        }


        //top
        {
            _vertexBuffer.Add(new Vector3(coordinate.x, endCoordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, endCoordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, endCoordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, endCoordinate.y, endCoordinate.z));


            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset + 1);
            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 3);
            _indexBuffer.Add(indexOffset + 2);

            indexOffset += 4;
        }

        //bottom
        {
            _vertexBuffer.Add(new Vector3(coordinate.x, coordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, coordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, coordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, coordinate.y, endCoordinate.z));


            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 1);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset + 3);

            indexOffset += 4;
        }

        //left
        {
            _vertexBuffer.Add(new Vector3(coordinate.x, coordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, coordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, endCoordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(coordinate.x, endCoordinate.y, coordinate.z));

            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 1);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset + 3);


            indexOffset += 4;
        }


        //right
        {
            _vertexBuffer.Add(new Vector3(endCoordinate.x, coordinate.y, coordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, coordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, endCoordinate.y, endCoordinate.z));
            _vertexBuffer.Add(new Vector3(endCoordinate.x, endCoordinate.y, coordinate.z));

            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 2);
            _indexBuffer.Add(indexOffset + 1);
            _indexBuffer.Add(indexOffset);
            _indexBuffer.Add(indexOffset + 3);
            _indexBuffer.Add(indexOffset + 2);
        }
    }

    public Mesh GenerateMesh()
    {
        Mesh optimizedMesh = new Mesh();

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                for (int z = 0; z < _gridSize; z++)
                {
                    ProccesCube(new Vector3Int(x, y, z));
                }
            }
        }
        optimizedMesh.Clear();
        optimizedMesh.vertices = _vertexBuffer.ToArray();
        optimizedMesh.triangles = _indexBuffer.ToArray();
        optimizedMesh.Optimize();


        optimizedMesh.RecalculateNormals();
        optimizedMesh.RecalculateTangents();

        return optimizedMesh;
    }
}
