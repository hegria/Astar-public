using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracBoss : MonoBehaviour
{
    public static PracBoss myBoss3;
    //얼음 보스 기본 컨셉;
    // Motion1 : 꼬리를 휘두른다 -> 스킬 1 얼음 조각 발사
    // Motion2 : 몸을 살짝 기우린다 -> 스킬 2 빔
    // 추가스킬 : 얼음으로 무적 후 자가 회복
    // 얼음 효과 데미지를 입으면 1초동안 공격, 스킬 못씀
    [SerializeField]
    GameObject IceShot;
    public GameObject mySubLaser, mylaser;
    float Check_Time;
    bool ismove,isSkill,isBasic;
    [SerializeField]
    SpriteRenderer[] ShotCollection;
    Transform LaserRangeStart;
    float XCamera;
    float deltaAngle;
    List<IEnumerator> mycoLst;
    bool isPattern;
    List<IEnumerator> mycos;
    List<bool> Boss3PatternList = new List<bool>();
    Coroutine myco = null;

    [SerializeField]
    Transform Origin,tran1,tran2, front, Tail;
    List<Coroutine> Attacks = new List<Coroutine>();
    float ParentSize;
    int[][][,] mylevel = new int[7][][,];
    public int BossLevel = 0;//0,1,2 => 하,중,상
    public int Boss3Phaze = 0;//0,1 => 1페이즈, 2페이즈;
    public bool myandyou;
    public int PatternNum;
    float HP;
    private float[,] DelayTable = new float[3,2]{{1.6f,1.5f},{1.5f,1.4f},{1.2f,1f}};
    private void Start() {
        myBoss3 = this;
        Boss3Phaze = 0;
        if(myandyou){
            BossLevel = PlayerInfo.playerInfo.level-1;
            HP = GetComponent<Enemy>().energy;
        }
        mylevel[0] = new int[2][,];
        mylevel[1] = new int[1][,];
        mylevel[2] = new int[2][,];
        mylevel[3] = new int[2][,];
        mylevel[4] = new int[3][,];
        mylevel[5] = new int[2][,];
        mylevel[6] = new int[1][,];
        mylevel[0][0] = new int[3,2]{{10,11},{10,12},{10,13}};
        mylevel[0][1] = new int[3,2]{{2,3},{3,3},{3,4}};
        mylevel[1][0] = new int[3,2]{{10,11},{10,12},{10,13}};
        mylevel[2][0] = new int[3,2]{{10,11},{10,12},{10,13}};
        mylevel[2][1] = new int[3,2]{{4,4},{4,6},{6,6}};
        mylevel[3][0] = new int[3,2]{{10,11},{10,12},{10,13}};
        mylevel[3][1] = new int[3,2]{{15,15},{18,18},{21,21}};
        mylevel[4][0] = new int[3,2]{{1,1},{2,2},{3,3}};
        mylevel[4][1] = new int[3,2]{{15,18},{16,20},{20,24}};
        mylevel[4][2] = new int[3,2]{{10,11},{10,12},{10,13}};
        mylevel[5][0] = new int[3,2]{{1,1},{1,1},{1,3}};
        mylevel[5][1] = new int[3,2]{{20,20},{25,25},{30,30}};
        mylevel[6][0] = new int[3,2]{{10,10},{15,15},{20,25}};
        XCamera = Camera.main.orthographicSize*Camera.main.aspect;
        mycoLst = new List<IEnumerator>(new IEnumerator[]{Basic_Attack1_Boss3(),Basic_Attack2_Boss3(),Basic_Attack3_Boss3()});
        switch(BossLevel){
            case 0 : 
            mycos = new List<IEnumerator>(new IEnumerator[]{B3P1(),B3P2(),B3P3(),B3P4(),B3P5(),B3P6()});
            break;
            case 1 :
            mycos = new List<IEnumerator>(new IEnumerator[]{B3P1(),B3P2(),B3P3(),B3P4(),B3P5(),B3P6(),B3P7()});
            break;
            case 2:
            mycos = new List<IEnumerator>(new IEnumerator[]{B3P1(),B3P2(),B3P3(),B3P4(),B3P5(),B3P6(),B3P7(),B3P8()});
            break;
        }
        for(int i = 0; i< mycos.Count;i++){
            Boss3PatternList.Add(false);
        }
        Debug.Log(Boss3PatternList[3]);
        for(int i =0; i< mycos.Count;i++){
            StartCoroutine(mycos[i]);
        }
        for(int i = 0;i<56;i++){
            Attacks.Add(myco);
        }
        ParentSize = transform.localScale.x;
        IceShot.GetComponent<SpriteRenderer>().sprite = ShotCollection[0].sprite;
        SkillCnt = mylevel[4][0][BossLevel,Boss3Phaze];
        StartCoroutine(MoveToPoint(Origin.position,1f));
        Invoke("Cycle",2);
    }
    public bool P;
    public int BP;
    private void Update() {
        if(P){
            Boss3PatternList[BP-1] = true;
            P=false;
        }
        if(myandyou){
            if(GetComponent<Enemy>().energy < HP/2f&&Boss3Phaze<1){
                Boss3Phaze++;
            }
        }
    }
    //이동, 기본 샷, 스킬에 해당하는 코루틴 만들기
    // 이동 -> 좌우 완하는 대로 이동 코루틴

    //이동 1 : 원하는 방향, 거리를 원하는 시간에 움직이기
    IEnumerator timetoframe(Vector3 dir,float Distance,float moveTime){
        yield return new WaitUntil(()=>!ismove);
        ismove = true;
        float fixtime = moveTime;
        Vector3 fixpos = transform.position;
        while(fixtime > 0){
        fixtime -= Time.deltaTime;
        transform.position = fixpos + dir.normalized*Distance*(1-fixtime/moveTime); // 방향*거리*(0부터~1까지 movetime동안 움직이는 실수값)
        yield return null;
        }
        ismove = false;
        yield break;
    }
    //이동 2 : 원하는 방향, 거리를 Lerp로 움직이기 => 시간관련 X
    IEnumerator MoveLerp(Vector3 dir,float Distance,float MoveSpeed){
            yield return new WaitUntil(()=>!ismove);
            Vector3 originPos = transform.position;
            ismove = true;
            for(int i = 0; i<30;i++){
                transform.position = Vector3.Lerp(transform.position,originPos + dir.normalized*Distance,MoveSpeed);
                yield return null;
            }
            transform.position = originPos + dir.normalized*Distance;
            ismove = false;
            yield break;
    }
    //이동 3 : 원하는 좌표로 원하는 시간에 움직이기
    IEnumerator MoveToPoint(Vector3 Pnt, float UseTime){
        yield return new WaitUntil(()=>!ismove);
        ismove = true;
        float fixtime = UseTime;
        float dis = Vector3.Distance(Pnt,transform.position);
        Vector3 origin = transform.position;
        while(fixtime > 0){
            fixtime -= Time.deltaTime;
            transform.position = origin + (Pnt-origin)*(1-fixtime/UseTime);
            yield return null;
        }
        ismove = false;
        yield break;
    }
    //이동 4 : 원하는 좌표로 Lerp로 움직이기
    IEnumerator MovetoPointByLerp(Vector3 Pnt, float Rate){
        yield return new WaitUntil(()=>!ismove);
        ismove = true;
        for(int i = 0; i<30;i++){
            transform.position = Vector3.Lerp(transform.position,Pnt,Rate);
            yield return null;
        }
        transform.position = Pnt;
        ismove = false;
        yield break;
    }
    //이동 5 : 중심과 반지름을 입력받고 원하는 시간동안 원 한바퀴 돌기(시작 부분 : 양수쪽 Y축 꼭지점부터)
    IEnumerator MoveAlongCircle(Vector3 center, float Radius, float Cool){
        yield return new WaitUntil(()=>!ismove);
        ismove = true;
        float fix = Cool;
        while(fix > 0){
            fix -= Time.deltaTime;
            float Arg = 2*Mathf.PI*(1-fix/Cool) + Mathf.PI*0.5f;
            transform.position = center + new Vector3(Radius*Mathf.Cos(Arg),0.5f*Radius*Mathf.Sin(Arg),0);
            yield return null;
        }
        ismove = false;
        yield break;
    }
    //이동 6 : 극점과 시작 위치 받아서 시작위치에서 극점으로 원하는 시간 안에 포물선으로 움직이기
    IEnumerator MoveAlongParaBola(Vector3 Center, Vector3 NowPnt, float Delay){
        if(Center.x != NowPnt.x){
            float fixtime = Delay;
        Vector3 startPnt = NowPnt;
        float p = (NowPnt.y-Center.y)/Mathf.Pow((NowPnt.x-Center.x),2);
        float startX = NowPnt.x;
        float CurrentX;
        while(fixtime > 0){
            fixtime -= Time.deltaTime;
            CurrentX = startX + (Center.x-startX)*(1-fixtime/Delay);
            transform.position = new Vector3(CurrentX,p*Mathf.Pow(CurrentX-Center.x,2) + Center.y,0);
            yield return null;
        }
        yield break;
        }
        else{
            float fixtime = Delay;
            Vector3 Startpos = NowPnt;
            while(fixtime>0){
                fixtime -= Time.deltaTime;
                transform.position = Startpos + (Center - Startpos)*(1-fixtime/Delay);
                yield return null;
            }
            yield break;
        }
    }
    public void Skill1On(){
        TailOn = true;
    }
    public void EndSkill1(){
        TailOn = false;
        GetComponent<Animator>().SetBool("Boss_Sk1", false);
    }
    bool TailOn;
    E_bullet temp;
    //스킬 만들기
    //1. 꼬리로 얼음 조각 분사
    int SkillCnt;
    // -> Boss Transform -> 반지름 Radius,  시작 각도 Argment, Clockwise + CounterClockWise -> int로 받기, 발사 주기 -> float, 발사 속도 -> float;
    IEnumerator TailAttack(float Radius, float StartArgment, int ClockWise, float Cool){
        yield return new WaitUntil(()=> !isSkill); // 스킬 사용까지 대기
        //레벨 디자인 변수들
        int iceNum = mylevel[4][1][BossLevel,Boss3Phaze]; // [12,14][15,17][18,20]
        float Vel = mylevel[4][2][BossLevel,Boss3Phaze]; //  [10,12][13,15][16,18]
        if(ClockWise > 0 ){
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            GetComponent<SpriteRenderer>().flipX = true;
        }
        GetComponent<Animator>().SetBool("Boss_Sk1",true);
        float saveAngle;
        isSkill = true;
        WaitForSeconds mydelay = new WaitForSeconds(Cool/iceNum);
        float deltaTheta = (90-StartArgment)*2/iceNum*Mathf.Deg2Rad;
        saveAngle = (90-(90-StartArgment)*ClockWise)*Mathf.Deg2Rad;
        yield return new WaitUntil(()=>TailOn);
        for(int i = 0; i<iceNum;i++){
            float theta = saveAngle + i*deltaTheta*ClockWise;
            if(myandyou){
                if(BossLevel ==2&&Boss3Phaze == 1){
                    temp = ObjectManager.GetBulletObject(13);
                }
                else{
                    temp = ObjectManager.GetBulletObject(10);
                }
            temp.transform.position = transform.position-new Vector3(Mathf.Cos(theta)*Radius,Mathf.Sin(theta)*Radius,0);
            temp.transform.rotation = Quaternion.Euler(0,0,theta*Mathf.Rad2Deg + 90);
            temp.Awakebullet();
            }
            else{
                GameObject myIce = Instantiate(IceShot, Tail.position-new Vector3(Mathf.Cos(theta)*Radius,Mathf.Sin(theta)*Radius,0),Quaternion.identity);
            myIce.transform.Rotate(new Vector3(0,0,theta*Mathf.Rad2Deg+90));
            myIce.GetComponent<IceBullet>().SetAwake(Vel);
            yield return mydelay;
            }
        }
        yield return new WaitUntil(()=>!TailOn);
        isSkill = false;//스킬 OFF;
        yield break;
    }
    //2. 얼음 레이져 발사
    //Child of Boss, Variance : LaserSpeed <- 레이져 속도, 시작 크기, 시작 딜레이, 스킬 사용 시간) + 데미지
    //시작 딜레이 -> 레이져 시작
    //레이져 -> y크기로 판별 후 stop! 스킬 시전 시간 맞추기
    Coroutine laserCo;
    bool startLaser, Endlaser;
    public void LaserOn(){
        startLaser = true;
    }
    public void LaserOff(){
        GetComponent<Animator>().SetBool("Boss_Sk2",false);
        startLaser = false;
    }
    public void LaserDown(){
        Endlaser = true;
    }
    IEnumerator IceBossSkill2(float lasertime){
        yield return new WaitUntil(()=>!isSkill);
            isSkill = true;
            GetComponent<Animator>().SetBool("Boss_Sk2", true);
            int LaserNum = mylevel[5][0][BossLevel,Boss3Phaze]; //[1,1],[1,1],[3,3];
            float LaserSize = mylevel[5][1][BossLevel,Boss3Phaze]; //[20,20],[25,25],[30,30]
            yield return new WaitUntil(()=>startLaser);
            GameObject mysub = Instantiate(mySubLaser,front.position+ Vector3.down*0.5f,Quaternion.identity);
            mysub.transform.SetParent(transform);
            mysub.GetComponent<SpriteRenderer>().color = new Color32(0,41,255,255);
            Coroutine mybig = StartCoroutine(Big_size(mysub,0,1/ParentSize,.5f));
            for(int i = 0; i<LaserNum;i++){
                GameObject minelaser = Instantiate(mylaser,mysub.transform);
                minelaser.GetComponent<SpriteRenderer>().color = new Color32(0,41,255,255);
                minelaser.transform.position = mysub.transform.position;
                laserCo=StartCoroutine(Laser(mysub,minelaser,i,lasertime,LaserSize,LaserSize*2,.15f));
            }
            yield return new WaitUntil(()=>movelaser);
            mysub.GetComponent<Rotation>().SetAwake(mylevel[6][0][BossLevel,Boss3Phaze]);
            yield return laserCo;
            yield return new WaitUntil(()=>!startLaser);
            Destroy(mysub);
            movelaser = false;
            isSkill = false;
            Endlaser = false;
            yield break;
    }
    bool movelaser;
    IEnumerator Big_size(GameObject mygm,float startSize,float Size, float Delay){
        mygm.transform.localScale = Vector3.one*startSize;
        float fixtime = Delay;
        while(fixtime > 0){
            fixtime -=Time.deltaTime;
            mygm.transform.localScale = Vector3.one*Size*(1-fixtime/Delay);
            yield return null;
        }
        yield break;
    }//속도를 시간으로 정하도록 수정할 것
    IEnumerator Laser(GameObject parentLaser,GameObject myla,int Lasertype,float Waittime, float Laser_Size, float Up,float down){
        Vector3 DeltaTheta;
        if(myandyou){
            DeltaTheta = (Character.chartrans.position - transform.position);
        }
        else{
            DeltaTheta = (Vector3.down*3f-transform.position - Vector3.up*1.5f);
        }
        float RealTheta = Mathf.Atan2(DeltaTheta.y,DeltaTheta.x)*Mathf.Rad2Deg;
        myla.transform.Rotate(0,0,RealTheta - 90);
        switch(Lasertype){
            case 0 :;
            break;
            case 1:
            myla.transform.Rotate(0,0,30);
            break;
            case 2:
            myla.transform.Rotate(0,0,-30);
            break;
        }
        myla.transform.localPosition += myla.transform.up*5;
        myla.GetComponent<Animator>().SetTrigger("Tri");
        yield return new WaitUntil(()=>myla.GetComponent<BossLaser>().SubLaser);
        myla.transform.localPosition -= myla.transform.up*5;
        myla.GetComponent<BoxCollider2D>().enabled = true;
        while(myla.transform.localScale.y < Laser_Size){
            float r = Time.deltaTime*Up;
            myla.transform.localScale += r*Vector3.up;
            myla.transform.Translate(Vector3.up*r*.3f);
            yield return null;
        }
        movelaser = true;
        yield return new WaitUntil(()=>Endlaser);
        for(int i = 0; i<50;i++){
            parentLaser.transform.localScale = Vector3.Lerp(myla.transform.localScale, new Vector3(0,0,0),down/2f);
            myla.transform.localScale = Vector3.Lerp(myla.transform.localScale,new Vector3(0,myla.transform.localScale.y,myla.transform.localScale.z),down);
            yield return null;
        }
        yield break;
    }
    //기본 공격 1 : 직선 얼음 일렬로 뿌리기
    IEnumerator Basic_Attack1_Boss3(){
        isBasic = true;
        float Vel = mylevel[0][0][BossLevel,Boss3Phaze];//[10,12][13,15][16,18]
        int ShotNum = mylevel[0][1][BossLevel,Boss3Phaze];//[2,3],[3,3][3,4];
        WaitForSeconds mywait = new WaitForSeconds(0.075f);
        GetComponent<Animator>().SetBool("Boss_at2", true);
        float A1 = Random.Range(-.25f,.25f);
        float A2 = Random.Range(-.25f,.25f);
        while(isBasic){
            for(int i = 0; i<ShotNum;i++){
                if(!myandyou){
                GameObject mygame1 = Instantiate(IceShot,tran1.position + Vector3.right*A1, Quaternion.Euler(0,0,180));
                GameObject mygame2 = Instantiate(IceShot,tran2.position + Vector3.right*A2, Quaternion.Euler(0,0,180));
                mygame1.GetComponent<IceBullet>().SetAwake(Vel);
                mygame2.GetComponent<IceBullet>().SetAwake(Vel);
                }
                else{
                if(BossLevel > 0&&Random.Range(0,3) == 0){
                    E_bulletPatten.Instan(1,tran1.position,Quaternion.identity);
                    E_bulletPatten.Instan(1,tran2.position,Quaternion.identity);
                }
                E_bulletPatten.Instan(10,tran1.position+Vector3.right*A1,Quaternion.Euler(0,0,180));
                E_bulletPatten.Instan(10,tran2.position+ Vector3.right*A2,Quaternion.Euler(0,0,180));
                }
                
                // => 유도샷도 레벨에 따라 하나씩 추가할 것
                yield return mywait;
            }
            yield return mywait;
        }
        GetComponent<Animator>().SetBool("Boss_at2",false);
        yield break;
    }
    IEnumerator Basic_Attack2_Boss3(){
        float Vel = mylevel[1][0][BossLevel,Boss3Phaze];//[10,12][13,15][16,18]
        float StartArgument = 55;
        WaitForSeconds mydelay = new WaitForSeconds(0.1f);
        isBasic = true;
        while(isBasic)
        {
            float Arg = Random.Range(StartArgument + 90, 270-StartArgument);
            switch(BossLevel){
                case 0:
                if(!myandyou){
                    GameObject myice = Instantiate(IceShot,front.position, Quaternion.Euler(0,0,Arg));
                    myice.GetComponent<IceBullet>().SetAwake(Vel);
                }
                else{
                    E_bullet temp = ObjectManager.GetBulletObject(20);
                    temp.transform.position = front.position;
                    temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                    temp.Awakebullet();
                }
                yield return mydelay;
                break;
                case 1:
                for(int i = 0; i<3;i++){
                    if(i == 0){
                        if(myandyou){
                            E_bullet temp = ObjectManager.GetBulletObject(20);
                        temp.transform.position = front.position;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(IceShot,front.position, Quaternion.Euler(0,0,Arg));
                            mygame.GetComponent<IceBullet>().SetAwake(Vel);
                        }
                    }
                    else{
                        if(!myandyou){
                        GameObject mygame = Instantiate(IceShot,front.position,Quaternion.Euler(0,0,Arg));
                        mygame.transform.localPosition += -mygame.transform.up*.5f + mygame.transform.right*.3f - (i-1)*mygame.transform.right*0.6f;
                        mygame.GetComponent<IceBullet>().SetAwake(Vel);
                        }
                        else{
                            E_bullet temp = ObjectManager.GetBulletObject(20);
                        temp.transform.position = front.position;
                        temp.transform.localPosition += -temp.transform.up*.5f + temp.transform.right*.3f - (i-1)*temp.transform.right*0.6f;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.Awakebullet();
                        }
                    }
                }
                yield return mydelay;
                break;
                case 2:
                for(int i = 0; i<5;i++){
                    if(i == 0){
                        if(myandyou){
                        E_bullet temp = ObjectManager.GetBulletObject(20);
                        temp.transform.position = front.position;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(IceShot,front.position, Quaternion.Euler(0,0,Arg));
                        mygame.GetComponent<IceBullet>().SetAwake(Vel);
                        }
                    }
                    else if(i<3){
                        if(myandyou){
                            E_bullet temp = ObjectManager.GetBulletObject(20);
                        temp.transform.position = front.position;
                        temp.transform.localPosition += -temp.transform.up*.5f + temp.transform.right*.3f - (i-1)*temp.transform.right*0.6f;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(IceShot,front.position,Quaternion.Euler(0,0,Arg));
                        mygame.transform.localPosition += -mygame.transform.up*0.5f + mygame.transform.right*.3f - (i-1)*mygame.transform.right*0.6f;
                        mygame.GetComponent<IceBullet>().SetAwake(Vel);
                        }
                    }
                    else{
                        if(myandyou){
                            E_bullet temp = ObjectManager.GetBulletObject(20);
                        temp.transform.position = front.position;
                        temp.transform.localPosition += -temp.transform.up + temp.transform.right*.6f - (i-3)*temp.transform.right*1.2f;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(IceShot,front.position,Quaternion.Euler(0,0,Arg));
                        mygame.transform.localPosition += -mygame.transform.up + mygame.transform.right*0.6f - (i-3)*mygame.transform.right*1.2f;
                        mygame.GetComponent<IceBullet>().SetAwake(Vel);
                        }
                    }
                }
                yield return mydelay;
                break;
            }
        }
        yield break;
    }
    //기본공격 3번 -> 직진으로 쏘기
    IEnumerator Basic_Attack3_Boss3(){
        isBasic = true;
        float AttackCool = BossLevel == 2 ? 0.3f:0.4f;
        float Vel = mylevel[2][0][BossLevel,Boss3Phaze];//[10,12][13,15][16,18]
        int AttackNum = mylevel[2][1][BossLevel,Boss3Phaze];//[4,6][6,8][8,12];
        float Angle = 45;
        WaitForSeconds mydelay = new WaitForSeconds(AttackCool);
        GetComponent<Animator>().SetBool("Boss_at1",true);
        while(isBasic)
        {
            switch(BossLevel){
                case 0 :
                deltaAngle = (180-2*Angle)/(AttackNum);
                for(int i = 0; i<AttackNum; i++){
                    if(myandyou){
                        E_bulletPatten.Instan(30,front.position,Quaternion.Euler(0,0,180));
                    }
                    else{
                        GameObject myattack = Instantiate(IceShot, front.position,Quaternion.Euler(0,0,180-Angle + deltaAngle*(i)));
                    myattack.GetComponent<IceBullet>().SetAwake(Vel);
                    }
                    // E_bulletPatten.Type0Gen(front);
                }
                break;
                case 2:
                deltaAngle = (180-2*Angle)/(AttackNum);
                for(int i = 0; i<AttackNum; i++){
                    if(myandyou){
                        int alpha = Random.Range(4,6);
                        E_bulletPatten.Instan(20+alpha,front.position,Quaternion.Euler(0,0,180));
                        E_bulletPatten.Instan(10,tran1.position,Quaternion.Euler(0,0,180));
                        E_bulletPatten.Instan(10,tran2.position,Quaternion.Euler(0,0,180));
                    }
                    else{
                    GameObject myattack = Instantiate(IceShot, front.position,Quaternion.Euler(0,0,180-Angle + deltaAngle*(i)));
                    myattack.GetComponent<IceBullet>().SetAwake(Vel);
                    GameObject myattack1 = Instantiate(IceShot, tran1.position,Quaternion.Euler(0,0,180-(90-Angle) + deltaAngle*(i)));
                    myattack1.GetComponent<IceBullet>().SetAwake(Vel);
                    GameObject myattack2 = Instantiate(IceShot, tran2.position,Quaternion.Euler(0,0,180-(90-Angle) + deltaAngle*(i)));
                    myattack2.GetComponent<IceBullet>().SetAwake(Vel);
                    }
                }
                break;
                case 1:
                deltaAngle = (180-2*Angle)/(AttackNum);
                for(int i = 0; i<AttackNum;i++){
                    if(myandyou){
                        E_bulletPatten.Instan(20,tran1.position,Quaternion.Euler(0,0,180));
                        E_bulletPatten.Instan(20,tran2.position,Quaternion.Euler(0,0,180));
                    }
                    else{
                        GameObject myattack = Instantiate(IceShot, tran1.position,Quaternion.Euler(0,0,180-(90-Angle) + deltaAngle*(i)));
                        myattack.GetComponent<IceBullet>().SetAwake(Vel);
                        GameObject myattack1 = Instantiate(IceShot, tran2.position,Quaternion.Euler(0,0,180-(90-Angle) + deltaAngle*(i)));
                        myattack1.GetComponent<IceBullet>().SetAwake(Vel);
                    }
                }
                break;
            }
            yield return mydelay;
        }
        GetComponent<Animator>().SetBool("Boss_at1",false);
        yield break;
    }
    IEnumerator Basic_Attack4_Boss3(){
        isBasic = true;
        float Delay = BossLevel == 2 ? 0.15f:0.2f;
        float vel = mylevel[3][0][BossLevel,Boss3Phaze];//[10,12][13,15][16,18]
        int Spread = mylevel[3][1][BossLevel,Boss3Phaze];//[15,15],[18,18],[21,21]
        float delta = 360/Spread;
        float plusAngle = delta/2f;
        int upindex = 0;
        WaitForSeconds mywait = new WaitForSeconds(Delay);
        GetComponent<Animator>().SetBool("Boss_at1",true);
        while(isBasic){
            upindex++;
            if(myandyou){
                for(int i = 0;i<Spread;i++){
                E_bullet temp = ObjectManager.GetBulletObject(0);
                temp.transform.position = transform.position + temp.transform.up;
                temp.transform.rotation = Quaternion.Euler(0,0,i*delta + plusAngle*((upindex%2)));
                temp.Awakebullet();
                }
            }
            else{
                for(int i = 0; i<Spread;i++){
                GameObject mygame = Instantiate(IceShot, transform.position,Quaternion.Euler(0,0,i*delta + plusAngle*(upindex%2)));
                mygame.transform.localPosition += mygame.transform.up;
                mygame.GetComponent<IceBullet>().SetAwake(vel);
            }
            }
            yield return mywait;
        }
        GetComponent<Animator>().SetBool("Boss_at1",false);
        yield break;
    }
    int index;
    int RanClock;
    void Cycle(){
        int a = Random.Range(0,2);
        RanClock = a == 1 ? 1:-1;
        index += Random.Range(1,Boss3PatternList.Count);
        Boss3PatternList[index%Boss3PatternList.Count] = true;
    }
    IEnumerator B3P1(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[0]);
            PatternNum = 1;
            isPattern = true;
            Attacks[0] = StartCoroutine(Basic_Attack3_Boss3());
            Attacks[1] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[1];
            Attacks[2] = StartCoroutine(timetoframe(Vector3.right*RanClock,2.5f,0.5f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[2];
            Attacks[3] = StartCoroutine(timetoframe(Vector3.left*RanClock,5f,1.2f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[3];
            Attacks[4] = StartCoroutine(timetoframe(Vector3.right*RanClock,5f,1.2f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[4];
            Attacks[5] = StartCoroutine(timetoframe(Vector3.left*RanClock,5f,1.2f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[5];
            Attacks[6] = StartCoroutine(MovetoPointByLerp(Origin.position,0.05f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[6];
            isBasic = false;
            yield return Attacks[0];
            isPattern = false;
            Boss3PatternList[0] = false;
            Invoke("Cycle",1f);
        }
    }
    IEnumerator B3P2(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[1]);
            isPattern = true;
            PatternNum = 2;
            Attacks[7] = StartCoroutine(Basic_Attack1_Boss3());
            float x = transform.position.x > 0 ? -1 : 1;
            Attacks[8] = StartCoroutine(MoveToPoint(new Vector3(transform.position.x,3,0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[8];
            Attacks[9] = StartCoroutine(MoveToPoint(new Vector3(x*(2.2f+Random.Range(-.3f,.3f)),2f + Random.Range(0,-0.5f),0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[9];
            isBasic = false;
            Attacks[10] = StartCoroutine(MovetoPointByLerp(new Vector3(x*(2+Random.Range(-.3f,.3f)),Origin.position.y-1,0),0.1f));
            //Attacks[10] = StartCoroutine(MoveToPoint(new Vector3(x*(2+Random.Range(-.3f,.3f)),Origin.position.y-1,0),DelayTable[BossLevel,Boss3Phaze]*0.5f));
            yield return new WaitForSeconds(0.5f);
            Attacks[11] = StartCoroutine(TailAttack(2,15,RanClock,1));
            yield return Attacks[10];
            yield return Attacks[11];
            Attacks[12] = StartCoroutine(Basic_Attack2_Boss3());
            Attacks[13] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[13];
            isBasic =false;
            yield return Attacks[12];
            isPattern = false;
            Boss3PatternList[1] = false;
            Invoke("Cycle",1f);
        }
    }
    IEnumerator B3P3(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[2]);
            int a = Random.Range(0,2);
            PatternNum = 3;
            isPattern = true;
            Attacks[14] = StartCoroutine(Basic_Attack3_Boss3());
            Attacks[15] = StartCoroutine(MovetoPointByLerp(Origin.position,0.1f));
            //Attacks[15] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]*0.5f));
            yield return Attacks[15];
            float X_pos = -2 + Random.Range(-0.5f,.15f);
            float Y_pos = 2f + Random.Range(-.3f,0.3f);
            switch(a){
                case 1:
                Attacks[16] = StartCoroutine(MoveToPoint(new Vector3(X_pos*RanClock,Y_pos,0),1.5f*DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[16];
                isBasic = false;
                yield return Attacks[14];
                Attacks[17] = StartCoroutine(IceBossSkill2(2f));
                yield return new WaitForSeconds(0.5f);
                Attacks[18] = StartCoroutine(timetoframe(Vector3.right*RanClock,2*Mathf.Abs(X_pos),1.5f*DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[18];
                yield return Attacks[17];
                Attacks[19] = StartCoroutine(Basic_Attack1_Boss3());
                Attacks[20] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[20];
                isBasic = false;
                yield return Attacks[19];
                break;
                case 0:
                Attacks[21] = StartCoroutine(MoveToPoint(new Vector3(X_pos*RanClock,Origin.position.y,0),DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[21];
                isBasic = false;
                yield return Attacks[14];
                Attacks[22] = StartCoroutine(IceBossSkill2(2));
                yield return new WaitForSeconds(0.5f);
                Attacks[23] = StartCoroutine(MoveToPoint(new Vector3(0, Y_pos,0),DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[23];
                Attacks[24] = StartCoroutine(MoveToPoint(new Vector3(-X_pos*RanClock,Origin.position.y,0),DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[22];
                yield return Attacks[24];
                Attacks[25] = StartCoroutine(Basic_Attack2_Boss3());
                Attacks[26] = StartCoroutine(MovetoPointByLerp(Origin.position,0.07f));
                //Attacks[26] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]*0.5f));
                yield return Attacks[26];
                isBasic = false;
                yield return Attacks[25];
                break;
            }
            isPattern = false;
            Boss3PatternList[2] = false;
            Invoke("Cycle",1f);
        }
    }
    IEnumerator B3P4(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[3]);
            PatternNum = 4;
            isPattern = true;
            float R = 1.5f + Random.Range(0,0.5f);
            Vector3 Center = Vector3.up*2;
            Attacks[27] = StartCoroutine(Basic_Attack4_Boss3());
            Attacks[28] = StartCoroutine(MoveToPoint(Center + Vector3.up*R,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[28];
            Attacks[29] = StartCoroutine(MoveAlongCircle(Center,R,2f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[29];
            Attacks[30] = StartCoroutine(MovetoPointByLerp(Origin.position,0.1f));
            //Attacks[30] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]*0.5f));
            yield return Attacks[30];
            isBasic = false;
            yield return Attacks[27];
            isPattern = false;
            Boss3PatternList[3] = false;
            Invoke("Cycle",1f);
        }
    }
    IEnumerator B3P5(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[4]);
            PatternNum = 5;
            isPattern = true;
            Attacks[31] = StartCoroutine(MovetoPointByLerp(Origin.position,0.1f));
            //Attacks[31] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]*0.5f));
            yield return Attacks[31];
            Attacks[32] = StartCoroutine(TailAttack(2,30,RanClock,0.7f));
            yield return Attacks[32];
            Attacks[33] = StartCoroutine(MoveAlongCircle(Origin.position - Vector3.up*0.5f,0.5f,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[33];
            yield return new WaitForSeconds(0.3f);
            if(myandyou){
                float a = Character.chartrans.position.x;
                Attacks[34] = StartCoroutine(MoveToPoint(new Vector3(a,-7,0),1.5f*DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[34];
            }
            else{
                Attacks[34] = StartCoroutine(MoveToPoint(Vector3.down*7,1.5f*DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[34];
            }
            yield return new WaitForSeconds(0.5f);
            transform.position = new Vector3(5*RanClock,-1.5f,0);
            yield return new WaitForSeconds(0.5f);
            Attacks[36] = StartCoroutine(Basic_Attack1_Boss3());
            Attacks[35] = StartCoroutine(MoveAlongParaBola(Origin.position + Vector3.left*RanClock*5,tran1.position,1.5f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[35];
            isBasic = false;
            yield return Attacks[36];
            transform.position = Origin.position + Vector3.up*4f;
            yield return new WaitForSeconds(0.7f);
            myco = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]));
            yield return myco;
            isPattern = false;
            Boss3PatternList[4] = false;
            Invoke("Cycle",1f);
        }
    }
    IEnumerator B3P6(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[5]);
            PatternNum = 6;
            isPattern = true;
            float Xpos = 2 + Random.Range(-.5f,.5f);
            float deltaX = Random.Range(0.5f,1.5f);
            Attacks[37] = StartCoroutine(Basic_Attack3_Boss3());
            Attacks[38] = StartCoroutine(MoveToPoint(new Vector3(Xpos*RanClock,Origin.position.y,0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[38];
            int way = transform.position.x > 0 ? -1:1;
            Attacks[39] = StartCoroutine(MovetoPointByLerp(new Vector3(transform.position.x+deltaX*way,2.5f+Random.Range(-.5f,0),0),0.05f));
            //Attacks[39] = StartCoroutine(MoveToPoint(new Vector3(transform.position.x+deltaX*way,2.5f+Random.Range(-.5f,0),0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[39];
            yield return new WaitForSeconds(0.5f);
            Attacks[40] = StartCoroutine(MovetoPointByLerp(new Vector3(transform.position.x + deltaX*way,Origin.position.y,0),0.05f));
            //Attacks[40] = StartCoroutine(MoveToPoint(new Vector3(transform.position.x + deltaX*way,Origin.position.y,0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[40];
            yield return new WaitForSeconds(0.5f);
            isBasic = false;
            yield return Attacks[37];
            isPattern = false;
            Boss3PatternList[5] = false;
            Invoke("Cycle",1f);
        }
    }
    //중 난이도
    IEnumerator B3P7(){  
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[6]);
            PatternNum = 7;
            isPattern = true;
            Attacks[41] = StartCoroutine(Basic_Attack4_Boss3());
            Attacks[42] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[42];
            Attacks[43] = StartCoroutine(MoveToPoint(new Vector3(2.5f*RanClock,3.7f,0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[43];
            Attacks[44] = StartCoroutine(MoveToPoint(new Vector3(-2.5f*RanClock,3.4f,0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[44];
            Attacks[45] = StartCoroutine(MoveToPoint(new Vector3(2.5f*RanClock,3f,0),DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[45];
            if(BossLevel == 2){
                Attacks[46] = StartCoroutine(MoveToPoint(new Vector3(-2.5f*RanClock,2.5f,0),DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[46];
                Attacks[47] = StartCoroutine(MoveToPoint(new Vector3(2.5f*RanClock,2f,0),DelayTable[BossLevel,Boss3Phaze]));
                yield return Attacks[47];
            }
            isBasic = false;
            yield return Attacks[41];
            Attacks[48] = StartCoroutine(MoveToPoint(Vector3.up*2,DelayTable[BossLevel,Boss3Phaze]));
            Attacks[49] = StartCoroutine(IceBossSkill2(2f));
            yield return Attacks[48];
            yield return new WaitForSeconds(0.5f);
            Attacks[50] = StartCoroutine(MoveToPoint(Origin.position,2*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[50];
            yield return Attacks[49];
            isPattern = false;
            Boss3PatternList[6] = false;
            Invoke("Cycle",1f);
        }
    }
    //상 난이도 => 즉사기
    IEnumerator B3P8(){
        
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss3PatternList[7]);
            PatternNum = 8;
            isPattern = true;
            Attacks[51] = StartCoroutine(Basic_Attack2_Boss3());
            Attacks[52] = StartCoroutine(MoveAlongParaBola(new Vector3(2.5f*RanClock,2.5f,0),transform.position,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[52];
            Attacks[53] = StartCoroutine(MoveAlongParaBola(new Vector3(-2.5f*RanClock,1,0),transform.position,DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[53];
            isBasic = false;
            yield return Attacks[51];
            Attacks[54] = StartCoroutine(IceBossSkill2(2f));
            Attacks[55] = StartCoroutine(MoveAlongParaBola(Origin.position,transform.position,2.5f*DelayTable[BossLevel,Boss3Phaze]));
            yield return Attacks[55];
            yield return Attacks[54];
            isPattern = false;
            Boss3PatternList[7] = false;
            Invoke("Cycle",1f);
        }
    }

}
