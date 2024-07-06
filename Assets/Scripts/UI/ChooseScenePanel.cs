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

    //当前选择场景索引值
    private int index = 0 ;
    //当前选择场景数据
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
            //隐藏自己
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //加载场景
            AsyncOperation async =   SceneManager.LoadSceneAsync(sceneInfo.Scenename);
            //关卡初始化
            async.completed += ((o) =>
            {
                GameLevelMgr.Instance.Init(sceneInfo);
            }); 
        });
        btnReturn.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //显示选角面板
            UIManager.Instance.ShowPanel<ChooseRolePanel>();
        });
        UpdataPanel();
    }

    //用于更新面板显示
    public void UpdataPanel()
    {
        sceneInfo = GameDateMgr.Instance.sceneInfoList[index];
        sceneImg.sprite = Resources.Load<Sprite>("UI/" + sceneInfo.res);
        txt.text = "名称:" + sceneInfo.name + "\n" + "描述:" + sceneInfo.tip;
    }
}
