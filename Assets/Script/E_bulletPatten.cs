using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_bulletPatten : MonoBehaviour
{
    // Start is called before the first frame update
    public static E_bulletPatten patten; 
    
    void Start()
    {
        patten = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void E_bulletGen(Transform e_transform, int type){
        switch(type){
            case 0:
                Type0Gen(e_transform);
                break;
            case 1:
                Type1Gen(e_transform);
                break;
            case 2:
                Type2Gen(e_transform);
                break;
            case 3:
                Type3Gen(e_transform);
                break;
            case 4:
                Type4Gen(e_transform);
                break;
            case 5:
                Type5Gen(e_transform);
                break;
            case 7:
                Type7Gen(e_transform);
                break;
            case 8:
                Type8Gen(e_transform);
                break;
            case 9:
                Type9Gen(e_transform);
                break;
            case 10:
                TypeBoss1Gen(e_transform);
                break;
            case 11:
                TypeBoss1Gen_2(e_transform);
                break;
            case 12:
                TypeBoss2Gen_1_r(e_transform);
                break;      
            case 13:
                TypeBoss2Gen_1_l(e_transform);
                break;         
            case 14:
                TypeBoss2Gen_2(e_transform);
                break;
            case 15:
                TypeBoss4Gen_1(e_transform);
                break;
            case 16:
                TypeBoss4Gen_2(e_transform);
                break;
            case 17:
                TypeBoss4Gen_3_l(e_transform);
                break;
            case 18:
                TypeBoss4Gen_3_r(e_transform);
                break;
            case 19:
                TypeBoss4Gen_4(e_transform);
                break;
            case 101:
                Type101Gen(e_transform);
                break;
            case 102:
                Type102Gen(e_transform);
                break;
        }
    }

    //직선 하나 소환 (일반이)
    public static void Type0Gen(Transform e_transform){
        E_bullet temp = ObjectManager.GetBulletObject(10);
        temp.transform.position = e_transform.position;
        temp.transform.rotation = Quaternion.Euler(0,0,180f);
        temp.Awakebullet();
    }
    // 유도 하나 (돌격이)
    public static void Type1Gen(Transform e_transform){
        E_bullet temp = ObjectManager.GetBulletObject(11);
        temp.transform.position = e_transform.position;
        temp.Awakebullet();
    }
    // 양쪽에서 하나 (위치)
    
    public static void Type2Gen(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<3;i++){
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,150f+30f*i);
            temp.Awakebullet();
        }
    }

    //양쪽에서 두개 솜

    public static void Type3Gen(Transform e_transform){
        E_bullet temp;
        Vector3 temp_vec;
        for(int i =0;i<2;i++){
            temp = ObjectManager.GetBulletObject(20);
            temp_vec = e_transform.position;
            if(i == 0){
                temp_vec.x += 0.2f;
            }else{
                temp_vec.x -= 0.2f;
            }
            temp.transform.position = temp_vec;
            temp.transform.rotation = Quaternion.Euler(0,0,180f);
            temp.Awakebullet();
        }
    }
    // 360도 18개
    public static void Type10Gen(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<18;i++){
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,i*20f);
            temp.Awakebullet();
        }
    }
    public static void Tpye10GenBeta(Transform e_transform, int SpreadNum, float Radius){
        E_bullet temp;
        float deltaTheta = 360/SpreadNum;
        for(int i =0;i<SpreadNum;i++){
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_transform.position + e_transform.up*Radius;
            temp.transform.rotation = Quaternion.Euler(0,0,i*deltaTheta);
            temp.Awakebullet();
        }
    }
    // 상하좌우 0.4
    public static void Type7Gen(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<1;i++){
            temp = ObjectManager.GetBulletObject(31);
            temp.transform.position = e_transform.position;
            temp.StartCoroutine(temp.AwakegivenTime(0.5f,8f));
        }
    }
    // 5발 10도씩(한 번에 발사)
    public static void Type5Gen(Transform e_transform){
        patten.StartCoroutine(Type5(e_transform));
    }
    public static IEnumerator Type5(Transform e_transform){
        for (int i = 0; i < 5;i++){
            E_bullet temp;
            if(i==4){
                temp = ObjectManager.GetBulletObject(30);
                temp.transform.position = e_transform.position;
                temp.transform.rotation = Quaternion.Euler(0,0,180);
                temp.StartCoroutine(temp.AwakegivenTime(0.5f, 7f));
            }else{
                temp = ObjectManager.GetBulletObject(20);
                temp.transform.position = e_transform.position;
                int offset = Random.Range(-60,61);
                temp.transform.rotation = Quaternion.Euler(0,0,180+offset);
                temp.Awakebullet();
            }
            yield return new WaitForSeconds(0.05f);
        }
            
    }
    public static void Type9Gen(Transform e_transform){
        patten.StartCoroutine(Type9(e_transform));
    }
    public static IEnumerator Type9(Transform e_transform){
        for (int i = 0; i < 3;i++){
            E_bullet temp;
            temp = ObjectManager.GetBulletObject(20);
            temp.transform.rotation = Quaternion.Euler(0,0,180f);
            
            temp.transform.position = e_transform.position;
            temp.Awakebullet(12);
            yield return new WaitForSeconds(0.1f);
        }
            
    }

    // 360도로 쏘는거 하나 Gen

    public static void Type101Gen(Transform e_transform){
        for (int i = 0; i < 1;i++){
            E_bullet temp = ObjectManager.GetBulletObject(23);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,180f);
            temp.Awakebullet(10);
        }
    }

    public static void Type102Gen(Transform e_transform){
        patten.StartCoroutine(Type102(e_transform));
    }
    public static IEnumerator Type102(Transform e_transform)
    {
        for (int i = 0; i < 4;i++){
            E_bullet temp = ObjectManager.GetBulletObject(20);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,180f);
            temp.Awakebullet();
            yield return new WaitForSecondsRealtime(0.12f);
        }
    }
    // x값 랜덤하게 탄 발사
    public static void TypeBoss1Gen(Transform e_transform){
        E_bullet temp = ObjectManager.GetBulletObject(20);
        temp.transform.position = e_transform.position;
        int offset = Random.Range(-50,51);
        temp.transform.rotation = Quaternion.Euler(0,0,180+offset);
        temp.Awakebullet();
    }

    // 3발 30도씩(한 번에 발사)
 
    // 3발 오른쪽으로 20도 간격으로 발사
    public static void TypeBoss2Gen_1_r(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<3;i++){
            temp = ObjectManager.GetBulletObject(20);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,110-i*20f);
            temp.Awakebullet();
        }
    }   public static void TypeBoss1Gen_2(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<3;i++){
            temp = ObjectManager.GetBulletObject(20);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,150+i*30f);
            temp.Awakebullet();
        }
    }
    // 3발 왼쪽으로 20도 간격으로 발사
    public static void TypeBoss2Gen_1_l(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<3;i++){
            temp = ObjectManager.GetBulletObject(20);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,250+i*20f);
            temp.Awakebullet();
        }
    }

    // 5발 10도씩(하나씩 발사)
    public static void TypeBoss2Gen_2(Transform e_transform){
        E_bullet temp;
        for(int i =0; i<6; i++){
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,150+i*10);
            temp.Awakebullet();
        }
    }

    public static void TypeBoss4Gen_1(Transform e_transform){
        E_bullet temp = ObjectManager.GetBulletObject(0);
        temp.transform.position = e_transform.position;
        int offset = Random.Range(-80,80);
        temp.transform.rotation = Quaternion.Euler(0,0,180+offset);
        temp.Awakebullet();
    }   
    public static void TypeBoss4Gen_2(Transform e_transform){
        E_bullet temp;
        for(int i =0; i<15; i++){
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,120+i*8);
            temp.Awakebullet();
        }
    }
    public static void TypeBoss4Gen_3_r(Transform e_transform){
        E_bullet temp = ObjectManager.GetBulletObject(4);
        temp.transform.position = e_transform.position;
        temp.transform.rotation = Quaternion.Euler(0,0,220);
        temp.Awakebullet();
    }
    public static void TypeBoss4Gen_3_l(Transform e_transform){
        E_bullet temp = ObjectManager.GetBulletObject(5);
        temp.transform.position = e_transform.position;
        temp.transform.rotation = Quaternion.Euler(0,0,-40);
        temp.Awakebullet();
    }
    public static void TypeBoss4Gen_4(Transform e_transform){
        E_bullet temp;
        for(int i =0; i<3; i++){
        temp = ObjectManager.GetBulletObject(12); 
        temp.transform.rotation = Quaternion.Euler(0,0,150 + i*20);   
        temp.transform.position = e_transform.position;
        temp.Awakebullet();
        }
    }
    // 5발 40도씩(한 번에 발사)
    public static void Type4Gen(Transform e_transform){
        patten.StartCoroutine(Type4(e_transform));
    }
    public static IEnumerator Type4(Transform e_transform){
        E_bullet temp;
        for(int i =0; i<3; i++){
            if(i==1){
                temp = ObjectManager.GetBulletObject(30);
            }else{
                temp = ObjectManager.GetBulletObject(20);
            }
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,150+i*30f);
            temp.Awakebullet();
            yield return new WaitForSeconds(0.05f);
        }
    }

    // 5발 10도씩(하나씩 발사)
    public static void Type8Gen(Transform e_transform){
        patten.StartCoroutine(Type8GenLogic(e_transform));
    }
    public static IEnumerator Type8GenLogic(Transform e_transform){
        E_bullet temp;
        for(int i =0;i<3;i++){
            temp = ObjectManager.GetBulletObject(20);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,160+i*20f); 
            temp.Awakebullet();
            yield return new WaitForSeconds(0.05f);
        }   
    }
    //랜덤 각도로 잰 + 여러개 쌍으로 나감 -> 조정예정
    public static void RandomGen(Transform e_Transform, int Mycase){
        E_bullet temp;
        float Arg = Random.Range(135, 225);
        switch(Mycase){
            case 0 : 
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_Transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,Arg); 
            temp.Awakebullet();
            break;
        }
    }
    public static void Instan(int Item, Vector3 e_Vector, Quaternion EulerAngle){
        E_bullet temp;
        temp = ObjectManager.GetBulletObject(Item);
        temp.transform.position = e_Vector;
        temp.transform.rotation = EulerAngle;
        temp.Awakebullet();
    }
}
