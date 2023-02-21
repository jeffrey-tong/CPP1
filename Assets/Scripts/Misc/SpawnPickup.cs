using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public Pickup[] pickupPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], this.transform);
    }
}
