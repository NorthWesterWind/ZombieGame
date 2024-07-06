using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    //�����������������
    private GameObject TowerObj;
    //��������������Ϣ
    public TowInfo towInfo;
    //������ɽ����������
    public List<int> towIdList;
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="id"></param>
    public void CreatTower(int id)
    {
        TowInfo towinfo = GameDateMgr.Instance.towInfoList[id - 1];
        //�ж��Ƿ�Ǯ�㹻
        if(towinfo.money > GameLevelMgr.Instance.player.money)
        {
            return; 
        }

        GameLevelMgr.Instance.player.AddMoney( -towinfo.money);

        if(TowerObj != null)
        {
            Destroy(TowerObj);
            TowerObj = null;
        }
        //ʵ����������
        TowerObj = Instantiate(Resources.Load<GameObject>(towinfo.res) , this.transform.position , Quaternion.identity);
        //��ʼ����
        TowerObj.GetComponent<TowerObject>().InitInfo(towinfo);
        //��¼������
        towInfo = towinfo;

        //����������  ���½���
        if(towInfo.nextLev != 0)
        {
            UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateSelectTower(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateSelectTower(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (towInfo != null && towInfo.nextLev == 0)
            return;
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateSelectTower(this);
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateSelectTower(null);     
    }
}
