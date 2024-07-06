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

    //�ṩ���ⲿ�ı䶯���л��ĺ���
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

    //�����������ִ���߼�
    public void OverAction()
    {
        endaction?.Invoke();
        endaction = null;
    }
   
}
