using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    public Button btnStart;
    public Button btnRank;
    public Button btnSet;
    public Button btnExit;
    public Button btnExitLogin;
    protected override void Init()
    {
        btnStart.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            UIManager.Instance.HidePanel<MainPanel>();
            //进入游戏界面
            UIManager.Instance.ShowPanel<GamePanel>();
        });
        btnRank.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            UIManager.Instance.HidePanel<MainPanel>();
            //进入排行榜界面
            UIManager.Instance.ShowPanel<RankPanel>();
        });
        btnSet.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            UIManager.Instance.HidePanel<MainPanel>();
            //进入设置界面
            UIManager.Instance.ShowPanel<SetPanel>();
        });
        btnExit.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            Application.Quit();
        });
        btnExitLogin.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_back");
            UIManager.Instance.HidePanel<MainPanel>();
            SceneManager.LoadSceneAsync("LoginScene");
        });
    }
}
