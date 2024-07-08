using System;

[Serializable]
public class Level : IDataObserver
{
    public bool[] Levels = new bool[10]
        { false, false, false, false, false, false, false, false, false, false }; 
    
    public void WriteData(ref SaveData data)
    {
        data.level.Levels = Levels;
    }

    public void ReadData(SaveData data)
    {
        Levels = data.level.Levels;
    }
}
