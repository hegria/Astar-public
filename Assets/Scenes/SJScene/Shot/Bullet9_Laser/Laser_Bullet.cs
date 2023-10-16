using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Bullet : MonoBehaviour
{
    [HideInInspector]
    public float Lerp_speed,Laser_size;
    public AudioSource sound;
    public void SetAwake(){
        sound.Play();
        StartCoroutine(Laser_start());
    }
    IEnumerator Laser_start()
    {
        for(int i = 0; i < 50; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(Laser_size, transform.localScale.y, 0), Lerp_speed);
            yield return null;
        }
        transform.localScale = new Vector3(Laser_size, transform.localScale.y, 0);
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i< 50; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, transform.localScale.y, 0), Lerp_speed);
            yield return null;
        }
        transform.localScale = new Vector3(0, transform.localScale.y, 0);
        Bullet_Object_Pooling.ReturnObject(11,gameObject);
    }
}
