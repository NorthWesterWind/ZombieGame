using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //Ŀ���
    public Transform targetpos;
    //��������Ŀ��� ��xyz�ϵ�ƫ����
    public Vector3 offsetPos;
    //ƫ����
    public float bodyHeight;
    //�ƶ�����ת�ٶ�
    public float moveSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetpos == null)
            return;   

        //���������Ҫ�����Ŀ���  ������������λ��
        Vector3 targetPos = targetpos.position + targetpos.forward * offsetPos.z;
        targetPos +=Vector3.up * offsetPos.y;
        targetPos += targetpos.right * offsetPos.x;

        //ʹ�ò�ֵ���� �ɿ쵽��
       this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

       //��ת����
       //�õ�����Ҫ����ĳ����ʱ����Ԫ��
       Quaternion targetRotation = Quaternion.LookRotation(targetpos.position + Vector3.up * bodyHeight  - this.transform.position);
       
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// ��������������Ŀ���
    /// </summary>
    /// <param name="pos"></param>
    public void SetTarget(Transform pos)
    {
        targetpos = pos;
    }

}
