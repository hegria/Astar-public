using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool SlotOpen;
    public Item_Bullet myBullet;
    public void SetAwake(){
        transform.GetChild(0).GetComponent<Image>().sprite = myBullet.BulletImg;
    }
    public void clickitem(){
        IvenManager.Myinven.Clickthis(this);
    }
}
