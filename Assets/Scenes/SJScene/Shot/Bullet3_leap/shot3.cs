using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot3 : MonoBehaviour
{
    GameObject myBullet;
    GameObject MyBarrier;
    List<int> BarrierCnt = new List<int>(new int[]{3,3,4,4,5});
    //cex
    // Start is called before the first frame update
    void Start()
    {
        Character.charact.isBarrier = true;
        MyBarrier = transform.GetChild(0).gameObject;
        StartCoroutine(Bullet3_shot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Bullet3_shot()
    {
        WaitForSeconds mywait = new WaitForSeconds(0.3f);
        while (true)
        {
            if(!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                    Check_Barrier();
                    myBullet = Bullet_Object_Pooling.GetObject(3);
                    myBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                    myBullet.transform.localRotation = Quaternion.identity;
                    myBullet.GetComponent<spread_script>().SetAwake(0);
                    myBullet.GetComponent<Sphere>().setAwake(0);
                    myBullet.GetComponent<Spin_obejct>().setAwake();
                    break;
                    case 2:
                    Check_Barrier();
                    myBullet = Bullet_Object_Pooling.GetObject(3);
                    myBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                    myBullet.transform.localRotation = Quaternion.identity;
                    myBullet.GetComponent<spread_script>().SetAwake(2);
                    myBullet.GetComponent<Sphere>().setAwake(0);
                    myBullet.GetComponent<Spin_obejct>().setAwake();
                    break;
                    case 3:
                    Check_Barrier();
                    myBullet = Bullet_Object_Pooling.GetObject(3);
                    myBullet.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                    myBullet.transform.localRotation = Quaternion.identity;
                    myBullet.GetComponent<spread_script>().SetAwake(3);
                    myBullet.GetComponent<Sphere>().setAwake(0);
                    myBullet.GetComponent<Spin_obejct>().setAwake();
                        break;
                    case 4:
                    Check_Barrier();
                        for(int i = 0; i < 2; i++)
                        {
                            myBullet = Bullet_Object_Pooling.GetObject(3);
                            myBullet.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            myBullet.transform.localRotation = Quaternion.identity;
                            myBullet.GetComponent<spread_script>().SetAwake(3);
                            myBullet.GetComponent<Sphere>().setAwake(i+1);
                            myBullet.GetComponent<Spin_obejct>().setAwake();
                        }
                        break;
                    case 5:
                    Check_Barrier();
                        for (int i = 0; i < 3; i++)
                        {
                            myBullet = Bullet_Object_Pooling.GetObject(3);
                            myBullet.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            myBullet.transform.localRotation = Quaternion.identity;
                            myBullet.GetComponent<spread_script>().SetAwake(3);
                            if(i == 0){
                                myBullet.GetComponent<Sphere>().setAwake(i);
                            }
                            else{
                               myBullet.GetComponent<Sphere>().setAwake(i+2); 
                            }
                            myBullet.GetComponent<Spin_obejct>().setAwake();
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
    void Check_Barrier(){
        if(Character.charact.isBarrier){
            MyBarrier.GetComponent<Barrier_manage>().SetAwake(BarrierCnt[Character.charact.power -1]);
            Character.charact.isBarrier = false;
        }
    }
}
