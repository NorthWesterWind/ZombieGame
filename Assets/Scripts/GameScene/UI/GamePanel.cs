using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHp;
    public Text txtHp;
    public Text txtWave;
    public Text txtMoney;

    //血条初始长度
    public float width = 400;

    //退出按钮
    public Button btnQuit;

    //下方造塔模块父对象  主要控制炮塔UI组件显隐
    public Transform Botbtn;

    //炮塔组件容器
    public List<Botbtn> BotbtnList = new List<Botbtn>();

    private TowerPoint point;

    private bool checkOpen = false;
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            GameLevelMgr.Instance.Clear();
            //隐藏自己
            UIManager.Instance.HidePanel<GamePanel>();
            //加载场景
            AsyncOperation async = SceneManager.LoadSceneAsync("SampleScene");
            //关卡初始化
            async.completed += ((o) =>
            {
                //显示开始界面
                UIManager.Instance.ShowPanel<BeginPanel>();
            });

        });
        //开始时默认隐藏下方造塔UI
        Botbtn.gameObject.SetActive(false);
        //锁定鼠标状态
        Cursor.lockState = CursorLockMode.Confined;
    }
    //更新血量方法
    public void UpdataHp(int hp , int Maxhp)
    {
        txtHp.text = hp.ToString() + "/" + Maxhp.ToString();
        (imgHp.transform as RectTransform).sizeDelta = new Vector2 ((float)hp/Maxhp * width , 38);
    }

    //更新波数文本显示
    public void UpdataWave(int num , int maxnum)
    {
        txtWave.text = num + "/" + maxnum;
    }

    //更新金币文本显示
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }

    //更新当前选中造塔点
    public void UpdateSelectTower(TowerPoint towerPoint)
    {
        if(towerPoint == null)
        {
            checkOpen = false;
            Botbtn.gameObject.SetActive(false);
        }
        else
        {
            checkOpen = true;
            this.point = towerPoint;
            if (point.towInfo == null)
            {
                for (int i = 0; i < BotbtnList.Count; i++)
                {

                    BotbtnList[i].gameObject.SetActive(true);
                    BotbtnList[i].Initinfo(point.towIdList[i], "数字键" + (i + 1));
                }
            }
            else
            {
                //升级造塔逻辑
                for (int i = 0; i < BotbtnList.Count; i++)
                {
                    BotbtnList[i].gameObject.SetActive(false);
                }
                BotbtnList[1].gameObject.SetActive(true);
                BotbtnList[1].Initinfo(point.towInfo.nextLev, "空格键升级");
            }
        }


       
    }
    public override void Update()
    {
        base.Update();
        if(!checkOpen)
            return;
            this.Botbtn.gameObject.SetActive(checkOpen);
        //用于造塔点  键盘输入  造塔
        if(point.towInfo == null)
        {
           
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                point.CreatTower(point.towIdList[0]);
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                point.CreatTower(point.towIdList[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                point.CreatTower(point.towIdList[2]);
            }
        }
        else
        {
            //升级炮塔
            if (Input.GetKeyDown(KeyCode.Space))
            {
                point.CreatTower(point.towInfo.nextLev);
            }
        }
    }
}
