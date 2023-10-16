using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_move : MonoBehaviour
{
    float H, V;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += H * speed * Time.deltaTime;
        pos.y += V * speed * Time.deltaTime;
        transform.position = pos;
    }
}
