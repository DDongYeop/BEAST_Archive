public class Spear : ThrownWeapon
{
    private bool isFalling;

    protected override void WeaponUpdate()
    {
        transform.up = rigidbody.velocity;

        // �������� ������ �� �� ���� ����������.
        if (rigidbody.velocity.y <= 0f && false == isFalling)
        {
            isFalling = true;
            rigidbody.gravityScale *= 1.5f;
        }
    }
}
