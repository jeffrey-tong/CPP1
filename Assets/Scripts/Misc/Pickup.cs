using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Powerup,
        Life,
        Score,
        Big
    }

    public PickupType currentPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController temp = collision.gameObject.GetComponent<PlayerController>();
            switch (currentPickup)
            {
                case PickupType.Powerup:
                    temp.StartJumpForceChange();
                    break;
                case PickupType.Life:
                    temp.lives++;
                    break;
                case PickupType.Score:
                    temp.score+=10;
                    break;
                case PickupType.Big:
                    temp.StartBigChange();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
