using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bomb_button : MonoBehaviour
{
    GameObject player;
    Image Boom_Img;
    public GameObject basic_bomb_shot;
    private void Awake()
    {
        Boom_Img = GetComponent<Image>();
    }
    public void bomb()
    {
        if(Boom_Img.fillAmount == 1)
        {
            StartCoroutine(fill_up(2f));
            GameObject mine = Instantiate(basic_bomb_shot, Character.chartrans.position, Quaternion.identity);
            mine.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        }
    }
    IEnumerator fill_up(float cool)
    {
        float a = cool;
        Boom_Img.fillAmount = 0;
        while(cool >=0)
        {
            cool -= Time.deltaTime;
            Boom_Img.fillAmount = (a-cool)/a;
            if(cool > 1f)
            {
                Boom_Img.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0:F0}", cool);
            }
            else
            {
                Boom_Img.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0:F1}", cool);
            }
            yield return new WaitForFixedUpdate();
        }
        Boom_Img.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<b>Boom";
    }
}
