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
    public AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (currentPickup)
            {
                case PickupType.Powerup:
                    collision.gameObject.GetComponent<PlayerController>().StartJumpForceChange();
                    break;
                case PickupType.Life:
                    GameManager.instance.lives++;
                    break;
                case PickupType.Score:
                    collision.gameObject.GetComponent<PlayerController>().score++;
                    break;
                case PickupType.Big:
                    collision.gameObject.GetComponent<PlayerController>().StartBigChange();
                    break;
            }
            if (pickupSound)
            {
                collision.gameObject.GetComponent<AudioSourceManager>().PlayOneShot(pickupSound, false);
            }
            Destroy(gameObject);
        }
    }
}
