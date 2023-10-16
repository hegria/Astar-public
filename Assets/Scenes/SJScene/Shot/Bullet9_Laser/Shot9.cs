using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot9 : MonoBehaviour
{
    float Zen_time = 1f;
    float y_dis;
    GameObject mylaser;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bullet9_shot());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Bullet9_shot()
    {
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        Zen_time = 1.3f;
                        y_dis = 6f - Character.chartrans.position.y+1.5f;
                        mylaser = Bullet_Object_Pooling.GetObject(9);
                        mylaser.transform.position = new Vector3(Character.chartrans.position.x, Character.chartrans.position.y + y_dis / 2f + 0.42f, 0);
                        mylaser.transform.localRotation = Quaternion.identity;
                        //mylaser.transform.localScale = new Vector3(mylaser.transform.localScale.x, y_dis, 0);
                        mylaser.transform.SetParent(Character.chartrans);
                        mylaser.GetComponent<Made_Laser>().SetAwake(0.4f,Zen_time - 0.2f);
                        break;
                    case 2:
                        Zen_time = 1f;
                        y_dis = 6f - Character.chartrans.position.y+1.5f;
                        mylaser = Bullet_Object_Pooling.GetObject(9);
                        mylaser.transform.position = new Vector3(Character.chartrans.position.x, Character.chartrans.position.y + y_dis / 2f + 0.42f, 0);
                        mylaser.transform.localRotation = Quaternion.identity;
                        //mylaser.transform.localScale = new Vector3(mylaser.transform.localScale.x, y_dis, 0);
                        mylaser.transform.SetParent(Character.chartrans);
                        mylaser.GetComponent<Made_Laser>().SetAwake(0.4f,Zen_time - 0.2f);
                        break;
                    case 3:
                        Zen_time = 0.8f;
                        y_dis = 6f - Character.chartrans.position.y+1.5f;
                        mylaser = Bullet_Object_Pooling.GetObject(9);
                        mylaser.transform.position = new Vector3(Character.chartrans.position.x, Character.chartrans.position.y + y_dis / 2f + 0.42f, 0);
                        mylaser.transform.localRotation = Quaternion.identity;
                        //mylaser.transform.localScale = new Vector3(mylaser.transform.localScale.x, y_dis, 0);
                        mylaser.transform.SetParent(Character.chartrans);
                        mylaser.GetComponent<Made_Laser>().SetAwake(0.4f,Zen_time - 0.2f);
                        break;
                    case 4:
                        Zen_time = 0.8f;
                        y_dis = 6f - Character.chartrans.position.y+1.5f;
                        mylaser = Bullet_Object_Pooling.GetObject(9);
                        mylaser.transform.position = new Vector3(Character.chartrans.position.x, Character.chartrans.position.y + y_dis / 2f + 0.42f, 0);
                        mylaser.transform.localRotation = Quaternion.identity;
                        //mylaser.transform.localScale = new Vector3(mylaser.transform.localScale.x, y_dis, 0);
                        mylaser.transform.SetParent(Character.chartrans);
                        mylaser.GetComponent<Made_Laser>().SetAwake(0.7f,Zen_time - 0.2f);
                        break;
                    case 5:
                        Zen_time = 0.8f;
                        y_dis = 6f - Character.chartrans.position.y+1.5f;
                        mylaser = Bullet_Object_Pooling.GetObject(9);
                        mylaser.transform.position = new Vector3(Character.chartrans.position.x, Character.chartrans.position.y + y_dis / 2f +0.42f, 0);
                        mylaser.transform.localRotation = Quaternion.identity;
                        //mylaser.transform.localScale = new Vector3(mylaser.transform.localScale.x, y_dis, 0);
                        mylaser.transform.SetParent(Character.chartrans);
                        mylaser.GetComponent<Made_Laser>().SetAwake(1f,Zen_time - 0.2f);
                        break;
                }
            }
            else if (Character.charact.isCleared)
            {
                yield break;
            }
            yield return new WaitForSeconds(Zen_time);
        }
    }
}
