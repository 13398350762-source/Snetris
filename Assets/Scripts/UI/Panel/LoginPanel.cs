using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnLogin;
    public Button btnRe;
    public Button btnExit;
    public Toggle togRem;
    public Toggle togShow;
    public InputField inputUN;
    public InputField inputPW;
    private Dictionary<string, string> registerDic;
    protected override void Init()
    {
        btnExit.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            Application.Quit();
        });

        btnLogin.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            if (inputUN.text.Length == 0)
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/input_error");
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("请输入用户名");
                return;
            }
            if (inputPW.text.Length == 0)
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/input_error");
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("请输入密码");
                return;
            }
            if (!registerDic.ContainsKey(inputUN.text))
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/input_error");
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("用户名不存在");
                inputUN.text = "";
                inputPW.text = "";
                return;
            }
            else
            {
                if (registerDic[inputUN.text] != inputPW.text)
                {
                    AudioManager.Instance.PlaySound("ArtRes/Audio/UI/input_error");
                    UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("密码错误");
                    inputPW.text = "";
                }
                else
                {
                    AudioManager.Instance.PlaySound("ArtRes/Audio/Account/login_success");
                    DataManger.Instance.loginData.username = inputUN.text;
                    DataManger.Instance.loginData.password = inputPW.text;
                    DataManger.Instance.loginData.isRem = togRem.isOn;
                    DataManger.Instance.SaveLoginData();
                    DataManger.Instance.username = inputUN.text;
                    AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene");
                    ao.completed += (operation) =>{
                        UIManager.Instance.ShowPanel<MainPanel>();
                        DataManger.Instance.UpdateUserRankData();
                        UIManager.Instance.HidePanel<LoginPanel>();
                    };
                }
            }
        });

        btnRe.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            UIManager.Instance.HidePanel<LoginPanel>();
            UIManager.Instance.ShowPanel<RegisterPanel>();
        });

        togRem.onValueChanged.AddListener((value) =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_toggle");
            DataManger.Instance.loginData.isRem = value;
        });

        togShow.onValueChanged.AddListener((value) =>{
            inputPW.contentType = value ? InputField.ContentType.Standard:InputField.ContentType.Password;
            inputPW.ForceLabelUpdate();
        });
        inputUN.onValueChanged.AddListener((value) =>{
            if (value.Length == 0)
                inputPW.text = "";
        });
        inputUN.text=DataManger.Instance.loginData.username;
        togRem.isOn = DataManger.Instance.loginData.isRem;
        if (togRem.isOn)
            inputPW.text = DataManger.Instance.loginData.password;
        registerDic = DataManger.Instance.registerData.infoList;
        // 菜单音乐
        AudioManager.Instance.PlayMusic("ArtRes/Audio/Music/menu_bgm");

    }
}
