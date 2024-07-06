using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterObject : MonoBehaviour
{
    //动画相关
    private Animator animator;
    //位移相关  寻路组件
    private NavMeshAgent agent;
    //一些不变的基础数据
    private MonsterInfo monsterInfo;

    //当前血量
    private int nowHp;

    public bool isDead = false;

    //怪物上一次攻击时间、
    private float frontTime;

     void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    //初始化
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //状态机加载
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //要变的当前血量
        nowHp = info.hp;
        //速度初始化
        agent.speed = agent.acceleration = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
    }

    //受伤
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        nowHp -= dmg;
        animator.SetTrigger("Wound");
       
        if (nowHp <= 0)
        {
            //死亡
            Dead();
        }
        else
        {
            //播放受伤音效
            GameDateMgr.Instance.PlaySound("Music/Wound");
        }
    }

    //死亡
    public void Dead()
    {
        isDead = true;
        agent.enabled = false; //停止移动
        animator.SetBool("Death", true); //播放死亡动画
        GameDateMgr.Instance.PlaySound("Music/dead");   //播放音效
        GameLevelMgr.Instance.player.AddMoney(monsterInfo.money); //玩家该关卡金币数增加
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateMoney(GameLevelMgr.Instance.player.money); //怪物死亡后 ， 更新游戏面板金币数显示
        
    }

    //死亡动画结束后执行的逻辑
    public void DeadEvent()
    {
        GameLevelMgr.Instance.RemoveMonsterNum(this); //怪物死亡  场景中怪物总数减一
        Destroy(this.gameObject);                   //怪物死亡  从场景中删除
         // 检测游戏是否胜利
        if (GameLevelMgr.Instance.CheckGameOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo( Convert.ToInt32(UIManager.Instance.GetPanel<GamePanel>("GamePanel").txtMoney.text) , true); //更新游戏结束面板上的金币数 
          
        }
            

    }

    //怪物出生动画结束后执行的逻辑
    public void BornOver()
    {
        //设置目标点
        agent.SetDestination(MainTowerObject.Instance.transform.position);
        //播放移动动画
        animator.SetBool("Run", true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //检测是否进行攻击
        if (isDead)
            return;
        //根据速度，来判断是否播放攻击动画
        animator.SetBool("Run" , agent.velocity != Vector3.zero);

        //通过判断距离来决定是否播放攻击动画
        if(Vector3.Distance(this.transform.position , MainTowerObject.Instance.transform.position) < 5 &&
            Time.time - frontTime >= monsterInfo.atkOffset)
        {
            frontTime = Time.time;
            animator.SetTrigger("Atk");
            GameDateMgr.Instance.PlaySound("Music/Eat");   //播放音效
        }
    }

    //伤害检测
    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));
        for(int i = 0; i < colliders.Length ; i++)
        {
            if(MainTowerObject.Instance.gameObject == colliders[i].gameObject)
            {
                //执行保护区受伤逻辑
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
