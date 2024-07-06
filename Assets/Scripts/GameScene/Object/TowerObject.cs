using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerObject : MonoBehaviour
{
    //������Ϣ
    private TowInfo towInfo;
    //�����
    public Transform FirePos;
    //��������תͷ��
    public Transform headPos;
    //������ת�ٶ�
    private int roundSpeed = 20;
    //Ŀ�����
    private MonsterObject targetObj;
    //����Ŀ�����  ����Ⱥ�幥��
    private List<MonsterObject> targetObjs;
    //��������
    private Vector3 monsterPos;
    //����ʱ��
    private float time;
    //��ʼ������
    public void InitInfo(TowInfo towInfo)
    {
        this.towInfo = towInfo;
    }
   

    // Update is called once per frame
    void Update()
    {
        if(towInfo.atkType == 1)
        { 
                //���幥��
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
                    //����ת��
                    headPos.rotation = Quaternion.Slerp(headPos.rotation, Quaternion.LookRotation(monsterPos - headPos.position), roundSpeed * Time.deltaTime);

                    if (Vector3.Angle(headPos.forward, monsterPos - headPos.position) < 5 &&
                         Time.time - time >= towInfo.offsetTime)
                    {
                        //���ﱻ��������
                        targetObj.Wound(towInfo.atk);
                        //������Ч��
                        GameObject effObj = GameObject.Instantiate(Resources.Load<GameObject>(towInfo.eff), FirePos.position, Quaternion.identity);
                        Destroy(effObj, 0.2f);
                        //������Ч
                        GameDateMgr.Instance.PlaySound("Music/Tower");
                        time = Time.time;
                    }
                }
                
            
        }
        else
        {
            //Ⱥ�幥��
            targetObjs = GameLevelMgr.Instance.FindAllTarget(headPos.position, towInfo.atkRange);

            if(targetObjs.Count >0 &&
                Time.time - time >= towInfo.offsetTime)
            {
                //������Ч��
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
