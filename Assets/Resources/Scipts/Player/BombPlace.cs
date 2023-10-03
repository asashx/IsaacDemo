using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
    public GameObject bombPrefab;
    void Start()
    {

    }
    void PlaceBomb()
    {
        GameObject bomb = ObjectPool.Instance.GetObject(bombPrefab);
        bomb.transform.position = transform.position + new Vector3(0f, -0.1f, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlaceBomb();
        }
    }
}
