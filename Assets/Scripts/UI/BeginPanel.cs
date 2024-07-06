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
            //�����������ת����
            Camera.main.GetComponent<CameraControl>().TurnLeft(() =>
            {
                //��ʾѡ�����
                UIManager.Instance.ShowPanel<ChooseRolePanel>();
            });
            UIManager.Instance.HidePanel<BeginPanel>(); //���ؿ�ʼ���
        });
        btnSeting.onClick.AddListener(() =>
        {
            //��ʾ�������
            UIManager.Instance.ShowPanel<SetingPanel>();
        });
        btnExit.onClick.AddListener(() =>
        {
            Application.Quit(); //�˳�
        });
    }

    
}
