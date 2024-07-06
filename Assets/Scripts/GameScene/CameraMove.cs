using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //目标点
    public Transform targetpos;
    //摄像机相对目标点 在xyz上的偏移量
    public Vector3 offsetPos;
    //偏移量
    public float bodyHeight;
    //移动和旋转速度
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

        //根据摄像机要看向的目标点  计算出摄像机的位置
        Vector3 targetPos = targetpos.position + targetpos.forward * offsetPos.z;
        targetPos +=Vector3.up * offsetPos.y;
        targetPos += targetpos.right * offsetPos.x;

        //使用插值运算 由快到慢
       this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

       //旋转计算
       //得到最终要看向某个点时得四元数
       Quaternion targetRotation = Quaternion.LookRotation(targetpos.position + Vector3.up * bodyHeight  - this.transform.position);
       
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 设置摄像机看向的目标点
    /// </summary>
    /// <param name="pos"></param>
    public void SetTarget(Transform pos)
    {
        targetpos = pos;
    }

}
