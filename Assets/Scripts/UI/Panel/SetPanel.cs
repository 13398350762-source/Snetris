using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : BasePanel
{
    public Button btnExit;
    public Toggle togAll;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderAll;
    public Slider sliderMusic;
    public Slider sliderSound;
    public Button btnDefault;
    public Button btnSure;
    public Text txtAll;
    public Text txtMusic;
    public Text txtSound;
    private Coroutine mycoroutine;
    private Coroutine mycoroutine2;
    private Coroutine mycoroutine3;
    private AudioData predata;
    protected override void Init()
    {
        InitAudioData();
        btnExit.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_back");
            UIManager.Instance.HidePanel<SetPanel>();
            UIManager.Instance.ShowPanel<MainPanel>();
            DataManger.Instance.audioData=predata;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });
        sliderMusic.onValueChanged.AddListener(value => {
            txtMusic.text = $"{value * 100:F0}%";
            DataManger.Instance.audioData.musicValue = sliderMusic.value;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });

        sliderSound.onValueChanged.AddListener(value => {
            txtSound.text = $"{value * 100:F0}%";
            DataManger.Instance.audioData.soundValue = sliderSound.value;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });

        sliderAll.onValueChanged.AddListener(value => {
            txtAll.text = $"{value * 100:F0}%";
            DataManger.Instance.audioData.allValue = sliderAll.value;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });
        btnSure.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_confirm");
            UIManager.Instance.ShowPanel<MainPanel>();
            SetAudioData();
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
            UIManager.Instance.HidePanel<SetPanel>();
            DataManger.Instance.SaveAudioData();
        });
        btnDefault.onClick.AddListener(() =>{
            AudioManager.Instance.PlaySound("ArtRes/Audio/UI/ui_click");
            DataManger.Instance.UpdateAudioData(new AudioData());
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
            LoadAudioData();
        });
        togAll.onValueChanged.AddListener((value) =>{
            if (mycoroutine != null)
            {
                StopCoroutine(mycoroutine);
                mycoroutine = null;
            }
            Image bk = togAll.transform.Find("ImgBK2").GetComponent<Image>();
            Transform pos = togAll.transform.Find("ImgbtnNormal");
            sliderAll.interactable = value;
            sliderMusic.interactable = value&&togMusic.isOn;
            sliderSound.interactable = value&&togSound.isOn;
            togMusic.interactable = value;
            togSound.interactable = value;
            if (value)
            {
                sliderAll.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_cyan");
                if(togMusic.isOn)
                sliderMusic.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_violet");
                if(togSound.isOn)
                sliderSound.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_mint");
                togMusic.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = togMusic.isOn ? 1 : 0;
                togSound.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = togSound.isOn ? 1 : 0;
                mycoroutine =StartCoroutine(Move(pos, new Vector3(42, 0, 0), 0.15f, bk,1));
            }
            else
            {
                sliderAll.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                sliderMusic.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                sliderSound.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                togMusic.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = 0;
                togSound.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = 0;
                mycoroutine =StartCoroutine(Move(pos, new Vector3(-42, 0, 0), 0.15f,bk,0));
            }
            DataManger.Instance.audioData.IsOpenAll = value;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });
        togMusic.onValueChanged.AddListener((value) =>{
            if (mycoroutine2 != null)
            {
                StopCoroutine(mycoroutine2);
                mycoroutine2 = null;
            }
            Image bk = togMusic.transform.Find("ImgBK2").GetComponent<Image>();
            Transform pos = togMusic.transform.Find("ImgbtnNormal");
            sliderMusic.interactable = value && togAll.isOn;
            if (value)
            {
                sliderMusic.fillRect.GetComponent<Image>().sprite = togAll.isOn? Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_violet"):Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                mycoroutine2 = StartCoroutine(Move(pos, new Vector3(42, 0, 0), 0.15f, bk, 1));
            }
            else
            {
                sliderMusic.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                mycoroutine2 = StartCoroutine(Move(pos, new Vector3(-42, 0, 0), 0.15f, bk, 0));
            }
            DataManger.Instance.audioData.IsOpenMusic = value;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });
        togSound.onValueChanged.AddListener((value) => {
            if (mycoroutine3 != null)
            {
                StopCoroutine(mycoroutine3);
                mycoroutine3 = null;
            }
            Image bk = togSound.transform.Find("ImgBK2").GetComponent<Image>();
            Transform pos = togSound.transform.Find("ImgbtnNormal");
            sliderSound.interactable = value && togAll.isOn;
            if (value)
            {
                sliderSound.fillRect.GetComponent<Image>().sprite = togAll.isOn ? Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_mint") : Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                mycoroutine3 = StartCoroutine(Move(pos, new Vector3(42, 0, 0), 0.15f, bk, 1));
            }
            else
            {
                sliderSound.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
                mycoroutine3 = StartCoroutine(Move(pos, new Vector3(-42, 0, 0), 0.15f, bk, 0));
            }
            DataManger.Instance.audioData.IsOpenSound = value;
            AudioManager.Instance.ApplySettings(DataManger.Instance.audioData);
        });

    }
    public void InitAudioData()
    {
        LoadAudioData();
        sliderAll.interactable = togAll.isOn;
        sliderMusic.interactable = togAll.isOn && togMusic.isOn;
        sliderSound.interactable = togAll.isOn && togSound.isOn;
        togAll.transform.Find("ImgbtnNormal").localPosition = togAll.isOn ? new Vector3(42, 0, 0) : new Vector3(-42, 0, 0);
        togMusic.transform.Find("ImgbtnNormal").localPosition = togMusic.isOn ? new Vector3(42, 0, 0) : new Vector3(-42, 0, 0);
        togSound.transform.Find("ImgbtnNormal").localPosition = togSound.isOn ? new Vector3(42, 0, 0) : new Vector3(-42, 0, 0);
        togMusic.interactable = togAll.isOn;
        togSound.interactable = togAll.isOn;
        if (togAll.isOn)
        {
            sliderAll.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_cyan");
            if (togMusic.isOn)
                sliderMusic.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_violet");
            if (togSound.isOn)
                sliderSound.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_mint");
            togMusic.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = togMusic.isOn ? 1 : 0;
            togSound.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = togSound.isOn ? 1 : 0;
            togAll.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = 1;
        }
        else
        {
            sliderAll.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
            sliderMusic.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
            sliderSound.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
            togMusic.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = 0;
            togSound.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = 0;
            togAll.transform.Find("ImgBK2").GetComponent<Image>().fillAmount = 0;
        }
        if (togMusic.isOn)
        {
            sliderMusic.fillRect.GetComponent<Image>().sprite = togAll.isOn ? Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_violet") : Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
        }
        else
        {
            sliderMusic.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
        }
        if (togSound.isOn)
        {
            sliderSound.fillRect.GetComponent<Image>().sprite = togAll.isOn ? Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_mint") : Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
        }
        else
        {
            sliderSound.fillRect.GetComponent<Image>().sprite = Resources.Load<Sprite>("ArtRes/UI/Sliders/Fills/slider_fill_disabled");
        }
        SetPreAudioData();
    }
    public void LoadAudioData() 
    {
        AudioData ad = DataManger.Instance.audioData;
        sliderAll.value = ad.allValue;
        sliderMusic.value = ad.musicValue;
        sliderSound.value = ad.soundValue;
        togAll.isOn = ad.IsOpenAll;
        togMusic.isOn = ad.IsOpenMusic;
        togSound.isOn = ad.IsOpenSound;
        txtMusic.text = $"{sliderMusic.value * 100:F0}%";
        txtSound.text = $"{sliderSound.value * 100:F0}%";
        txtAll.text = $"{sliderAll.value * 100:F0}%";
    }

    IEnumerator Move(Transform pos, Vector3 endpos, float duration,
                     Image img, float target)
    {
        Vector3 startPos = pos.localPosition;
        float startFill = img.fillAmount;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Mathf.SmoothStep(0f, 1f, t);

            pos.localPosition = Vector3.Lerp(startPos, endpos, t);
            img.fillAmount = Mathf.Lerp(startFill, target, t);

            yield return null;
        }

        pos.localPosition = endpos;
        img.fillAmount = target;
    }
    public void SetAudioData()
    {
        AudioData data = new AudioData();
        data.allValue = sliderAll.value;
        data.soundValue = sliderSound.value;
        data.musicValue = sliderMusic.value;
        data.IsOpenAll=togAll.isOn;
        data.IsOpenMusic = togMusic.isOn;
        data.IsOpenSound = togSound.isOn;
        DataManger.Instance.UpdateAudioData(data);
    }
    private void SetPreAudioData()
    {
        predata = new AudioData();
        predata.allValue = DataManger.Instance.audioData.allValue;
        predata.soundValue = DataManger.Instance.audioData.soundValue;
        predata.musicValue = DataManger.Instance.audioData.musicValue;
        predata.IsOpenAll = DataManger.Instance.audioData.IsOpenAll;
        predata.IsOpenMusic = DataManger.Instance.audioData.IsOpenMusic;
        predata.IsOpenSound = DataManger.Instance.audioData.IsOpenSound;
    }
}
