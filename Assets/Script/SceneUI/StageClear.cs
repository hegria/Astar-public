using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageClear : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI curgold;
    
    public TextMeshProUGUI curscore;
    bool isend = false;
    void Start()
    {
        UIsound.uIsound.gameObject.SetActive(true);
        UIsound.uIsound.StageClear();
        curscore.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curscore));
        curgold.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curgold));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isend){
            curscore.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curscore));
            curgold.SetText(string.Format("{0:n0}",PlayerInfo.playerInfo.curgold));
        }
    }
    public void nextstage(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.NextStage();
    }

    public void gotoLobby(){
        UIsound.uIsound.Clicked();
        isend = true;
        PlayerInfo.playerInfo.gotoLobby();
    }
}
