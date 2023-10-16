using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWSceneControl : MonoBehaviour
{
    public Enemy[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy[0].AwakeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
