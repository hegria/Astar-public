using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRange : MonoBehaviour
{
    Vector3 endpoint;
    public void SetAwake(){
        endpoint = transform.position - Vector3.up*2*Camera.main.orthographicSize;
        StartCoroutine(LookRange());
    }
    IEnumerator LookRange(){
        for(int i = 0; i< 30; i++){
            transform.position = Vector3.Lerp(transform.position,endpoint,0.2f);
            yield return null;
        }
    }
}
