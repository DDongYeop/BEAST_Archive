public class Bow : ThrownWeapon
{
    private bool isFalling;

    protected override void WeaponUpdate()
    {
        transform.up = rigidbody.velocity;

        // �������� ������ �� ���� 0.2�� 
        if (rigidbody.velocity.y <= 0f && false == isFalling)
        {
            isFalling = true;
            rigidbody.mass *= 0.2f;
        }
    }
}
