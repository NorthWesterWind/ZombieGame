using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    //Ѫ�����
    public int hp;
    public int maxHp;
    //�Ƿ�����
    private bool isDead;

    //�ܹ������˿��ٻ�ȡ��λ��
    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;


    private void Awake()
    {
        instance = this;
    }

    //�Լ��ܵ��˺�
    public void Wound(int value)
    {
        if (isDead)
            return;
        
        hp -= value;
        if(hp <= 0)
        {
            hp = 0;
            isDead = true;
            //��Ϸ����
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(Convert.ToInt32(UIManager.Instance.GetPanel<GamePanel>("GamePanel").txtMoney.text) * 0.6f), false); //������Ϸ�����������
        }
        UpdateHp(hp , maxHp);
        
    }

    //����Ѫ��
    public void UpdateHp(int hp , int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;

        //ͬ�����½�����Ѫ������ʾ
        GamePanel panel =   UIManager.Instance.GetPanel<GamePanel>("GamePanel");
        panel.UpdataHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
