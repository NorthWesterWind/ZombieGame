using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// �ؿ�������
/// </summary>
public class GameLevelMgr 
{
     private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance { get { return instance; } }

    public PlayerObject player;

    //��¼���������г��ֵ�
    public List<MonsterPoint> monsterPointList = new List<MonsterPoint>();

    //�ܹ��ﲨ��
    public int MaxMonsterWave = 0;
    //��ǰ���ﲨ��
    public int nowMonsterWave = 0;

    //��������
    public List<MonsterObject> MonsterList = new List<MonsterObject>();
    private GameLevelMgr()
    {

    }
    public void Init(SceneInfo sceneInfo)
    {
        UIManager.Instance.ShowPanel<GamePanel>();

        //��ȡ��¼����ѡ����Ϸ��ɫ��Ϣ
        RoleInfo roleInfo = GameDateMgr.Instance.selectRoleInfo;
        //��ȡ������ ��ҵĳ���λ��
        Transform BornPos = GameObject.Find("HeroBornPos").transform;

        //�ڳ����д�����ɫ
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), BornPos.position ,BornPos.rotation);
        //��ʼ����Ҷ���
        player = heroObj.GetComponent<PlayerObject>();
        player.InitAtkandMoney(roleInfo.atk , sceneInfo.money  );


        //����������λ������
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
    }


    /// <summary>
    /// ��¼һ���ж��ٹ�
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxNum(int num)
    {
        MaxMonsterWave += num;
        nowMonsterWave = MaxMonsterWave;
        //���½���
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdataWave(nowMonsterWave, MaxMonsterWave);
    }

    public void ChangeNowWaveNum(int num)
    {
        nowMonsterWave -= num;
        //���½���
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdataWave(nowMonsterWave, MaxMonsterWave);
    }


    /// <summary>
    /// ��¼���ֵ�
    /// </summary>
    /// <param name="point"></param>
    public void AddMonsterPoint(MonsterPoint point)
    {
        monsterPointList.Add(point);
    }
    /// <summary>
    /// �����Ϸ�Ƿ�ʤ��
    /// </summary>
    /// <returns></returns>
    public bool CheckGameOver()
    {
        for(int i = 0; i < monsterPointList.Count; i++)
        {
            //������ڳ��ֵ㻹û�г������ �� �򷵻�false
            if(!monsterPointList[i].CheckOver())
                return false;
        }
        if(MonsterList.Count > 0 )
            return false;
        return true;
    }

    //���ڸı��������
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
    /// �ҵ���������
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public MonsterObject FindTarget(Vector3 pos , int range)
    {
        for (int i = 0; i < MonsterList.Count; i++)
        { 
            //������������
            if (Vector3.Distance(pos,MonsterList[i].transform.position) <= range
                && !MonsterList[i].isDead)
            {
              return MonsterList[i];
            }
        }
        return null;
    }
    /// <summary>
    /// �ҵ��������㹥�������Ķ���
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<MonsterObject> FindAllTarget(Vector3 pos, int range)
    { 
        List<MonsterObject> list = new List<MonsterObject>();
        for (int i = 0; i < MonsterList.Count; i++)
        {
            //������������
            if (Vector3.Distance(pos, MonsterList[i].transform.position) <= range
                && !MonsterList[i].isDead)
                list.Add(MonsterList[i]);
        }
        return list;
    }

    /// <summary>
    /// ��չؿ�����������
    /// </summary>
    public void Clear()
    {
        monsterPointList.Clear();
        player = null;
        MonsterList.Clear();
        MaxMonsterWave = nowMonsterWave = 0;
    }
}
