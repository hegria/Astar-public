using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInfo playerInfo;
    [System.Serializable]
    public class UserInfo{
        public int HighScore;
        public int gold;
        public int[] workshop;
    }

    public int Anchorfixed;

    public int HighScore;
    public int gold;
    //강화내역(Array를 통해)
    //ingame
    public int curscore;
    public int curgold;
    public int level;// 1 : easy 2 : normal 3 : hard
    public int stage;

    public int ingamelife;
    public int ingamebomb;
    public int power;

    public void setgame(){
        weaponnum = 0;
        power = 1;
        ingamelife = 2 + PlayerInfo.playerInfo.workshop[0];
        ingamebomb = 3;
    }

    public int[] workshop;
    public int weaponnum;

    void Awake()
    {
        weaponnum = 0;
        if(playerInfo == null){
            playerInfo = this;
        } else{
            Destroy(gameObject);
        }
        if(File.Exists(Application.persistentDataPath+"Mydata.json")){
            string temp = File.ReadAllText(Application.persistentDataPath + "Mydata.json");
            UserInfo info = JsonConvert.DeserializeObject<UserInfo>(temp);
            HighScore = info.HighScore;
            gold = info.gold;
            workshop = info.workshop;
        } else{
            workshop = new int[4];
            gold = 0;
            HighScore = 0;
            Write();
        }
        if(PlayerPrefs.HasKey("Anchored")){
            Anchorfixed = PlayerPrefs.GetInt("Anchored");
        } else{
            Anchorfixed = 0;
            PlayerPrefs.SetInt("Anchored", Anchorfixed);
        }        
        
        PlayerInfo.playerInfo.curgold = 0;
        PlayerInfo.playerInfo.curscore = 0;
        DontDestroyOnLoad(this);
    }
    public void setAnchered(){
        if(Anchorfixed ==0)
            Anchorfixed = 1;
        else
            Anchorfixed = 0;
        PlayerPrefs.SetInt("Anchored", Anchorfixed);
    }
    public void Write(){
        UserInfo data = new UserInfo();
        data.HighScore = HighScore;
        data.gold = gold;
        data.workshop = workshop;
        string a = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + "Mydata.json", a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //점수 반영 (게임 종료씬이나 꺼버리는 경우)
    public void settlement(){
        gold += curgold;
        curgold = 0;
        curscore = 0;
        stage = 1;
        Write();
    }
    public void StageClear(){
        SceneManager.LoadScene(6);
    }
    public void NextStage(){
        power = Character.charact.power;
        stage++;
        SceneManager.LoadScene(4);
    }
    public void gotoLobby(){
        settlement();
        SceneManager.LoadScene(1);
    }
    public void Ending(){
        SceneManager.LoadScene(9);
    }
}
