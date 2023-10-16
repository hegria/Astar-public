using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot6 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bullet6_shot());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Bullet6_shot()
    {
        WaitForSeconds mywait = new WaitForSeconds(0.7f);
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        for(int i = 0; i < 1; i++)
                        {
                            GameObject mygame = Bullet_Object_Pooling.GetObject(6);
                            mygame.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            mygame.transform.localRotation = Quaternion.identity;
                            mygame.GetComponent<Sword_Bullet>().SetAwake(i,1.5f);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject mygame = Bullet_Object_Pooling.GetObject(6);
                            mygame.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            mygame.transform.localRotation = Quaternion.identity;
                            mygame.GetComponent<Sword_Bullet>().SetAwake(0,1.5f*(1+i));
                            yield return new WaitForSeconds(0.1f);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject mygame = Bullet_Object_Pooling.GetObject(6);
                            mygame.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            mygame.transform.localRotation = Quaternion.identity;
                            mygame.GetComponent<Sword_Bullet>().SetAwake(0,3);
                            yield return new WaitForSeconds(0.1f);
                        }
                        break;
                    case 4:
                        for(int j = 0; j<1; j++)
                        {
                            List<int> mylst = new List<int>(new int[]{4,2,2});
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject mygame = Bullet_Object_Pooling.GetObject(6);
                                mygame.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                                mygame.transform.localRotation = Quaternion.identity;
                                mygame.GetComponent<Sword_Bullet>().SetAwake(i,mylst[i]*0.75f);
                            }
                            //yield return new WaitForSeconds(0.1f);
                        }
                        break;
                    case 5:
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject mygame = Bullet_Object_Pooling.GetObject(6);
                                mygame.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                                mygame.transform.localRotation = Quaternion.identity;
                                mygame.GetComponent<Sword_Bullet>().SetAwake(i,3);
                            }
                            // yield return new WaitForSeconds(0.1f);
                            // GameObject mygame1 = Bullet_Object_Pooling.GetObject(6);
                            // mygame1.transform.position = Character.chartrans.position+ Vector3.up*0.42f;
                            // mygame1.transform.localRotation = Quaternion.identity;
                            // mygame1.GetComponent<Sword_Bullet>().SetAwake(0,3);
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
