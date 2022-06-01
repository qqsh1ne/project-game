using UnityEngine;
using UnityEngine.Serialization;

public class SimpleEnemy : Unit
{
    private const float AttackRange = 10f;
    
    private const float ShootCooldown = 2f;
    private const float DeathDuration = 1.32f;

    [SerializeField] public new Transform transform;
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rigidBody;

    [SerializeField] private int damage;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Player player;
    [SerializeField] private int health;

    public SpriteRenderer sprite;
    public bool canShoot = true;

    private void Start() => sprite = GetComponentInChildren<SpriteRenderer>();

    private void Update()
    {
        if (animator.GetBool("IsDead"))
            return;
        
        if ((player.transform.position - transform.position).magnitude < AttackRange && canShoot)
            Shoot();
    }

    private void Shoot()
    {
        animator.SetInteger("State", (int) SimpleEnemyState.Shoot);

        var target = FindTarget();
        var position = GetParentPosition();

        sprite.flipX = (target - position).x < 0;
        CreateBullet(position, target);
        canShoot = false;
        Invoke(nameof(SetCanShoot), ShootCooldown);
    }

    private Vector3 FindTarget()
    {
        var target = player.transform.position;
        target.y += 0.5f;
        return target;
    }

    private Vector3 GetParentPosition()
    {
        var position = transform.position;
        position.y += 0.5f;
        return position;
    }

    private void CreateBullet(Vector3 position, Vector3 target)
    {
        var newBullet = Instantiate(bullet, position, bullet.transform.rotation);
        
        newBullet.Parent = gameObject;
        newBullet.Direction = target - position;
        newBullet.Damage = damage;
        newBullet.transform.rotation =
            Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, newBullet.Direction));
    }

    public override void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }
    
    protected override void Die()
    {
        animator.SetBool("IsDead", true);
        animator.SetInteger("State", 3);
        
        Destroy(gameObject, DeathDuration);
    }
    
    private void SetCanShoot() => canShoot = true;
}