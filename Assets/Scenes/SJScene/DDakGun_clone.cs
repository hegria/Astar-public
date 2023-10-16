using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDakGun_clone : MonoBehaviour
{
    float delay_time = 1f;
    public bool verify_Bullet4;
    public static DDakGun_clone mygun;
    public static int cnt;
    // Start is called before the first frame update

    
    public int weapon_num = 1;
    [Space(10)]
    public bullet1 mybullet1;
    [Space(10)]
    public bullet2 mybullet2;
    [Space(10)]
    public bullet3 mybullet3;
    [Space(10)]
    public bullet4 mybullet4;
    [Space(10)]
    public bullet5 mybullet5;
    // Start is called before the first frame update
    void Start()
    {
        mygun = this;
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Character.chartrans.position;

        if (verify_Bullet4 && cnt == 0)
        {
            cnt++;
            switch (Character.charact.power)
            {
                case 1:
                    Instantiate(mybullet4.level1, transform.position, Quaternion.identity);
                    break;

                case 2:
                    Instantiate(mybullet4.level2, transform.position, Quaternion.identity);
                    break;

                case 3:
                    Instantiate(mybullet4.level3, transform.position, Quaternion.identity);
                    break;
            }
        }
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (!Character.charact.isCleared && !Character.charact.isboom)
            {
                switch (weapon_num)
                {
                    case 1:
                        delay_time = 0.2f;
                        switch(Character.charact.power)
                        {
                            case 1:
                                Instantiate(mybullet1.level1, transform.position, Quaternion.identity);
                                break;

                            case 2:
                                Instantiate(mybullet1.level2, transform.position, Quaternion.identity);
                                break;

                            case 3:
                                Instantiate(mybullet1.level3, transform.position, Quaternion.identity);
                                break;
                        }
                        break;

                    case 2:
                        delay_time = 0.2f;
                        switch (Character.charact.power)
                        {
                            case 1:
                                Instantiate(mybullet2.level1, transform.position, Quaternion.identity);
                                break;

                            case 2:
                                Instantiate(mybullet2.level2, transform.position, Quaternion.identity);
                                break;

                            case 3:
                                Instantiate(mybullet2.level3, transform.position, Quaternion.identity);
                                break;
                        }
                        break;

                    case 3: // ���� �ӽ��Դϴ�!
                        delay_time = 0.4f;
                        switch (Character.charact.power)
                        {
                            case 1:
                                Instantiate(mybullet3.level_non, transform.position, Quaternion.identity);
                                break;

                            case 2:
                                Instantiate(mybullet3.level_non, transform.position, Quaternion.identity);
                                break;

                            case 3:
                                Instantiate(mybullet3.level_non, transform.position, Quaternion.identity);
                                break;
                        }
                        break;

                    case 4:
                        delay_time = 0.5f;
                        switch (Character.charact.power)
                        {
                            case 1:
                                Instantiate(mybullet5.level1, transform.position, Quaternion.identity);
                                break;

                            case 2:
                                Instantiate(mybullet5.level2, transform.position, Quaternion.identity);
                                break;

                            case 3:
                                Instantiate(mybullet5.level3, transform.position, Quaternion.identity);
                                break;
                        }
                        break;
                }
            }
            else if (Character.charact.isCleared)
            {
                yield break;
            }
            yield return new WaitForSeconds(delay_time);
        }
    }
}
[System.Serializable]
public class bullet1
{
    public GameObject level1, level2, level3;
}
[System.Serializable]
public class bullet2
{
    public GameObject level1, level2, level3;
}
[System.Serializable]
public class bullet3
{
    public GameObject level_non;
}
[System.Serializable]
public class bullet4
{
    public GameObject level1, level2, level3;
}
[System.Serializable]
public class bullet5
{
    public GameObject level1, level2, level3;
}
