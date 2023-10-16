using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet5_script : MonoBehaviour
{
    public GameObject my_bomb;
    bool check_enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - Character.chartrans.position.y > 5f || check_enemy)
        {
            Instantiate(my_bomb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            check_enemy = true;
        }
    }
}
