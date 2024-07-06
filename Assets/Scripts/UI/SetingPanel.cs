using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���������
/// </summary>
public class SetingPanel : BasePanel
{
    public Slider sliderSound;
    public Slider sliderMusic;
    public Toggle togSound;
    public Toggle togMusic;
    public Button btnExit;


    public override void Init()
    {
        //�Կؼ�ֵ���г�ʼ��
        sliderMusic.value = GameDateMgr.Instance.musicData.MusicValue;
        sliderSound.value = GameDateMgr.Instance.musicData.SoundValue;
        togMusic.isOn = GameDateMgr.Instance.musicData.MusicIsOpen;
        togSound.isOn = GameDateMgr.Instance.musicData.SoundIsOpen;

        //�¼�����
        sliderSound.onValueChanged.AddListener((v) =>
        {
           
            GameDateMgr.Instance.musicData.SoundValue = v;
        });
        sliderMusic.onValueChanged.AddListener((v) =>
        {
            BKMusic.Instance.ChangeVolume(v);
           GameDateMgr.Instance.musicData.MusicValue = v;
        });
        togSound.onValueChanged.AddListener((b) =>
        {
            GameDateMgr.Instance.musicData.SoundIsOpen = b;
        });
        togMusic.onValueChanged.AddListener((b) =>
        {
            BKMusic.Instance.IsOpen(b);
            GameDateMgr.Instance.musicData.MusicIsOpen = b;
        });
        btnExit.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<SetingPanel>();
            //���ؿ�ʼ���
            UIManager.Instance.ShowPanel<BeginPanel>();
            //��¼������Ч����
            GameDateMgr.Instance.SaveMusicData();
        });
    }

    
}
