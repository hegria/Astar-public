using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IvenManager : MonoBehaviour
{
    public static IvenManager Myinven;
    public ItemManager Im;
    private void Awake() {
        Myinven = this;
        Im.setAwake();
    }
    public void SetAwake(){
        for(int i = 0; i< Im.ItemKind.Count;i++){
            Item_Bullet target = Im.ItemKind[i];
            GameObject slotobj = transform.GetChild(i).gameObject;
            slotobj.transform.localScale = Vector3.one;
            ItemSlot myslot = slotobj.GetComponent<ItemSlot>();
            myslot.myBullet = target;
            myslot.SetAwake();
        }
    }
    public GameObject Site;
    public Image SiteImg;
    public TextMeshProUGUI SiteFlavor;
    public TextMeshProUGUI SiteInfo;
    public TextMeshProUGUI SiteName;
    public RectTransform rect;
    public ItemSlot AfterSlot;
    public void Clickthis(ItemSlot myslot){
        UIsound.uIsound.Clicked();
        SiteImg.sprite = myslot.myBullet.BulletImg;
        rect.localPosition = new Vector3(rect.localPosition.x,-250);
        SiteFlavor.text = string.Format("<b>{0}", myslot.myBullet.FlavorText);
        SiteInfo.text = string.Format("<b>{0}",myslot.myBullet.ItemInfo);
        SiteName.text = string.Format("<b>{0}",myslot.myBullet.BulletName);
        Site.SetActive(true);

        AfterSlot = myslot;
    }
}
[System.Serializable]
public class LockBullet{
    public List<int> CheckLock = new List<int>(new int[]{0,0,0,0,0,0,0,0,0});
}
