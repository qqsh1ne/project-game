using UnityEngine;
using UnityEngine.Serialization;

public class Player : Unit
{
    private const float ShootCooldown = 0.3f;
    private const float DeathDuration = 1.1f;

    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rigidBody;

    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    [SerializeField] private Bullet bullet;
    [SerializeField] private new Transform transform;
    [SerializeField] private HealthBar healthBar;

    private int _health;
    public SpriteRenderer sprite;

    private Animation _animation;
    private Camera _camera;
    private bool _canShoot = true;

    private void Awake()
    {
        _camera = Camera.main;
        sprite = GetComponentInChildren<SpriteRenderer>();
        _health = maxHealth;
        healthBar.SetMaxHealth(_health);
    }

    private void Update()
    {
        if (animator.GetBool("IsDead"))
            return;
        
        if (Input.GetButtonDown("Fire1") && _canShoot)
            Shoot();
    }

    private void Shoot()
    {
        animator.SetInteger("State", (int) PlayerState.ShootForward);
        CreateBullet();
        _canShoot = false;
        Invoke(nameof(SetCanShoot), ShootCooldown);
    }

    private void CreateBullet()
    {
        var position = transform.position;
        position.y += 0.5f;
        var target = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        var newBullet = Instantiate(bullet, position, bullet.transform.rotation);
        newBullet.Parent = gameObject;
        newBullet.Direction = target - position;
        newBullet.Damage = damage;
        newBullet.transform.rotation =
            Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, newBullet.Direction));
    }

    public override void ReceiveDamage(int damage)
    {
        _health -= damage;
        
        healthBar.SetHealth(_health);

        if (_health <= 0)
            Die();
    }

    protected override void Die()
    {
        animator.SetBool("IsDead", true);
        animator.SetInteger("State", 5);
        
        Destroy(gameObject, DeathDuration);
    }

    private void SetCanShoot() => _canShoot = true;
}