using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void ReceiveDamage(int damage)
    {
        Die();
    }

    protected virtual void Die() => Destroy(gameObject);
}