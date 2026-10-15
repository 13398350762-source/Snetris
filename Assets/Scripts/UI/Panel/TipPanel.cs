using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Text txtTip;
    public Button btnSure;
    protected override void Init()
    {
        btnSure.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_confirm");
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeInfo(string str)
    {
        txtTip.text = str;
    }

    override public void Hide(UnityAction callback)
    {
        AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_close");
        base.Hide(callback);
    }
    public override void Show()
    {
        AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_open");
        base.Show();
    }
}
