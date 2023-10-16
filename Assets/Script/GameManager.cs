using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] transforms;
    const int TransHigh = 0;
    const int TransDigonal = 1;
    const int TransHighBet = 2;
    const int TransSide = 3;
    public GameObject[] Boss;
    public int nexttrans;
    public int nextenemy;
    public bool ispaused;
    public int stage;
    int genindex;
    public static GameManager gameManager;
    public GameObject UIs;
    
    public GameObject WeaponBox;
    public string[][] csv;

    public AudioSource mainbgm;
    public AudioSource subbgm;
    public AudioSource sfx;

    public AudioClip[] sounds;
    void Start()
    {
        UIsound.uIsound.stop();
        stage = PlayerInfo.playerInfo.stage;
        genindex = 1;
        csv = CSVReader.Read("stage"+stage.ToString());
        ispaused =false;
        gameManager = this;
        StartCoroutine(TalkManager.talkManager.Gamestart(0, PlayerInfo.playerInfo.stage));
        ObjectManager.objectManager.AwakeSetting();
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bossdead(){
        Character.charact.isboom = true;
        Invoke("RealDead", 3);
    }
    public void RealDead(){
        Character.charact.isboom = false;
        if(TalkManager.talkManager.stageindex ==2){
            Character.charact.isCleared = true;
        }
        TalkManager.talkManager.startdialog();
    }

    public IEnumerator GenFinalBoss(){
        Character.charact.bossdead = false;
        Character.charact.isboom = true;
        GameObject bossa = Instantiate(Boss[stage - 1], transforms[TransHigh].GetChild(0));
        bossa.transform.SetParent(null);
        yield return new WaitForSeconds(1.0f);
        Character.charact.isboom = false;

    }

    IEnumerator Generate(){
        if(genindex == 1 ){
            yield return new WaitForSeconds(3.0f);
        }
        if(genindex == csv.Length){
            
            yield return new WaitForSeconds(4.0f);
            mainbgm.Stop();
            mainbgm.clip = sounds[3];
            mainbgm.volume = 0.8f;
            mainbgm.Play();
            Character.charact.isboom = true;
            yield return new WaitForSeconds(2.0f);
            if(stage == 5){
                Character.charact.isboom = false;
                GameObject bossa = Instantiate(Boss[stage - 2], transforms[TransHigh].GetChild(0));
                bossa.GetComponent<Enemy>().energy /= 2;
                bossa.transform.SetParent(null);
                yield break;
            }
            GameObject boss = Instantiate(Boss[stage - 1], transforms[TransHigh].GetChild(0));
            boss.transform.SetParent(null);
            yield return new WaitForSeconds(1.0f);
            Character.charact.isboom = false;
            Character.charact.joystick.SetActive(false);
            TalkManager.talkManager.startdialog();
            yield break;
        }
        int objindex = 0;
        while(true){
            if(objindex >= csv[genindex].Length||csv[genindex][objindex].Equals("")){
                break;
            }
            switch(int.Parse(csv[genindex][objindex])){
                    case 0:
                        StartCoroutine(genSmall_side(int.Parse(csv[genindex][objindex+1]),int.Parse(csv[genindex][objindex+2])));
                        break;
                    case 1:
                        StartCoroutine(genSmall_stright(int.Parse(csv[genindex][objindex+1]),int.Parse(csv[genindex][objindex+2])));
                        break;
                    case 2:
                        StartCoroutine(genMiddle_Long(int.Parse(csv[genindex][objindex+1]),int.Parse(csv[genindex][objindex+2])));
                        break;
                    case 3:
                        StartCoroutine(genMiddle_Wide(int.Parse(csv[genindex][objindex+1]),int.Parse(csv[genindex][objindex+2])));
                        break;
                    case 4:
                        StartCoroutine(genLarge_Starwars(int.Parse(csv[genindex][objindex+1]),int.Parse(csv[genindex][objindex+2])));
                        break;
                    case 5:
                        StartCoroutine(genLarge_Bird(int.Parse(csv[genindex][objindex+1]),int.Parse(csv[genindex][objindex+2])));
                        break;
                    //7891011
                    case 6:
                        StartCoroutine(genStageMob(int.Parse(csv[genindex][objindex + 1]), int.Parse(csv[genindex][objindex + 2])));
                        break;
            }
            objindex += 3;
        }
        genindex++;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Generate());
    }
   


    //대각이들 만듬 기본적으로 postype 0이면 왼대각 1면 오른 대각
    IEnumerator genSmall_side(int postype,int returntype){
        for (int i = 0;i<4;i++){
            Enemy temp = ObjectManager.GetEnemyObject(1);
            temp.transform.position = transforms[TransDigonal].GetChild(postype).position;
            temp.transform.rotation = transforms[TransDigonal].GetChild(postype).rotation;
            temp.AwakeEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }
    //돌격이, 1이냐 2냐에 따라 위치가 좀 달라짐 0 왼쪽에서 오른쪽으로 , 1 오른쪽에서 왼쪽으로
    IEnumerator genSmall_stright(int postype,int returntype){
        int transindex = postype+1;
        Vector3 gendir = new Vector3();
        if(postype == 0){
            gendir.x = 0.5f;
        }else{
            gendir.x = -0.5f;
        }
        for (int i = 0;i<4;i++){
            Enemy temp;
            if(i==3&&returntype!=0){
                temp = ObjectManager.GetEnemyObject(6);
            }else{
                temp = ObjectManager.GetEnemyObject(0);
            }
            temp.transform.position = transforms[TransHigh].GetChild(transindex).position;
            temp.transform.Translate(gendir*i);
            if(i == 3){
                temp.returntype = returntype;
            }
            temp.AwakeEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }
    // 0 넣으면 3개 중 왼 오, 1넣으면 2개 왼 오
    IEnumerator genMiddle_Long(int postype,int returntype){
        
        for (int i = 0; i < 3; i++)
        {
            Enemy temp = ObjectManager.GetEnemyObject(2);
            temp.transform.position = transforms[TransHigh].GetChild(i).position;
            temp.enemyidx = i;
            if(i == 2){
                temp.returntype = returntype;
            }
            temp.AwakeEnemy();
            yield return new WaitForSeconds(1.0f);
        }


        yield break;
    }

    IEnumerator genMiddle_Wide(int postype,int returntype){

        for(int i=postype;i<3;i++){
            Enemy temp = ObjectManager.GetEnemyObject(3);
            temp.transform.position = transforms[TransHighBet].GetChild(i).position;
            temp.enemyidx = i;
            if(i == 2){
                temp.returntype = returntype;
            }
            temp.AwakeEnemy();
            yield return new WaitForSeconds(1.0f);
        }
        yield break;
    }
    IEnumerator genLarge_Starwars(int postype,int returntype){
        Enemy temp = ObjectManager.GetEnemyObject(4);
        temp.transform.position = transforms[TransHighBet].GetChild(postype).position;
        temp.returntype = returntype;
        temp.AwakeEnemy();
      
        yield return new WaitForSeconds(2.0f);
        yield break;
    }
    
    IEnumerator genLarge_Bird(int postype,int returntype){
        for (int i = 0; i < 2; i++)
        {
            Enemy temp = ObjectManager.GetEnemyObject(5);
            temp.transform.position = transforms[TransHigh].GetChild(i+1).position;
            if(i == 1){
                temp.returntype = returntype;
            }
            temp.enemyidx = i;
            temp.AwakeEnemy();
            yield return new WaitForSeconds(2.0f);
        }
        yield break;

    }
    IEnumerator genStageMob(int postype,int returntype){
        switch(PlayerInfo.playerInfo.stage){
            case 1:
                for (int i = 0; i < 2;i++){
                    Enemy temp = ObjectManager.GetEnemyObject(7);
                    temp.transform.position = transforms[TransSide].GetChild(i).position;
                    temp.enemyidx = i;
                    if(i==1){
                        temp.movdir.x = -temp.movdir.x;
                    }
                    if(i == 1){
                        temp.returntype = returntype;
                    }
                    temp.AwakeEnemy();
                    yield return new WaitForSeconds(2.0f);
                    
                }
                break;
            case 2:
                for (int i = 0; i < 2;i++){
                    Enemy temp = ObjectManager.GetEnemyObject(8);
                    temp.transform.position = transforms[TransHigh].GetChild(i+1).position;
                    temp.enemyidx = i;
                    if(i == 1){
                        temp.returntype = returntype;
                    }
                    temp.AwakeEnemy();
                    yield return new WaitForSeconds(2.0f);
                    
                }
                break;
            case 3:
                for (int i = 0; i < 2;i++){
                    Enemy temp = ObjectManager.GetEnemyObject(9);
                    temp.transform.position = transforms[TransHighBet].GetChild(i+1).position;
                    temp.enemyidx = i;
                    if(i == 1){
                        temp.returntype = returntype;
                    }
                    temp.AwakeEnemy();
                    yield return new WaitForSeconds(2.0f);
                    
                }
                break;
            case 4:
                for (int i = 0; i < 2;i++){
                    Enemy temp = ObjectManager.GetEnemyObject(10);
                    temp.transform.position = transforms[TransSide].GetChild(i).position;
                    temp.enemyidx = i;
                    if(i==1){
                        temp.movdir.x = -temp.movdir.x;
                    }
                    if(i == 1){
                        temp.returntype = returntype;
                    }
                    temp.AwakeEnemy();
                    yield return new WaitForSeconds(2.0f);
                    
                }
                break;
            case 5:
                for (int i = postype; i < 3;i++){
                    Enemy temp = ObjectManager.GetEnemyObject(11);
                    temp.transform.position = transforms[TransHigh].GetChild(i).position;
                    temp.enemyidx = i;
                    if(i == 2){
                        temp.returntype = returntype;
                    }
                    temp.AwakeEnemy();
                    yield return new WaitForSeconds(2.0f);
                    
                }
                break;
        }
        

        yield break;

    }
    public void ouchsfx(){
        sfx.clip = sounds[0];
        sfx.volume = 0.6f;
        sfx.Play();
    }
    public void upfx() {
        sfx.clip = sounds[1];
        sfx.volume = 0.6f;
        sfx.Play();
    }
    public void rotatefx() {
        sfx.clip = sounds[2];
        sfx.volume = 0.8f;
        sfx.Play();
    }

}
