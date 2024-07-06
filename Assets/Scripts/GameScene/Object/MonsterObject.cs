using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterObject : MonoBehaviour
{
    //�������
    private Animator animator;
    //λ�����  Ѱ·���
    private NavMeshAgent agent;
    //һЩ����Ļ�������
    private MonsterInfo monsterInfo;

    //��ǰѪ��
    private int nowHp;

    public bool isDead = false;

    //������һ�ι���ʱ�䡢
    private float frontTime;

     void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    //��ʼ��
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //״̬������
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //Ҫ��ĵ�ǰѪ��
        nowHp = info.hp;
        //�ٶȳ�ʼ��
        agent.speed = agent.acceleration = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
    }

    //����
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        nowHp -= dmg;
        animator.SetTrigger("Wound");
       
        if (nowHp <= 0)
        {
            //����
            Dead();
        }
        else
        {
            //����������Ч
            GameDateMgr.Instance.PlaySound("Music/Wound");
        }
    }

    //����
    public void Dead()
    {
        isDead = true;
        agent.enabled = false; //ֹͣ�ƶ�
        animator.SetBool("Death", true); //������������
        GameDateMgr.Instance.PlaySound("Music/dead");   //������Ч
        GameLevelMgr.Instance.player.AddMoney(monsterInfo.money); //��Ҹùؿ����������
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateMoney(GameLevelMgr.Instance.player.money); //���������� �� ������Ϸ���������ʾ
        
    }

    //��������������ִ�е��߼�
    public void DeadEvent()
    {
        GameLevelMgr.Instance.RemoveMonsterNum(this); //��������  �����й���������һ
        Destroy(this.gameObject);                   //��������  �ӳ�����ɾ��
         // �����Ϸ�Ƿ�ʤ��
        if (GameLevelMgr.Instance.CheckGameOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo( Convert.ToInt32(UIManager.Instance.GetPanel<GamePanel>("GamePanel").txtMoney.text) , true); //������Ϸ��������ϵĽ���� 
          
        }
            

    }

    //�����������������ִ�е��߼�
    public void BornOver()
    {
        //����Ŀ���
        agent.SetDestination(MainTowerObject.Instance.transform.position);
        //�����ƶ�����
        animator.SetBool("Run", true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //����Ƿ���й���
        if (isDead)
            return;
        //�����ٶȣ����ж��Ƿ񲥷Ź�������
        animator.SetBool("Run" , agent.velocity != Vector3.zero);

        //ͨ���жϾ����������Ƿ񲥷Ź�������
        if(Vector3.Distance(this.transform.position , MainTowerObject.Instance.transform.position) < 5 &&
            Time.time - frontTime >= monsterInfo.atkOffset)
        {
            frontTime = Time.time;
            animator.SetTrigger("Atk");
            GameDateMgr.Instance.PlaySound("Music/Eat");   //������Ч
        }
    }

    //�˺����
    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));
        for(int i = 0; i < colliders.Length ; i++)
        {
            if(MainTowerObject.Instance.gameObject == colliders[i].gameObject)
            {
                //ִ�б����������߼�
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
