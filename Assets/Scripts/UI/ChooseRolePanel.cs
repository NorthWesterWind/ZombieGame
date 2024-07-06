using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseRolePanel : BasePanel
{
    //面板控件声明

    public Text txtMoney;
    public Text txtName;

    public Button btnLeft;
    public Button btnRight;

    public Button btnStart;
    public Button btnReturn;

    public Button btnUnlock;
    public Text UnlockMoney;

    //角色预设体坐标信息
    private Transform heropos;
    //当前角色对象
    private GameObject nowHeroObj;
    //当前使用的角色数据
    public RoleInfo nowRoleInfo;
    //当前使用的角色索引值
    public int index = 0;
    public override void Init()
    {
        //获取角色对象坐标
        heropos = GameObject.Find("HeroPos").transform;

        //更新玩家当前金币数
        txtMoney.text = GameDateMgr.Instance.playerData.Money.ToString();

        //控件事件监听
        btnLeft.onClick.AddListener(() =>
        {
            --index;
            if (index < 0)
                index = GameDateMgr.Instance.roleList.Count - 1;

            UpdateHero();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++index;
            if (index > GameDateMgr.Instance.roleList.Count - 1)
                index = 0;
            UpdateHero();
        });

        btnStart.onClick.AddListener(() =>
        {
            //记录选择的角色信息
            GameDateMgr.Instance.selectRoleInfo = nowRoleInfo;
            //隐藏自己
            UIManager.Instance.HidePanel<ChooseRolePanel>(true);
            //显示游戏场景选择面板
            UIManager.Instance.ShowPanel<ChooseScenePanel>();

        });

        btnReturn.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<ChooseRolePanel>(true);
            //播放摄像机右转动画  显示开始面板
            Camera.main.GetComponent<CameraControl>().TurnRight(() =>
            {
                //动画结束 显示开始面板
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
            
        });

        btnUnlock.onClick.AddListener(() =>
        {
            //判断当前玩家的钱是否能买的起
            if(GameDateMgr.Instance.playerData.Money >= nowRoleInfo.lockMoney)
            {
                GameDateMgr.Instance.playerData.buyheroList.Add(nowRoleInfo.id);
                GameDateMgr.Instance.playerData.Money -= nowRoleInfo.lockMoney;
                txtMoney.text = GameDateMgr.Instance.playerData.Money.ToString();
                GameDateMgr.Instance.SavePlayerData();
                UpdateHero();
                TipPanel t = UIManager.Instance.ShowPanel<TipPanel>();
                t.ChangeTip("购买成功！");
            }
            else
            {
                //买不起  显示提示面板
               TipPanel t = UIManager.Instance.ShowPanel<TipPanel>();
                t.ChangeTip("金币不足！");
            }
            
        });
        
        UpdateHero();
    }
    /// <summary>
    /// 更新角色显示
    /// </summary>
    public void UpdateHero()
    {
        if(nowHeroObj != null)
        {
            Destroy(nowHeroObj);
            nowHeroObj = null;
        }
        //根据索引值 获取当前角色信息
        nowRoleInfo = GameDateMgr.Instance.roleList[index];

        txtName.text = nowRoleInfo.tips;
        //实例化角色对象
        nowHeroObj = Instantiate<GameObject>(Resources.Load<GameObject>(nowRoleInfo.res), heropos.position, heropos.rotation);

        Destroy(nowHeroObj.GetComponent<PlayerObject>());
        txtName.text = nowRoleInfo.tips;
        UnlockStatus();
    }
    /// <summary>
    /// 显示解锁按钮逻辑
    /// </summary>
    public void UnlockStatus()
    {
        if (nowRoleInfo.lockMoney > 0 && !GameDateMgr.Instance.playerData.buyheroList.Contains(nowRoleInfo.id))
        {
            btnUnlock.gameObject.SetActive(true);
            UnlockMoney.text = "$:" + nowRoleInfo.lockMoney;
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnlock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }

    }

    public override void Hide(UnityAction action)
    {
        base.Hide(action);
        if (nowHeroObj != null)
        {
            DestroyImmediate(this.nowHeroObj);
            this.nowHeroObj = null;
        }

    }
}
