using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Player player;
    
    void FixedUpdate()
    {
        if (player.animator.GetBool("IsDead"))
        {
            player.rigidBody.velocity = new Vector2();
            return;
        }

        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player.rigidBody.velocity = direction * velocity;

        if (player.rigidBody.velocity.magnitude != 0)
            player.animator.SetInteger("State", (int) PlayerState.Run);
        else if (!player.animator.GetBool("IsDead"))
            player.animator.SetInteger("State", (int) PlayerState.Idle);

        player.sprite.flipX = direction.x < 0;
    }
}