using UnityEngine;
using System.Collections.Generic;
public sealed class RoomManager : MonoBehaviour
{
    [SerializeField]
    GameObject _roomGenerator = null, _parent = null;

    [SerializeField]
    private List<RoomGenerator> _roomGenerators = new List<RoomGenerator>();
    public List<RoomGenerator> RoomGenerators { get => _roomGenerators; }

    public int MinWidth, MaxWidth, MinHeight, MaxHeight, MinDepth, MaxDepth;

    public int _numberOfRects = 1, _maxOffset = 0;

    public int _numberOfRooms = 6, _spawnAttempts = 10;

    private int _gridSize;
    public int GridSize { get => _gridSize; }

    private void Start()
    {
        MinWidth = 6;
        MaxWidth = 10;
        MinHeight = 6;
        MaxHeight = 10;
        MinDepth = 6;
        MaxDepth = 10;

        _numberOfRects = 1;
        _maxOffset = 0;
        _numberOfRooms = 6;
        _spawnAttempts = 10;
    }

    public void GenerateRooms()
    {
        _gridSize = FindObjectOfType<GridRenderer>().Size;

        //Generate rooms
        for (int idx = 0; idx < _numberOfRooms; idx++)
        {
            GameObject roomGeneratorObject = Instantiate(_roomGenerator, _parent.transform);
            RoomGenerator roomGenerator = roomGeneratorObject.GetComponent<RoomGenerator>();
            roomGenerator.GenerateRoom(MinWidth, MaxWidth, MinHeight, MaxHeight, MinDepth, MaxDepth, _numberOfRects, _maxOffset);

            roomGeneratorObject.transform.position = new Vector3(0, 0, _gridSize);
            _roomGenerators.Add(roomGenerator);
        }
    }

    public void PlaceRooms()
    {
        List<RoomGenerator> toRemove = new List<RoomGenerator>();
        for (int idx = 0; idx < _roomGenerators.Count; idx++)
        {
            if (PlaceRoom(idx, _spawnAttempts) == false)
            {
                toRemove.Add(_roomGenerators[idx]);
                _roomGenerators[idx].transform.position = new Vector3(0, 0, _gridSize);
            }
        }

        //Remove rooms with wrong locations
        foreach (RoomGenerator room in toRemove)
        {
            _roomGenerators.Remove(room);
            Destroy(room.gameObject);
        }
    }

    private bool PlaceRoom(int roomIdx, int attempts)
    {
        for (int idx = 0; idx < attempts; idx++)
        {
            int XPos = Random.Range(-_gridSize / 2, _gridSize / 2 + 1);
            int YPos = Random.Range(-_gridSize / 2, _gridSize / 2 + 1);
            int ZPos = Random.Range(-_gridSize / 2, _gridSize / 2 + 1);

            _roomGenerators[roomIdx].gameObject.transform.position = new Vector3(XPos, YPos, ZPos);

            if (IsInGrid(roomIdx) && NotOverlappingWithRooms(roomIdx))
            {
                return true;
            }
        }

        return false;
    }

    private List<RectInt3D> GetOtherRoomRects(int currentRoomIdx)
    {
        List<RectInt3D> otherRects = new List<RectInt3D>();

        for (int rectIdx = 0; rectIdx < _roomGenerators.Count; rectIdx++)
        {
            if (currentRoomIdx == rectIdx)
                continue;

            Vector3 worldPos = _roomGenerators[rectIdx].transform.position;
            Vector3Int worldPosInt = new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);

            foreach (RectInt3D rectangle in _roomGenerators[rectIdx].Room)
            {
                RectInt3D worldRect = rectangle.GetWorldRect(worldPosInt);
                otherRects.Add(worldRect.Outside);
            }
        }

        return otherRects;
    }

    private bool IsInGrid(int roomIdx)
    {
        int gridStart = -_gridSize / 2;

        Vector3 worldPos = _roomGenerators[roomIdx].transform.position;
        Vector3Int worldPosInt = new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);

        RectInt3D grid = new RectInt3D(gridStart, gridStart, gridStart, _gridSize, _gridSize, _gridSize);
        foreach (RectInt3D rectangle in _roomGenerators[roomIdx].Room)
        {
            RectInt3D worldRect = rectangle.GetWorldRect(worldPosInt);
            if (grid.Contains(worldRect) == false)
                return false;
        }

        return true;
    }

    private bool NotOverlappingWithRooms(int roomIdx)
    {
        List<RectInt3D> otherRects = GetOtherRoomRects(roomIdx);

        Vector3 worldPos = _roomGenerators[roomIdx].transform.position;
        Vector3Int worldPosInt = new Vector3Int((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);

        foreach (RectInt3D rectangle in _roomGenerators[roomIdx].Room)
        {
            RectInt3D worldRect = rectangle.GetWorldRect(worldPosInt);
            foreach (RectInt3D otherRectangle in otherRects)
            {
                if (worldRect.overlaps(otherRectangle) == true)
                    return false;
            }
        }
        return true;
    }
}
