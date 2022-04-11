using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float velocity = 10f;
    private Rigidbody2D _rigitBody;
    private SpriteRenderer _sprite;

    void Start()
    {
        _rigitBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        var direction = new Vector3();
        if (Input.GetButton("Horizontal"))
            direction = (transform.right * Input.GetAxis("Horizontal")).normalized;
        if (Input.GetButton("Vertical"))
            direction = (transform.up * Input.GetAxis("Vertical")).normalized;

        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + direction, velocity * Time.deltaTime);

        _sprite.flipX = direction.x < 0;
    }
}