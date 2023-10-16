using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    float theta;
    public float speed;
    // Start is called before the first frame update
    public void SetAwake()
    {
        int a = Random.Range(0,3);
        if(a<2){
            gameObject.transform.rotation = Quaternion.Euler(0,0,Random.Range(-10f,10f));
        }
        else{
            gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
        }
        gameObject.GetComponent<Rigidbody2D>().AddForce(speed * transform.up,ForceMode2D.Impulse);
        // theta = Random.Range(60f, 120f)*Mathf.Deg2Rad;
        // gameObject.GetComponent<Rigidbody2D>().AddForce(speed * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)), ForceMode2D.Impulse);
    }
    private void Update() {
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(8,gameObject);
        }
    }
}
