using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class SaveDataManager : MonoBehaviour
{
  
    public T Save<T>(T dataType, string path)
    {
        string jsonData = JsonUtility.ToJson(dataType);
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
        return default;
    }

    public T Load<T>(string path)
    {
        try
        {
            StreamReader reader = new StreamReader(path,System.Text.Encoding.UTF8);
            string datastr = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<T>(datastr);
        }
        catch
        {
            Debug.LogWarning("ÉçÅ[Éhé∏îs");
            return default;
        }
    }
}

[Serializable]
public class SaveData
{
    public List<ItemData> ItemDatas;
}

[Serializable]
public class ItemData
{
    public string ItemPath;
    public string Title;
    public string Author;
    public string Explanation;
}