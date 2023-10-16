using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot2 : MonoBehaviour
{
    GameObject my_Bullet2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bullet2_shot());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Bullet2_shot()
    {
        WaitForSeconds mywait = new WaitForSeconds(0.2f);
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        for(int i = 0; i<2; i++)
                        {
                            my_Bullet2 = Bullet_Object_Pooling.GetObject(2);
                            my_Bullet2.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            my_Bullet2.transform.localRotation = Quaternion.identity;
                            my_Bullet2.GetComponent<tri_shot>().SetAwake(i+1);
                        }
                        break;
                    case 2:
                        for(int i =0; i<4; i++)
                        {
                            my_Bullet2 = Bullet_Object_Pooling.GetObject(2);
                            my_Bullet2.transform.localRotation = Quaternion.identity;
                            if (i < 2)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position + Vector3.right*0.6f + Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i+1);
                            }
                            else
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+Vector3.left*0.6f+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i-1);
                            }
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 6; i++)
                        {
                            my_Bullet2 = Bullet_Object_Pooling.GetObject(2);
                            my_Bullet2.transform.localRotation = Quaternion.identity;
                            if (i < 2)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ new Vector3(2, 1, 1) * 0.3f+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i+1);
                            }
                            else if(i>=2 && i<4)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ new Vector3(-2, 1, 1) * 0.3f+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i-1);
                            }
                            else if(i>=4)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i-1);
                            }
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 8; i++)
                        {
                            my_Bullet2 = Bullet_Object_Pooling.GetObject(2);
                            my_Bullet2.transform.localRotation = Quaternion.identity;
                            if (i < 2)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ new Vector3(2, 1, 1) * 0.3f+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i+1);
                            }
                            else if(i>=2 && i<4)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ new Vector3(-2, 1, 1) * 0.3f+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i-1);
                            }
                            else if( i<6)
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i-1);
                            }
                            else
                            {
                                my_Bullet2.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                                my_Bullet2.GetComponent<tri_shot>().SetAwake(i-1);
                            }
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 6; i++)
                        {
                            my_Bullet2 = Bullet_Object_Pooling.GetObject(2);
                            my_Bullet2.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            my_Bullet2.transform.localRotation = Quaternion.identity;
                            my_Bullet2.GetComponent<tri_shot>().SetAwake(i+1);
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
