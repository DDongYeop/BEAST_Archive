using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public List<ThrownWeaponInfo> weaponInfoList = new List<ThrownWeaponInfo>();
	public List<SkillInfo> skillInfoList = new List<SkillInfo>();
	public Level level;
	public SkillInfo SkillInfo;
}

public class SaveLoadManager : MonoSingleton<SaveLoadManager>
{
	public ThrownWeaponInfo[] defaultWeaponInfos;
	public SkillInfo[] defaultSkillInfos;
	public SkillInfo defaultSkillInfo;
    private List<IDataObserver> observers;

	public SaveData data;
	private string path;
	private string fileName = "data.json";

    public override void Init()
    {
        path = Path.Combine(Application.persistentDataPath, "savefiles");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        observers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataObserver>().ToList();
        LoadData();
		//SaveData();
    }

	public void SaveData()
	{
		string filePath = Path.Combine(path, fileName);
		if (!File.Exists(filePath))
		{
			File.Create(filePath);
		}

		data = new SaveData();

        foreach (IDataObserver observer in observers)
		{
			observer.WriteData(ref data);
		}

		string jsonStr = JsonUtility.ToJson(data, true);
		byte[] jsonByte = System.Text.Encoding.UTF8.GetBytes(jsonStr);
		string jsonBase64 = System.Convert.ToBase64String(jsonByte);

		File.WriteAllText(filePath, jsonBase64);
		Debug.Log("Data Saved");
	}

	public void LoadData()
	{
		bool isNewData = false;
		string filePath = Path.Combine(path, fileName);
		if (!File.Exists(filePath))
		{
			Debug.LogError(filePath + "Does not exists");
			if (!File.Exists(filePath))
			{
				isNewData = true;
                File.Create(filePath);
            }
		}
		
		string jsonBase64 = File.ReadAllText(filePath);
        byte[] jsonByte = System.Convert.FromBase64String(jsonBase64);
        string jsonStr = System.Text.Encoding.UTF8.GetString(jsonByte);
        data = JsonUtility.FromJson<SaveData>(jsonStr);

        foreach (IDataObserver observer in observers)
        {
            observer.ReadData(data);
        }
        Debug.Log("Data Loaded");


        if(isNewData)
		{
			Debug.Log("New Data");
			data = new SaveData();
			data.weaponInfoList = defaultWeaponInfos.ToList();
			data.skillInfoList = defaultSkillInfos.ToList();
            data.SkillInfo = defaultSkillInfo;

            jsonStr = JsonUtility.ToJson(data, true);
            jsonByte = System.Text.Encoding.UTF8.GetBytes(jsonStr);
            jsonBase64 = System.Convert.ToBase64String(jsonByte);

            File.WriteAllText(filePath, jsonBase64);
        }
    }
}
