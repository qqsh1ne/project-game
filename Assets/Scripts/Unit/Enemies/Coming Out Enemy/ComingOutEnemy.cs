using System;
using UnityEngine;

public class ComingOutEnemy : MonoBehaviour
{
    [SerializeField] private SimpleEnemy source;
    [SerializeField] private float velocity;
    [SerializeField] private Transform endPoint;

    private bool _endPointVisited;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        SetState();
        if (source.animator.GetBool("IsDead") || !source.canShoot || _endPointVisited)
        {
            source.rigidBody.velocity = new Vector2();
            return;
        }

        var target = endPoint.position;
        source.rigidBody.velocity = (target - source.transform.position).normalized * velocity;
        CheckEndPointVisiting(target);
    }

    private void SetState()
    {
        if (source.rigidBody.velocity.magnitude != 0)
            source.animator.SetInteger("State", (int) SimpleEnemyState.Run);
        else if (!source.animator.GetBool("IsDead"))
            source.animator.SetInteger("State", (int) SimpleEnemyState.Idle);
    }

    private void CheckEndPointVisiting(Vector3 target)
    {
        if (Mathf.Abs(source.transform.position.x - target.x) < 0.5f &&
            Mathf.Abs(source.transform.position.y - target.y) < 0.5f)
            _endPointVisited = !_endPointVisited;
    }
}