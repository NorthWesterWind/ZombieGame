using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ť������
/// </summary>
public class Botbtn : MonoBehaviour
{
    public Image BotImg;
    public Text txtNumber;
    public Text txtMoney;

    /// <summary>
    /// ��ʼ����ť��Ϣ�ķ���
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputstr"></param>
    public void Initinfo(int id , string inputstr)
    {
        TowInfo info = GameDateMgr.Instance.towInfoList[id - 1];
        this.BotImg.sprite = Resources.Load<Sprite>(info.imgRes);
        this.txtMoney.text ="$ "+ info.money;
        this.txtNumber.text = inputstr;
        //�ж�Ǯ������
        if (info.money > GameLevelMgr.Instance.player.money)
            this.txtMoney.text = "��Ҳ���";
    }
}
