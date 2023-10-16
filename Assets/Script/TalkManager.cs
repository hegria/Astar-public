using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;


[System.Serializable]
public class DialogData{
    public int talker; // 0 아스타 1 마더
    public string dialog;
    public int sprite;
    
    public DialogData(int talker,string dialog,int sprite){
        this.talker = talker;
        this.dialog = dialog;
        this.sprite = sprite;
    }
}

public class TalkManager : MonoBehaviour
{
    public static TalkManager talkManager;
    public int stageindex;
    //stage + 0 시작 1 중간 2 끝
    int dialogindex;
    string localstr;
    int localstridx;
    string localnowtalk;

    public GameObject location;
    public TextMeshProUGUI locationtmp;

    public List<List<DialogData>> wholetalk;

    public GameObject dialogobj;
    public TextMeshProUGUI nametmp;
    public TextMeshProUGUI dialogtmp;
    public Sprite[] charsprite;
    public Sprite[] computersprite;

    public GameObject bustupobj;
    public Image bustupimg;
    public GameObject Computerobj;
    public Image Computerimg;


    public bool nowtalking;

    public int strindex;
    public bool sayingends;
    string talkmsg;
    string nowtalk;
    public GameObject talkerobj;
    public GameObject dialobj;
    public GameObject maskobj;

    public Image maskimg;

