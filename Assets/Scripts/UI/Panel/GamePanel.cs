using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btnHome;
    public Toggle togStop;
    public Button btnDown;
    public Button btnRight;
    public Button btnLeft;
    public Text txtRecord;
    public Text txtCombo;
    public Text txtScore;
    public Transform gameBK;
    protected override void Init()
    {
        btnHome.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_back");
            //保存当前游戏记录
            UIManager.Instance.HidePanel<GamePanel>();
            UIManager.Instance.ShowPanel<MainPanel>();
            Time.timeScale = 1.0f;
            // 菜单音乐
            AudioManager.Instance.PlayMusic("ArtRes/Audio/Music/menu_bgm");
        });

        togStop.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            GameManager.Instance.isPause = !value;
                if (value)
                {
                    //游戏正常进行
                    togStop.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    togStop.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                }
                else
                {
                    //暂停游戏
                    togStop.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    togStop.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    Time.timeScale = 0.0f;
                }
        });

        btnDown.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            GameManager.Instance.ChangeDirection(Direction.Down);
        });

        btnRight.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            GameManager.Instance.ChangeDirection(Direction.Right);
        });

        btnLeft.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            GameManager.Instance.ChangeDirection(Direction.Left);
        });
        txtRecord.text = DataManger.Instance.GetBestRecord().ToString();
        // 游戏音乐
        AudioManager.Instance.PlayMusic("ArtRes/Audio/Music/game_bgm");
    }
    public void UpateBoard()
    {
        BoardData data = GameManager.Instance.GetBoardData();
        foreach (Transform child in gameBK.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < BoardData.Row; i++)
        {
            for (int j = 0; j < BoardData.Col; j++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("Game/ImgItem"));
                if (data.board[i, j] == BlockColor.Null)
                    obj.transform.GetChild(0).GetComponent<Image>().enabled = false;
                else
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/Blocks/Tiles/" + BoardData.GetColorRes(data.board[i, j]));
                obj.transform.SetParent(gameBK, false);
            }
        }
    }

    public void UpdateScore(int score,int combo)
    {
        txtScore.text = score.ToString("N0");
        txtCombo.text= "× "+combo.ToString();
    }
}
