using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSeting;
    public Button btnExit;

   
    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {   
            //播放摄像机左转动画
            Camera.main.GetComponent<CameraControl>().TurnLeft(() =>
            {
                //显示选角面板
                UIManager.Instance.ShowPanel<ChooseRolePanel>();
            });
            UIManager.Instance.HidePanel<BeginPanel>(); //隐藏开始面板
        });
        btnSeting.onClick.AddListener(() =>
        {
            //显示设置面板
            UIManager.Instance.ShowPanel<SetingPanel>();
        });
        btnExit.onClick.AddListener(() =>
        {
            Application.Quit(); //退出
        });
    }

    
}
