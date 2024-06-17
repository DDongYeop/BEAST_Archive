using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격")]
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform throwTransform;
    private Vector3 throwPositionOffset = new Vector3(-0.1f, 0.2f, 0);

    [Header("발사 경로 표현")]
    [SerializeField] private Vector2 throwRange;
    [SerializeField] private int countOfPoints;
    [SerializeField] private float timeIntervalInPoints = 0.01f;

    [Header("Display")]
    [SerializeField] private DisplayThrownWeapon mainDisplay;
    [SerializeField] private DisplayThrownWeapon subDisplay; 

    // Component
    private WeaponController weaponController;
    private LineRenderer lineRenderer;

    // Throw
    [Header("Stat")]
    [SerializeField] private ThrownWeaponStat equipWeaponStat; // 현재 장착 중인 무기 정보
    public string EquipWeaponId => equipWeaponStat.WeaponId; 

    private ThrowInfo throwInfo;

    // 추후 get; private set;
    public TreasureData equipTreasureData; // 현재 장착 중인 보물 정보
    private int throwCount;
    public int ThrowCount => throwCount;

    private float maxForceValue = 49;

    private void Awake()
    {
        weaponController = transform.Find("WeaponController").GetComponent<WeaponController>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = countOfPoints;
        lineRenderer.enabled = false;

        throwInfo = new ThrowInfo();
        throwInfo.throwForce = throwForce;
    }

    private void OnEnable()
    {
        weaponController.OnWeaponStatChanged += AcceptChangeWeaponStat;
        weaponController.OnTresureDataChanged += AcceptChangeTreasureData;
    }

    private void OnDisable()
    {
        weaponController.OnWeaponStatChanged -= AcceptChangeWeaponStat;
        weaponController.OnTresureDataChanged -= AcceptChangeTreasureData;
    }

    #region Aiming

    public bool Aiming(Vector3 startPos, Vector3 endPos)
    {
        throwInfo.startPosition = startPos;
        throwInfo.currentPosition = endPos;

        if (throwInfo.direction.x < throwRange.x && throwInfo.direction.y < throwRange.y)
        {
            return false;
        }

        mainDisplay.SetForwardAngle(throwInfo.direction);
        subDisplay.SetLayerToFront("UI");
        ShowTrajectory(throwInfo.force * (maxForceValue / equipWeaponStat.WeaponMass));
        return true;
    }

    public void EndAiming()
    {
        lineRenderer.enabled = false;

        mainDisplay.OnDisplay(equipWeaponStat);
        subDisplay.OnDisplay(equipWeaponStat);
    }

    // 발사 경로 표현
    private void ShowTrajectory(Vector2 force)
    {
        float timeStep = timeIntervalInPoints; 

        Vector3[] trajectoryPoints = new Vector3[countOfPoints];
        Vector3 startPosition = throwTransform.position;
        Vector3 velocity = force / equipWeaponStat.WeaponMass;

        for (int i = 0; i < countOfPoints; i++)
        {
            trajectoryPoints[i] = startPosition;

            Vector3 acceleration = Physics2D.gravity;
            velocity += acceleration * timeStep;
            startPosition += velocity * timeStep;
        }

        // 경로 시각화
        lineRenderer.enabled = true;
        lineRenderer.SetPositions(trajectoryPoints);
    }

    #endregion

    public void Throw()
    {
        #region Treasure Func

        TreasureData tresureData = null;

        // 보물 효과 발동 가능한지 확인하고, 발사횟수를 조정해 줍니다.
        // 보물 효과가 발동할 시점이라면 데이터를 할당해 줍니다.
        if (throwCount >= equipTreasureData.CountForRecharge)
        {
            throwCount = 0;
            tresureData = equipTreasureData;
        }
        else
        {
            throwCount++;
        }

        #endregion

        // UI 업데이트
        Scene_InGame _UI = UIManager_InGame.Instance.GetScene("Scene_InGame") as Scene_InGame;
        _UI.OnThrow(throwCount, equipTreasureData.CountForRecharge);
        
        // 무기 생성 후 발사
        ThrownWeapon thrownWeapon = PoolManager.Instance.Pop(equipWeaponStat.WeaponId) as ThrownWeapon;
        thrownWeapon.transform.parent = null;
        thrownWeapon.transform.position = throwTransform.position;
        thrownWeapon.ThrowThisWeapon(throwInfo.force, tresureData);

        // 0.4초 뒤 무기 이미지 켜주기
        StartCoroutine(OnDisplayAfterDelay(0.5f));

        // 사운드
        PoolManager.Instance.Pop("ThrowSound");
    }

    private IEnumerator OnDisplayAfterDelay(float deleyTime = 0f)
    {
        // 무기 이미지 꺼주기
        mainDisplay.OffDisplay();
        subDisplay.SetLayerToBack();

        yield return new WaitForSeconds(deleyTime);
        mainDisplay.OnDisplay(equipWeaponStat);
        subDisplay.OnDisplay(equipWeaponStat);
    }

    // 무기 스탯 변경
    private void AcceptChangeWeaponStat(ThrownWeaponStat newWeaponStat)
    {
        if (equipWeaponStat == newWeaponStat)
        {
            return;
        }
        
        equipWeaponStat = newWeaponStat;
        StartCoroutine(OnDisplayAfterDelay());
    }

    // 보물 장착
    private void AcceptChangeTreasureData(TreasureData treasureData)
    {
        if (equipTreasureData == treasureData)
        {
            return;
        }

        equipTreasureData = treasureData;
    }
}
