using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 炮塔按钮数据类
/// </summary>
public class Botbtn : MonoBehaviour
{
    public Image BotImg;
    public Text txtNumber;
    public Text txtMoney;

    /// <summary>
    /// 初始化按钮信息的方法
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputstr"></param>
    public void Initinfo(int id , string inputstr)
    {
        TowInfo info = GameDateMgr.Instance.towInfoList[id - 1];
        this.BotImg.sprite = Resources.Load<Sprite>(info.imgRes);
        this.txtMoney.text ="$ "+ info.money;
        this.txtNumber.text = inputstr;
        //判断钱够不够
        if (info.money > GameLevelMgr.Instance.player.money)
            this.txtMoney.text = "金币不足";
    }
}
