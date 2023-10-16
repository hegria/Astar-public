using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tri_shot : MonoBehaviour
{
    float up;
    public float y_axis_speed, Angle_speed;
    float max_Length, kappa;
    float Origin_x_value, Origin_y_value;
    public int shot_case;
    public void SetAwake(int Bullet_num){
        up = 0;
        Origin_x_value = transform.position.x;
        Origin_y_value = transform.position.y;
        StartCoroutine(my_trigo(Bullet_num));
    }
    // Update is called once per frame
    void Update()
    {
        up += Angle_speed*Time.deltaTime;
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(2,gameObject);
        }
    }
    IEnumerator my_trigo(int num)
    {
        switch(num)
        {
            case 1:
                max_Length = 1/2f;
                kappa = 3;
                while (true)
                {
                    transform.position = new Vector3(Origin_x_value + (Mathf.Sin(up*kappa)) * max_Length, Origin_y_value + 0.7f*up*y_axis_speed, 0);
                    yield return null;
                }
            case 2:
                max_Length = 1/2f;
                kappa = 3;
                while (true)
                {
                    transform.position = new Vector3(Origin_x_value - (Mathf.Sin(up*kappa)) * max_Length, Origin_y_value + 0.7f*up *y_axis_speed, 0);
                    yield return null;
                }
            case 3:
                max_Length = 2;
                kappa =2;
                while (true)
                {
                    transform.position = new Vector3(Origin_x_value + (Mathf.Sin(up * kappa)) * max_Length, Origin_y_value + 0.7f * up * y_axis_speed, 0);
                    yield return null;
                }
            case 4:
                max_Length = 2;
                kappa = 2;
                while (true)
                {
                    transform.position = new Vector3(Origin_x_value - (Mathf.Sin(up * kappa)) * max_Length, Origin_y_value + 0.7f * up * y_axis_speed, 0);
                    yield return null;
                }
            case 5:
                max_Length = 2.5f;
                kappa = 1f;
                while (true)
                {
                    transform.position = new Vector3(Origin_x_value + (Mathf.Sin(up * kappa)) * max_Length, Origin_y_value + 0.7f * up * y_axis_speed, 0);
                    yield return null;
                }
            case 6:
                max_Length = 2.5f;
                kappa = 1f;
                while (true)
                {
                    transform.position = new Vector3(Origin_x_value - (Mathf.Sin(up * kappa)) * max_Length, Origin_y_value + 0.7f * up * y_axis_speed, 0);
                    yield return null;
                }
        }
    }
}
