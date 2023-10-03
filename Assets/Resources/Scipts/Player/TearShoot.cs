using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearShoot : MonoBehaviour
{
    public float interval = 0.5f;
    public GameObject tearPrefab;
    protected Transform eyesPos;
    protected Vector2 direction;
    protected float timer;

    protected virtual void Start()
    {
        eyesPos = transform.Find("Eyes");
    }

    protected virtual void Update()
    {
        Shoot();
    }

    // 射击方向
    protected virtual void Shoot()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
        }

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (timer == 0)
            {
                timer = interval;
                Fire();
            }
        }
    }

    // 发射眼泪
    protected virtual void Fire()
    {
        GameObject tear = ObjectPool.Instance.GetObject(tearPrefab);
        tear.transform.position = eyesPos.position;

        tear.GetComponent<Tears>().SetSpeed(direction);

        if (direction == Vector2.down)
        {
            tear.GetComponent<SpriteRenderer>().sortingLayerName = "AbovePlayer";
        }
    }
}
