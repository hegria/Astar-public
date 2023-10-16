using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ShopItemManager workmanager;
    public List<ShopItemInfo> shopItems = new List<ShopItemInfo>();
void Start()
    {
        workmanager = this;
        for (int i = 0; i < 4;i++){
            ShopItemInfo curitem = transform.GetChild(i).GetComponent<ShopItemInfo>();
            curitem.index = i+1;
            curitem.AwakeSetting();
            shopItems.Add(curitem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
