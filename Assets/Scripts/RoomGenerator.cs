using System.Collections.Generic;
using UnityEngine;

public sealed class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject _cube = null;

    private List<GameObject> _cubes = new List<GameObject>();
    public List<GameObject> Cubes { get => _cubes; }

    private List<RectInt3D> _room = new List<RectInt3D>();
    public List<RectInt3D> Room { get => _room; }

    private int _minWidth, _maxWidth, _minHeight, _maxHeight, _minDepth, _maxDepth, _numberOfRects, _maxOffset;

    public bool IsConnectionMade = false;

    public void GenerateRoom(int minWidth, int maxWidth, int minHeight, int maxHeight, int minDepth, int maxDepth, int numberOfRects, int maxOffset)
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
        _minDepth = minDepth;
        _maxDepth = maxDepth;
        _numberOfRects = numberOfRects;
        _maxOffset = maxOffset;

        GenerateRoomFull();
        HollowRoom();
        RemoveDuplicates();
    }

    private void GenerateRoomFull()
    {
        for (int idx = 0; idx < _numberOfRects; idx++)
        {
            GenerateRect(transform);
        }
    }

    private void HollowRoom()
    {
        foreach (RectInt3D room in _room)
        {
            foreach (GameObject cube in _cubes.ToArray())
            {
                if (room.Inside.Contains(cube.transform.position))
                {
                    _cubes.Remove(cube);
                    Destroy(cube);
                }
            }
        }
    }

    public void HollowRoom(List<GameObject> objects)
    {
        foreach (RectInt3D room in _room)
        {
            Vector3 worldPos = transform.position;
            Vector3Int worldPosInt = new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);

            RectInt3D worldRect = room.GetWorldRect(worldPosInt);

            foreach (GameObject @object in objects)
            {
                foreach (Transform piece in @object.transform)
                {
                    if (worldRect.Inside.Contains(piece.position))
                    {
                        Destroy(piece.gameObject);
                    }
                }
            }
        }
    }

    private void RemoveDuplicates()
    {
        List<Vector3> cubePositions = new List<Vector3>();
        foreach (GameObject cube in _cubes.ToArray())
        {
            if (cubePositions.Contains(cube.transform.position))
            {
                _cubes.Remove(cube);
                Destroy(cube);
            }
            else
            {
                cubePositions.Add(cube.transform.position);
            }
        }
    }


    private void GenerateRect(Transform parent)
    {
        int XPos = Random.Range(0, _maxOffset + 1);
        int YPos = Random.Range(0, _maxOffset + 1);
        int ZPos = Random.Range(0, _maxOffset + 1);

        int width = Random.Range(_minWidth, _maxWidth + 1);
        int height = Random.Range(_minHeight, _maxHeight + 1);
        int depth = Random.Range(_minDepth, _maxDepth + 1);

        RectInt3D rect = new RectInt3D(XPos, YPos, ZPos, width, height, depth);

        for (int x = rect.x; x <= rect.Width; ++x)
        {
            for (int y = rect.y; y <= rect.Height; ++y)
            {
                for (int z = ZPos; z <= rect.Depth; ++z)
                {
                    if (x == XPos || x == rect.Width || y == YPos || y == rect.Height || z == ZPos || z == rect.Depth)
                    {
                        GameObject cube = Instantiate(_cube, new Vector3(x, y, z), Quaternion.identity, parent);
                        _cubes.Add(cube);
                    }
                }
            }
        }

        _room.Add(rect);
    }

    public Vector3Int CenterPoint()
    {
        Vector3Int centerPoint = Vector3Int.zero;

        int yPos = int.MaxValue;

        foreach (RectInt3D rectangle in _room)
        {
            Vector3 worldPos = transform.position;
            Vector3Int worldPosInt = new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);

            if (yPos > worldPos.y)
            {
                yPos = worldPosInt.y;
            }

            centerPoint += rectangle.GetWorldRectCenter(worldPosInt);
        }

        centerPoint.x /= _room.Count;
        centerPoint.y /= _room.Count;
        centerPoint.z /= _room.Count;

        return centerPoint;
    }

    public Vector3Int CenterPointBottom()
    {
        Vector3Int centerPoint = Vector3Int.zero;

        int yPos = int.MaxValue;

        foreach (RectInt3D rectangle in _room)
        {
            Vector3 worldPos = transform.position;
            Vector3Int worldPosInt = new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);

            if (yPos > worldPos.y)
            {
                yPos = worldPosInt.y;
            }

            centerPoint += rectangle.GetWorldRectCenter(worldPosInt);
        }

        centerPoint.x /= _room.Count;
        centerPoint.y = yPos + 2;
        centerPoint.z /= _room.Count;

        return centerPoint;
    }
}
