using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open;
    GameObject Camera;
    GameObject Player;
    GameObject Loading;
    private Vector3 targetCameraPosition;
    private bool isMoving;
    public bool isUp, isDown, isLeft, isRight;
    public float xOffset, yOffset;
    public float x, y;
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        Player = GameObject.Find("Isaac");
        Loading = GameObject.Find("LoadManager");
        xOffset = 6.1f;
        yOffset = 4.6f;
        x = 18.5f;
        y = 11f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isUp)
            {
                Loading.transform.position = new Vector3(Loading.transform.position.x, Loading.transform.position.y + y, Loading.transform.position.z);
                MoveCamera(new Vector3(Camera.transform.position.x, Camera.transform.position.y + y, Camera.transform.position.z));
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + yOffset, Player.transform.position.z);
            }
            else if (isDown)
            {
                Loading.transform.position = new Vector3(Loading.transform.position.x, Loading.transform.position.y - y, Loading.transform.position.z);
                MoveCamera(new Vector3(Camera.transform.position.x, Camera.transform.position.y - y, Camera.transform.position.z));
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - yOffset, Player.transform.position.z);
            }
            else if (isLeft)
            {
                Loading.transform.position = new Vector3(Loading.transform.position.x - x, Loading.transform.position.y, Loading.transform.position.z);
                MoveCamera(new Vector3(Camera.transform.position.x - x, Camera.transform.position.y, Camera.transform.position.z));
                Player.transform.position = new Vector3(Player.transform.position.x - xOffset, Player.transform.position.y, Player.transform.position.z);
            }
            else if (isRight)
            {
                Loading.transform.position = new Vector3(Loading.transform.position.x + x, Loading.transform.position.y, Loading.transform.position.z);
                MoveCamera(new Vector3(Camera.transform.position.x + x, Camera.transform.position.y, Camera.transform.position.z));
                Player.transform.position = new Vector3(Player.transform.position.x + xOffset, Player.transform.position.y, Player.transform.position.z);
            }
        }
    }

    private void MoveCamera(Vector3 target)
    {
        targetCameraPosition = target;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, targetCameraPosition, 0.1f);
            if (Vector3.Distance(Camera.transform.position, targetCameraPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
