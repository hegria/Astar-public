using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoolTime : MonoBehaviour
{
    Image myimg;
    //public GameObject basic_bomb_shot;
    public void bomb()
    {
        myimg = GetComponent<Image>();
        if(myimg.fillAmount == 1)
        {
            StartCoroutine(fill_up(3f));
        }
    }
    IEnumerator fill_up(float cool)
    {
        float a = cool;
        myimg.fillAmount = 0;
        while(cool >=0)
        {
            cool -= Time.deltaTime;
            myimg.fillAmount = (a-cool)/a;
            if(cool > 1f)
            {
                myimg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0:F0}", cool);
            }
            else
            {
                myimg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0:F1}", cool);
            }
            yield return new WaitForFixedUpdate();
        }
        myimg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<b>Boom";
    }
}
