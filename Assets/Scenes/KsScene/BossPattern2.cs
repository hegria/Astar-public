using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BossPattern2 : MonoBehaviour
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
            Invoke("BP1", 4);
            curPatternCount++;
        }
        else
        {
            Invoke("Think", 4);
        }
    }
    IEnumerator bossAttack1(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        BossAnimator2.SetBool("Boss_at1",true);
        StartCoroutine(bossAttack1_move());
        StartCoroutine(bossAttack1_bullet(4,3));
    }
    // transformidx  == 0 >center 1 >right arm 2> left arm 3> r wing 4> l wing
    // 12 -> r 13 -> l
    // 몇초단위로 어떻게 소환한다.
    IEnumerator bossAttack1_bullet(int transformidx1, int transformidx2){
         for (int i = 0; i < 7; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[transformidx1],12);
            E_bulletPatten.E_bulletGen(transforms[transformidx2],13);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator bossAttack1_move(){ 
        enemy.movdir.y = -6;
        yield return new WaitForSeconds(2.8f);
        BossAnimator2.SetBool("Boss_at1",false);
        transform.position = new Vector3(0,5.5f,0);
        enemy.movdir.y = -4;
        for(int i=0; i<4; i++){
            enemy.movdir.y += 1;
            yield return new WaitForSeconds(0.3f);
        }
    }
    //패턴2:  // 기 모은 동작 후 탄 방사형으로 순차적 발사
    void BP2()
    {
        StartCoroutine(bossAttack2(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP2", 3.5f);
            curPatternCount++;
        }
        else
        {
            Invoke("Think", 3.5f);
        }
    }
    IEnumerator bossAttack2(float waittime){
        yield return new WaitForSeconds(waittime);
        BossAnimator2.SetBool("Boss_at2",true);
        yield return new WaitForSeconds(1.5f);
        BossAnimator2.SetBool("Boss_at2",false);
             StartCoroutine(bossAttack2_move());
             StartCoroutine(bossAttack2_bullet());
    }
    IEnumerator bossAttack2_bullet(){
        for (int i = 0; i < 4; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[0],14);
            yield return new WaitForSeconds(0.5f);
            
        }
    }
    IEnumerator bossAttack2_move(){ 
            enemy.movdir.y = 1;
            yield return new WaitForSeconds(1);
            enemy.movdir.y = -1;
            yield return new WaitForSeconds(1);
            enemy.movdir.y = 0;
    }

     // 해치 열리면서 유도형 탄 발사
    void BP3()
    {
        StartCoroutine(bossAttack3(0));
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("BP3", 3.6f);
            curPatternCount++;
        }
        else
            Invoke("Think", 3.6f);
    }
    IEnumerator bossAttack3(float waittime){
        yield return new WaitForSeconds(waittime);
            StartCoroutine(bossAttack3_move());
            StartCoroutine(bossAttack3_bullet(14));
    }
    IEnumerator bossAttack3_bullet(int tan){
        for (int i = 0; i < tan; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[1],0);
            E_bulletPatten.E_bulletGen(transforms[2],0);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator bossAttack3_move(){ 
            enemy.movdir.x = -3;
            enemy.movdir.y = -1;
            yield return new WaitForSeconds(0.5f);
            enemy.movdir.x = 0;
            enemy.movdir.y = 2;
            yield return new WaitForSeconds(0.5f);
            enemy.movdir.x = 3;
            enemy.movdir.y = -1;
            yield return new WaitForSeconds(1);
            enemy.movdir.x = 0;
            enemy.movdir.y = 2;
            yield return new WaitForSeconds(0.5f);
            enemy.movdir.x = -3;
            enemy.movdir.y = -1;
            yield return new WaitForSeconds(0.5f);
            enemy.movdir.x = 0;
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
        BossAnimator2.SetBool("Boss_at3",true);
            StartCoroutine(bossAttack4_move());
            StartCoroutine(bossAttack4_bullet(6));
    }  
    IEnumerator bossAttack4_bullet(int tan){
        for (int i = 0; i < tan; i++)
        {    
            E_bulletPatten.E_bulletGen(transforms[5],1);
            E_bulletPatten.E_bulletGen(transforms[6],1);
            yield return new WaitForSeconds(0.3f);
        }
        BossAnimator2.SetBool("Boss_at3",false);
    }    
    IEnumerator bossAttack4_move(){ 
        enemy.movdir.y = -2;
        yield return new WaitForSeconds(0.9f);
        enemy.movdir.y = 2;
        yield return new WaitForSeconds(0.9f);
        enemy.movdir.y = 0;
    }    
}
