using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Laser : MonoBehaviour
{
    private int LaserCase;
    public void SetAwake(int type,float actiontime, float laserSize,float Upsize, float DownSize){
        LaserCase = type;
        StartCoroutine(Laser(actiontime,laserSize,Upsize,DownSize));
    }
    IEnumerator Laser(float Waittime, float Laser_Size, float Up,float down){
        switch(LaserCase){
            case 0 :
            transform.rotation = Quaternion.Euler(0,0,180);
            break;
            case 1:
            transform.rotation = Quaternion.Euler(0,0,150);
            break;
            case 2:
            transform.rotation = Quaternion.Euler(0,0,210);
            break;
        }
        while(transform.localScale.y < Laser_Size){
            float r = Time.deltaTime*Up;
            transform.localScale += r*Vector3.up;
            transform.Translate(Vector3.up*r*.3f);
            yield return null;
        }
        yield return new WaitForSeconds(Waittime);
        for(int i = 0; i<50;i++){
            transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(0,transform.localScale.y,transform.localScale.z),down);
            yield return null;
        }
        Destroy(gameObject);
    }
}
