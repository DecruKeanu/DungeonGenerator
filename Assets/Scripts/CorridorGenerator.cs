using UnityEngine;
using System.Collections.Generic;

public sealed class CorridorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _corridor = null, _corridorCorner = null, _parent = null;

    private List<RoomGenerator> _roomGenerators;

    public List<GameObject> _corridorObjects = new List<GameObject>();

    private Direction _previousDirection = Direction.forward;

    public bool IsInRoom { get; set; } = true;

    public enum Direction
    {
        forward,
        backward,
        left,
        right,
        up,
        down
    }


    public void CreateCorridors()
    {
        _roomGenerators = FindObjectOfType<RoomManager>().RoomGenerators;

        for (int idx = 0; idx < _roomGenerators.Count; idx++)
        {
            //Connection is been made to the closest room that does not have a connection yet, if this can't be found -1 is returned
            int closestRoomIdx = FindClosestRoom(idx);
            if (closestRoomIdx != -1)
                CreateConnection(_roomGenerators[idx], _roomGenerators[closestRoomIdx]);

            //StartRoom and EndRoom are beign hollowed to remove unwanted corridorObjects
            _roomGenerators[idx].HollowRoom(_corridorObjects);
            if (closestRoomIdx != -1)
                _roomGenerators[closestRoomIdx].HollowRoom(_corridorObjects);
        }
    }


    private int FindClosestRoom(int fromIndex)
    {
        int closestRoomIndex = -1;
        float minDistance = Mathf.Infinity;

        for (int roomIdx = 0; roomIdx < _roomGenerators.Count; roomIdx++)
        {
            if (fromIndex == roomIdx)
                continue;

            float distance = Vector3.Distance(_roomGenerators[roomIdx].transform.position, _roomGenerators[fromIndex].transform.position);

            if (distance < minDistance && _roomGenerators[roomIdx].IsConnectionMade == false)
            {
                closestRoomIndex = roomIdx;
                minDistance = distance;

                _roomGenerators[fromIndex].IsConnectionMade = true;
            }
        }

        return closestRoomIndex;
    }

    private void CreateConnection(RoomGenerator from, RoomGenerator to)
    {
        //start from center of room on the floor
        transform.position = from.CenterPoint();
        Vector3Int targetPoint = to.CenterPoint();

        while (transform.position != targetPoint)
        {
            Direction agentDirection = CalculateDirection(targetPoint);
            MoveAgent(agentDirection);
            CreateCorridor(agentDirection);
        }
    }

    private Direction CalculateDirection(Vector3 targetPos)
    {
        Direction direction = Direction.forward;
        Vector3Int agentPos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);

        if (agentPos.x < targetPos.x)
        {
            direction = Direction.right;
        }
        else if (agentPos.x > targetPos.x)
        {
            direction = Direction.left;
        }
        else if (agentPos.z < targetPos.z)
        {
            direction = Direction.forward;
        }
        else if (agentPos.z > targetPos.z)
        {
            direction = Direction.backward;
        }
        else if (agentPos.y < targetPos.y)
        {
            direction = Direction.up;
        }
        else if (agentPos.y > targetPos.y)
        {
            direction = Direction.down;
        }

        return direction;
    }

    private void MoveAgent(Direction direction)
    {
        Vector3 agentPos = transform.position;

        switch (direction)
        {
            case Direction.left:
                agentPos.x -= 1;
                break;
            case Direction.right:
                agentPos.x += 1;
                break;
            case Direction.forward:
                agentPos.z += 1;
                break;
            case Direction.backward:
                agentPos.z -= 1;
                break;
            case Direction.up:
                agentPos.y += 1;
                break;
            case Direction.down:
                agentPos.y -= 1;
                break;
        }

        transform.position = agentPos;
    }

    private void CreateCorridor(Direction direction)
    {
        GameObject corridorObject;
        Vector3 agentPos = transform.position;
        bool isDirectionChanged = (direction != _previousDirection);

        if (isDirectionChanged)
        {
            corridorObject = Instantiate(_corridorCorner, agentPos, Quaternion.identity, _parent.transform);

            switch (direction)
            {
                case Direction.left:
                    if (_previousDirection == Direction.forward)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 0, 0));
                    }
                    if (_previousDirection == Direction.backward)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, -90, 0));
                    }
                    if (_previousDirection == Direction.up)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, -90, 90));
                    }
                    if (_previousDirection == Direction.down)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 90, -90));
                    }
                    break;
                case Direction.right:
                    if (_previousDirection == Direction.forward)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, -90, 0));
                    }
                    if (_previousDirection == Direction.backward)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 90, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x - 1, agentPos.y, agentPos.z + 1);
                    }
                    if (_previousDirection == Direction.up)
                    {
                        corridorObject.transform.Rotate(new Vector3(90, 180, 0));
                    }
                    if (_previousDirection == Direction.down)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, 180, 0));
                    }
                    break;
                case Direction.forward:
                    if (_previousDirection == Direction.left)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, -90, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x + 1, agentPos.y, agentPos.z - 1);
                    }
                    if (_previousDirection == Direction.right)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 180, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y, agentPos.z);
                    }
                    if (_previousDirection == Direction.up)
                    {
                        corridorObject.transform.Rotate(new Vector3(90, -90, 0));
                    }
                    if (_previousDirection == Direction.down)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, -90, 0));
                    }
                    break;
                case Direction.backward:
                    if (_previousDirection == Direction.left)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 0, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y, agentPos.z);
                    }
                    if (_previousDirection == Direction.right)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 90, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x - 1, agentPos.y, agentPos.z + 1);

                    }
                    if (_previousDirection == Direction.up)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, 0, 90));
                    }
                    if (_previousDirection == Direction.down)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 0, 90));
                    }
                    break;
                case Direction.up:
                    if (_previousDirection == Direction.forward)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 0, 90));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y - 1, agentPos.z - 1);
                    }
                    if (_previousDirection == Direction.backward)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 180, 90));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y - 1, agentPos.z + 1);
                    }
                    if (_previousDirection == Direction.left)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, -90, 90));
                        corridorObject.transform.position = new Vector3(agentPos.x - 1, agentPos.y, agentPos.z);
                    }
                    if (_previousDirection == Direction.right)
                    {
                        corridorObject.transform.Rotate(new Vector3(0, 90, 90));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y - 1, agentPos.z);
                    }
                    break;
                case Direction.down:
                    if (_previousDirection == Direction.forward)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, 90, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y, agentPos.z);
                    }
                    if (_previousDirection == Direction.backward)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, -90, 0));
                        corridorObject.transform.position = new Vector3(agentPos.x, agentPos.y, agentPos.z);
                    }
                    if (_previousDirection == Direction.left)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                    if (_previousDirection == Direction.right)
                    {
                        corridorObject.transform.Rotate(new Vector3(-90, 180, 0));
                    }
                    break;
            }
        }
        else
        {
            corridorObject = Instantiate(_corridor, agentPos, Quaternion.identity, _parent.transform);

            switch (direction)
            {
                case Direction.left:
                    corridorObject.transform.Rotate(new Vector3(0, 90, 0));
                    break;
                case Direction.right:
                    corridorObject.transform.Rotate(new Vector3(0, 90, 0));
                    break;
                case Direction.up:
                    corridorObject.transform.Rotate(new Vector3(90, 0, 0));
                    break;
                case Direction.down:
                    corridorObject.transform.Rotate(new Vector3(90, 0, 0));
                    break;
            }
        }

        _corridorObjects.Add(corridorObject);
        _previousDirection = direction;
    }
}
