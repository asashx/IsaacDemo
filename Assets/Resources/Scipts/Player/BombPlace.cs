using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
    public GameObject bombPrefab;
    private PlayerProperty playerProperty;
    void Start()
    {
        playerProperty = GetComponent<PlayerProperty>();
    }
    void PlaceBomb()
    {
        if (playerProperty.bombNum > 0)
        {
            GameObject bomb = ObjectPool.Instance.GetObject(bombPrefab);
            bomb.transform.position = transform.position + new Vector3(0f, -0.1f, 0f);

            playerProperty.AddBomb(-1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlaceBomb();
        }
    }
}
