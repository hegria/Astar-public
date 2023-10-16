using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartEasygame(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.level = 1;
        PlayerInfo.playerInfo.setgame();
        SceneManager.LoadScene(4);
    }
    
    public void StartNormalgame(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.level = 2;
        PlayerInfo.playerInfo.setgame();
        SceneManager.LoadScene(4);
    }
    public void StartHardgame(){
        UIsound.uIsound.Clicked();
        PlayerInfo.playerInfo.level = 3;
        PlayerInfo.playerInfo.setgame();
        SceneManager.LoadScene(4);
    }
    public void synopsis(){
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(3);
    }
    public void back(){
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(1);
    }
}
