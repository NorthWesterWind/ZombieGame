using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnClose;
    public Text txtTip;
    public override void Init()
    {
        btnClose.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    //�޸��ı�����
    public void ChangeTip(string str)
    {
        txtTip.text = str;
    }
}
