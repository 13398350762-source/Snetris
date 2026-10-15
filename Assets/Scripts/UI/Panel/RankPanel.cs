using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : BasePanel
{
    public Button btnExit;
    public Toggle togAll;
    public Toggle togPerson;
    public Transform rank;
    List<RankItem> ranklistAll;
    List<RankItem> ranklistPerson;
    List<GameObject> rankobjList=new List<GameObject>();
    protected override void Init()
    {
        ranklistAll = DataManger.Instance.rankDataAll.ranklist;
        ranklistPerson=DataManger.Instance.rankDataPerson.ranklist;
        btnExit.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            UIManager.Instance.HidePanel<RankPanel>();
            UIManager.Instance.ShowPanel<MainPanel>();
        });
        togAll.onValueChanged.AddListener((value) =>{

            if (value)
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_toggle");
                updateRankDataAll();
            }

        });
        togPerson.onValueChanged.AddListener((value) => {

            if (value)
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_toggle");
                updateRankDataPerson();
            }

        });
        togAll.isOn = true;
    }
    private void updateRankDataAll()
    {
        ClearRankData();
        for (int i = 0; i < ranklistAll.Count && i < 100; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Rank/RankItem"));
            obj.transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();
            obj.transform.GetChild(2).GetComponent<Text>().text = ranklistAll[i].username;
            obj.transform.GetChild(3).GetComponent<Text>().text = ranklistAll[i].record.ToString();
            obj.transform.GetChild(4).GetComponent<Text>().text = ranklistAll[i].date;
            Image bk = obj.transform.GetComponent<Image>();
            Image medal = obj.transform.GetChild(1).GetComponent<Image>();
            if (i >= 3)
            {
                medal.enabled = false;
                if (ranklistAll[i].username == DataManger.Instance.username)
                {
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_self");
                }
                else
                {
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_normal");
                }
            }
            else
            {
                medal.enabled = true;
                if (i == 0)
                {
                    medal.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/medal_gold");
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_gold");
                }
                else if (i == 1)
                {
                    medal.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/medal_silver");
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_silver");
                }
                else
                {
                    medal.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/medal_bronze");
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_bronze");
                }
            }
            rankobjList.Add(obj);
            obj.transform.SetParent(rank, false);
        }
    }
    private void updateRankDataPerson()
    {
        ClearRankData();
        for (int i = 0; i < ranklistPerson.Count && i < 100; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Rank/RankItem"));
            obj.transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();
            obj.transform.GetChild(2).GetComponent<Text>().text = ranklistPerson[i].username;
            obj.transform.GetChild(3).GetComponent<Text>().text = ranklistPerson[i].record.ToString();
            obj.transform.GetChild(4).GetComponent<Text>().text = ranklistPerson[i].date;
            Image bk = obj.transform.GetComponent<Image>();
            Image medal = obj.transform.GetChild(1).GetComponent<Image>();
            if (i >= 3)
            {
                medal.enabled = false;
                bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_normal");
            }
            else
            {
                medal.enabled = true;
                if (i == 0)
                {
                    medal.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/medal_gold");
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_gold");
                }
                else if (i == 1)
                {
                    medal.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/medal_silver");
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_silver");
                }
                else
                {
                    medal.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/medal_bronze");
                    bk.sprite = Resources.Load<Sprite>("ArtRes/UI/Leaderboard/row_bronze");
                }
            }
            rankobjList.Add(obj);
            obj.transform.SetParent(rank, false);
        }
    }
    private void ClearRankData()
    {
        foreach(GameObject obj in rankobjList)
            Destroy(obj);
        rankobjList.Clear();
    }
}
