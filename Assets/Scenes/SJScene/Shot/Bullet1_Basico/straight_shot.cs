using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straight_shot : MonoBehaviour
{
    public int shot_case = 0;
    float theta;
    public float speed;
    public void setAwake(int Bullet_num){
        counting(Bullet_num);
        gameObject.GetComponent<Rigidbody2D>().AddForce(speed*new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)), ForceMode2D.Impulse);
    }
    private void Update() {
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(1,gameObject);
        }
    }
    void counting(int number)
    {
        switch (number)
        {
            case 0:
                theta = Mathf.PI/2f;
                break;
            case 3:
                theta = 5 / 12f*Mathf.PI;
                break;
            case 4:
                theta = 7 / 12f * Mathf.PI ;
                break;
            case 1:
                theta = 85 / 180f * Mathf.PI;
                break;
            case 2:
                theta = 95 / 180f * Mathf.PI;
                break;
        }
    }
}
