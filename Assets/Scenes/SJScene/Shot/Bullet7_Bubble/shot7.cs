using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot7 : MonoBehaviour
{
    GameObject MyBubble;
    float Zen_time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bullet7_shot());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Bullet7_shot()
    {
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (Character.charact.power)
                {
                    case 1:
                        Zen_time = 0.7f;
                        MyBubble = Bullet_Object_Pooling.GetObject(7);
                        MyBubble.transform.position = Character.chartrans.position + Vector3.up * 0.1f+ Vector3.up*0.42f;
                        MyBubble.transform.localRotation = Quaternion.identity;
                        MyBubble.GetComponent<Bubble_shot>().SetAwake();
                        break;
                    case 2:
                        Zen_time = 0.5f;
                        MyBubble = Bullet_Object_Pooling.GetObject(7);
                        MyBubble.transform.position = Character.chartrans.position + Vector3.up * 0.1f+ Vector3.up*0.42f;
                        MyBubble.transform.localRotation = Quaternion.identity;
                        MyBubble.GetComponent<Bubble_shot>().SetAwake();
                        break;
                    case 3:
                        Zen_time = 0.3f;
                        MyBubble = Bullet_Object_Pooling.GetObject(7);
                        MyBubble.transform.position = Character.chartrans.position + Vector3.up * 0.1f+ Vector3.up*0.42f;
                        MyBubble.transform.localRotation = Quaternion.identity;
                        MyBubble.GetComponent<Bubble_shot>().SetAwake();
                        break;
                    case 4:
                        Zen_time = 0.5f;
                        for (int j = 0; j < 2; j++)
                        {
                            MyBubble = Bullet_Object_Pooling.GetObject(7);
                            MyBubble.transform.position = Character.chartrans.position + Vector3.up * 0.1f+ Vector3.up*0.42f;
                            MyBubble.transform.localRotation = Quaternion.identity;
                            MyBubble.GetComponent<Bubble_shot>().SetAwake();
                        }
                        break;
                    case 5:
                        Zen_time = 0.3f;
                        for (int j = 0; j < 2; j++)
                        {
                            MyBubble = Bullet_Object_Pooling.GetObject(7);
                            MyBubble.transform.position = Character.chartrans.position + Vector3.up * 0.1f+ Vector3.up*0.42f;
                            MyBubble.transform.localRotation = Quaternion.identity;
                            MyBubble.GetComponent<Bubble_shot>().SetAwake();
                        }
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
