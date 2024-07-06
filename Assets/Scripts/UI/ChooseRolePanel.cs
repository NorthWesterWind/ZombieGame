using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseRolePanel : BasePanel
{
    //���ؼ�����

    public Text txtMoney;
    public Text txtName;

    public Button btnLeft;
    public Button btnRight;

    public Button btnStart;
    public Button btnReturn;

    public Button btnUnlock;
    public Text UnlockMoney;

    //��ɫԤ����������Ϣ
    private Transform heropos;
    //��ǰ��ɫ����
    private GameObject nowHeroObj;
    //��ǰʹ�õĽ�ɫ����
    public RoleInfo nowRoleInfo;
    //��ǰʹ�õĽ�ɫ����ֵ
    public int index = 0;
    public override void Init()
    {
        //��ȡ��ɫ��������
        heropos = GameObject.Find("HeroPos").transform;

        //������ҵ�ǰ�����
        txtMoney.text = GameDateMgr.Instance.playerData.Money.ToString();

        //�ؼ��¼�����
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
            //��¼ѡ��Ľ�ɫ��Ϣ
            GameDateMgr.Instance.selectRoleInfo = nowRoleInfo;
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseRolePanel>(true);
            //��ʾ��Ϸ����ѡ�����
            UIManager.Instance.ShowPanel<ChooseScenePanel>();

        });

        btnReturn.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseRolePanel>(true);
            //�����������ת����  ��ʾ��ʼ���
            Camera.main.GetComponent<CameraControl>().TurnRight(() =>
            {
                //�������� ��ʾ��ʼ���
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
            
        });

        btnUnlock.onClick.AddListener(() =>
        {
            //�жϵ�ǰ��ҵ�Ǯ�Ƿ��������
            if(GameDateMgr.Instance.playerData.Money >= nowRoleInfo.lockMoney)
            {
                GameDateMgr.Instance.playerData.buyheroList.Add(nowRoleInfo.id);
                GameDateMgr.Instance.playerData.Money -= nowRoleInfo.lockMoney;
                txtMoney.text = GameDateMgr.Instance.playerData.Money.ToString();
                GameDateMgr.Instance.SavePlayerData();
                UpdateHero();
                TipPanel t = UIManager.Instance.ShowPanel<TipPanel>();
                t.ChangeTip("����ɹ���");
            }
            else
            {
                //����  ��ʾ��ʾ���
               TipPanel t = UIManager.Instance.ShowPanel<TipPanel>();
                t.ChangeTip("��Ҳ��㣡");
            }
            
        });
        
        UpdateHero();
    }
    /// <summary>
    /// ���½�ɫ��ʾ
    /// </summary>
    public void UpdateHero()
    {
        if(nowHeroObj != null)
        {
            Destroy(nowHeroObj);
            nowHeroObj = null;
        }
        //��������ֵ ��ȡ��ǰ��ɫ��Ϣ
        nowRoleInfo = GameDateMgr.Instance.roleList[index];

        txtName.text = nowRoleInfo.tips;
        //ʵ������ɫ����
        nowHeroObj = Instantiate<GameObject>(Resources.Load<GameObject>(nowRoleInfo.res), heropos.position, heropos.rotation);

        Destroy(nowHeroObj.GetComponent<PlayerObject>());
        txtName.text = nowRoleInfo.tips;
        UnlockStatus();
    }
    /// <summary>
    /// ��ʾ������ť�߼�
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
