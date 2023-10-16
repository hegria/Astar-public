using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLobby : MonoBehaviour
{
    public void goLobby(){
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(1);
    }
}
