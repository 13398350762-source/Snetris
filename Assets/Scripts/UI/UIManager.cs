using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    static private UIManager instance = new UIManager();
    static public UIManager Instance => instance;
    public Dictionary<string, BasePanel> panelDic=new Dictionary<string, BasePanel>();
    private Transform parent;
    private UIManager() 
    {
        parent = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(parent);
    }
    public T ShowPanel<T>()where T: BasePanel
    {
        string name=typeof(T).Name;
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;
        string path = "UI/Panel/" + name;
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(path));
        obj.transform.SetParent(parent, false);
        T panel = obj.GetComponent<T>();
        panelDic.Add(name, panel);
        panel.Show();
        return panel;
    }

    public void HidePanel<T>(bool isFade=false)where T: BasePanel
    {
        string name= typeof(T).Name;
        if (panelDic.ContainsKey(name))
        {
            if (isFade)
            {
                panelDic[name].Hide(() => {
                    GameObject.Destroy(panelDic[name].gameObject);
                    panelDic.Remove(name);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[name].gameObject);
                panelDic.Remove(name);
            }
        }
    }

    public T GetPanel<T>()where T: BasePanel
    {
        if (panelDic.ContainsKey(typeof(T).Name))
            return panelDic[typeof(T).Name] as T;
        return null;
    }
}
