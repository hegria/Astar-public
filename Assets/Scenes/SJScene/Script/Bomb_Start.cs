using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Start : MonoBehaviour
{
    public GameObject my_bomb;
    float ypos;
    public float bombtime;
    private void Awake() {
        ypos = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= ypos+5.0f)
        {
            GameObject a = Instantiate(my_bomb, transform.position, Quaternion.identity);
            a.GetComponent<Sub_Bomb_script>().boomtime = bombtime;
            Destroy(gameObject);
        }
    }
}
