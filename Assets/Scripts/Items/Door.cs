using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isOpening;

    private void Update()
    {
        if (!_isOpening)
            animator.SetBool("IsClosed", true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Unit>().CompareTag("Player")) return;
        _isOpening = true;
        animator.SetBool("IsClosed", false);
    }
}
