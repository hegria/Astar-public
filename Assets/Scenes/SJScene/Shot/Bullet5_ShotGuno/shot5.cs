using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot5 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Bullet5_shot());
    }

    IEnumerator Bullet5_shot()
    {
        WaitForSeconds mywait = new WaitForSeconds(0.45f);
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        for(int i = 0; i < 5; i++)
                        {
                            GameObject myshot = Bullet_Object_Pooling.GetObject(5);
                            myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<Bullet6_lerp>().SetAwake(0);
                        }
                        break;
                    case 2:
                        for (int i = 0; i <8; i++)
                        {
                            GameObject myshot = Bullet_Object_Pooling.GetObject(5);
                            myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<Bullet6_lerp>().SetAwake(0);
                        }
                        
                        break;
                    case 3:
                        for (int i = 0; i < 11; i++)
                        {
                            GameObject myshot = Bullet_Object_Pooling.GetObject(5);
                            myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<Bullet6_lerp>().SetAwake(0);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < 14; i++)
                        {
                            GameObject myshot = Bullet_Object_Pooling.GetObject(5);
                            myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<Bullet6_lerp>().SetAwake(1);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < 17; i++)
                        {
                            GameObject myshot = Bullet_Object_Pooling.GetObject(5);
                            myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<Bullet6_lerp>().SetAwake(1);
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
