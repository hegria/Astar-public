using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    // Start is called before the first frame update
    // Weapon을 Chracter의 2번째 child로 만들어야하나? 

    public int weaponnum;

    public Sprite[] weaponsprite;
    public SpriteRenderer spriteRenderer;
    
    public void Awakesetting()
    {
        while(true){
            weaponnum = Random.Range(1,9);
            if(weaponnum != PlayerInfo.playerInfo.weaponnum){
                break;
            }
        }
        spriteRenderer.sprite = weaponsprite[weaponnum];
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -Character.ymax){
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            PlayerInfo.playerInfo.weaponnum = weaponnum;
            Character.charact.get_weapon(false);
            Destroy(gameObject);
        }
    }
}
