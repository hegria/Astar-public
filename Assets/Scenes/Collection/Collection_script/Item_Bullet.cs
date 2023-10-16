using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour
{
    public int ItemIndex;
    public Sprite BulletImg;
    public string BulletName;
    public Sprite[] SpriteCollection = new Sprite[9];
    public string FlavorText;
    public string ItemInfo;
    public void SetAwake(){
        switch(ItemIndex){
            case 0:
            BulletName = "클래식";
            BulletImg = SpriteCollection[0];
            FlavorText ="Space Time지 선정 우주시대를 대표하는 장비 1위에 선정된 무기입니다.\n아스타는 너무 전형적이라고 싫어하지만 언제나 출전할 때는 이 장비를 장착하는거 보니 억지로 싫어하는 척하는 게 분명합니다.\n우주에는 결국 돌고돌아 클래식이라는 말이 있지요.";
            ItemInfo = "기초탄을 발사합니다";
            break;
            case 1:
            BulletName = "위저드 스태프";
            BulletImg = SpriteCollection[1];
            FlavorText ="'고도의 발전된 기술은 <b>마법</b>과 같다'는 말이 있죠. 그런의미에서 요즘의 무기 기술은 비전공자들이나 소비지자들에겐 마법처럼 보인다고 해도 과언이 아니에요.\n하지만 아직도 이 의문의 마법 스태프의 원리는 수수깨끼라고 합니다. 물리법칙을 무시하는 듯한 곡률로 움직이는 응축된 고 에너지 구체는 그 자체로 <b>마법</b>이라고 볼 수 있겠네요.";
            ItemInfo = "마법을 난사합니다.";
            break;
            case 2:
            BulletName = "닌자의 두루마리";
            BulletImg = SpriteCollection[2];
            FlavorText ="과거 지구의 일본이라는 나라의 암살자를 뜻하는 말인 <i>닌자</i>는 왜 인지 모르게 지구 매니아들 사이에서 큰 인기가 끌었습니다.\n그들은 지구의 현실성 없는 <i>닌자</i> 매체들을 보면서 상상을 펼치다 못해 이런 괴상한 무기를 만들어 버리고 말았죠.\n하지만 괴상한 디자인과 기능에도 불구하고 좋은 성능으로 유명해져 널리 쓰이게 됬다는게 아이러니하네요.";
            ItemInfo = "분열하는 수리검과 나뭇잎 방어막을 제공합니다.";
            break;
            case 3:
            BulletName = "에너미체이서";
            BulletImg = SpriteCollection[3];
            FlavorText ="상대의 열반응을 추적하던 과거의 유도미사일은 돌고 돌아 돌아 샤용자를 추격하기도 했죠.\n 현재의 유도미사일은 다른 원리를 적용하여 이를 해결했다고 하는데...\n적이 없던 곳에서 안절부절하듯이 빙글빙글 도는 걸 보면 완전히 해결이 되었는지는 모르겠군요.";
            ItemInfo = "적을 추격하는 미사일을 발사합니다.";
            break;
            case 4:
            BulletName = "윈체스터";
            BulletImg = SpriteCollection[4];
            FlavorText ="힙스터들의 유토피아 20XX년대 '지구'의 무기중 하나를 모티브로 한 무기입니다.\n'샷건'이라는 호쾌한 개념과 '윈체스터'의 유니크한 디자인은 수많은 매니아들을 매료시켰다고 되어 있네요.\n저번에 아스타의 방에서 윈체스터 모형을 본적이 있던 것 같은데..";
            ItemInfo = "방사탄을 발사합니다.";
            break;
            case 5:
            BulletName = "쯔바이핸더";
            BulletImg = SpriteCollection[5];
            FlavorText ="40XX년도가 된 지금도 <b>냉병기</b>에 환장하는 우주인들이 있습니다.\n하지만 요즘시대에 근접격투라니 자살희망이나 다름없잖아요? 아무래도 냉병기 애호가들도 그걸 인식했는지 '<i>검기</i>'라는 걸 만들어 냈죠.\n근데 이럼 '냉병기'는 허울 뿐인게 아닐까 생각이 드네요.";
            ItemInfo = "검기를 발사합니다.";
            break;
            case 6:
            BulletName = "버블보블";
            BulletImg = SpriteCollection[6];
            FlavorText ="이 매니악한 무기는 어릴적 동심을 자극하는 것 같지만 적등에겐 굉장히 까다로운 병기입니다.\n거품 처럼 보이는 탄은 주위의 에너지를 받아서 점점 커지게 되고, 적에게 닿아 터지게 되면 그 에너지가 잔류하면서 계속 피해를 입히게 되죠.\n이걸 만든 제작자와는 평생 마주치지 않았으면 하네요.";
            ItemInfo = "점점 커지는 거품을 발사합니다.";
            break;
            case 7:
            BulletName = "M134 미니건";
            BulletImg = SpriteCollection[7];
            FlavorText ="힙스터들의 유토피아 20XX년대 '지구'의 무기중 하나를 모티브로 한 무기입니다.\n큰 총신에서 수많은 총알이 나가는 걸 보면 스트레스 해소에 좋다고 하네요.\n 하지만, 총도 총알도 절대 작지 않은데 왜 이름이 '미니건'인지는 의문이 가네요.";
            ItemInfo = "총알을 무작위로 난사합니다.";
            break;
            case 8:
            BulletName = "레이저 블레스터";
            BulletImg = SpriteCollection[8];
            FlavorText ="<i>'오 레이저 나의 마음을 꽤뚫는 구나!' - 한 레이저 애호가의 노래</i>\n무기 애호가 중에는 변태가 참 많다고 하지만 그 중 레이저 애호가는 그 궤를 달리하는 것으로 유명하죠.\n이들을 만난다면 왠만해선 레이저에 대해선 말하지 말도록 하세요. 저번에 아스타가 레이저 너비에 대해서 한마디 하고, 3시간동안 설교를 들었던 적이 있었거든요.";
            ItemInfo = "고출력 레이저를 방출합니다.";
            break;
        }
    }
}


