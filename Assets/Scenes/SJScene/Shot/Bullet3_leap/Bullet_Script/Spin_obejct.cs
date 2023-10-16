using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_obejct : MonoBehaviour
{
    float theta;
    public float angular_Velocity;
    public void setAwake(){
        theta = 0;
        StartCoroutine(spin());
    }
    private void Update()
    {
        theta += Time.deltaTime;
    }
    IEnumerator spin()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, theta*angular_Velocity));
            yield return null;
        }
    }
}
