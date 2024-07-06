using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家数据类
/// </summary>
public class PlayerData 
{
    //玩家拥有金币数
    public int Money;

    //记录已解锁英雄ID
    public List<int> buyheroList =  new List<int>() { 1 , 2};
}
