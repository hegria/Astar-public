using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{
    public int index;
    public int curlevel;
    public string itemname;
    public string flavortext;
    public string upgradetext;
    public string followingtext;
    public int[] cost;
    public float[] effects;
    public Sprite iconsprite;
    public Image iconimg;

    public GameObject[] curlevels;


    // Start is called before the first frame update
    public void AwakeSetting(){
        switch(index){
            case 1:
                itemname = "선체강화";
                flavortext = " <i>하체가 든든해야 상체가 산다. - 어느 내구도 마니아</i>\n아스타 같이 즉흥 곡예를 좋아하는 막무가내들에게 반드시 기체에 신경을 써야하죠. 과거 지구에서는 이를 <i>국밥</i>이라고 칭하더군요.\n흐음.. 왜인지 이유에 대해서는 데이터가 사라져있네요...";
                followingtext = "";
                upgradetext = "시작 라이프 수 +";
                cost = new int[] { 100, 500, 2500, 12500, 50000 };
                effects = new float[] { 0f,1f, 2f, 3f, 4f, 5f };
                break;
            case 2:
                //폭탄 지속시간
                itemname = "폭탄강화";
                flavortext = "<i>예술은 예술이다! - 폭파광 미치광이</i>\n폭탄은 폭발 했을 때의 엄청난 화력으로 강력한 한방으로'만' 생각되지만, 또한 폭발 할 때 주위의 탄들을 모두 지워버려 생존을 도모하는데에도 큰 공헌을 하죠. 뭐, 아스타는 그런 건 신경안쓰는 것 같지만요.";
                upgradetext = "폭탄 지속시간 증가 +";
                followingtext = "초";
                cost = new int[] { 100, 500, 2500, 12500, 50000 };
                effects = new float[] { 0f,0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
                break;
            case 3:
                itemname = "곡예강화";
                flavortext = "<i>짜릿해, 늘 새로워, 도는게 최고야 - 곡예비행 전문가, 베럴롤러</i>\n아슬아슬한 비행으로 여러 장애물이나 적들의 탄알을 피해내는 곡예 비행은 전투기 조종사들에겐 필수 소양이라고 볼 수 있을 겁니다.\n특히 저희같이 작은 우주선은 더욱요. 하지만 요리조리 피하다가 상대방이 약올라 폭탄을 발사하게 하지 않도록 주의하세요.";
                upgradetext = "다음 플립 쿨타임 -";
                followingtext = "초";
                cost = new int[] { 500, 1500, 7500, 25000, 100000 };
                effects = new float[] { 0f,0.3f, 0.6f, 0.9f, 1.2f, 1.5f };
                break;
            case 4:
                itemname = "출력강화";
                flavortext = "<i>나는 무적이고 화력은 신이다! - 공방의 화력신봉자</i>\n과거 지구에는 공격이 최선의 방어라는 말이 있었다고 합니다. 적이 쏘기전에 먼저 터트린다는 말은 매우 무식해보이지만, 그렇다고 틀린말은 절대 아니죠. 하지만 출력이 늘어난다 해도, 맞추지 않으면 소용없다는 사실을 잊지 마세요.";
                upgradetext = "데미지 증가율 *";
                followingtext = "배";
                cost = new int[] { 500, 1500, 7500, 25000, 100000 };
                effects = new float[] { 1.0f,1.1f, 1.2f, 1.3f, 1.4f, 1.5f };
                break;
        }
        curlevel = PlayerInfo.playerInfo.workshop[index - 1];
        setIMGS();
    }

    public void isclicked(){
        UIsound.uIsound.Clicked();
        WorkshopManager.workshop.curindex = index-1;
        WorkshopManager.workshop.clicked();
    }
    public void setIMGS(){
        for (int i = 0; i < curlevel;i++){
            curlevels[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        curlevel = PlayerInfo.playerInfo.workshop[index - 1];
        setIMGS();
        
        
    }
}
