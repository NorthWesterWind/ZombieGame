using System.Collections.Generic;
using UnityEngine;

public  class UIManager 
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;  

    //����CanvasΨһ��
    private Transform canvas;

    //�������
    public Dictionary<string ,BasePanel> panelDic = new Dictionary<string ,BasePanel>();

    
    public UIManager()
    {
        
        Canvas canvastemp = GameObject.Instantiate(Resources.Load<Canvas>("UI/Canvas"));
        this.canvas = canvastemp.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T ShowPanel<T>() where T : BasePanel
    {
       if(panelDic.ContainsKey(typeof(T).Name))
            return (T)panelDic[typeof(T).Name];
       GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/Panel/" + typeof(T).Name));
       obj.transform.SetParent(canvas,false);
       T panel = obj.GetComponent<T>();
       panelDic.Add(typeof(T).Name, panel);
       panel.Show();
       return panel;
       
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="delete"></param>
    public void HidePanel<T>(bool delete = false)
    {
        if (panelDic.ContainsKey(typeof(T).Name))
        {
            if (!delete) //��Ҫ����Ч��
            {
                GameObject.Destroy(panelDic[typeof(T).Name].gameObject);
                panelDic.Remove(typeof(T).Name);
            }
            else        //����Ҫ����Ч��
            {
                panelDic[typeof(T).Name].Hide(() =>
                {

                    GameObject.Destroy(panelDic[typeof(T).Name].gameObject);
                    panelDic.Remove(typeof(T).Name);
                });
            }
           
           
        }
       
    }
    /// <summary>
    /// �������л�ȡ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        else
            return null;
    }
}
