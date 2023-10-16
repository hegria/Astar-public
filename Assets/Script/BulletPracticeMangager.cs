using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPracticeMangager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gameObjects;
    void Start()
    {
        foreach (GameObject gameObjecta in gameObjects){
            gameObjecta.GetComponent<E_bullet>().Awakebullet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
