using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    public int Damage => _damage;
    private Rigidbody2D rigidbody;

    [HideInInspector] public bool IsFlying = true;  

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidbody.velocity = velocity;
    }

    private void Update()
    {
        if (IsFlying && rigidbody)
            transform.up = rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsFlying)
            Destroy(gameObject);
    }

    public void SetRigidbody(Rigidbody2D rigid)
    {
        rigidbody = rigid;
    }
}
