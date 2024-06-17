using UnityEngine;

public class Boomerang : ThrownWeapon
{
    [SerializeField] private float maxRotateSpeed;
    [SerializeField] private float minRotateSpeed;
    [SerializeField] private float dampingFactor;

    [SerializeField] private float fallingVelocityTrigger;
    
    [SerializeField] private float returnSpeed;

    private float rotateSpeed;
    private bool isFalling;

    private Transform playerTransform;
    private float time = 0f;
    private Vector2 targetDirection;

    public override void Init()
    {
        base.Init();
        rotateSpeed = minRotateSpeed;
        isFalling = false;
        playerTransform = GameManager.Instance.PlayerTrm;
        time = 0f;
    }

    protected override void WeaponUpdate()
    {
        // ȸ��
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        rotateSpeed = Mathf.Clamp((rotateSpeed + dampingFactor * Time.deltaTime), minRotateSpeed, maxRotateSpeed);

        if (isFalling)
        {
           ReturnToPlayer();
        }
        else
        {
            // �������� ������ ��
            if (rigidbody.velocity.y <= fallingVelocityTrigger && false == isFalling)
            {
                isFalling = true;
                rigidbody.velocity = Vector2.zero;
                rigidbody.gravityScale *= 0.1f;
                targetDirection = (playerTransform.position - transform.position).normalized;
            }
        }
    }

    private void ReturnToPlayer()
    {
        // SLerp�� ����Ͽ� �ε巴�� ������
        // time += Time.deltaTime;
        // Vector2 returnDirection = Vector3.Slerp(transform.forward, targetDirection, time);
        transform.Translate(targetDirection * returnSpeed * Time.deltaTime, Space.World);
    }
}
