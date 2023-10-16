using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide_shot : MonoBehaviour
{
    Vector3 norm_vec;
    float theta;
    public float Out_velocity;
    List<int> shuffle;
    void Start()
    {
        StartCoroutine(Bullet4_shot());
    }
    Vector3 convert_Angle(float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
    private List<T> GetShufffleList<T>(List<T> lst){
        for(int i = lst.Count - 1;i>0;i--){
            int rnd = Random.Range(0,i);
            T temp = lst[i];
            lst[i] = lst[rnd];
            lst[rnd] = temp;
        }
        return lst;
    }
    IEnumerator Bullet4_shot()
    {
        WaitForSeconds mywait = new WaitForSeconds(0.3f);
        while (true)
        {
            norm_vec = new Vector3(Mathf.Cos(Random.Range(0, Mathf.PI)), Mathf.Sin(Random.Range(0, Mathf.PI)), 0);
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        for(int i = 0; i < 1; i++)
                        {
                            theta = 90;
                            GameObject t_mis = Bullet_Object_Pooling.GetObject(4);
                            t_mis.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            t_mis.GetComponent<Rigidbody2D>().velocity = -convert_Angle(theta) * Out_velocity;
                            t_mis.GetComponent<guide>().SetAwake();
                        }
                        break;
                    case 2:
                    shuffle = new List<int>(new int[]{0,1});
                    GetShufffleList(shuffle);
                        for (int i = 0; i < 2; i++)
                        {
                            theta = 180 / 3f * (shuffle[i] + 1);
                            GameObject t_mis = Bullet_Object_Pooling.GetObject(4);
                            t_mis.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            t_mis.GetComponent<Rigidbody2D>().velocity = -convert_Angle(theta) * Out_velocity;
                            t_mis.GetComponent<guide>().SetAwake();
                            yield return new WaitForSeconds(0.1f);
                        }
                        break;
                    case 3:
                    shuffle = new List<int>(new int[]{0,1,2});
                    GetShufffleList(shuffle);
                        for (int i = 0; i < 3; i++)
                        {
                            theta = 180 / 4f * (shuffle[i] + 1);
                            GameObject t_mis = Bullet_Object_Pooling.GetObject(4);
                            t_mis.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            t_mis.GetComponent<Rigidbody2D>().velocity = -convert_Angle(theta) * Out_velocity;
                            t_mis.GetComponent<guide>().SetAwake();
                            yield return new WaitForSeconds(0.1f);
                        }
                        break;
                    case 4:
                    shuffle = new List<int>(new int[]{0,1,2,3});
                    GetShufffleList(shuffle);
                        for (int i = 0; i < 4; i++)
                        {
                            theta = 180 / 5f * (shuffle[i] + 1);
                            GameObject t_mis = Bullet_Object_Pooling.GetObject(4);
                            t_mis.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            t_mis.GetComponent<Rigidbody2D>().velocity = -convert_Angle(theta) * Out_velocity;
                            t_mis.GetComponent<guide>().SetAwake();
                            yield return new WaitForSeconds(0.1f);
                        }
                        break;
                    case 5:
                    shuffle = new List<int>(new int[]{0,1,2,3,4});
                    GetShufffleList(shuffle);
                        for (int i = 0; i < 5; i++)
                        {
                            theta = 180 / 6f * (shuffle[i] + 1);
                            GameObject t_mis = Bullet_Object_Pooling.GetObject(4);
                            t_mis.transform.position = Character.chartrans.position + Vector3.up*0.42f;
                            t_mis.GetComponent<Rigidbody2D>().velocity = -convert_Angle(theta) * Out_velocity;
                            t_mis.GetComponent<guide>().SetAwake();
                            yield return new WaitForSeconds(0.1f);
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
