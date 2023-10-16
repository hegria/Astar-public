using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_bullet : MonoBehaviour
{
    int distanceNum = 2;
    public int bullettype; // 0 이면 직선 1 이면 유도탄
    public int bulletkind;
    Rigidbody2D bulletrig;
    [HideInInspector]
    public int SpreadNum = 6;
    
    float theta;

    public AudioSource audioSource;

    // Start is called before the first frame update

    public IEnumerator AwakegivenTime(float time, float power = 8f){
        yield return new WaitForSeconds(time);
        Awakebullet(power);
    }
    public void Awakebullet(float power = 8f)
    {
        audioSource.Play();
        theta = 0;
        bulletrig = gameObject.GetComponent<Rigidbody2D>();
        switch(bullettype){
            // 방향 지정해줘야함
            case 0:
                bulletrig.AddForce(power*transform.up,ForceMode2D.Impulse);
                break;
            // 유도탄이니까 알아서 날아감
            case 1:
                Vector3 dirs = Character.chartrans.position - transform.position;
                float angle = Mathf.Atan2(dirs.y,dirs.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bulletrig.AddForce(power*transform.up,ForceMode2D.Impulse);
                break;
            // 일정 시점에서 360도로 퍼짐
            case 2:
            bulletrig.AddForce(power*transform.up,ForceMode2D.Impulse);
            StartCoroutine(Case2(SpreadNum));
            break;
            // 일정 시점에서 전방으로 90도로 퍼짐
            case 3:
            bulletrig.AddForce(power*transform.up,ForceMode2D.Impulse);
            StartCoroutine(Case3(SpreadNum));
            break;
            // 곡선으로 날아감(180도 돌려주어야함)
            case 4:
            StartCoroutine(Go_Trigonal(5,0.7f,1));
            break;
            //이건 case 4의 반대 방향으로(180도 돌려주어야함)
            case 5:
            StartCoroutine(Go_Trigonal(5,0.7f,-1));
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        theta += Time.deltaTime;
        float distance = Vector3.Distance(transform.position,Character.chartrans.position);
        if(!GameManager.gameManager.ispaused&&distance <= distanceNum){
            PlayerInfo.playerInfo.curscore += 1;
        }
        if((transform.position.y <= -Character.ymax - 0.5f||transform.position.y>= Character.ymax+0.5f||transform.position.x <= -Character.xmax - 0.5f||transform.position.x >=Character.ymax+0.5f)||Character.charact.bossdead){
            ObjectManager.ReturnBulletObject(this);
        }
    }
    IEnumerator Case2(int SpreadNum){
        while(true){
            if(transform.position.y < -2.5f){
                Pung1(transform,SpreadNum);
                ObjectManager.ReturnBulletObject(this);
            }
            yield return null;
        }
    }
    IEnumerator Case3(int spreadNum){
        while(true){
            if(transform.position.y < -1f){
                Pung2(transform,spreadNum);
                ObjectManager.ReturnBulletObject(this);
            }
            yield return null;
        }
    }
    void Pung1(Transform e_transform, int SpreadNum){
        E_bullet temp;
        float deltaAngle = 360f/SpreadNum;
        for(int i =0;i<SpreadNum;i++){
            temp = ObjectManager.GetBulletObject(20);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0,i*deltaAngle);
            temp.Awakebullet();
        }
    }
    void Pung2(Transform e_transform, int spreadNum){
        float deltaAngle = 90f/(spreadNum-1);
        E_bullet temp;
        for(int i =0;i<spreadNum;i++){
            temp = ObjectManager.GetBulletObject(0);
            temp.transform.position = e_transform.position;
            temp.transform.rotation = Quaternion.Euler(0,0, 135 + i*deltaAngle);
            temp.Awakebullet();
        }
    }
    IEnumerator Go_Trigonal(float Radius, float Angle, int clock){
        while(true){
            transform.Translate(new Vector3(Mathf.Cos(theta*Angle),clock*Mathf.Sin(theta*Angle),0)*theta*Radius*Time.deltaTime);
            yield return null;
        }
    }
}
