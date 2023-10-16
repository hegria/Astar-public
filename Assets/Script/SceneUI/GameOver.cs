using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI score;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI Highscoretmpro;
    public GameObject Highscore;
    public bool onupdate = true;
    public GameObject NewRecord;
    public GameObject Popup;
    void Start()
    {
        if(PlayerInfo.playerInfo.stage == 5){
            UIsound.uIsound.StageClear();
        }else{
            UIsound.uIsound.GameOver();
        }
        Popup.SetActive(false);
        if(PlayerInfo.playerInfo.level == 0){
            Popup.SetActive(true);
        }
        if(PlayerInfo.playerInfo.HighScore<=PlayerInfo.playerInfo.curscore){
            PlayerInfo.playerInfo.HighScore = PlayerInfo.playerInfo.curscore;
            NewRecord.SetActive(true);
        }
        else{
            Highscore.SetActive(true);
            Highscoretmpro.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.HighScore));
        }
        score.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curscore));
        gold.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curgold));
    }

    // Update is called once per frame
    void Update()
    {
        if(onupdate){

            Highscoretmpro.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.HighScore));
            score.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curscore));
            gold.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curgold));
        }
    }
    public void okay(){
        UIsound.uIsound.Clicked();
        Popup.SetActive(false);
    }
    public void regame(){
        UIsound.uIsound.Clicked();
        onupdate = false;
        PlayerInfo.playerInfo.settlement();
        PlayerInfo.playerInfo.setgame();
        SceneManager.LoadScene(4);
    }
    public void gotoRobby(){
        UIsound.uIsound.Clicked();
        onupdate = false;
        PlayerInfo.playerInfo.settlement();
        PlayerInfo.playerInfo.gotoLobby();
    }
}
