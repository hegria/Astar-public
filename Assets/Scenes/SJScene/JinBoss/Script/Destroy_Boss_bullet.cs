using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Boss_bullet : MonoBehaviour
{
    public bool sub;
    public Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7f&&!sub){
            Destroy(gameObject);
        }
        if(sub){
            if(Vector3.Distance(pos,transform.position)>4f){
                Destroy(gameObject);
            }
        }
    }
}
