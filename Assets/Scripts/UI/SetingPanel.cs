using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 设置面板类
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
        //对控件值进行初始化
        sliderMusic.value = GameDateMgr.Instance.musicData.MusicValue;
        sliderSound.value = GameDateMgr.Instance.musicData.SoundValue;
        togMusic.isOn = GameDateMgr.Instance.musicData.MusicIsOpen;
        togSound.isOn = GameDateMgr.Instance.musicData.SoundIsOpen;

        //事件监听
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
            //隐藏自己
            UIManager.Instance.HidePanel<SetingPanel>();
            //返回开始面板
            UIManager.Instance.ShowPanel<BeginPanel>();
            //记录音乐音效数据
            GameDateMgr.Instance.SaveMusicData();
        });
    }

    
}
