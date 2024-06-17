using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0.0f;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(float x)
    {
        rigidbody.velocity = new Vector2(x, 0) * MoveSpeed;
    }

    // ¡ÔΩ√ ∏ÿ√„
    public void StopImmediately(bool withYAxis = true)
    {
        // if (withYAxis)
        // {
        //     rigidbody.velocity = Vector2.zero;
        // }
        // else
        // {
        //     rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        // }
    }
}
