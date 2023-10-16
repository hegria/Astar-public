using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5 : MonoBehaviour
{   
    public int Boss5Level,Boss5Phaze;
    public static Boss5 boss5;
    public GameObject Myshot, mySubLaser,mylaser;
    bool isSkill,isbasic;
    public bool ismove;
    Transform shotStartpoint;
    public Transform Origin;
    private int[][][,] levlst = new int[7][][,];
    private float[,] DelayTable = new float[2,2]{{1.6f,1.4f},{1.2f,1f}}; 
    List<IEnumerator> mycoLst = new List<IEnumerator>();
    List<int> PlusMinus = new List<int>(new int[]{-1,1});
    bool isPattern;
    List<IEnumerator> mycos;
    List<bool> Boss5PatternList = new List<bool>();
    [SerializeField]
    Transform[] transet;
    float ParentSize;
    Animator myani;
    public bool MyandYou;
    List<Coroutine> Attacks = new List<Coroutine>();
    Coroutine myco = null;
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
    IEnumerator speedUp(float Standard,float EndValue,float myTime){
        float fixtime = myTime;
        float startStand = Standard;
        while(fixtime > 0){
            fixtime-=Time.deltaTime;
            Standard = startStand + (EndValue-startStand)*(1-fixtime/myTime);
            yield return null;
        }
    }
    E_bullet temp;
    //Basic1 레벨 디자인 => ShotNum[6,8,8,10],StartAngle = 15, Velocity[10,15,15,20]
    IEnumerator Basic1_Boss5(float StartAngle,float Cool,int myclock){
        isbasic = true;
        WaitForSeconds mycool = new WaitForSeconds(Cool);
        while(isbasic){
            int ShotNum = levlst[0][0][Boss5Level,Boss5Phaze];
            float velo = levlst[0][0][Boss5Level,Boss5Phaze];
            float deltaAngle = 2*(90-StartAngle)/ShotNum;
            if(MyandYou){
                if(myclock > 0){
                for(int i = 0; i<ShotNum;i++){
                    float Arg = StartAngle + deltaAngle*i;
                    temp = ObjectManager.GetBulletObject(34);
                    temp.transform.position = transet[0].position;
                    temp.transform.rotation = Quaternion.Euler(0,0,Arg+180);
                    temp.Awakebullet();
                }
                }
                else{
                for(int i = 0; i<ShotNum;i++){
                    float Arg = StartAngle + deltaAngle*i;
                    temp = ObjectManager.GetBulletObject(35);
                    temp.transform.position = transet[0].position;
                    temp.transform.rotation = Quaternion.Euler(0,0,Arg+180);
                    temp.Awakebullet();
                }
                }
            }
            else{
                for(int i =0;i<ShotNum;i++){
                float Arg = StartAngle+deltaAngle*i;
                GameObject mishot = Instantiate(Myshot,transet[0].position,Quaternion.Euler(0,0,Arg));
                mishot.transform.localScale *=0.7f;
                mishot.GetComponent<Boss5_bullet>().clock = myclock;
                mishot.GetComponent<Boss5_bullet>().setAwake(velo,2);
            }
            }
            yield return mycool;
        }
        yield break;
    }//기본 난사 공격

    GameObject Ba2Shot;
    //Basic2-LevelDesign => ShotNum[3,4,4,5], SpreadNum[6,6,8,8], Vel[10,11,14,15];
    IEnumerator Basic_Attack2_Boss5(){
        float Cool = levlst[1][0][Boss5Level,Boss5Phaze]/10f;
        float Vel = levlst[1][2][Boss5Level,Boss5Phaze];;//[10,12][13,15][16,18]
        int SpreadNum = levlst[1][1][Boss5Level,Boss5Phaze];
        float StartArgument = 30;
        WaitForSeconds mydelay = new WaitForSeconds(Cool);
        int a = RanClock == 0? 2:3;
        isbasic = true;
        while(isbasic)
        {
            float Arg = Random.Range(StartArgument + 90, 270-StartArgument);
            switch(Boss5Level){
                case 0:
                for(int i = 0; i<3;i++){
                    if(i == 0){
                        if(MyandYou){
                            E_bullet temp = ObjectManager.GetBulletObject(30+a);
                        temp.transform.position = transet[0].position;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.SpreadNum = SpreadNum;
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(Myshot,transet[0].position, Quaternion.Euler(0,0,Arg-180));
                            mygame.GetComponent<Boss5_bullet>().setAwake(Vel,1);
                        }
                    }
                    else{
                        if(!MyandYou){
                        GameObject mygame = Instantiate(Myshot,transet[0].position,Quaternion.Euler(0,0,Arg-180));
                        mygame.transform.localPosition += -mygame.transform.up*.5f + mygame.transform.right*.3f - (i-1)*mygame.transform.right*0.6f;
                        mygame.GetComponent<Boss5_bullet>().setAwake(Vel,1);
                        }
                        else{
                            E_bullet temp = ObjectManager.GetBulletObject(30+a);
                        temp.transform.position = transet[0].position;
                        temp.transform.localPosition += -temp.transform.up*.5f + temp.transform.right*.3f - (i-1)*temp.transform.right*0.6f;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.Awakebullet();
                        }
                    }
                }
                yield return mydelay;
                break;
                case 1:
                for(int i = 0; i<5;i++){
                    if(i == 0){
                        if(MyandYou){
                        E_bullet temp = ObjectManager.GetBulletObject(30+a);
                        temp.transform.position = transet[0].position;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.SpreadNum = SpreadNum;
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(Myshot,transet[0].position, Quaternion.Euler(0,0,Arg-180));
                        mygame.GetComponent<Boss5_bullet>().setAwake(Vel,1);
                        }
                    }
                    else if(i<3){
                        if(MyandYou){
                            E_bullet temp = ObjectManager.GetBulletObject(30+a);
                        temp.transform.position = transet[0].position;
                        temp.transform.localPosition += -temp.transform.up*.5f + temp.transform.right*.3f - (i-1)*temp.transform.right*0.6f;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.SpreadNum = SpreadNum;
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(Myshot,transet[0].position,Quaternion.Euler(0,0,Arg-180));
                        mygame.transform.localPosition += -mygame.transform.up*0.5f + mygame.transform.right*.3f - (i-1)*mygame.transform.right*0.6f;
                        mygame.GetComponent<Boss5_bullet>().setAwake(Vel,1);
                        }
                    }
                    else{
                        if(MyandYou){
                            E_bullet temp = ObjectManager.GetBulletObject(30+a);
                        temp.transform.position = transet[0].position;
                        temp.transform.localPosition += -temp.transform.up + temp.transform.right*.6f - (i-3)*temp.transform.right*1.2f;
                        temp.transform.rotation = Quaternion.Euler(0,0,Arg);
                        temp.SpreadNum = SpreadNum;
                        temp.Awakebullet();
                        }
                        else{
                            GameObject mygame = Instantiate(Myshot,transet[0].position,Quaternion.Euler(0,0,Arg-180));
                        mygame.transform.localPosition += -mygame.transform.up + mygame.transform.right*0.6f - (i-3)*mygame.transform.right*1.2f;
                        mygame.GetComponent<Boss5_bullet>().setAwake(Vel,1);
                        }
                    }
                }
                yield return mydelay;
                break;
            }
        }
        yield break;
    }
    //Basic3 Level => ShotNum[3,3,3,3], Delay[1,0.9,0.7,0.5]; Vel[10,11,12,13]; 
    IEnumerator Basic3_Boss5(float Cool){
        isbasic = true;
        WaitForSeconds mycool = new WaitForSeconds(Cool);
        while(isbasic){
            int ShotNum = levlst[2][0][Boss5Level,Boss5Phaze];
            float Delay = levlst[2][1][Boss5Level,Boss5Phaze]/10f;
            float velo = levlst[2][2][Boss5Level,Boss5Phaze];
            if(MyandYou){
                temp = ObjectManager.GetBulletObject(41);
            temp.transform.position = transet[0].position;
            temp.transform.rotation = Quaternion.Euler(0,0,180);
            GameObject myTemp = temp.gameObject;
            Coroutine myco1 = StartCoroutine(Big_size(myTemp,.5f,0.3f/ParentSize,Delay));
            yield return myco1;
            myTemp.transform.SetParent(null);
            temp.Awakebullet();
            }
            else{
                GameObject mishot = Instantiate(Myshot,transet[0].position,Quaternion.identity,transet[0]);
                Coroutine myco = StartCoroutine(Big_size(mishot,0.5f,2/ParentSize,Delay));
                yield return myco;
                mishot.transform.SetParent(null);
                mishot.GetComponent<Boss5_bullet>().setAwake(velo,0);
            }
            yield return mycool;
        }
        yield break;
    }//거대한 공격

    IEnumerator Big_size(GameObject mygm,float startSize,float Size,float Delay){
        mygm.transform.localScale = Vector3.one*startSize;
        float fixtime = Delay;
        while(fixtime > 0){
            fixtime -=Time.deltaTime;
            mygm.transform.localScale = Vector3.one*Size*(1-fixtime/Delay);
            yield return null;
        }
        yield break;
    }//속도를 시간으로 정하도록 수정할 것(완료)
    Coroutine laserCo;
    //Skill1 Level => LaserNum[1,3,3,3], LaserVel[30,35,40,45]
    bool wing;
    public void WingStart(){
        wing = true;
    }
    public void WingEnd(){
        wing = false;
    }
    bool laseron;
    public void LaserStart(){
        offlaser = false;
        laseron = true;
        for(int i = 0; i<30;i++){
            myani.speed = Mathf.Lerp(myani.speed,14/180f,0.1f);
        }
        myani.speed = 14/180f;
    }
    public void LaserActionEnd(){
        laseron = false;
        myani.SetBool("Bress",false);
    }
    bool offlaser;
    public void LaserEnd(){
        offlaser = true;
        for(int i = 0; i<30; i++){
            myani.speed = Mathf.Lerp(myani.speed,1,0.1f);
        }
        myani.speed = 1;
    }
    IEnumerator Skill1_Boss5(float lasertime){
            yield return new WaitUntil(()=>!isSkill);
            isSkill = true;
            int LaserNum = levlst[3][0][Boss5Level,Boss5Phaze];
            float LaserVel = levlst[3][1][Boss5Level,Boss5Phaze];
            yield return new WaitUntil(()=>!wing);
            GetComponent<Animator>().SetBool("Bress",true);
            yield return new WaitUntil(()=>laseron);
            GameObject mysub = Instantiate(mySubLaser,transet[0].position -  Vector3.up*0.42f,Quaternion.identity,transform);
            mysub.GetComponent<SpriteRenderer>().color = new Color32(255,209,0,255);
            Coroutine mybig = StartCoroutine(Big_size(mysub,0,1/ParentSize,.5f));
            for(int i = 0; i<LaserNum;i++){
                GameObject minelaser = Instantiate(mylaser,mysub.transform);
                minelaser.GetComponent<SpriteRenderer>().color = new Color32(255,209,0,255);
                minelaser.transform.position = mysub.transform.position;
                laserCo=StartCoroutine(Laser(minelaser,i,lasertime,20,LaserVel,.15f));
            }
            yield return new WaitUntil(()=>movelaser);
            mysub.GetComponent<Rotation>().SetAwake(levlst[6][0][Boss5Level,Boss5Phaze]);
            yield return laserCo;
            yield return new WaitUntil(()=>!laseron);
            Destroy(mysub);
            movelaser = false;
            isSkill = false;
            yield break;
    }//브레스
    bool movelaser;
    IEnumerator Laser(GameObject myla,int Lasertype,float Waittime, float Laser_Size, float Up,float down){
        Vector3 DeltaTheta = (Vector3.down*3f-transform.position -  Vector3.up*0.42f);
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
        yield return new WaitUntil(()=>offlaser);
        //yield return new WaitForSeconds(Waittime);
        for(int i = 0; i<50;i++){
            myla.transform.localScale = Vector3.Lerp(myla.transform.localScale,new Vector3(0,myla.transform.localScale.y,myla.transform.localScale.z),down);
            yield return null;
        }
        yield break;
    }
    
    bool skill2;
    public void Skill2On(){
        for(int i = 0; i<60;i++){
            myani.speed = Mathf.Lerp(myani.speed,0.3f,0.1f);
        }
        myani.speed = 0.3f;
        skill2 = true;
    }
    public void Skill2Action(){
        for(int i = 0; i<60;i++){
            myani.speed = Mathf.Lerp(myani.speed,1,0.1f);
        }
        myani.speed = 1;
    }
    public void Skill2Off(){
        skill2 = false;
        myani.SetBool("Wing",false);
    }
    public void Skill2Timing(){
        GoSkill = true;
    }
    bool GoSkill;
    //Skill2 Level => Size[3,4,4,5], Vel[10,12,14,16];
    IEnumerator Skill2_Boss5(){
            yield return new WaitUntil(()=>!isSkill);
            GoSkill = false;
            float BulletSize = levlst[4][0][Boss5Level,Boss5Phaze]/100f;
            float Vel = levlst[4][1][Boss5Level,Boss5Phaze];
            isSkill =true;
            yield return new WaitUntil(()=>!wing);
            myani.SetBool("Wing",true);
            yield return new WaitUntil(()=>skill2);
            yield return new WaitForSeconds(0.3f);
            yield return new WaitUntil(()=>GoSkill);
            for(int i = 0; i<2; i++){
                if(MyandYou){
                    temp = ObjectManager.GetBulletObject(40);
                    temp.transform.localScale = Vector3.one*0.3f;
                    temp.transform.position = transet[i+1].position;
                    temp.transform.rotation = Quaternion.Euler(0,0,180);
                    temp.transform.localScale = Vector3.one*BulletSize;;
                    temp.Awakebullet();
                }
                else{
                    GameObject myAir = Instantiate(Myshot,transet[i+1].position,Quaternion.identity);
                myAir.transform.localScale *=BulletSize;
                myAir.GetComponent<Boss5_bullet>().setAwake(Vel,0);
                }
            }
            yield return new WaitUntil(()=>!skill2);
            isSkill = false;
            yield break;
    }//날개 -> 폭풍
    bool Skill3;
    public void Skill3SpeedOn(){
        for(int i = 0; i<60;i++){
            myani.speed = Mathf.Lerp(myani.speed,0.5f,0.1f);
        }
        myani.speed = 0.5f;
    }
    public void Skill3SpeedOff(){
        for(int i = 0; i<60;i++){
            myani.speed = Mathf.Lerp(myani.speed,1,0.1f);
        }
        myani.speed = 1;
    }
    public void Skill3ActionOn(){
        Skill3 = true;
    }
    public void Skill3ActionOff(){
        myani.SetBool("Iron",false);
        Skill3=false;
    }
    //Skill3 Level => ShotNum[8,10,10,12], Vel[10,12,14,16]
    IEnumerator TailAttack(float Radius, float StartArgment, int ClockWise){
        yield return new WaitUntil(()=>!isSkill); // 스킬 사용까지 대기
        isSkill = true;
        float saveAngle;
        int shotNum = levlst[5][0][Boss5Level,Boss5Phaze];
        float vel = levlst[5][1][Boss5Level,Boss5Phaze];
        float deltaTheta = (90-StartArgment)*2/shotNum*Mathf.Deg2Rad;
        saveAngle = (90-(90-StartArgment)*ClockWise)*Mathf.Deg2Rad;
        yield return new WaitUntil(()=>wing);
        myani.SetBool("Iron",true);
        yield return new WaitUntil(()=>Skill3);
        int a = Boss5Level == 2 ? 3:0;
        for(int i = 0; i<shotNum;i++){
            float theta = saveAngle + i*deltaTheta*ClockWise;
            if(MyandYou){
                E_bullet temp;
                temp = ObjectManager.GetBulletObject(30+a);
                temp.transform.position = transet[0].position-new Vector3(Mathf.Cos(theta)*Radius,Mathf.Sin(theta)*Radius);
                temp.transform.rotation = Quaternion.Euler(0,0,theta*Mathf.Rad2Deg + 90);
                temp.Awakebullet();
            }
            else{
                GameObject myfire = Instantiate(Myshot, transet[0].position-new Vector3(Mathf.Cos(theta)*Radius,Mathf.Sin(theta)*Radius,0),Quaternion.identity);
            myfire.transform.Rotate(new Vector3(0,0,theta*Mathf.Rad2Deg-90));
            myfire.GetComponent<Boss5_bullet>().setAwake(vel,0);
            }
        }
        yield return new WaitUntil(()=>!Skill3);
        isSkill = false;
        yield break;
    }
    WaitForSeconds PatternDelay = new WaitForSeconds(.5f);
    //패턴 1 : 움직이며 레이져
    IEnumerator B5P1(){
        int plus;
        while(true){
            yield return new WaitUntil(()=> !isPattern&&Boss5PatternList[0]);
            isPattern = true;
            if(MyandYou){
                plus = transform.position.x > Character.chartrans.position.x ? -1 : 1;
            }
            else{
                plus = transform.position.x > 0 ? -1 : 1;
            }
            Attacks[0] = StartCoroutine(MoveToPoint(new Vector3(2*plus,3,0),DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[0];
            Attacks[1] = StartCoroutine(Skill1_Boss5(2));
            yield return new WaitForSeconds(1f);
            StartCoroutine(timetoframe(Vector3.left*plus,4,3*DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[1];
            Attacks[2] =StartCoroutine(MoveToPoint(Origin.position,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[2];
            yield return PatternDelay;
            Boss5PatternList[0] = false;
            isPattern = false;
            Invoke("Cycle", 1f);
        }
    }
    Coroutine mybaco1,mybaco2;
    //패턴 2 : 곡선 탄 난사
    IEnumerator B5P2(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[1]);
            isPattern = true;
        int plus = transform.position.x > 0 ? 1 : -1;
        Attacks[3] = StartCoroutine(timetoframe(Vector3.down, 1.5f + Random.Range(-0.5f,-0.5f),DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[3];
        StartCoroutine(Basic1_Boss5(10,0.2f,plus));
        StartCoroutine(Basic1_Boss5(10,0.2f,-plus));
        yield return new WaitForSeconds(2f);
        Attacks[4] = StartCoroutine(MoveToPoint(Origin.position,2*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[4];;
        isbasic = false;
        yield return PatternDelay;
        isPattern = false;
        Boss5PatternList[1] = false;
        Invoke("Cycle", 1f);
        }
    }
    //패턴 3 : 기본 샷 2번 난사
    IEnumerator B5P3(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[2]);
            isPattern = true;
        Attacks[5] = StartCoroutine(timetoframe(Vector3.down,0.5f+Random.Range(-.5f,.5f),DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[5];
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Basic_Attack2_Boss5());
        yield return new WaitForSeconds(3f);
        Attacks[6] = StartCoroutine(MoveToPoint(Origin.position,2f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[6];
        isbasic = false;
        yield return PatternDelay;
        isPattern = false;
        Boss5PatternList[2] = false;
        Invoke("Cycle", 1f);
        }
    }
    //패턴 4 : 앞에서 레이져 쏘기
    Coroutine mycr;
    IEnumerator B5P4(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[3]);
            isPattern = true;
        StartCoroutine(Basic_Attack2_Boss5());
        int plus = transform.position.x > 0 ? -1:1;
        Attacks[7] = StartCoroutine(MoveToPoint(new Vector3(2.5f*plus,2.5f,0), .5f*DelayTable[Boss5Level,Boss5Phaze]));
        isbasic = false;
        yield return Attacks[7];
        Attacks[9] = StartCoroutine(Skill1_Boss5(2));
        yield return Attacks[9];
        Attacks[8] = StartCoroutine(MoveToPoint(Origin.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[8];
        yield return PatternDelay;
        isPattern= false;
        Boss5PatternList[3] = false;
        Invoke("Cycle", 1f);
        }
    }
    //패턴 5 : 꼬리로 공격
        IEnumerator B5P5(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[4]);
            isPattern = true;
        Attacks[10] = StartCoroutine(MoveToPoint(Origin.position,1.5f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[10];
        Attacks[11] = StartCoroutine(MoveAlongCircle(Origin.position + Vector3.down*0.3f,0.3f,0.5f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[11];
        Attacks[12] = StartCoroutine(TailAttack(1,40,1));
        yield return Attacks[12];
        Attacks[13] = StartCoroutine(MoveToPoint(Origin.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[13];
        isPattern = false;
        Boss5PatternList[4] = false;
        Invoke("Cycle", 1f);
        }
    }
    IEnumerator B5P6(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[5]);
            isPattern = true;
        StartCoroutine(Basic1_Boss5(20,0.2f,-1));
        Attacks[14] = StartCoroutine(MoveToPoint(Origin.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[14];
        Attacks[15] =StartCoroutine(Skill2_Boss5());
        yield return Attacks[15];
        StartCoroutine(MoveToPoint(Vector3.right*Random.Range(-2.5f,2.5f) + Vector3.down*Random.Range(0,1f)*2f + Vector3.up*transform.position.y,.5f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return new WaitForSeconds(2f);
        Attacks[16] =StartCoroutine(MoveToPoint(Origin.position,0.5f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[16];
        isbasic = false;
        yield return PatternDelay;
        isPattern= false;
        Boss5PatternList[5] = false;
        Invoke("Cycle", 1f);
        }
    }
    IEnumerator B5P7(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[6]);
            isPattern = true;
        int a = Random.Range(0,2);
        StartCoroutine(mycoLst[a]);
        Attacks[17] = StartCoroutine(MoveToPoint(Origin.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[17];
        Attacks[18] = StartCoroutine(timetoframe(Vector3.right*RanClock,2f,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[18];
        isbasic = false;
        StartCoroutine(Skill1_Boss5(2));
        Attacks[19] = StartCoroutine(timetoframe(Vector3.down,2f,DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[19];
        Attacks[20] = StartCoroutine(timetoframe(Vector3.left*RanClock, 4f, 2f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[20];
        StartCoroutine(mycoLst[(a+1)%3]);
        Attacks[21] = StartCoroutine(timetoframe(Vector3.up,2f,1*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[21];
        Attacks[22] = StartCoroutine(MoveToPoint(Origin.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[22];
        yield return PatternDelay;
        isPattern = false;
        Boss5PatternList[6]=false;
        Invoke("Cycle", 1f);
        isbasic = false;
        }
    }
    IEnumerator B5P8(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[7]);
            isPattern = true;
        Attacks[23] = StartCoroutine(MoveToPoint(new Vector3(3*RanClock,3,0),DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[23];
        Attacks[24] = StartCoroutine(MoveToPoint(Vector3.up*2f,0.5f*DelayTable[Boss5Level,Boss5Phaze]));
        StartCoroutine(mycoLst[Random.Range(0,2)]);
        yield return Attacks[24];
        Attacks[25] = StartCoroutine(TailAttack(1,40,RanClock));
        yield return new WaitForSeconds(1.5f);
        yield return Attacks[25];
        Attacks[26] = StartCoroutine(MoveToPoint(new Vector3(-3*RanClock,3,0),DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[26];
        Attacks[27] = StartCoroutine(MoveLerp(Vector3.right*RanClock,3,0.1f));
        isbasic = false;
        yield return Attacks[27];
        yield return PatternDelay;
        isPattern = false;
        Boss5PatternList[7]= false;
        Invoke("Cycle", 1f);
        }
    }
    IEnumerator B5P9(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[8]);
            isPattern = true;
        int way = transform.position.x > 0 ? -1 : 1;
        Attacks[28] = StartCoroutine(MoveToPoint(new Vector3(transform.position.x,10,0),2f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[28];
        transform.position = new Vector3(8f*way,3,0);
        yield return new WaitForSeconds(1f);
        Attacks[29] =StartCoroutine(timetoframe(Vector3.left*way,16f,3f*DelayTable[Boss5Level,Boss5Phaze]));
        StartCoroutine(Basic_Attack2_Boss5());
        yield return Attacks[29];
        isbasic = false;
        transform.position = Vector3.up*10;
        yield return new WaitForSeconds(1f);
        Attacks[30] = StartCoroutine(MoveToPoint(Origin.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
        yield return Attacks[30];
        yield return PatternDelay;
        isPattern = false;
        Boss5PatternList[8]= false;
        Invoke("Cycle", 1f);
        }
    }
    IEnumerator B5P10(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[9]);
            isPattern = true;
            StartCoroutine(Basic_Attack2_Boss5());
            Attacks[31] = StartCoroutine(MoveToPoint(new Vector3(2*RanClock,1,0),1f*DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[31];
            isbasic = false;
            Attacks[32] = StartCoroutine(Skill1_Boss5(2f));
            Attacks[33] = StartCoroutine(MoveAlongParaBola(Vector3.up*2,transform.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[33];
            Attacks[34] = StartCoroutine(MoveAlongParaBola(new Vector3(-2*RanClock,1.5f,0),transform.position,1f*DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[34];
            yield return Attacks[32];
            StartCoroutine(Basic3_Boss5(0.2f));
            Attacks[35] = StartCoroutine(timetoframe(Vector3.down,0.5f,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[35];
            Attacks[36] = StartCoroutine(MoveAlongParaBola(Origin.position,transform.position,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[36];
            isbasic = false;
            isPattern = false;
            Boss5PatternList[9] = false;
            Invoke("Cycle", 1f);;
        }
    }
    IEnumerator B5P11(){
        while(true){
            yield return new WaitUntil(()=>!isPattern&&Boss5PatternList[10]);
            isPattern = true;
            StartCoroutine(Basic1_Boss5(15,0.2f,1));
            StartCoroutine(Basic1_Boss5(15,0.3f,-1));
            Attacks[37] = StartCoroutine(timetoframe(Vector3.right*RanClock,2f,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[37];
            Vector3 a = transform.position;
            Attacks[38] = StartCoroutine(MoveAlongCircle(a - Vector3.up*0.5f,0.5f,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[38];
            isbasic =false;
            Attacks[39] = StartCoroutine(MoveAlongParaBola(Vector3.up,transform.position,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[39];
            Attacks[40] = StartCoroutine(TailAttack(1,15,1));
            yield return Attacks[40];
            Attacks[41] = StartCoroutine(Skill2_Boss5());
            Attacks[42] = StartCoroutine(MoveToPoint(Origin.position,DelayTable[Boss5Level,Boss5Phaze]));
            yield return Attacks[41];
            yield return Attacks[42];
            isPattern = false;
            Boss5PatternList[10] = false;
            Invoke("Cycle", 1f);
        }
    }
    int RanClock = 1;
    public int BP;
    public bool P;
    float Hp;
    private void Start() {
        boss5 = this;
        Boss5Phaze = 0;
        if(MyandYou){
            Boss5Level = PlayerInfo.playerInfo.level-2;
            Hp = GetComponent<Enemy>().energy;
        }
        shotStartpoint = transform.GetChild(1);
        levlst[0] = new int[2][,];
        levlst[1] = new int[3][,];
        levlst[2] = new int[3][,];
        levlst[3] = new int[2][,];
        levlst[4] = new int[2][,];
        levlst[5] = new int[2][,];
        levlst[6] = new int[1][,];

        levlst[0][0] = new int[2,2]{{6,8},{8,10}};
        levlst[0][1] = new int[2,2]{{10,15},{15,20}};
        levlst[1][0] = new int[2,2]{{5,5},{4,4}};
        levlst[1][1] = new int[2,2]{{3,3},{4,4}};
        levlst[1][2] = new int[2,2]{{10,11},{14,15}};
        levlst[2][0] = new int[2,2]{{3,3},{3,3}};
        levlst[2][1] = new int[2,2]{{10,9},{7,5}};
        levlst[2][2] = new int[2,2]{{10,11},{12,13}};
        levlst[3][0] = new int[2,2]{{1,3},{3,3}};
        levlst[3][1] = new int[2,2]{{30,35},{40,45}};
        levlst[4][0] = new int[2,2]{{40,40},{45,50}};
        levlst[4][1] = new int[2,2]{{10,12},{14,16}};
        levlst[5][0] = new int[2,2]{{12,14},{16,18}};
        levlst[5][1] = new int[2,2]{{10,12},{14,16}};
        levlst[6][0] = new int[2,2]{{15,15},{20,25}};
        mycoLst.Add(Basic1_Boss5(10,.2f,1));
        mycoLst.Add(Basic_Attack2_Boss5());
        mycoLst.Add(Basic3_Boss5(0.5f));
        myani = GetComponent<Animator>();
        if(Boss5Level == 0){
            mycos = new List<IEnumerator>(new IEnumerator[]{B5P1(),B5P2(),B5P3(),B5P4(),B5P5(),B5P6(),B5P7(),B5P8()});
        }
        if(Boss5Level == 1){
            mycos = mycos = new List<IEnumerator>(new IEnumerator[]{B5P1(),B5P2(),B5P3(),B5P4(),B5P5(),B5P6(),B5P7(),B5P8(),B5P9(),B5P10(),B5P11()});
        }
        for(int i = 0; i<mycos.Count;i++){
            Boss5PatternList.Add(false);
        }
        for(int i = 0;i<56;i++){
            Attacks.Add(myco);
        }
        foreach(var a in mycos){
            StartCoroutine(a);
        }
        ParentSize = transform.localScale.x;
        StartCoroutine(MoveToPoint(Origin.position,1f));
        Invoke("Cycle",2);
    }
    int index;
    void Cycle(){
        RanClock = PlusMinus[Random.Range(0,2)];
        index += Random.Range(1,Boss5PatternList.Count);
        Boss5PatternList[index%Boss5PatternList.Count] = true;
    }
    private void Update() {
        if(P){
            Boss5PatternList[BP-1] = true;
            P=false;
        }
        if(GetComponent<Enemy>().energy < Hp/2f&&MyandYou&&Boss5Phaze<1){
            Boss5Phaze++;
        }
    }
}
