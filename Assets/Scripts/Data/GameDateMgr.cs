using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDateMgr 
{
    private static GameDateMgr instance = new GameDateMgr();
    public static GameDateMgr Instance => instance;
    
    //�������ݽṹ��
    public MusicData musicData;
    //��ɫ��������
    public List<RoleInfo> roleList;
    //��ҵ�ǰ����
    public PlayerData playerData;
    //����ѡ������
    public List<SceneInfo> sceneInfoList;
    //����������Ϣ
    public List<MonsterInfo> monsterInfoList;
    //���ѡ��Ľ�ɫ��Ϣ
    public RoleInfo selectRoleInfo;
    //��������
    public List<TowInfo> towInfoList;
    public  GameDateMgr()
    {
        //���ر�����������
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //���ؽ�ɫ����
        roleList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //��ʼ���������
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //��ʼ����������
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //��ȡ��������
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        //��ȡ��������
        towInfoList = JsonMgr.Instance.LoadData<List<TowInfo>>("TowInfo");
    }

    //�ṩ���ⲿ�����������ݷ���
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    //�ṩ���ⲿ����������ݵķ���
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerDate");
    }

    /// <summary>
    /// ���ڲ�����Ч
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

        //����һ���ɾ��
        GameObject.Destroy(obj, 1);
    }
}
