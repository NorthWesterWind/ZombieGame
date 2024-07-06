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

    //Ѫ����ʼ����
    public float width = 400;

    //�˳���ť
    public Button btnQuit;

    //�·�����ģ�鸸����  ��Ҫ��������UI�������
    public Transform Botbtn;

    //�����������
    public List<Botbtn> BotbtnList = new List<Botbtn>();

    private TowerPoint point;

    private bool checkOpen = false;
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            GameLevelMgr.Instance.Clear();
            //�����Լ�
            UIManager.Instance.HidePanel<GamePanel>();
            //���س���
            AsyncOperation async = SceneManager.LoadSceneAsync("SampleScene");
            //�ؿ���ʼ��
            async.completed += ((o) =>
            {
                //��ʾ��ʼ����
                UIManager.Instance.ShowPanel<BeginPanel>();
            });

        });
        //��ʼʱĬ�������·�����UI
        Botbtn.gameObject.SetActive(false);
        //�������״̬
        Cursor.lockState = CursorLockMode.Confined;
    }
    //����Ѫ������
    public void UpdataHp(int hp , int Maxhp)
    {
        txtHp.text = hp.ToString() + "/" + Maxhp.ToString();
        (imgHp.transform as RectTransform).sizeDelta = new Vector2 ((float)hp/Maxhp * width , 38);
    }

    //���²����ı���ʾ
    public void UpdataWave(int num , int maxnum)
    {
        txtWave.text = num + "/" + maxnum;
    }

    //���½���ı���ʾ
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }

    //���µ�ǰѡ��������
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
                    BotbtnList[i].Initinfo(point.towIdList[i], "���ּ�" + (i + 1));
                }
            }
            else
            {
                //���������߼�
                for (int i = 0; i < BotbtnList.Count; i++)
                {
                    BotbtnList[i].gameObject.SetActive(false);
                }
                BotbtnList[1].gameObject.SetActive(true);
                BotbtnList[1].Initinfo(point.towInfo.nextLev, "�ո������");
            }
        }


       
    }
    public override void Update()
    {
        base.Update();
        if(!checkOpen)
            return;
            this.Botbtn.gameObject.SetActive(checkOpen);
        //����������  ��������  ����
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
            //��������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                point.CreatTower(point.towInfo.nextLev);
            }
        }
    }
}
