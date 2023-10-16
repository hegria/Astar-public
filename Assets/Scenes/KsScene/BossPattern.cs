using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BossPattern : MonoBehaviour
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

    public Animator BossAnimator1;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        enemy = gameObject.GetComponent<Enemy>();
        BossAnimator1 = GetComponent<Animator>();
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

    //패턴1: 좌우로 이동하며 직선으로 4발
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
        // 0 -> 오른쪽 1 -> 왼쪽으로 한번
        if(bossAtackControl1 == 0){
            rend.flipX = false;
            BossAnimator1.SetBool("Boss_at1_r",true);
            StartCoroutine(bossAttack1_move(1,0));
            StartCoroutine(bossAttack1_bullet(1));
            bossAtackControl1 =1;
        }
        else if(bossAtackControl1 == 1){
            rend.flipX = true;
            BossAnimator1.SetBool("Boss_at1_r",true);
            StartCoroutine(bossAttack1_move(-1,0));
            StartCoroutine(bossAttack1_bullet(0));
            bossAtackControl1 = 0;
        }
    }
    // transformidx  == 0 >left 1 >right
    // 몇초단위로 어떻게 소환한다.
    IEnumerator bossAttack1_bullet(int transformidx){
         for (int i = 0; i < 4; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[transformidx],0);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 4; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[transformidx],0);
            yield return new WaitForSeconds(0.2f);
        }
        BossAnimator1.SetBool("Boss_at1_r",false);
    }
    IEnumerator bossAttack1_move(float vecx, float vecy){ 
        enemy.movdir.x = vecx;
        enemy.movdir.y = vecy;
        yield return new WaitForSeconds(1);
        enemy.movdir.x = -vecx;
        enemy.movdir.y = -vecy;
        yield return new WaitForSeconds(1);
    }
    //패턴2: 좌우로 빠른 속도로 이동하며 방사형으로 10탄 발사 (HP 50% 미만이면 더 많이 발사 이 패턴으로)
    void BP2()
    {
        StartCoroutine(bossAttack2(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP2", 2);
            curPatternCount++;
        }
        else
        {
            Invoke("Think", 2);
            enemy.movdir.x = 0;
        }
    }
    IEnumerator bossAttack2(float waittime){
        yield return new WaitForSeconds(waittime);
        if(enemy.energy <= enemy.startenergy/2){
             if(bossAtackControl2 == 0){
                 StartCoroutine(bossAttack2_move(2,0));
                 StartCoroutine(bossAttack2_bullet(20));
                 bossAtackControl2 =1;
             }
             else if(bossAtackControl2 == 1){
                StartCoroutine(bossAttack2_move(-2,0));
                StartCoroutine(bossAttack2_bullet(20));
                bossAtackControl2 =0;
             }
        }
        else
        {
            if(bossAtackControl2 == 0){
                 StartCoroutine(bossAttack2_move(2,0));
                 StartCoroutine(bossAttack2_bullet(10));
                 bossAtackControl2 =1;
             }
             else if(bossAtackControl2 == 1){
                 StartCoroutine(bossAttack2_move(-2,0));
                 StartCoroutine(bossAttack2_bullet(10));
                 bossAtackControl2 =0;
            }
        }
    }
    // 랜덤한 x값에 떨어지는 탄 필요함!
    IEnumerator bossAttack2_bullet(int tan){
        for (int i = 0; i < tan; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[2],10);
            yield return new WaitForSeconds(0.2f);
            
        }
    }
    IEnumerator bossAttack2_move(float vecx, float vecy){ 
            enemy.movdir.x = vecx;
            enemy.movdir.y = vecy;
            yield return new WaitForSeconds(1);
            enemy.movdir.x = -vecx;
            enemy.movdir.y = -vecy;
            yield return new WaitForSeconds(1);
    }

     // 해치 열리면서 유도형 탄 발사
    void BP3()
    {
        StartCoroutine(bossAttack3(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP3", 2);
            curPatternCount++;
        }
        else
            Invoke("Think", 2);
    }
    IEnumerator bossAttack3(float waittime){
        yield return new WaitForSeconds(waittime);
        BossAnimator1.SetBool("Boss_at3",true);
        if(bossAtackControl3 == 0){
            StartCoroutine(bossAttack3_move(1,-0.5f));
            StartCoroutine(bossAttack3_bullet(2));
            bossAtackControl3 =1;
            
        }
        else if(bossAtackControl3 == 1){
            StartCoroutine(bossAttack3_move(-1,-0.5f));
            StartCoroutine(bossAttack3_bullet(2));
            bossAtackControl3 = 0;
        }
    }
    IEnumerator bossAttack3_bullet(int tan){
        for (int i = 0; i < tan; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[3],1);
            E_bulletPatten.E_bulletGen(transforms[4],1);
            E_bulletPatten.E_bulletGen(transforms[5],1);
            E_bulletPatten.E_bulletGen(transforms[6],1);
            yield return new WaitForSeconds(0.6f);
        }
        BossAnimator1.SetBool("Boss_at3",false);
    }
    IEnumerator bossAttack3_move(float vecx, float vecy){ 
            enemy.movdir.x = vecx;
            enemy.movdir.y = vecy;
            yield return new WaitForSeconds(1);
            enemy.movdir.x = -vecx;
            enemy.movdir.y = -vecy;
            yield return new WaitForSeconds(1);
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
          BossAnimator1.SetBool("Boss_at4",true);
            StartCoroutine(bossAttack4_move(0,-1));
            StartCoroutine(bossAttack4_bullet(2));
    }  
    IEnumerator bossAttack4_bullet(int tan){
        for (int i = 0; i < tan; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[0],11);
            E_bulletPatten.E_bulletGen(transforms[1],11);
            yield return new WaitForSeconds(0.3f);
        }
        BossAnimator1.SetBool("Boss_at4",false);
    }    
    IEnumerator bossAttack4_move(float vecx, float vecy){ 
        enemy.movdir.x = vecx;
        enemy.movdir.y = vecy;
        yield return new WaitForSeconds(1);
        enemy.movdir.x = -vecx;
        enemy.movdir.y = -vecy;
        yield return new WaitForSeconds(1);
    }    
}
