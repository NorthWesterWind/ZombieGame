using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
   private UnityEngine.Animator animator;

    public UnityAction endaction;
    void Start()
    {

        animator = this.GetComponent<UnityEngine.Animator>();

    }

    //提供给外部改变动画切换的函数
    public void TurnLeft(UnityAction action)
    {
        animator.SetTrigger("TurnLeft");
        endaction += action;    
    }

    public void TurnRight(UnityAction action)
    {
        animator.SetTrigger("TurnRight");
        endaction += action;
    }

    //动画播放完后执行逻辑
    public void OverAction()
    {
        endaction?.Invoke();
        endaction = null;
    }
   
}
