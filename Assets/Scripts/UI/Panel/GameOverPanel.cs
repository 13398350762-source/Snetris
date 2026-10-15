using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text txtScore;
    public Button btnRe;
    public Button btnHome;
    protected override void Init()
    {
        btnHome.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_back");
            UIManager.Instance.ShowPanel<MainPanel>();
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            Time.timeScale = 1.0f;
            // 菜单音乐
            AudioManager.Instance.PlayMusic("ArtRes/Audio/Music/menu_bgm");
        });
        btnRe.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_confirm");
            UIManager.Instance.HidePanel<GameOverPanel>();
            GameManager.Instance.StartGame();
        });
    }
}
