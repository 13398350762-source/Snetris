using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnLogin;
    public Button btnRe;
    public InputField inputUN;
    public InputField inputPW;
    public InputField inputSure;
    public Toggle togShow;
    public Toggle togShow2;
    private Dictionary<string,string> registerDic;
    protected override void Init()
    {
        registerDic = DataManger.Instance.registerData.infoList;
        btnLogin.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_back");
            UIManager.Instance.HidePanel<RegisterPanel>();
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        
        btnRe.onClick.AddListener(() => {
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
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("密码不能为空");
                return;
            }
            if (inputPW.text != inputSure.text)
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/input_error");
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("两次密码不一致");
                return;
            }
            if (registerDic.ContainsKey(inputUN.text))
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/UI/input_error");
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("用户名已存在");
                inputUN.text = "";
                inputPW.text = "";
                inputSure.text = "";
            }
            else
            {
                AudioManager.Instance.PlaySound("ArtRes/Audio/Account/register_success");
                LoginPanel panel=UIManager.Instance.ShowPanel<LoginPanel>();
                panel.inputUN.text= inputUN.text;
                panel.inputPW.text= inputPW.text;
                UIManager.Instance.HidePanel<RegisterPanel>();
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("注册成功");
                DataManger.Instance.AddRegister(panel.inputUN.text, panel.inputPW.text);
            }
        });

        togShow.onValueChanged.AddListener((value) =>{
            if (value)
                inputPW.contentType = InputField.ContentType.Standard;
            else
                inputPW.contentType = InputField.ContentType.Password;
            inputPW.ForceLabelUpdate();
        });

        togShow2.onValueChanged.AddListener((value) => {
            if (value)
                inputSure.contentType = InputField.ContentType.Standard;
            else
                inputSure.contentType = InputField.ContentType.Password;
            inputSure.ForceLabelUpdate();
        });

        inputPW.onValueChanged.AddListener((value) =>{
            if (value.Length == 0)
                inputSure.text = "";
        });
        inputUN.onValueChanged.AddListener((value) =>
        {
            if (value.Length == 0)
            {
                inputPW.text = "";
                inputSure.text = "";
            }

        });
    }
}
