using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WorkshopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI gold;
    public GameObject selected;
    public GameObject notslected;
    public static WorkshopManager workshop;

    ShopItemInfo curiteminfo;
    public GameObject Popup;

    public TextMeshProUGUI itemname;
    public TextMeshProUGUI flavortext;
    public TextMeshProUGUI currenteffect;
    public TextMeshProUGUI nexteffect;
    public TextMeshProUGUI nextgold;


    //버튼 무효화
    public int curindex;

    public void okay(){
        UIsound.uIsound.Clicked();
        Popup.SetActive(false);
    }
    public void gotoLobby(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.gotoLobby();
    }
    public void Upgrade(){
        if(curiteminfo.curlevel>=5){
            return;

        }
        if(PlayerInfo.playerInfo.gold<curiteminfo.cost[curiteminfo.curlevel]){
            UIsound.uIsound.Clicked();
            Popup.SetActive(true);
        }else{
            UIsound.uIsound.upgraded();
            PlayerInfo.playerInfo.gold -= curiteminfo.cost[curiteminfo.curlevel];
            curiteminfo.curlevel++;
            PlayerInfo.playerInfo.workshop[curindex]++;
            clicked();
            PlayerInfo.playerInfo.settlement();
        }
        //팝업작업
    }
    public void clicked(){
        curiteminfo = ShopItemManager.workmanager.shopItems[curindex];
        itemname.SetText(curiteminfo.itemname);
        //TODO 이미지작업
        flavortext.SetText(curiteminfo.flavortext);
        currenteffect.SetText(curiteminfo.upgradetext + curiteminfo.effects[curiteminfo.curlevel].ToString() + curiteminfo.followingtext);
        if(curiteminfo.curlevel>=5){
            nexteffect.SetText("최고 단계 입니다.");
            nextgold.SetText("0");
        }else{
            nexteffect.SetText(curiteminfo.upgradetext + curiteminfo.effects[curiteminfo.curlevel+1].ToString() + curiteminfo.followingtext);
            nextgold.SetText(curiteminfo.cost[curiteminfo.curlevel].ToString());
        }
        
        selected.SetActive(true);
        notslected.SetActive(false);
    }

    void Start()
    {
        workshop = this;
        selected.SetActive(false);
        notslected.SetActive(true);
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        gold.SetText(string.Format("{0}G",PlayerInfo.playerInfo.gold));
    }
}
