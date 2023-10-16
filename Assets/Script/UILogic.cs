using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    
    public TextMeshProUGUI ScoreTMP;
    public TextMeshProUGUI GoldTMP;
    public Image[] lifeimage;
    public Image[] boomimage;

    public Image[] UIMask;

    public TextMeshProUGUI stageTMP;
    public GameObject pauseimage;

    public GameObject checkmark;
    public GameObject tips;

    public Image cursorimg;

    public GameObject popup;

    public void gotoLobby(){
        UIsound.uIsound.Clicked();
        popup.SetActive(true);
    }
    public void Okay(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.gotoLobby();
    }
    public void NO(){
        UIsound.uIsound.Clicked();
        popup.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        life();
        boom();
        if(PlayerInfo.playerInfo.Anchorfixed == 0){
            Character.charact.joystick.SetActive(false);
            checkmark.SetActive(false);
            tips.SetActive(true);
        }else{
            Character.charact.joystick.SetActive(true);
            checkmark.SetActive(true);
            tips.SetActive(false);
        }
        stageTMP.SetText("Stage " + PlayerInfo.playerInfo.stage.ToString());
        if(PlayerInfo.playerInfo.stage >=4){
            foreach(Image img in UIMask){
                img.color = new Color(1, 1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GoldTMP.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curgold));
        ScoreTMP.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curscore));
        
        life();
        boom();
    }
    public void setAncher(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.setAnchered();
        
        if(PlayerInfo.playerInfo.Anchorfixed == 0){
            Character.charact.joystick.SetActive(false);
            checkmark.SetActive(false);
            tips.SetActive(true);
        }else{
            Character.charact.joystick.SetActive(true);
            checkmark.SetActive(true);
            tips.SetActive(false);
        }
    }

    public void pause(){
        UIsound.uIsound.Clicked();
        if(PlayerInfo.playerInfo.Anchorfixed == 0){
            checkmark.SetActive(false);
            tips.SetActive(true);
        }else{
            checkmark.SetActive(true);
            tips.SetActive(false);
        }
        if(!GameManager.gameManager.ispaused){
            GameManager.gameManager.ispaused = true;
            pauseimage.SetActive(true);
            Time.timeScale = 0f;
    
        }else{
            GameManager.gameManager.ispaused = false;
            pauseimage.SetActive(false);
            popup.SetActive(false);
            if(TalkManager.talkManager.nowtalking){
                return;
            }
            Time.timeScale =1f;
    
        }
    }
    public void life(){
        for (int i = 0; i <7; i++){
            lifeimage[i].color = new Color(1,1,1,0);
        }
        for (int i = 0; i<Character.charact.life;i++){

            lifeimage[i].color = new Color(1,1,1,1);
        }
    }
    public void boom(){
        for (int i = 0; i <7; i++){
            boomimage[i].color = new Color(1,1,1,0);
        }
        for (int i = 0; i<Character.charact.boomnum;i++){

            boomimage[i].color = new Color(1,1,1,1);
        }
    }
}
