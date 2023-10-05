using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;//用来存放各个位置的门

    public bool roomLeft, roomRight, roomUp, roomDown;//判断上下左右是否有房间

    public bool isWaytoBoss, isBoss, isWaytoItem, isItem, isStart, isNormal;//判断房间类型

    void Awake()
    {
        isWaytoBoss = false;
        isBoss = false;
        isWaytoItem = false;
        isItem = false;
        isStart = false;
        isNormal = true;
    }
    
    void Start()
    {
        //对应方向的门是否显示，关联对应方向是否有其他房间
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

}