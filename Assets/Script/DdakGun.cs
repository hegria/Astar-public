using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdakGun : MonoBehaviour
{
    public GameObject bullet,bullet2,bullet3,tri_bullet4,tri_bullet5;
    Rigidbody2D bulletrig;
    Vector2 bulletvec;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Character.chartrans.position;
    }    
    IEnumerator Fire(){
        yield return new WaitForSeconds(1f);
        while(true){
            if(!Character.charact.isCleared&&!Character.charact.isboom){
                switch(Character.charact.power)
                {
                    case 1:
                        Instantiate(bullet, transform.position, new Quaternion(0, 0, 0, 0));
                        break;

                    case 2:
                        Instantiate(bullet2, transform.position, new Quaternion(0, 0, 0, 0));
                        break;
                    
                    case 3:
                        Instantiate(bullet3, transform.position, new Quaternion(0, 0, 0, 0));
                        break;

                    case 4:
                        Instantiate(tri_bullet4, transform.position, Quaternion.identity);
                        break;

                    case 5:
                        Instantiate(tri_bullet5, transform.position, Quaternion.identity);
                        break;
                       
                }
            }
            else if (Character.charact.isCleared){
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
