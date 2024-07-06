using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //专门用于控制面板透明度的组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 10;

    private bool IsShow = false;
    private UnityAction hidecallback = null;

    protected virtual void Awake()
    {
       canvasGroup = this.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
            canvasGroup = this.AddComponent<CanvasGroup>();
    }
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 子类继承必须重写该方法
    /// 用于注册面板控件
    /// </summary>
    public abstract void Init();
   
    /// <summary>
    /// 面板显示时调用
    /// </summary>
    public virtual void Show()
    {
        alphaSpeed = 0;
        IsShow = true;
    }

    /// <summary>
    /// 面板隐藏时调用
    /// </summary>
    public virtual void Hide(UnityAction action)
    {
        alphaSpeed = 1;
        IsShow = false;
        hidecallback = action;
    }

   public virtual void Update()
    {
        if(IsShow && alphaSpeed != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha > 1)
                canvasGroup.alpha = 1;
        }
        if(!IsShow && alphaSpeed != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha < 0)
            {
                canvasGroup.alpha = 0;
            }
           if(canvasGroup.alpha == 0)
               hidecallback?.Invoke();
        }
    }
}
