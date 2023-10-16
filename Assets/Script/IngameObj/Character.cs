using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{

    //Order in layer 정리 3 이상으로
    //이동관련
    private Vector3 fp;
    private Vector3 lp;
    public static Character charact;
    public static Transform chartrans;
    public static Transform bullettrans;
    public const float xmax = 2.9f;
    public const float ymax = 5.0f;
    //스테이지 변수(탄 제어)
    public bool isCleared;

    public bool bossdead = false;
    //폭탄 제어
    public bool isboom;
    public int boomnum;
    public int power;
    public bool isflip;
    public bool immortal;

    public bool buttonclicked;
    public float fliptime;
    public float bombtime;
    public int life;
    public UILogic ui;



    public GameObject boomobj;
    public GameObject weapons;
    public GameObject weaponons;
    public bool isBarrier;
    
    public GameObject Boom_Img,Filp_img;

    public GameObject joystick;
    RectTransform joysticrect;
    RectTransform joyhandlerect;
    Animator char_animation;
    SpriteRenderer char_spriteR;

    public void gonextStage(){
        PlayerInfo.playerInfo.ingamelife = life;
        PlayerInfo.playerInfo.ingamebomb = boomnum;
    }

    private void Start() {
        charact = this;
        chartrans = this.transform;
        isflip = false;
        isboom = false;
        life = PlayerInfo.playerInfo.ingamelife;
        boomnum = PlayerInfo.playerInfo.ingamebomb;
        power = PlayerInfo.playerInfo.power;
        fliptime = 0.75f;

        joysticrect = joystick.GetComponent<RectTransform>();
        joyhandlerect = joystick.transform.GetChild(0).GetComponent<RectTransform>();
        char_animation = gameObject.GetComponent<Animator>();
        char_spriteR = gameObject.GetComponent<SpriteRenderer>();
        bombtime = 1.0f + PlayerInfo.playerInfo.workshop[1] * 0.1f;
        Character.charact.get_weapon(true);

    }


    public void boom(){
        if(!isboom&& boomnum >0&&Boom_Img.GetComponent<Image>().fillAmount == 1){
            StartCoroutine(boomcor());
        }
    }
    public void flip(){
        if(!isflip&&Filp_img.GetComponent<Image>().fillAmount == 1){
            char_animation.SetTrigger("Flip");
            GameManager.gameManager.rotatefx();
            StartCoroutine(flipcor());
        }
    }

    IEnumerator flipcor(){
        isflip = true;
        immortal = true;
        StartCoroutine(fill_up(Filp_img.GetComponent<Image>(),5f-0.3f*PlayerInfo.playerInfo.workshop[2]));
        yield return new WaitForSeconds(fliptime);
        isflip = false;
        immortal = false;
    }

    IEnumerator boomcor(){
        boomnum--;
        ui.boom();
        isboom = true;
        immortal = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(fill_up(Boom_Img.GetComponent<Image>(),2.0f + PlayerInfo.playerInfo.workshop[1] * 0.1f));
        Vector3 tempvec = transform.position;
        GameObject mine = Instantiate(boomobj,tempvec,transform.rotation);
        mine.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12, ForceMode2D.Impulse);
        mine.GetComponent<Bomb_Start>().bombtime = bombtime;
        yield return new WaitForSeconds(bombtime);
        isboom = false;
        immortal = false;
    }
    IEnumerator fill_up(Image ButtonImg,float cool)
    {
        string mytext = ButtonImg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        float a = cool;
        ButtonImg.fillAmount = 0;
        while(cool >=0)
        {
            cool -= Time.deltaTime;
            ButtonImg.fillAmount = (a-cool)/a;
            if(cool > 1f)
            {
                ButtonImg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0:F0}", cool);
            }
            else
            {
                ButtonImg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0:F1}", cool);
            }
            yield return new WaitForFixedUpdate();
        }
        ButtonImg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("<b>{0}", mytext);
    }
    IEnumerator hit(){
        GameManager.gameManager.ouchsfx();
        life--;
        boomnum = 3;
        if(power>1){
            power--;
            isBarrier = true;
        }
        immortal = true;
        for (int i = 0; i < 5;i++){
            if(i%2==0){
                char_spriteR.color = new Color(1, 1, 1, 0.5f);
            }else{
                char_spriteR.color = new Color(1, 1, 1, 1);
            }
            yield return new WaitForSeconds(0.1f);
        }
        char_spriteR.color = new Color(1, 1, 1, 1);
        if(!isflip&&!isboom){
            immortal = false;
        }
        
    }
    Vector3 go;
    Vector2 goHand;
    bool nowstart = false;

    // Update is called once per frame
    void Update()
    {
        if(PlayerInfo.playerInfo.Anchorfixed == 0){
            if (Input.touchCount == 1 && !TalkManager.talkManager.nowtalking && !isCleared)
            {
            Touch touch = Input.GetTouch(0);
            //Unity 창에서 시험을 못해봄 개 ㅄ색기
            switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                        {
                            nowstart = true;
                            joysticrect.anchoredPosition = touch.position;
                            joystick.SetActive(true);
                            fp = touch.position;
                            lp = touch.position;
                        }
                        break;
                    case TouchPhase.Moved:
                        if (nowstart)
                        {

                            lp = touch.position;
                            go = lp - fp;
                            goHand = lp - fp;
                            if (goHand.magnitude >= 200)
                            {
                                goHand = goHand.normalized * 200;
                            }

                            if (transform.position.x >= xmax && go.x >= 0)
                            {
                                go.x = 0f;
                            }
                            else if (transform.position.x <= -xmax && go.x <= 0)
                            {
                                go.x = 0f;
                            }
                            if (transform.position.y >= ymax && go.y >= 0)
                            {
                                go.y = 0f;
                            }
                            else if (transform.position.y <= -ymax && go.y <= 0)
                            {
                                go.y = 0f;
                            }
                            if (go.magnitude > 100)
                            {

                                transform.Translate(5 * Time.deltaTime * go.normalized);
                            }
                            else
                            {
                                transform.Translate(2.5f * Time.deltaTime * go.normalized);
                            }
                            if (goHand.x == 0)
                            {
                                char_animation.SetBool("Tilled", false);
                            }
                            else
                            {
                                char_animation.SetBool("Tilled", true);
                            }
                            if (goHand.x > 0)
                            {
                                char_spriteR.flipX = true;
                            }
                            else
                            {
                                char_spriteR.flipX = false;
                            }
                            joyhandlerect.anchoredPosition = goHand;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (nowstart)
                        {
                            lp = touch.position;
                            fp = touch.position;
                            joystick.SetActive(false);
                            char_animation.SetBool("Tilled", false);
                            nowstart = false;
                        }

                        break;
                }
           
            }
        }else{
            if(!TalkManager.talkManager.nowtalking && !isCleared){
                joysticrect.anchoredPosition = new Vector2(783, 340);
                if(Input.touchCount==1){
                    Touch touch = Input.GetTouch(0);
                    //Unity 창에서 시험을 못해봄 개 ㅄ색기
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                            {
                                nowstart = true;
                                fp = new Vector3(783, 340);
                                lp = touch.position;
                                Debug.Log(lp);
                                go = lp - fp;
                                goHand = lp - fp;
                                if (goHand.magnitude >= 200)
                                {
                                    goHand = goHand.normalized * 200;
                                }

                                if (transform.position.x >= xmax && go.x >= 0)
                                {
                                    go.x = 0f;
                                }
                                else if (transform.position.x <= -xmax && go.x <= 0)
                                {
                                    go.x = 0f;
                                }
                                if (transform.position.y >= ymax && go.y >= 0)
                                {
                                    go.y = 0f;
                                }
                                else if (transform.position.y <= -ymax && go.y <= 0)
                                {
                                    go.y = 0f;
                                }
                                if (go.magnitude > 100)
                                {

                                    transform.Translate(5 * Time.deltaTime * go.normalized);
                                }
                                else
                                {
                                    transform.Translate(2.5f * Time.deltaTime * go.normalized);
                                }
                                if (goHand.x == 0)
                                {
                                    char_animation.SetBool("Tilled", false);
                                }
                                else
                                {
                                    char_animation.SetBool("Tilled", true);
                                }
                                if (goHand.x > 0)
                                {
                                    char_spriteR.flipX = true;
                                }
                                else
                                {
                                    char_spriteR.flipX = false;
                                }
                                joyhandlerect.anchoredPosition = goHand;
                            }
                            break;
                        case TouchPhase.Moved:
                        if (nowstart)
                        {

                            lp = touch.position;
                            go = lp - fp;
                            goHand = lp - fp;
                            Debug.Log(lp);
                            if (goHand.magnitude >= 200)
                            {
                                goHand = goHand.normalized * 200;
                            }

                            if (transform.position.x >= xmax && go.x >= 0)
                            {
                                go.x = 0f;
                            }
                            else if (transform.position.x <= -xmax && go.x <= 0)
                            {
                                go.x = 0f;
                            }
                            if (transform.position.y >= ymax && go.y >= 0)
                            {
                                go.y = 0f;
                            }
                            else if (transform.position.y <= -ymax && go.y <= 0)
                            {
                                go.y = 0f;
                            }
                            if (go.magnitude > 100)
                            {

                                transform.Translate(5 * Time.deltaTime * go.normalized);
                            }
                            else
                            {
                                transform.Translate(2.5f * Time.deltaTime * go.normalized);
                            }
                            if (goHand.x == 0)
                            {
                                char_animation.SetBool("Tilled", false);
                            }
                            else
                            {
                                char_animation.SetBool("Tilled", true);
                            }
                            if (goHand.x > 0)
                            {
                                char_spriteR.flipX = true;
                            }
                            else
                            {
                                char_spriteR.flipX = false;
                            }
                            joyhandlerect.anchoredPosition = goHand;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (nowstart)
                        {
                            lp = touch.position;
                            fp = touch.position;
                            char_animation.SetBool("Tilled", false);
                            nowstart = false;
                            joyhandlerect.anchoredPosition = new Vector2(0, 0);
                        }

                        break;
                }
           
                }
            }
        }
        


        if(life == 0){
            Gameover();
        }
        if(isCleared){
            if(PlayerInfo.playerInfo.stage == 4){
                transform.Translate(new Vector3(Random.Range(-1.0f,1.0f), 1.0f, 0) * Time.deltaTime * 5);
            } else{
                transform.Translate(new Vector3(0, 1.0f, 0) * Time.deltaTime * 5);
            }
        }
    }
    public void Gameover(){
        PlayerInfo.playerInfo.weaponnum = 0;
        SceneManager.LoadScene(5);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!immortal)
        {
            if (other.gameObject.CompareTag("E_bullet"))
            {
                StartCoroutine(hit());
                ui.life();
            }
            if(other.gameObject.CompareTag("Enemy")){
                
                StartCoroutine(hit());
                ui.life();
            }
        }
        if(other.gameObject.CompareTag("Lifeup")&&life<7){
            
            GameManager.gameManager.upfx();
            life++;
            ui.life();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Boomup")&&boomnum<7){
            GameManager.gameManager.upfx();
            boomnum++;
            ui.boom();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Powerup")&&power<5){
            GameManager.gameManager.upfx();
            power++;
            isBarrier = true;
            Destroy(other.gameObject);
        }
    }

    public void get_weapon(bool isinit){
        if(!isinit){
            Destroy(Character.chartrans.GetChild(1).gameObject);
            Character.chartrans.GetChild(2).gameObject.GetComponent<Weapon>().goodbye();
        }
        GameObject weapon = Instantiate(weapons.transform.GetChild(PlayerInfo.playerInfo.weaponnum).gameObject,Character.chartrans);
        weapon.transform.Translate(new Vector3(0, 1.5f));
        Instantiate(weaponons.transform.GetChild(PlayerInfo.playerInfo.weaponnum).gameObject,Character.chartrans);
    }


}
