using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    bool hello = false;
    float alpha,beta;
    Vector3 myvec;
    float Velocity = 10;
    public void SetAwake(float Vel){
        hello = true;
        Velocity = Vel;
        Vector3 DeltaTheta;
        DeltaTheta = (Character.chartrans.position - transform.position);
        alpha= Mathf.Atan2(DeltaTheta.y,DeltaTheta.x)*Mathf.Rad2Deg;
    }    
    void Update()
    {
        if(hello){
            myvec = (Character.chartrans.position - transform.position);
            beta = Mathf.Atan2(myvec.y,myvec.x)*Mathf.Rad2Deg;
            int r = beta - alpha > 0 ? 1 : -1;
            transform.Rotate(0,0,Time.deltaTime*Velocity*r);
        }
    }
}
