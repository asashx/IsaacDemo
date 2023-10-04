using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    protected virtual void PickedUp()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerProperty playerProperty = other.GetComponent<PlayerProperty>();

            if (playerProperty != null)
            {
                switch (itemName)
                {
                    case "Bomb":
                        playerProperty.AddBomb(1);
                        break;
                    case "Coin":
                        playerProperty.AddCoin(1);
                        break;
                    case "Key":
                        playerProperty.AddKey(1);
                        break;
                    default:
                        break;
                }
            }
            PickedUp();
        }
    }
}
