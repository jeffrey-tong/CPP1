using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public Pickup[] pickupPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, pickupPrefab.Length);
        switch (num)
        {
            case 0:
                Instantiate(pickupPrefab[0], this.transform);
                break;
            case 1:
                Instantiate(pickupPrefab[1], this.transform);
                break;
            case 2:
                Instantiate(pickupPrefab[2], this.transform);
                break;
            case 3:
                Instantiate(pickupPrefab[3], this.transform);
                break;
            default:
                break;
        }
    }
}
