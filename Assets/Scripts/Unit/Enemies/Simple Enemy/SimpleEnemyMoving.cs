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
        
        var target = _endPointVisited ? startPoint.position : endPoint.position;
        var direction = target - source.transform.position;
        source.rigidBody.velocity = direction.normalized * velocity;
        
        if (source.rigidBody.velocity.magnitude != 0)
            source.animator.SetInteger("State", (int) SimpleEnemyState.Run);
        else if (!source.animator.GetBool("IsDead"))
            source.animator.SetInteger("State", (int) SimpleEnemyState.Idle);
            
        if (Mathf.Abs(source.transform.position.x - target.x) < 0.5f && Mathf.Abs(source.transform.position.y - target.y) < 0.5f)
            _endPointVisited = !_endPointVisited;
        
        source.sprite.flipX = direction.x < 0;
    }

    private bool CanRun() => !source.animator.GetBool("IsDead") || source.canShoot;
}