using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Made_Laser : MonoBehaviour
{
    public GameObject main_laser;
    public float laser_size, up_speed = 0.1f;
    public bool EndLaser;
    Animator myAn;
    public AudioSource audioSource;
    public void SetAwake(float MyLaserSize, float MyLaserSpeed){
        EndLaser = false;
        myAn = GetComponent<Animator>();
        myAn.speed = 1/MyLaserSpeed;
        StartCoroutine(Made(MyLaserSize));
    }
    public void BulletLaserOn(){
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public void BulletLaserOff(){
        GetComponent<BoxCollider2D>().enabled = false;
        EndLaser = true;
    }
    IEnumerator Made(float MyLaserSize)
    {
        transform.localScale = new Vector3(5*MyLaserSize,transform.localScale.y,transform.localScale.z);
        yield return new WaitForSeconds(0.15f);
        audioSource.Play();
        GetComponent<Animator>().SetTrigger("LaserOn");
        yield return new WaitUntil(()=>EndLaser);
        Bullet_Object_Pooling.ReturnObject(9,gameObject);
        // GameObject mylaser = Bullet_Object_Pooling.GetObject(11);
        // mylaser.transform.position = transform.position;
        // mylaser.transform.localRotation = Quaternion.identity;
        // mylaser.transform.SetParent(Character.chartrans);
        // mylaser.transform.localScale = new Vector3(0, transform.localScale.y, 0);
        // mylaser.GetComponent<Laser_Bullet>().Lerp_speed = MyUpSize;
        // mylaser.GetComponent<Laser_Bullet>().Laser_size = MyLaserSize;
        // mylaser.GetComponent<Laser_Bullet>().SetAwake();
        // Bullet_Object_Pooling.ReturnObject(9,gameObject);
    }
}
