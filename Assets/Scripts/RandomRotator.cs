using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;

    void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
