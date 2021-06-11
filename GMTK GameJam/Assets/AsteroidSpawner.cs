using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float rotationSpeed;
    public Transform spawn01, spawn02;

    private void Awake()
    {
        spawn01 = transform.Find("Spawner 1");
        spawn02 = transform.Find("Spawner 2");
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
