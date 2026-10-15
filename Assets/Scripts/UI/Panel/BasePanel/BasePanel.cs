using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel:MonoBehaviour
{
    private CanvasGroup canvasGroup;
    bool isShow = true;
    public float speed = 2f;
    private UnityAction hidecallback;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = this.AddComponent<CanvasGroup>();
        Init();
    }

    private void Update()
    {
        if (isShow)
        {
            canvasGroup.alpha += speed*Time.unscaledDeltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha-= speed*Time.unscaledDeltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha= 0;
                hidecallback?.Invoke();
            }
        }
    }
    virtual public void Show()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    virtual public void Hide(UnityAction callback)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hidecallback = callback;
    }
    protected abstract void Init();
}
