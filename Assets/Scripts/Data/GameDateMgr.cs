using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDateMgr 
{
    private static GameDateMgr instance = new GameDateMgr();
    public static GameDateMgr Instance => instance;
    
    //音乐数据结构类
    public MusicData musicData;
    //角色数据容器
    public List<RoleInfo> roleList;
    //玩家当前数据
    public PlayerData playerData;
    //场景选择数据
    public List<SceneInfo> sceneInfoList;
    //怪物配置信息
    public List<MonsterInfo> monsterInfoList;
    //玩家选择的角色信息
    public RoleInfo selectRoleInfo;
    //炮塔数据
    public List<TowInfo> towInfoList;
    public  GameDateMgr()
    {
        //加载背景音乐数据
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //加载角色数据
        roleList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //初始化玩家数据
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //初始化场景数据
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //读取怪物数据
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        //读取炮塔数据
        towInfoList = JsonMgr.Instance.LoadData<List<TowInfo>>("TowInfo");
    }

    //提供给外部保存音乐数据方法
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    //提供给外部保存玩家数据的方法
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerDate");
    }

    /// <summary>
    /// 用于播放音效
    /// </summary>
    /// <param name="res"></param>
    public void PlaySound(string res)
    {
        GameObject obj = new GameObject();
        AudioSource audio = obj.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>(res);
        audio.volume = musicData.SoundValue;
        audio.mute = !musicData.SoundIsOpen;
        audio.Play();

        //播放一秒后删除
        GameObject.Destroy(obj, 1);
    }
}