    // Start is called before the first frame update
    private void Awake()
    {
        maskobj.SetActive(false);
        talkManager = this;
        localnowtalk = null;
        localstridx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Padeouted(float time){
        maskobj.SetActive(true);
        talkerobj.SetActive(false);
        dialobj.SetActive(false);
        for (int i = 0; i < 50;i++)
        {
            maskimg.color = new Color(0, 0, 0, i / 50f);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(time);
        for (int i = 49; i >= 0;i--)
        {
            maskimg.color = new Color(0, 0, 0, i / 50f);
            yield return null;
        }
        maskobj.SetActive(false);
        sayingends = true;
        talkerobj.SetActive(true);
        dialobj.SetActive(true);
        if (nowtalking)
        {
            nexts();
        }
    }
    public IEnumerator Gamestart(float time,int stagenum){
        Time.timeScale = 0f;
        Character.charact.isboom = true;
        nowtalking =true;
        GameManager.gameManager.UIs.SetActive(false);
        maskobj.SetActive(true);
        dialogobj.SetActive(false);
        switch(PlayerInfo.playerInfo.stage){
            case 1:
                localstr = "<size=50>Stage 1</size>\nMeR-cU-Ry\n<size=60>Planet</size>\n<size=40>4D65+52E1101";
                break;
            case 2:
                localstr = "<size=50>Stage 2</size>\nSAn-D-MaN\n<size=60>Planet</size>\n<size=40>5341+49E1010";
                break;
            case 3:
                localstr = "<size=50>Stage 3</size>\nIC-e-B0-rN\n<size=60>Planet</size>\n<size=40>4943+42E0001";
                locationtmp.color = new Color(0.2f, 0.2f, 0.2f, 1f);
                break;
            case 4:
                localstr = "<size=50>Stage 4</size>\nFiR-e-bA-SE\n<size=60>Planet</size>\n<size=40>4669+71E1000";
                break;
            case 5:
                localstr = "<size=50>Stage 5</size>\n???\n<size=60>Planet?</size>\n<size=40>2.220446049250313E-16";
                break;
        }
        StartCoroutine(Locationsays());
        yield return new WaitForSecondsRealtime(time);
        for (int i = 49; i >= 0;i--)
        {
            maskimg.color = new Color(0, 0, 0, i / 50f);
            Character.charact.transform.Translate(new Vector3(0, 0.07f, 0));
            yield return null;
        }
        maskobj.SetActive(false);
        sayingends = true;
        dialogobj.SetActive(true);
        AwakeSetting(stagenum);
    }

    
    IEnumerator Locationsays(){
        if(localstr[localstridx].Equals('<')){
            while(!localstr[localstridx].Equals('>')){
                localnowtalk += localstr[localstridx++];
            }
            localnowtalk += localstr[localstridx++];
        }
        localnowtalk += localstr[localstridx];
        locationtmp.SetText(localnowtalk);
        localstridx++;
        if(localstridx == localstr.Length){
            yield return new WaitForSecondsRealtime(1.0f);
            location.SetActive(false);
            yield break;
        }else{
            if(localstr[localstridx].Equals(' ')){
                
                yield return new WaitForSecondsRealtime(1f/40);
            }else{
            yield return new WaitForSecondsRealtime(1f/20);
            }
        }
        StartCoroutine(Locationsays());
    }


    public void AwakeSetting(int stagenum){
        //Reads stage data
        dialogobj.SetActive(true);
        stageindex = 0;
        dialogindex = 0;
        string filename = "stagedialog" + stagenum.ToString();
        if(stagenum ==4 && PlayerInfo.playerInfo.level == 1){
            filename += "0";
        }
        string temp = (Resources.Load(filename) as TextAsset).text;
        wholetalk = JsonConvert.DeserializeObject<List<List<DialogData>>>(temp);

        setdialogs();
    }
    public void startdialog(){
        Character.charact.isboom = true;
        nowtalking =true;
        GameManager.gameManager.UIs.SetActive(false);
        dialogobj.SetActive(true);
        setdialogs();
        Time.timeScale = 0f;
    }
    public void endtalkbtn(){
        UIsound.uIsound.Clicked();
        endtalk();
    }
    public void endtalk(){
        if(stageindex == 2){
            if(PlayerInfo.playerInfo.stage==4&&PlayerInfo.playerInfo.level==1){
                PlayerInfo.playerInfo.level--;
                Character.charact.Gameover();
            }
            Character.charact.gonextStage();
            StartCoroutine(stageClear());
        }else{
            if(stageindex == 1 &&PlayerInfo.playerInfo.stage==5){
                GameManager.gameManager.StartCoroutine(GameManager.gameManager.GenFinalBoss());
            }
            GameManager.gameManager.UIs.SetActive(true);

            if (PlayerInfo.playerInfo.Anchorfixed == 1)
            {
                Character.charact.joystick.SetActive(true);
            }
            Character.charact.isboom = false;
        }
        if(stageindex == 0){
            GameManager.gameManager.WeaponBox.SetActive(true);
            GameManager.gameManager.WeaponBox.GetComponent<WeaponBox>().Awakesetting();
        }
        stageindex++;
        dialogindex = 0;
        dialogobj.SetActive(false);
        Time.timeScale = 1f;
        nowtalking = false;
    }
    public void nexts(){
        UIsound.uIsound.Clicked();
        if(GameManager.gameManager.ispaused){
            return;
        }
        if(sayingends){
            if(dialogindex<wholetalk[stageindex].Count-1){
                dialogindex++;
                setdialogs();
            } else{
                endtalk();

            }
        }else{
            StopCoroutine("says");
            strindex = talkmsg.Length-1;
            StartCoroutine(says());
        }
    }
    IEnumerator stageClear(){
        Character.charact.isCleared = true;
        yield return new WaitUntil(() => Character.chartrans.position.y >= Character.ymax+1.0f);
        if(PlayerInfo.playerInfo.stage==5){
            PlayerInfo.playerInfo.Ending();
            yield break;
        }
        PlayerInfo.playerInfo.StageClear();
        
    }
    public void setdialogs(){
        strindex = 0;
        string name = null;
        nowtalk = null;
        sayingends = false;
        talkerobj.SetActive(true);
        bustupobj.SetActive(true);
        Computerobj.SetActive(false);
        bustupimg.color = new Color(1, 1, 1, 1);
        switch(wholetalk[stageindex][dialogindex].talker){
            case 0:
                name = "아스타";
                bustupimg.sprite = charsprite[0];
                break;
            case 1:
                name = "마더";
                bustupimg.sprite = charsprite[1];
                break;
            case 2:
                name = "시스템";
                bustupimg.color = new Color(1, 1, 1, 0);
                break;
            case 3:
                bustupobj.SetActive(false);
                Computerobj.SetActive(true);
                Computerimg.sprite = computersprite[wholetalk[stageindex][dialogindex].sprite];
                name = "컴퓨터";
                break;
            case 4:
                name = "아틀라스";
                bustupimg.sprite = charsprite[2];
                break;
            case 5:
                name = "헤임달";
                bustupimg.sprite = charsprite[3];
                break;
            case 6:
                name = "???";
                bustupimg.sprite = charsprite[3];
                break;
            case -1:
                name = "없음";
                bustupimg.color = new Color(1, 1, 1, 0);
                talkerobj.SetActive(false);
                break;
            case 99:
                name = "암전";
                StartCoroutine(Padeouted(1.0f));
                return;
        } 
        nametmp.SetText(name);
        talkmsg = wholetalk[stageindex][dialogindex].dialog;
        StartCoroutine(says());
    }
    IEnumerator says(){
        if(strindex >= talkmsg.Length-1){
            dialogtmp.SetText(talkmsg);
            sayingends = true;
            yield break;   
        }else{
            if(talkmsg[strindex].Equals('<')){
                while(!talkmsg[strindex].Equals('>')){
                    nowtalk += talkmsg[strindex++];
                }
                nowtalk += talkmsg[strindex++];
            }
            nowtalk += talkmsg[strindex];
            dialogtmp.SetText(nowtalk);
            yield return new WaitForSecondsRealtime(1f/60f);
            strindex++;
            
            StartCoroutine(says());
        }
    }
}

