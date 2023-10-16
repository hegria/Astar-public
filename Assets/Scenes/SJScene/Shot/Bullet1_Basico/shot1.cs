using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot1 : MonoBehaviour
{
    GameObject myBullet, SSgBullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bullet1_shot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Bullet1_shot()
    {
        WaitForSeconds mywait = new WaitForSeconds(0.2f);
        while (true)
        {
            if(!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        myBullet = Bullet_Object_Pooling.GetObject(1);
                        myBullet.transform.position = Character.chartrans.position+Vector3.up*0.42f;
                        myBullet.transform.localRotation = Quaternion.identity;
                        myBullet.GetComponent<straight_shot>().setAwake(0);
                        break;
                    case 2:
                        for(int i = 0; i < 2; i++)
                        {
                            myBullet = Bullet_Object_Pooling.GetObject(1);
                            myBullet.transform.position = Character.chartrans.position - Vector3.right * 0.2f + i*Vector3.right*0.4f+ Vector3.up*0.42f;
                            myBullet.transform.localRotation = Quaternion.identity;
                            myBullet.GetComponent<straight_shot>().setAwake(0);
                        }
                        break;
                    case 3:
                    SSgBullet = Bullet_Object_Pooling.GetObject(12);
                    SSgBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                    SSgBullet.transform.localRotation = Quaternion.identity;
                    SSgBullet.GetComponent<SSgBullet>().setAwake(0);
                        for (int i = 0; i < 3; i++)
                        {
                            myBullet = Bullet_Object_Pooling.GetObject(1);
                            myBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myBullet.transform.localRotation = Quaternion.identity;
                            myBullet.GetComponent<straight_shot>().setAwake(i);

                        }
                        break;
                    case 4:
                    SSgBullet = Bullet_Object_Pooling.GetObject(12);
                    SSgBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                    SSgBullet.transform.localRotation = Quaternion.identity;
                    SSgBullet.GetComponent<SSgBullet>().setAwake(0);
                        for (int i = 0; i < 4; i++)
                        {
                            myBullet = Bullet_Object_Pooling.GetObject(1);
                            myBullet.transform.localRotation = Quaternion.identity;
                            if( i < 2)
                            {
                                myBullet.transform.position = Character.chartrans.position - Vector3.right * 0.2f + i*Vector3.right*0.4f+ Vector3.up*0.42f;
                                myBullet.GetComponent<straight_shot>().setAwake(0);
                            }
                            else
                            {
                                myBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                                myBullet.GetComponent<straight_shot>().setAwake(i+1);
                            }
                        }
                        break;
                    case 5:
                        for(int j = 0; j<2; j++){
                            SSgBullet = Bullet_Object_Pooling.GetObject(12);
                            SSgBullet.transform.position = Character.chartrans.position - Vector3.right * 0.2f + j*Vector3.right*0.4f+ Vector3.up*0.42f;
                            SSgBullet.transform.localRotation = Quaternion.identity;
                            SSgBullet.GetComponent<SSgBullet>().setAwake(0);
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            myBullet = Bullet_Object_Pooling.GetObject(1);
                            myBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myBullet.transform.localRotation = Quaternion.identity;
                            myBullet.GetComponent<straight_shot>().setAwake(i);
                        }
                        break;
                }
            }
            else if (Character.charact.isCleared)
            {
                yield break;
            }
            yield return mywait;
        }
    }
}
