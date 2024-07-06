using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerObject : MonoBehaviour
{
    //炮塔信息
    private TowInfo towInfo;
    //开火点
    public Transform FirePos;
    //炮塔可旋转头部
    public Transform headPos;
    //炮塔旋转速度
    private int roundSpeed = 20;
    //目标怪物
    private MonsterObject targetObj;
    //所有目标怪物  用于群体攻击
    private List<MonsterObject> targetObjs;
    //怪物坐标
    private Vector3 monsterPos;
    //攻击时间
    private float time;
    //初始化方法
    public void InitInfo(TowInfo towInfo)
    {
        this.towInfo = towInfo;
    }
   

    // Update is called once per frame
    void Update()
    {
        if(towInfo.atkType == 1)
        { 
                //单体攻击
                if (targetObj == null
                    || targetObj.isDead    
                    || Vector3.Distance(this.transform.position ,targetObj.transform.position) > towInfo.atkRange  )
                {
                    targetObj = GameLevelMgr.Instance.FindTarget(this.transform.position , towInfo.atkRange);

                }
                if (targetObj == null)
                    return;
                else
                {
                    monsterPos = targetObj.transform.position;
                    monsterPos.y = headPos.position.y;
                    //炮塔转向
                    headPos.rotation = Quaternion.Slerp(headPos.rotation, Quaternion.LookRotation(monsterPos - headPos.position), roundSpeed * Time.deltaTime);

                    if (Vector3.Angle(headPos.forward, monsterPos - headPos.position) < 5 &&
                         Time.time - time >= towInfo.offsetTime)
                    {
                        //怪物被击中受伤
                        targetObj.Wound(towInfo.atk);
                        //创建特效、
                        GameObject effObj = GameObject.Instantiate(Resources.Load<GameObject>(towInfo.eff), FirePos.position, Quaternion.identity);
                        Destroy(effObj, 0.2f);
                        //播放音效
                        GameDateMgr.Instance.PlaySound("Music/Tower");
                        time = Time.time;
                    }
                }
                
            
        }
        else
        {
            //群体攻击
            targetObjs = GameLevelMgr.Instance.FindAllTarget(headPos.position, towInfo.atkRange);

            if(targetObjs.Count >0 &&
                Time.time - time >= towInfo.offsetTime)
            {
                //创建特效、
                GameObject effObj = GameObject.Instantiate(Resources.Load<GameObject>(towInfo.eff), FirePos.position, Quaternion.identity);
                Destroy(effObj, 0.2f);

                for (int i = 0; i < targetObjs.Count; i++)
                {
                    targetObjs[i].Wound(towInfo.atk);
                }
                time = Time.time;
            }
             
            
        }
    }
}
