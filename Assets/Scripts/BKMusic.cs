using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对背景音乐进行管理
/// </summary>
public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    
    public AudioSource bksource;
    private void Awake()
    {
        instance = this;
        bksource = this.GetComponent<AudioSource>();
        MusicData musicData = GameDateMgr.Instance.musicData;
        IsOpen(musicData.MusicIsOpen);
        ChangeVolume(musicData.MusicValue);
    }

    //用于外部改变
    //音乐开关
    public void IsOpen(bool IsOpen)
    {
        bksource.mute = !IsOpen;
    }
    //音效值
    public void ChangeVolume(float value)
    {
        bksource.volume = value;
    }
}
