using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    //血量相关
    public int hp;
    public int maxHp;
    //是否死亡
    private bool isDead;

    //能够被别人快速获取到位置
    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;


    private void Awake()
    {
        instance = this;
    }

    //自己受到伤害
    public void Wound(int value)
    {
        if (isDead)
            return;
        
        hp -= value;
        if(hp <= 0)
        {
            hp = 0;
            isDead = true;
            //游戏结束
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(Convert.ToInt32(UIManager.Instance.GetPanel<GamePanel>("GamePanel").txtMoney.text) * 0.6f), false); //更新游戏结束面板金币数
        }
        UpdateHp(hp , maxHp);
        
    }

    //更新血量
    public void UpdateHp(int hp , int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;

        //同步更新界面上血量的显示
        GamePanel panel =   UIManager.Instance.GetPanel<GamePanel>("GamePanel");
        panel.UpdataHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
