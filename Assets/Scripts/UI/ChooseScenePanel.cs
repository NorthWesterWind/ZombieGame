using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnReturn;
    public Image sceneImg;
    public Text txt;

    //��ǰѡ�񳡾�����ֵ
    private int index = 0 ;
    //��ǰѡ�񳡾�����
    private SceneInfo sceneInfo;
    public override void Init()
    {
        btnLeft.onClick.AddListener(() =>
        {
            --index;
            if(index < 0)
            {
                index = GameDateMgr.Instance.sceneInfoList.Count - 1;
            }
            UpdataPanel();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++index;
            if (index > GameDateMgr.Instance.sceneInfoList.Count - 1)
            {
                index = 0;
            }
            UpdataPanel();
        });
        btnStart.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //���س���
            AsyncOperation async =   SceneManager.LoadSceneAsync(sceneInfo.Scenename);
            //�ؿ���ʼ��
            async.completed += ((o) =>
            {
                GameLevelMgr.Instance.Init(sceneInfo);
            }); 
        });
        btnReturn.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //��ʾѡ�����
            UIManager.Instance.ShowPanel<ChooseRolePanel>();
        });
        UpdataPanel();
    }

    //���ڸ��������ʾ
    public void UpdataPanel()
    {
        sceneInfo = GameDateMgr.Instance.sceneInfoList[index];
        sceneImg.sprite = Resources.Load<Sprite>("UI/" + sceneInfo.res);
        txt.text = "����:" + sceneInfo.name + "\n" + "����:" + sceneInfo.tip;
    }
}
