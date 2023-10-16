using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_moving : MonoBehaviour
{
    float r;//각속도 시간에 따라 증가하는 변수
    public float Rotation_Velocity,Angle_Speed; // 1. 반지름이 올라가는 속도 변수 2. 각속도 조절 변수
    [HideInInspector]
    public Vector3 parpos; // 이건 탄이 터지기전 위치 받아오기(이 탄을 삭제시키기 위해 받아옴)
    Coroutine myco;
    public void setAwake(float Angle){
        r = 0;
        Angle_Speed = 10f;
        transform.rotation = Quaternion.Euler(0,0,Angle);
        myco = StartCoroutine(circle());
    } // 이건 자체 시작함수로 rotation으로 회전, r 초기화, 회전 코루틴 시작;

    // Update is called once per frame
    void Update()
    {
        r += Time.deltaTime;
        //Angle_Speed -= Time.deltaTime;
        if (Vector3.Distance(parpos, transform.position) > 3f)
        {
            StopCoroutine(myco);
            Bullet_Object_Pooling.ReturnObject(10,gameObject);
        }
    }
    IEnumerator circle()
    {
        while (true)
        {
            transform.Translate( new Vector3(Mathf.Cos(r*Angle_Speed+Mathf.PI/2f), Mathf.Sin(r*Angle_Speed + Mathf.PI / 2f), 0) * r * Rotation_Velocity);
            yield return null;
        }
    }
}
