using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int bullettype;
    public int Bullet_Number;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(bullettype == 0)
            {
                other.gameObject.GetComponent<Enemy>().energy -= damage*(1.0f+0.1f*PlayerInfo.playerInfo.workshop[3]);
                other.gameObject.GetComponent<Enemy>().ouchouch();
                if(Bullet_Number == 4){
                    int enemytype = other.gameObject.GetComponent<Enemy>().enemytype;
                    if (!(enemytype == 6 || enemytype == 0 || enemytype == 1))
                    {
                        Explosion ex = ObjectManager.GetExlposionObject(0);
                        ex.transform.position = transform.position;
                    }
                }
                Bullet_Object_Pooling.ReturnObject(Bullet_Number,gameObject);
            } 
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (bullettype == 1)
            {
                other.gameObject.GetComponent<Enemy>().energy -= damage * (1.0f + 0.1f * PlayerInfo.playerInfo.workshop[3])*Time.deltaTime*50f;
                other.gameObject.GetComponent<Enemy>().ouchouch();
            }
            if(Bullet_Number == 12){
                GetComponent<SSgBullet>().Vel *= 0.3f;
            }
        }
    }
}
