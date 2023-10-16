using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot8 : MonoBehaviour
{
    GameObject myshot;
    float zen_time, myPO_instansiate;
    void Start()
    {
        StartCoroutine(Bullet8_shot());
    }

    IEnumerator Bullet8_shot()
    {
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            { 
                switch (Character.charact.power)
                {
                    case 1:
                        zen_time = 0.1f;
                        myshot = Bullet_Object_Pooling.GetObject(8);
                        myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                        myshot.transform.localRotation = Quaternion.identity;
                        myshot.GetComponent<MachineGun>().SetAwake();
                        break;
                    case 2:
                        zen_time = 0.07f;
                        myshot = Bullet_Object_Pooling.GetObject(8);
                        myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                        myshot.transform.localRotation = Quaternion.identity;
                        myshot.GetComponent<MachineGun>().SetAwake();
                        break;
                    case 3:
                        zen_time = 0.05f;
                        myshot = Bullet_Object_Pooling.GetObject(8);
                        myshot.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                        myshot.transform.localRotation = Quaternion.identity;
                        myshot.GetComponent<MachineGun>().SetAwake();
                        break;
                    case 4:
                        zen_time = 0.08f;
                        for(int i = 0; i<2;i++){
                            myshot = Bullet_Object_Pooling.GetObject(8);
                            myshot.transform.position = Character.chartrans.position- Vector3.right*0.2f + Vector3.right*0.4f*i+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<MachineGun>().SetAwake();
                        }
                        break;
                    case 5:
                        zen_time = 0.06f;
                        for(int i = 0; i<2;i++){
                            myshot = Bullet_Object_Pooling.GetObject(8);
                            myshot.transform.position = Character.chartrans.position- Vector3.right*0.2f + Vector3.right*0.4f*i+ Vector3.up*0.42f;
                            myshot.transform.localRotation = Quaternion.identity;
                            myshot.GetComponent<MachineGun>().SetAwake();
                        }
                        break;
                }
            }
            else if (Character.charact.isCleared)
            {
                yield break;
            }
            yield return new WaitForSeconds(zen_time);
        }
    }
}
