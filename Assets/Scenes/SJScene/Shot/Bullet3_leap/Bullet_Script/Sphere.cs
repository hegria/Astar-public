using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public int shot_case = 0;
    float theta;
    public float speed;
    public void setAwake(int Bullet_num){
        counting(Bullet_num);
        gameObject.GetComponent<Rigidbody2D>().AddForce(speed*new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)), ForceMode2D.Impulse);
    }
    void counting(int number)
    {
        switch (number)
        {
            case 0:
                theta = Mathf.PI/2f;
                break;
            case 1:
                theta = 80*Mathf.Deg2Rad;
                break;
            case 2:
                theta = 100*Mathf.Deg2Rad ;
                break;
            case 3:
                theta = 75 / 180f * Mathf.PI;
                break;
            case 4:
                theta = 105 / 180f * Mathf.PI;
                break;
        }
    }
}
