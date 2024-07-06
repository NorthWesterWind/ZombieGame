using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //玩家属性
    private int atk;
    public int money;
    //旋转速度
    public float turnspeed = 50;

    private Animator animator;

    //远程武器  开火点
    public Transform FirePos;
 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region 控制相关
        //应用动作的位移
        animator.SetFloat("Vspeed", Input.GetAxis("Vertical"));
        animator.SetFloat("Hspeed", Input.GetAxis("Horizontal"));

        //旋转
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * turnspeed * Time.deltaTime);
        //蹲下
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(2, 1);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(2, 0);
        }

        //开火
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("fire");
        }

        //翻滚
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetTrigger("Roll");
        }
        #endregion
    }
    /// <summary>
    /// 修改攻击力和金钱
    /// </summary>
    /// <param name="Atk"></param>
    /// <param name="Money"></param>
    public void InitAtkandMoney(int Atk , int Money)
    {
        atk = Atk;
        money = Money;
        UpdataMoney();
    }


    /// <summary>
    /// 专门用于处理刀武器攻击动作的伤害检测事件
    /// </summary>
    public void KnifeEvent()
    {
        GameDateMgr.Instance.PlaySound("Music/Knife");   //播放音效
        //进行伤害检测  范围检测
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        for(int i = 0; i < colliders.Length; i++)
        {
            MonsterObject m = colliders[i].gameObject.GetComponent<MonsterObject>();
            if(m != null && !m.isDead )
            {
                //攻击特效创建
                GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>(GameDateMgr.Instance.selectRoleInfo.effRes));
                obj.transform.position = colliders[i].transform.position;
                Destroy(obj, 1);
                m.Wound(this.atk);
                break;
            }
               
        }
    }

    public void ShootEvent()
    {
        GameDateMgr.Instance.PlaySound("Music/Gun");   //播放音效
        //进行伤害检测
        RaycastHit[] raycastHits =  Physics.RaycastAll(new Ray(FirePos.position, this.transform.forward) , 1000 , 1 << LayerMask.NameToLayer("Monster"));
        for(int j = 0; j < raycastHits.Length; j++)
        {
            MonsterObject m = raycastHits[j].collider.gameObject.GetComponent<MonsterObject>();
            if (m != null && !m.isDead)
            {
                //攻击特效创建
                GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>(GameDateMgr.Instance.selectRoleInfo.effRes));
                obj.transform.position = raycastHits[j].transform.position;
                obj.transform.rotation = Quaternion.LookRotation(raycastHits[j].normal);
                Destroy(obj, 1);
                m.Wound(this.atk);
                break;
            }
              
        }
    }

    public void UpdataMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>("GamePanel").UpdateMoney(money);
    }

    public void AddMoney(int num)
    {
        this.money += num;
        UpdataMoney();
    }
}
