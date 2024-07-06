using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //�������
    private int atk;
    public int money;
    //��ת�ٶ�
    public float turnspeed = 50;

    private Animator animator;

    //Զ������  �����
    public Transform FirePos;
 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region �������
        //Ӧ�ö�����λ��
        animator.SetFloat("Vspeed", Input.GetAxis("Vertical"));
        animator.SetFloat("Hspeed", Input.GetAxis("Horizontal"));

        //��ת
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * turnspeed * Time.deltaTime);
        //����
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(2, 1);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(2, 0);
        }

        //����
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("fire");
        }

        //����
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetTrigger("Roll");
        }
        #endregion
    }
    /// <summary>
    /// �޸Ĺ������ͽ�Ǯ
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
    /// ר�����ڴ������������������˺�����¼�
    /// </summary>
    public void KnifeEvent()
    {
        GameDateMgr.Instance.PlaySound("Music/Knife");   //������Ч
        //�����˺����  ��Χ���
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        for(int i = 0; i < colliders.Length; i++)
        {
            MonsterObject m = colliders[i].gameObject.GetComponent<MonsterObject>();
            if(m != null && !m.isDead )
            {
                //������Ч����
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
        GameDateMgr.Instance.PlaySound("Music/Gun");   //������Ч
        //�����˺����
        RaycastHit[] raycastHits =  Physics.RaycastAll(new Ray(FirePos.position, this.transform.forward) , 1000 , 1 << LayerMask.NameToLayer("Monster"));
        for(int j = 0; j < raycastHits.Length; j++)
        {
            MonsterObject m = raycastHits[j].collider.gameObject.GetComponent<MonsterObject>();
            if (m != null && !m.isDead)
            {
                //������Ч����
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
