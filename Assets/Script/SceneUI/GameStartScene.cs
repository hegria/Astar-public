using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScene : MonoBehaviour
{
    public GameObject Credit;
    public GameObject Creditbtn;

    public void crediton(){
        UIsound.uIsound.Clicked();
        Credit.SetActive(true);
        Creditbtn.SetActive(false);
    }
    public void creditoff(){
        UIsound.uIsound.Clicked();
        Credit.SetActive(false);
        Creditbtn.SetActive(true);
    }
    // Start is called before the first frame update
    private void Awake() {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1080, 1920, true);
    }


    void Start()
    {
             
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void start(){
        UIsound.uIsound.Clicked();
        SceneManager.LoadScene(1);
    }
}
