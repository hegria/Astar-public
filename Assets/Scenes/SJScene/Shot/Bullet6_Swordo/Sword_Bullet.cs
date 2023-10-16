using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Bullet : MonoBehaviour
{
    public int shot_case;
    float seta;
    public float speed, up_speed;
    public float radius;
    Vector3 mysize;
    public AudioSource audioSource;
    private void Update() {
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(6,gameObject);
        }
    }
    private void Awake() {
        mysize = transform.localScale;
    }
    public void SetAwake(int Shot_case, float Radius){
        audioSource.Play();
        transform.localScale = mysize;
        counting(Shot_case);
        transform.Rotate(new Vector3(0, 0, seta * Mathf.Rad2Deg-90));
        gameObject.GetComponent<Rigidbody2D>().AddForce(speed * new Vector2(Mathf.Cos(seta), Mathf.Sin(seta)), ForceMode2D.Impulse);
        StartCoroutine(size_up(Radius));
    }
    IEnumerator size_up(float my_radius)
    {
        while(transform.localScale.x < my_radius)
        {
            transform.localScale += mysize * up_speed * Time.deltaTime*my_radius/4f;
            yield return null;
        }
    }
    void counting(int number)
    {
        switch (number)
        {
            case 0:
                seta = Mathf.PI / 2f;
                break;
            case 1:
                seta = 5 / 12f * Mathf.PI;
                break;
            case 2:
                seta = 7 / 12f * Mathf.PI;
                break;
            case 3:
                seta = 80 / 180f * Mathf.PI;
                break;
            case 4:
                seta = 100 / 180f * Mathf.PI;
                break;
        }
    }
}
