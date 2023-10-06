using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    public Direction direction;         // 房间的方向

    [Header("房间信息")]
    public GameObject roomPrefab;       // 房间预制体
    public GameObject bossroomPrefab;   // Boss房间预制体
    public GameObject itemroomPrefab;   // 物品房间预制体
    public int roomNumber = 6;          // 房间数量

    [Header("位置控制")]
    public Transform generatorPoint;    // 生成房间的位置
    public float xOffset = 18.5f;         // 房间之间的水平间距
    public float yOffset = 11f;         // 房间之间的垂直间距
    public LayerMask roomLayer;         // 房间的层级

    public List<Room> rooms = new List<Room>(); // 生成的房间列表
    public List<Room> onewayRooms = new List<Room>(); // 单向房间列表

    void Start()
    {
        GenerateRooms();
    }

    void Update()
    {
        // 生成房间
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //reload current scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        }
    }

    void GenerateRooms()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            ChangePointPos();
        }

        rooms[0].isStart = true;

        Room farRoom = rooms[rooms.Count - 1];

        foreach (Room room in rooms)
        {
            SetRoomDoor(room, room.transform.position);
        }

        foreach (Room room in rooms)
        {
            if (CountDoorNumber(room) == 1 && room != rooms[0])
            {
                onewayRooms.Add(room);
            }
        }

        foreach (Room room in rooms)
        {
            if (room.transform.position.sqrMagnitude > farRoom.transform.position.sqrMagnitude)
            {
                farRoom = room;
            }
        }

        SpecialRooms(farRoom);
    }

    void SpecialRooms(Room farRoom)
    {
        Room selectedRoom = null;
        if (onewayRooms.Count > 0)
        {
            int randomIndex = Random.Range(0, onewayRooms.Count);
            selectedRoom = onewayRooms[randomIndex];
            Vector3 bossRoomPosition = GetAdjacentPosition(selectedRoom.transform.position);
            GameObject bossRoomObject = Instantiate(bossroomPrefab, bossRoomPosition, Quaternion.identity);
            selectedRoom.isWaytoBoss = true;
            Room bossRoom = bossRoomObject.GetComponent<Room>();
            bossRoom.isBoss = true;
            bossRoom.isNormal = false;
            SetRoomDoor(bossRoom, bossRoomPosition);
            SetRoomDoor(selectedRoom, selectedRoom.transform.position);
            onewayRooms.RemoveAt(randomIndex);
        }
        else
        {
            Vector3 bossRoomPosition = GetAdjacentPosition(farRoom.transform.position);
            GameObject bossRoomObject = Instantiate(bossroomPrefab, bossRoomPosition, Quaternion.identity);
            farRoom.isWaytoBoss = true;
            Room bossRoom = bossRoomObject.GetComponent<Room>();
            bossRoom.isBoss = true;
            bossRoom.isNormal = false;
            SetRoomDoor(bossRoom, bossRoomPosition);
            SetRoomDoor(farRoom, farRoom.transform.position);
        }
        if (onewayRooms.Count > 0)
        {
            int randomIndex = Random.Range(0, onewayRooms.Count);
            selectedRoom = onewayRooms[randomIndex];
            Vector3 itemRoomPosition = GetAdjacentPosition(selectedRoom.transform.position);
            GameObject itemRoomObject = Instantiate(itemroomPrefab, itemRoomPosition, Quaternion.identity);
            selectedRoom.isWaytoItem = true;
            Room itemRoom = itemRoomObject.GetComponent<Room>();
            itemRoom.isItem = true;
            itemRoom.isNormal = false;
            SetRoomDoor(itemRoom, itemRoomPosition);
            SetRoomDoor(selectedRoom, selectedRoom.transform.position);
            onewayRooms.RemoveAt(randomIndex);
        }
        else
        {
            do
            {
                int randomIndex = Random.Range(0, rooms.Count);
                selectedRoom = rooms[randomIndex];
            } while (CountDoorNumber(selectedRoom) > 2 || selectedRoom.isWaytoBoss);

            Vector3 itemRoomPosition = GetAdjacentPosition(selectedRoom.transform.position);
            GameObject itemRoomObject = Instantiate(itemroomPrefab, itemRoomPosition, Quaternion.identity);
            selectedRoom.isWaytoItem = true;
            Room itemRoom = itemRoomObject.GetComponent<Room>();
            itemRoom.isItem = true;
            itemRoom.isNormal = false;
            SetRoomDoor(itemRoom, itemRoomPosition);
            SetRoomDoor(selectedRoom, selectedRoom.transform.position);
        }
    }

    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position = new Vector3(generatorPoint.position.x, generatorPoint.position.y + yOffset, generatorPoint.position.z);
                    break;
                case Direction.down:
                    generatorPoint.position = new Vector3(generatorPoint.position.x, generatorPoint.position.y - yOffset, generatorPoint.position.z);
                    break;
                case Direction.left:
                    generatorPoint.position = new Vector3(generatorPoint.position.x - xOffset, generatorPoint.position.y, generatorPoint.position.z);
                    break;
                case Direction.right:
                    generatorPoint.position = new Vector3(generatorPoint.position.x + xOffset, generatorPoint.position.y, generatorPoint.position.z);
                    break;
            }

            if (isIsolated(generatorPoint.position) && !isOverlapping(generatorPoint.position))
            {
                break;
            }
        } while (isOverlapping(generatorPoint.position));
    }

    public void SetRoomDoor(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), 0.2f, roomLayer);
    }

    int CountDoorNumber(Room room)
    {
        int doorNumber = 0;

        if (room.roomUp)
        {
            doorNumber++;
        }
        if (room.roomDown)
        {
            doorNumber++;
        }
        if (room.roomLeft)
        {
            doorNumber++;
        }
        if (room.roomRight)
        {
            doorNumber++;
        }

        return doorNumber;
    }

    bool isOverlapping(Vector3 roomPosition)
    {
        return Physics2D.OverlapCircle(roomPosition, 0.2f, roomLayer);
    }

    Vector3 GetAdjacentPosition(Vector3 roomPosition)
    {
        Vector3 adjacentPosition = Vector3.zero;
        Vector3[] offsets = new Vector3[]
        {
        new Vector3(0f, yOffset, 0f),
        new Vector3(0f, -yOffset, 0f),
        new Vector3(-xOffset, 0f, 0f),
        new Vector3(xOffset, 0f, 0f)
        };

        bool exist = false;

        foreach (Vector3 offset in offsets)
        {
            Vector3 newPosition = roomPosition + offset;

            if (isIsolated(newPosition) && !isOverlapping(newPosition))
            {
                adjacentPosition = newPosition;
                exist = true;
                break;
            }
        }
        if (!exist)
        {
            foreach (Vector3 offset in offsets)
            {
                Vector3 newPosition = roomPosition + offset;

                if (!isOverlapping(newPosition))
                {
                    adjacentPosition = newPosition;
                    break;
                }
            }
        }

        return adjacentPosition;
    }

    bool isIsolated(Vector3 position)
    {
        int count = 0;

        if (Physics2D.OverlapCircle(position + new Vector3(0f, yOffset, 0f), 0.2f, roomLayer))
        {
            count++;
        }
        if (Physics2D.OverlapCircle(position + new Vector3(0f, -yOffset, 0f), 0.2f, roomLayer))
        {
            count++;
        }
        if (Physics2D.OverlapCircle(position + new Vector3(-xOffset, 0f, 0f), 0.2f, roomLayer))
        {
            count++;
        }
        if (Physics2D.OverlapCircle(position + new Vector3(xOffset, 0f, 0f), 0.2f, roomLayer))
        {
            count++;
        }
        if (count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
