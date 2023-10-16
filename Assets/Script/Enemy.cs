using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemytype;
    public int score;
    public int gold;
    public Vector3 movdir;
    public float energy;
    public float startenergy;

    public int deadtype;

    public GameObject boomup;
    public GameObject lifeup;
    public GameObject powerup;
    public int returntype; // 0 리턴안함 1 boom 리턴, 2 life 리턴, 3 파워 리턴 
    float totalenergy;
    public int enemyidx; // 몇번째인지 알려줘야함.
    public bool isboss;
    bool isdying;
    bool isouch;
    public bool awaken= false;
    SpriteRenderer spriteRenderer;

    Animator e_ani;
    public bool YouandMe;


    public Transform[] shoottrans;
    //int pattenindex;
    // Start is called before the first frame update
    private void Awake(){
        energy *= 1.0f + 0.5f * (PlayerInfo.playerInfo.level - 1);
        score = (int) (score * (1.0f + 0.5f * (float)(PlayerInfo.playerInfo.level - 1)));
        gold = (int) (gold * (1.0f + 0.5f * (float)(PlayerInfo.playerInfo.level - 1)));
        totalenergy = energy;
        startenergy = energy;
        e_ani = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();  
        isdying = false;
    }
    private void Start() { 

    }
    public void AwakeEnemy()
    {
        awaken= false;
        isouch = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        //pattenindex = 0;
        energy = totalenergy;
        StartCoroutine(enemypatten());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isboss&&awaken&&(transform.position.y <= -Character.ymax - 0.5f||transform.position.y>= Character.ymax+0.5f||transform.position.x <= -Character.xmax - 0.5f||transform.position.x >=Character.ymax+0.5f))
        {
            ObjectManager.ReturnEnemyObject(this);
        }
        if(!YouandMe){
            transform.Translate(Time.deltaTime * movdir);
        }
        if (energy <= 0)
        {
            
            if(isboss){
                if(!isdying){
                    Character.charact.bossdead = true;
                    PlayerInfo.playerInfo.curscore += score;
                    PlayerInfo.playerInfo.curgold += gold;
                    GameManager.gameManager.Bossdead();
                    StartCoroutine(Bossdead());
                }
                return;
            }
            if(isboss){
                GameManager.gameManager.Bossdead();
                Destroy(gameObject);
                return;
            }
            switch (returntype)
            {
                case 0:
                    break;
                case 1:
                    Instantiate(boomup, transform.position, new Quaternion(0, 0, 0, 0));
                    break;
                case 2:
                    Instantiate(lifeup, transform.position, new Quaternion(0, 0, 0, 0));
                    break;
                case 3:
                    Instantiate(powerup, transform.position, new Quaternion(0, 0, 0, 0));
                    break;
            }
            PlayerInfo.playerInfo.curscore += score;
            PlayerInfo.playerInfo.curgold += gold;
            Explosion expl = ObjectManager.GetExlposionObject(deadtype);
            expl.Awakesound();
            expl.transform.position = transform.position;
            ObjectManager.ReturnEnemyObject(this);
        }
    }


    IEnumerator Bossdead(){
        isdying = true;
        for (int i = 0; i < 3;i++){
            Explosion explosion = ObjectManager.GetExlposionObject(0);
            explosion.Awakesound();
            explosion.transform.position = new Vector3(transform.position.x+Random.Range(-1f,1f), transform.position.y+Random.Range(-1f,1f));
            yield return new WaitForSeconds(0.1f);
            explosion = ObjectManager.GetExlposionObject(0);
            explosion.Awakesound();
            explosion.transform.position = new Vector3(transform.position.x+Random.Range(-1f,1f), transform.position.y+Random.Range(-1f,1f));
            yield return new WaitForSeconds(0.1f);
            explosion = ObjectManager.GetExlposionObject(Random.Range(1,3));
            explosion.Awakesound();
            explosion.transform.position = new Vector3(transform.position.x+Random.Range(-0.5f,0.5f), transform.position.y+Random.Range(-0.5f,0.5f));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        
        Explosion exp = ObjectManager.GetExlposionObject(deadtype);
        exp.transform.position = transform.position;
        exp.Awakesound();
        Destroy(gameObject);
        yield break;
    }

    public IEnumerator ouch(){
        isouch = true;
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        isouch = false;

    }

    public void ouchouch(){
        if (!isouch)
        {
            StartCoroutine(ouch());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Boom"))
        {
            float damage = other.gameObject.GetComponent<Boom>().dmg;
            ouchouch();
            energy -= 10 * damage;
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Boom"))
        {
            float damage = other.gameObject.GetComponent<Boom>().dmg;
            ouchouch();
            energy -= damage;
        }
    }

    // 구체화 더 필요함.
    IEnumerator enemypatten()
    {
        yield return new WaitUntil(() => transform.position.y <= Character.ymax && transform.position.x <= Character.xmax
        && transform.position.x >= -Character.xmax);
        awaken = true;
        switch (enemytype)
        {
            //돌격이
            case 6:
            case 0:
                yield return new WaitForSeconds(0.5f);
                E_bulletPatten.E_bulletGen(transform, 0);
                
                break;
            //사이드
            case 1:
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 1; i++)
                {
                    E_bulletPatten.E_bulletGen(transform, 1);
                    yield return new WaitForSeconds(0.2f);
                }
                break;
            // 중몹패턴
            case 2:
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 5; i++)
                {
                    if (i == 1){
                        movdir.y = -1.5f;
                    }
                    E_bulletPatten.E_bulletGen(shoottrans[0], 2);
                    yield return new WaitForSeconds(1.0f);
                }
                break;
            case 3:
                switch(enemyidx){
                    case 1:
                        movdir.x = -0.1f;
                        break;
                    case 2:
                        movdir.x = 0.1f;
                        break;
                }
                yield return new WaitForSeconds(0);
                for (int i = 0; i < 1; i++)
                {
                    E_bulletPatten.E_bulletGen(transform, 3);
                    yield return new WaitForSeconds(0.2f);
                }
                break;
            case 4:
                 yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 20; i++)
                {
                    if(i == 0|| i%8 == 6){
                        movdir.x = 0.25f;
                        movdir.y = 0f;
                    } else if(i%8 == 2){
                        movdir.x = -0.25f;
                    }
                    if(i%3 == 0){
                        e_ani.SetTrigger("Atk");
                        E_bulletPatten.E_bulletGen(shoottrans[0], 4);
                        E_bulletPatten.E_bulletGen(shoottrans[1], 4);
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                break;
            case 5:
                if(enemyidx ==0){
                    movdir.x = 0.5f;
                }else{
                    movdir.x = -0.5f;
                }
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 20; i++)
                {
                    if(i==2){
                        movdir.x = 0;
                    }
                    e_ani.SetTrigger("Atk");
                    E_bulletPatten.E_bulletGen(shoottrans[0], 5);
                    E_bulletPatten.E_bulletGen(shoottrans[1], 5);
                    yield return new WaitForSeconds(1.0f);
                }
                break;
            case 7:
                yield return new WaitForSeconds(0.2f);
                movdir.x /= 5f;
                movdir.y = 1.0f;
                for (int i = 0; i < 20;i++){
                    E_bulletPatten.E_bulletGen(shoottrans[0], 7);
                    yield return new WaitForSeconds(1f);
                }
                    break;
            case 8:
                movdir.y = -1.0f;
                for (int i = 0; i < 10;i++){
                    if(i%4 == 0){
                        movdir.x = 0.5f;
                    }else if (i%4==2){
                        movdir.x = -0.5f;
                    }
                    if (i==3){
                        movdir.y = -0.5f;
                    }
                    if(enemyidx == 1){
                        movdir.x = -movdir.x;
                    }
                    E_bulletPatten.E_bulletGen(shoottrans[0], 8);
                    yield return new WaitForSeconds(0.5f);
                }
                break;
            case 9:
                for (int i = 0; i < 10; i++){
                    if(i == 1){
                    }
                    E_bulletPatten.E_bulletGen(shoottrans[i%2], 9);
                    yield return new WaitForSeconds(0.8f);
                }
                break;
            case 10:
                yield return new WaitForSeconds(0.2f);
                movdir.x /= 5f;
                movdir.y = -1.0f;
                for (int i = 0; i < 20;i++){
                    E_bulletPatten.E_bulletGen(shoottrans[0], 101);
                    yield return new WaitForSeconds(1f);
                }
                break;
            case 11:
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 20;i++){
                    E_bulletPatten.E_bulletGen(shoottrans[i%2],102);
                    yield return new WaitForSeconds(0.6f);
                }
                    break;
        }
    }
    IEnumerator changedir(int pos){
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 46; i++)
        {
            int degree = i;
            if (pos == 1)
            {
                degree = -degree;
            }
            transform.transform.rotation = Quaternion.Euler(0, 0, i);
            yield return null;
        }
    }
            
}