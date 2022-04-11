using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float velocity = 10f;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        var position = target.position;
        position.z = -10f;
        transform.position = Vector3.Lerp(transform.position, position, velocity * Time.deltaTime);
    }
}
