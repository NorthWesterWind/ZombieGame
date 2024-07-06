using System.Collections.Generic;
using UnityEngine;

public  class UIManager 
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;  

    //保持Canvas唯一性
    private Transform canvas;

    //面板容器
    public Dictionary<string ,BasePanel> panelDic = new Dictionary<string ,BasePanel>();

    
    public UIManager()
    {
        
        Canvas canvastemp = GameObject.Instantiate(Resources.Load<Canvas>("UI/Canvas"));
        this.canvas = canvastemp.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }

    /// <summary>
    /// 显示面板
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
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="delete"></param>
    public void HidePanel<T>(bool delete = false)
    {
        if (panelDic.ContainsKey(typeof(T).Name))
        {
            if (!delete) //需要显隐效果
            {
                GameObject.Destroy(panelDic[typeof(T).Name].gameObject);
                panelDic.Remove(typeof(T).Name);
            }
            else        //不需要显隐效果
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
    /// 从容器中获取面板对象
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
