using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    //造塔点关联的塔对象
    private GameObject TowerObj;
    //关联的塔数据信息
    public TowInfo towInfo;
    //造塔点可建造的三个塔
    public List<int> towIdList;
    /// <summary>
    /// 建造炮塔
    /// </summary>
    /// <param name="id"></param>
    public void CreatTower(int id)
    {
        TowInfo towinfo = GameDateMgr.Instance.towInfoList[id - 1];
        //判断是否钱足够
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
        //实例化塔对象
        TowerObj = Instantiate(Resources.Load<GameObject>(towinfo.res) , this.transform.position , Quaternion.identity);
        //初始化塔
        TowerObj.GetComponent<TowerObject>().InitInfo(towinfo);
        //记录塔数据
        towInfo = towinfo;

        //建造炮塔后  更新界面
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
