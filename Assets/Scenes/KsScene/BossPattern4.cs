using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BossPattern4 : MonoBehaviour
{
    // 오브젝트 풀링이 편하니까..
    public int bossAtackControl1 = 0;
    public int bossAtackControl2 = 0;
    public int bossAtackControl3 = 0;

    // 보스 페턴
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount; // 패턴 반복 횟수   <- 입력하는걸로 하지말자.
    public Transform[] transforms;

    float time;
    Enemy enemy;
    SpriteRenderer rend;

    public Animator BossAnimator2;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        enemy = gameObject.GetComponent<Enemy>();
        BossAnimator2 = GetComponent<Animator>();
        StartCoroutine(BossEnter());
    }
    // 보스 등장 코루틴
    IEnumerator BossEnter(){
        for(int i=0; i<4; i++){
            enemy.movdir.y += 1;
            yield return new WaitForSeconds(0.35f);
        }
        Think();
    }
    // Update is called once per frame
    void Update()
    {
        //조이스틱
        time+= Time.deltaTime;
    }
    

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        /* 랜덤으로 패턴 나오게 할때
        patternIndex = Random.Range(0,3);
        */ 
        switch (patternIndex)
        {
            case 0:
                BP1();
                break;
            case 1:
                BP2();
                break;
            case 2:
                BP3();
                break;
            case 3:
                BP4();
                break;
        }
    }

    //패턴1: 앞으로 돌진하면서 옆으로 탄 발사
    void BP1()
    {
        StartCoroutine(bossAttack1(0f));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP1", 2);
            curPatternCount++;
        }
        else
        {
            Invoke("Think", 2);
        }
    }
    IEnumerator bossAttack1(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        BossAnimator2.SetBool("Boss_at1",true);
        if(bossAtackControl1 == 0){
                 StartCoroutine(bossAttack1_move(-3));
                 bossAtackControl1 =1;
                 StartCoroutine(bossAttack1_bullet());
             }
             else if(bossAtackControl1 == 1){
                 StartCoroutine(bossAttack1_move(5));
                 StartCoroutine(bossAttack1_2_bullet());
                 yield return new WaitForSeconds(1);
                 BossAnimator2.SetBool("Boss_at4",true);
                 bossAtackControl1 =0;
             }
        
    }
    IEnumerator bossAttack1_bullet(){
         for (int i = 0; i < 4; i++)
        {   
            E_bulletPatten.E_bulletGen(transforms[4],17);
            E_bulletPatten.E_bulletGen(transforms[5],17);
            E_bulletPatten.E_bulletGen(transforms[6],17);
            E_bulletPatten.E_bulletGen(transforms[7],18);
            E_bulletPatten.E_bulletGen(transforms[8],18);
            E_bulletPatten.E_bulletGen(transforms[9],18);
            yield return new WaitForSeconds(0.5f);
        }
        BossAnimator2.SetBool("Boss_at1",false);
    }
    IEnumerator bossAttack1_2_bullet(){
        yield return new WaitForSeconds(1.05f);
         for (int i = 0; i < 20; i++)
        {   
            E_bulletPatten.E_bulletGen(transforms[1],10);
            yield return new WaitForSeconds(0.04f);
        }
        BossAnimator2.SetBool("Boss_at4",false);
    }
    IEnumerator bossAttack1_move(int vecy){ 
        enemy.movdir.y = vecy;
        yield return new WaitForSeconds(1);
        enemy.movdir.y = -vecy;
        yield return new WaitForSeconds(1);
    }
    //패턴2:  // 기 모은 동작 후 탄 방사형으로 순차적 발사
    void BP2()
    {
        StartCoroutine(bossAttack2(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP2", 4);
            curPatternCount++;
        }
        else
        {
            Invoke("Think", 4);
        }
    }
    IEnumerator bossAttack2(float waittime){
         yield return new WaitForSeconds(waittime);
         StartCoroutine(bossAttack2_move());
         yield return new WaitForSeconds(0.2f);
         BossAnimator2.SetBool("Boss_at2",true);
         StartCoroutine(bossAttack2_bullet());
    }
    IEnumerator bossAttack2_bullet(){
        for (int i = 0; i < 60; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[2],15);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator bossAttack2_move(){ 
        enemy.movdir.y = -10;
        yield return new WaitForSeconds(0.2f);
        enemy.movdir.y = 0;
        yield return new WaitForSeconds(3);
        enemy.movdir.y = 10;
        yield return new WaitForSeconds(0.2f);
        enemy.movdir.y = 0;
        BossAnimator2.SetBool("Boss_at2",false);
    }

     // 해치 열리면서 유도형 탄 발사
    void BP3()
    {
        StartCoroutine(bossAttack3(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP3", 3);
            curPatternCount++;
        }
        else
            Invoke("Think", 3);
    }
    IEnumerator bossAttack3(float waittime){
        yield return new WaitForSeconds(waittime);
        if(bossAtackControl3 == 0){
            StartCoroutine(bossAttack3_move(5,2));
            StartCoroutine(bossAttack3_bullet());
            bossAtackControl3 =1;
        }
        else if(bossAtackControl3 == 1){
            StartCoroutine(bossAttack3_move(-5,2));
            StartCoroutine(bossAttack3_bullet());
            bossAtackControl3 = 0;
        }
    }
    IEnumerator bossAttack3_bullet(){
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 2; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[3],19);
            yield return new WaitForSeconds(0.4f);
        }
        BossAnimator2.SetBool("Boss_at3",false);
    }
    IEnumerator bossAttack3_move(int vecx, int vecy){
        enemy.movdir.x = -vecx;
        enemy.movdir.y = vecy;
        yield return new WaitForSeconds(0.5f);
        BossAnimator2.SetBool("Boss_at3",true);
        enemy.movdir.x = vecx;
        enemy.movdir.y = -vecy;
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector3(0,6,0);
        enemy.movdir.x = 0;
        enemy.movdir.y = -2;
        yield return new WaitForSeconds(1);
        enemy.movdir.y = 0;
    }    

    // 양손에서 동시에 150 180 210도 방향으로 3발 한번에 발사/ 3번씩   
    void BP4()
    {
        StartCoroutine(bossAttack4(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP4", 2);
            curPatternCount++;
        }
        else
            Invoke("Think", 2);
    }
    IEnumerator bossAttack4(float waittime){
        yield return new WaitForSeconds(waittime);
        BossAnimator2.SetBool("Boss_at4",true);
            StartCoroutine(bossAttack4_move());
            StartCoroutine(bossAttack4_bullet());
    }  
    IEnumerator bossAttack4_bullet(){
        for (int i = 0; i < 18 ; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[1],16);
            yield return new WaitForSeconds(0.1f);
        }
        BossAnimator2.SetBool("Boss_at4",false);
    }    
    IEnumerator bossAttack4_move(){ 
        enemy.movdir.y = -1;
        yield return new WaitForSeconds(0.9f);
        enemy.movdir.y = 1;
        yield return new WaitForSeconds(0.9f);
        enemy.movdir.y = 0;
    }    
}
