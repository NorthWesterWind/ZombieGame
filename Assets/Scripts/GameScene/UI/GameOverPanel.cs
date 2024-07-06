using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text txtStatus;
    public Text txtTip;
    public Text txtMoney;
    public Button btnOK;
    public override void Init()
    {
        btnOK.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<GameOverPanel>();
            //隐藏游戏面板
            UIManager.Instance.HidePanel<GamePanel>();

            GameLevelMgr.Instance.Clear();

            //切换场景 
            SceneManager.LoadScene("SampleScene");
           
        });

    }
    /// <summary>
    /// 初始化面板信息
    /// </summary>
    /// <param name="money"></param>
    /// <param name="isWin"></param>
     public void InitInfo( int money , bool isWin)
    {
        
      txtMoney.text ="$:" + money;
        if (!isWin)
        {
           txtStatus.text = "失败";
           txtTip.text = "获得失败奖励";
        }
        else
        {
           txtStatus.text = "成功";
           txtTip.text = "获得成功奖励";
        }
        //更新玩家数据信息
        GameDateMgr.Instance.playerData.Money += GameLevelMgr.Instance.player.money; // 将关卡中金币数保存到本地
        GameDateMgr.Instance.SavePlayerData();
    }

    public override void Show()
    {
        base.Show();
        Cursor.lockState = CursorLockMode.None; // 游戏结束 解锁鼠标
    }

}
