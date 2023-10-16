using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item_Bullet> ItemKind = new List<Item_Bullet>();
    public void setAwake(){
        for(int i  =0;i<transform.childCount;i++){
           Item_Bullet myitem = transform.GetChild(i).GetComponent<Item_Bullet>();
           myitem.ItemIndex = i;
           myitem.SetAwake();
           ItemKind.Add(myitem);
       }
       IvenManager.Myinven.SetAwake();
    }
}
