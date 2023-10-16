using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LobbyUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start() {
        if(UIsound.uIsound.bgmclipnum != 5){
            UIsound.uIsound.stop();
            UIsound.uIsound.inLobby();

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void start(){
        
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(2);
    }
    public void gotoCollection(){
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(7);
    }
    public void gotoWorkShop(){
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(8);
    }
}
