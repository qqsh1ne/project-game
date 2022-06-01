using UnityEngine;

public class SimpleEnemyMoving : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SimpleEnemy source;
    [SerializeField] private float velocity;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private bool _endPointVisited;

    private void FixedUpdate()
    {
        if (source.animator.GetBool("IsDead") || !source.canShoot)
        {
            source.rigidBody.velocity = new Vector2();
            return;
        }

        var target = FindTarget();
        var direction = target - source.transform.position;
        source.rigidBody.velocity = direction.normalized * velocity;
        SetState();
        CheckEndPointVisiting(target);
        source.sprite.flipX = direction.x < 0;
    }

    private Vector3 FindTarget()
    {
        Vector3 target;
        if (source.isAttacked &&
            (player.transform.position - source.transform.position).magnitude <= SimpleEnemy.VisionRange)
            target = player.transform.position;
        else
            target = _endPointVisited ? startPoint.position : endPoint.position;
        return target;
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