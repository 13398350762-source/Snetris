using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public enum JsonType
{
    JsonUtlity,
    LitJson
}
public class JsonManager
{

    private static JsonManager instance =new JsonManager();
    public static JsonManager Instance => instance;
    private JsonManager() { }
    public void Save(object obj, string filename, JsonType type = JsonType.LitJson)
    {
        string path=Application.persistentDataPath + "/" + filename;
        string str= "";
        switch (type)
        {
            case JsonType.JsonUtlity:str = JsonUtility.ToJson(obj);
                break;
            case JsonType.LitJson: str = JsonMapper.ToJson(obj);
                break;
        }
        File.WriteAllText(path,str);
    }

    public T Load<T>(string filename, JsonType type = JsonType.LitJson)where T:new()
    {
        string path=Application.streamingAssetsPath+"/" + filename;
        if (!File.Exists(path))
        {
            path=Application.persistentDataPath+"/" + filename;
        }
        if(!File.Exists(path))
            return new();
        string str = File.ReadAllText(path);
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtlity:data=JsonUtility.FromJson<T>(str);
                break;
            case JsonType.LitJson:data=JsonMapper.ToObject<T>(str);
                break;
        }
        return data;
    }
}
