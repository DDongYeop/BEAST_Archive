using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    public Action<ThrownWeaponStat> OnWeaponStatChanged;
    public Action<TreasureData> OnTresureDataChanged;

    [SerializeField] private List<ThrownWeaponStat> weaponStats = new ();
    private Dictionary<string, ThrownWeaponStat> weaponStatContainer = new ();

    [SerializeField] private List<TreasureData> tresureDatas = new();
    private Dictionary<string, TreasureData> tresureDataContainer = new();

    private ThrownWeaponStat currentWeaponStat;
    private TreasureData currentTreasureData;

    private void Awake()
    {
        foreach (var stat in weaponStats)
        {
            weaponStatContainer.Add(stat.WeaponId, stat);
        }

        foreach (var data in tresureDatas)
        {
            tresureDataContainer.Add(data.TreasureId, data);
        }
    }

    private void Start()
    {
        // 마지막에 장착했었던 WeaponStat, TreasureData 기억해서 장착
        AttemptChangeTresureData("Sturn");
    }

    public void AttemptChangeWeaponStat(string weaponId)
    {
        if (weaponStatContainer.ContainsKey(weaponId))
        {
            currentWeaponStat = weaponStatContainer[weaponId];
            Debug.Log(weaponId);
            RequestChangeWeaponStat();
        }
        else
        {
            Debug.LogError($"weaponStatContainer is not exist {weaponId} key...");
        }
    }

    private void RequestChangeWeaponStat()
    {
        OnWeaponStatChanged?.Invoke(currentWeaponStat);
    }

    public void AttemptChangeTresureData(string tresureId)
    {
        if (tresureDataContainer.ContainsKey(tresureId))
        {
            currentTreasureData = tresureDataContainer[tresureId];
            RequestChangeTresureData();
        }
        else
        {
            Debug.LogError($"tresureDataContainer is not exist {tresureId} key...");
        }
    }

    private void RequestChangeTresureData()
    {
        OnTresureDataChanged?.Invoke(currentTreasureData);
    }

}
