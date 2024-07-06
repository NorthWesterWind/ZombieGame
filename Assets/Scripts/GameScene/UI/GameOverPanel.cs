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
            //�����Լ�
            UIManager.Instance.HidePanel<GameOverPanel>();
            //������Ϸ���
            UIManager.Instance.HidePanel<GamePanel>();

            GameLevelMgr.Instance.Clear();

            //�л����� 
            SceneManager.LoadScene("SampleScene");
           
        });

    }
    /// <summary>
    /// ��ʼ�������Ϣ
    /// </summary>
    /// <param name="money"></param>
    /// <param name="isWin"></param>
     public void InitInfo( int money , bool isWin)
    {
        
      txtMoney.text ="$:" + money;
        if (!isWin)
        {
           txtStatus.text = "ʧ��";
           txtTip.text = "���ʧ�ܽ���";
        }
        else
        {
           txtStatus.text = "�ɹ�";
           txtTip.text = "��óɹ�����";
        }
        //�������������Ϣ
        GameDateMgr.Instance.playerData.Money += GameLevelMgr.Instance.player.money; // ���ؿ��н�������浽����
        GameDateMgr.Instance.SavePlayerData();
    }

    public override void Show()
    {
        base.Show();
        Cursor.lockState = CursorLockMode.None; // ��Ϸ���� �������
    }

}
