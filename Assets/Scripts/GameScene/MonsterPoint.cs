using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    public int maxWave;           //�����ж��ٲ�
    public int monsterNumOneWave; //ÿ����������
    private int nowNum;          //��¼��ǰ���Ĺ��ﻹ�ж���ֻû�д���
    public List<int> monsterIDs;  //��¼����ID
    private int nowID;            //��¼��ǰ��Ҫ����ʲôID�Ĺ���
    public float creatOffsetTime; //��ֻ���ﴴ�����ʱ��
    public float delayTime;       //���벨֮��ļ��ʱ��
    public float firstDelaytime;  //��һ�����ﴴ���ļ��ʱ��


    void Start()
    {
        Invoke("Createwave" , firstDelaytime);
        GameLevelMgr.Instance.AddMonsterPoint(this); //���ó��ֵ��¼
        GameLevelMgr.Instance.UpdateMaxNum(maxWave); //������󲨴���
    }

    private void Createwave()
    {
        if(maxWave == 0)
            return;
        //�õ���ǰ�������ID
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        //��ǰ�������ж���ֻ
        nowNum = monsterNumOneWave;
        //��������
        CreateMonster();
        //���ٲ���
        --maxWave;

        GameLevelMgr.Instance.ChangeNowWaveNum(1);  //����һ���� ���¹ؿ�������
    }
    
    private void CreateMonster()
    {
        //��������
        MonsterInfo Info = GameDateMgr.Instance.monsterInfoList[nowID - 1];

        GameObject o = Instantiate(Resources.Load<GameObject>(Info.res), this.transform.position, Quaternion.identity);
        MonsterObject monsterObject = o.AddComponent<MonsterObject>();
        //��ʼ������
        monsterObject.InitInfo(Info);

        --nowNum;    //�ò��ι���������һ

        GameLevelMgr.Instance.AddMonsterNum(monsterObject); //�������Ѵ��������� + 1
        if(nowNum == 0)
        {
            //�ò��ι��ﴴ�����
            if (maxWave > 0)
                Invoke("Createwave" , delayTime);
        }
        else
        {
            //�ò��ι���δ�������
            Invoke("CreateMonster" , creatOffsetTime);
        }

    }
    //���ó��ֵ����й����Ƿ����
    public bool CheckOver()
    {
        if(nowNum > 0)
            return false;
        return true;
    }
}
