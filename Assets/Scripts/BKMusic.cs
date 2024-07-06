using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ա������ֽ��й���
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

    //�����ⲿ�ı�
    //���ֿ���
    public void IsOpen(bool IsOpen)
    {
        bksource.mute = !IsOpen;
    }
    //��Чֵ
    public void ChangeVolume(float value)
    {
        bksource.volume = value;
    }
}
