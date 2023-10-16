using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgound : MonoBehaviour
{
    public float velocity;

    public Sprite[] sprites;
    SpriteRenderer im;

    // Start is called before the first frame update
    void Start()
    {
        im = gameObject.GetComponent<SpriteRenderer>();
        switch(PlayerInfo.playerInfo.stage){
            case 1:
                im.sprite = sprites[0];
                break;
            case 2:
                im.sprite = sprites[Random.Range(0, 4) + 1];
                break;
            case 3:
                im.sprite = sprites[Random.Range(0, 4) + 5];
                break;
            case 4:
                im.sprite = sprites[9];
                break;
            case 5:
                im.sprite = sprites[Random.Range(0, 4) + 10];
                break;

        }
    }

    Vector3 govet = new Vector3(0, 6.12f * 3, 0);
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime* new Vector3(0,velocity,0));
        if(transform.position.y<=-12.24f){
            transform.Translate(new Vector3(0, 6.12f * 4, 0));
        }
    }
}

