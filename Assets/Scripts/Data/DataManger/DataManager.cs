using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManger
{
    static private DataManger instance=new DataManger();
    static public DataManger Instance => instance;
    public LoginData loginData;
    public RegisterData registerData;
    public AudioData audioData;
    public RankData rankDataAll;
    public RankData rankDataPerson;
    public string username;
    private DataManger() {
        loginData = JsonManager.Instance.Load<LoginData>("LoginData");
        if(loginData==null)
            loginData = new LoginData();
        registerData = JsonManager.Instance.Load<RegisterData>("RegisterData");
        if(registerData == null)
            registerData = new RegisterData();
        audioData = JsonManager.Instance.Load<AudioData>("AudioData");
        if(audioData==null)
            audioData = new AudioData();
        rankDataAll = JsonManager.Instance.Load<RankData>("RankData");
        if (rankDataAll == null)
            rankDataAll = new RankData();
    }
    public void SaveLoginData()
    {
        JsonManager.Instance.Save(loginData, "LoginData");
    }
    public void SaveRegisterData()
    {
        JsonManager.Instance.Save(registerData, "RegisterData");
    }
    public void SaveAudioData()
    {
        JsonManager.Instance.Save(audioData, "AudioData");
    }
    public void SaveRankData()
    {
        JsonManager.Instance.Save(rankDataAll, "RankData");
    }
    public void AddRegister(string username,string password)
    {
        registerData.infoList.Add(username,password);
        SaveRegisterData();
    }
    public void UpdateAudioData(AudioData data)
    {
        audioData = new AudioData
        {
            IsOpenAll = data.IsOpenAll,
            IsOpenMusic = data.IsOpenMusic,
            IsOpenSound = data.IsOpenSound,
            allValue = data.allValue,
            musicValue = data.musicValue,
            soundValue = data.soundValue
        };
    }
    public void UpdateUserRankData()
    {
        rankDataPerson = new RankData();
        foreach (RankItem item in rankDataAll.ranklist)
        {
            if (item.username == username)
                rankDataPerson.ranklist.Add(item);
        }
    }
    public void AddRankItem(RankItem item)
    {
        rankDataAll.ranklist.Add(item);
        rankDataPerson.ranklist.Add(item);
        rankDataAll.ranklist.Sort((a, b) => { return b.record.CompareTo(a.record); });
        rankDataPerson.ranklist.Sort((a, b) => { return b.record.CompareTo(a.record); });
        SaveRankData();
    }
    public int GetBestRecord()
    {
        if (rankDataAll.ranklist.Count == 0)
            return 0;
        else
            return rankDataAll.ranklist[0].record;
    }
}
