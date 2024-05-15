using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 5f; // Lifetime of the rock in seconds

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
