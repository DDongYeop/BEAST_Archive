using UnityEngine;

[CreateAssetMenu(menuName = "SO/ThronwWeapon/WeaponStat")]
public class ThrownWeaponStat : ScriptableObject
{
    [SerializeField] private string weaponId;
    public string WeaponId => weaponId;

    // 무기 이미지
    [SerializeField] private Sprite mainWeaponSprite;
    public Sprite MainWeaponSprite => mainWeaponSprite;
    [SerializeField] private Sprite subWeaponSprite;
    public Sprite SubWeaponSprite => subWeaponSprite;

    // 공격력
    [SerializeField] private int damage;
    public int Damage => damage;

    // 한 번의 공격 중 발사 횟수
    [SerializeField] private int throwCount = 1;
    public int ThrowCount => throwCount;

    // 발사체 무게
    [SerializeField] private float weaponMass = 2.0f;
    public float WeaponMass => weaponMass;

    // 무거운 무기인지.
    [SerializeField] private bool isHeavy = false;
    public bool IsHeavy => isHeavy;

    [SerializeField] private bool isSharp = true;
    public bool IsSharp => isSharp;
}
