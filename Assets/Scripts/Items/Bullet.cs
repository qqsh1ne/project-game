using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float Velocity = 20f;
    private const float BulletLifeTime = 2.5f;
    
    private GameObject _parent;
    private Transform _transform;

    public GameObject Parent { set => _parent = value; }

    public Vector3 Direction { get; set; }

    public int Damage { get; set; }

    private void Start() => Destroy(gameObject, BulletLifeTime);

    private void Awake() => _transform = GetComponent<Transform>();

    private void Update()
    { 
        var newDirection = Direction;
        newDirection.x *= 10000;
        newDirection.y *= 10000;
        
        _transform.position =
            Vector3.MoveTowards(_transform.position, newDirection, Velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var unit = otherCollider.GetComponent<Unit>();

        if (unit.gameObject == _parent) return;
        if (unit) 
            unit.ReceiveDamage(Damage);
        Destroy(gameObject);
    }
}