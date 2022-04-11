using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float Velocity = 35f;

    private SpriteRenderer _sprite;
    private Vector3 _direction;
    public Vector3 Direction
    {
        set => Direction = value;
    }

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
}