using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 关卡管理类
/// </summary>
public class GameLevelMgr 
{
     private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance { get { return instance; } }

    public PlayerObject player;

    //记录场景中所有出怪点
    public List<MonsterPoint> monsterPointList = new List<MonsterPoint>();

    //总怪物波数
    public int MaxMonsterWave = 0;
    //当前怪物波数
    public int nowMonsterWave = 0;

    //怪物容器
    public List<MonsterObject> MonsterList = new List<MonsterObject>();
    private GameLevelMgr()
    {

    }
    public void Init(SceneInfo sceneInfo)
    {
        UIManager.Instance.ShowPanel<GamePanel>();

        //获取记录的已选择游戏角色信息
        RoleInfo roleInfo = GameDateMgr.Instance.selectRoleInfo;
        //获取场景中 玩家的出生位置
        Transform BornPos = GameObject.Find("HeroBornPos").transform;

        //在场景中创建角色
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), BornPos.position ,BornPos.rotation);
        //初始化玩家对象
        player = heroObj.GetComponent<PlayerObject>();
        player.InitAtkandMoney(roleInfo.atk , sceneInfo.money  );


        //摄像机看向点位置设置
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
    }


    /// <summary>
    /// 记录一共有多少怪
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxNum(int num)
    {
        MaxMonsterWave += num;
        nowMonsterWave = MaxMonsterWave;
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdataWave(nowMonsterWave, MaxMonsterWave);
    }

    public void ChangeNowWaveNum(int num)
    {
        nowMonsterWave -= num;
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdataWave(nowMonsterWave, MaxMonsterWave);
    }


    /// <summary>
    /// 记录出怪点
    /// </summary>
    /// <param name="point"></param>
    public void AddMonsterPoint(MonsterPoint point)
    {
        monsterPointList.Add(point);
    }
    /// <summary>
    /// 检查游戏是否胜利
    /// </summary>
    /// <returns></returns>
    public bool CheckGameOver()
    {
        for(int i = 0; i < monsterPointList.Count; i++)
        {
            //如果存在出怪点还没有出完怪物 ， 则返回false
            if(!monsterPointList[i].CheckOver())
                return false;
        }
        if(MonsterList.Count > 0 )
            return false;
        return true;
    }

    //用于改变怪物数量
    public void AddMonsterNum(MonsterObject monster)
    {
        MonsterList.Add(monster);
    }
    public void RemoveMonsterNum(MonsterObject monster)
    { 
        if(MonsterList.Contains(monster))
           MonsterList.Remove(monster);
    }
    /// <summary>
    /// 找到攻击对象
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public MonsterObject FindTarget(Vector3 pos , int range)
    {
        for (int i = 0; i < MonsterList.Count; i++)
        { 
            //遍历怪物容器
            if (Vector3.Distance(pos,MonsterList[i].transform.position) <= range
                && !MonsterList[i].isDead)
            {
              return MonsterList[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 找到所有满足攻击条件的对象
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<MonsterObject> FindAllTarget(Vector3 pos, int range)
    { 
        List<MonsterObject> list = new List<MonsterObject>();
        for (int i = 0; i < MonsterList.Count; i++)
        {
            //遍历怪物容器
            if (Vector3.Distance(pos, MonsterList[i].transform.position) <= range
                && !MonsterList[i].isDead)
                list.Add(MonsterList[i]);
        }
        return list;
    }

    /// <summary>
    /// 清空关卡管理类数据
    /// </summary>
    public void Clear()
    {
        monsterPointList.Clear();
        player = null;
        MonsterList.Clear();
        MaxMonsterWave = nowMonsterWave = 0;
    }
}
