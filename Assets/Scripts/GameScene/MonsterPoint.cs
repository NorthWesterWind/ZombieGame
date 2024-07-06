using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    public int maxWave;           //怪物有多少波
    public int monsterNumOneWave; //每波怪物总数
    private int nowNum;          //记录当前波的怪物还有多少只没有创建
    public List<int> monsterIDs;  //记录怪物ID
    private int nowID;            //记录当前波要创建什么ID的怪物
    public float creatOffsetTime; //单只怪物创建间隔时间
    public float delayTime;       //波与波之间的间隔时间
    public float firstDelaytime;  //第一波怪物创建的间隔时间


    void Start()
    {
        Invoke("Createwave" , firstDelaytime);
        GameLevelMgr.Instance.AddMonsterPoint(this); //将该出怪点记录
        GameLevelMgr.Instance.UpdateMaxNum(maxWave); //更新最大波次数
    }

    private void Createwave()
    {
        if(maxWave == 0)
            return;
        //得到当前波怪物的ID
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        //当前波怪物有多少只
        nowNum = monsterNumOneWave;
        //创建怪物
        CreateMonster();
        //减少波数
        --maxWave;

        GameLevelMgr.Instance.ChangeNowWaveNum(1);  //出了一波怪 更新关卡类数据
    }
    
    private void CreateMonster()
    {
        //创建怪物
        MonsterInfo Info = GameDateMgr.Instance.monsterInfoList[nowID - 1];

        GameObject o = Instantiate(Resources.Load<GameObject>(Info.res), this.transform.position, Quaternion.identity);
        MonsterObject monsterObject = o.AddComponent<MonsterObject>();
        //初始化数据
        monsterObject.InitInfo(Info);

        --nowNum;    //该波次怪物数量减一

        GameLevelMgr.Instance.AddMonsterNum(monsterObject); //场景上已创建怪物数 + 1
        if(nowNum == 0)
        {
            //该波次怪物创建完毕
            if (maxWave > 0)
                Invoke("Createwave" , delayTime);
        }
        else
        {
            //该波次怪物未创建完毕
            Invoke("CreateMonster" , creatOffsetTime);
        }

    }
    //检查该出怪点所有怪物是否出完
    public bool CheckOver()
    {
        if(nowNum > 0)
            return false;
        return true;
    }
}
