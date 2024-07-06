using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //ר�����ڿ������͸���ȵ����
    private CanvasGroup canvasGroup;
    //���뵭�����ٶ�
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
    /// ����̳б�����д�÷���
    /// ����ע�����ؼ�
    /// </summary>
    public abstract void Init();
   
    /// <summary>
    /// �����ʾʱ����
    /// </summary>
    public virtual void Show()
    {
        alphaSpeed = 0;
        IsShow = true;
    }

    /// <summary>
    /// �������ʱ����
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
