using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager objectManager;

    public GameObject[] enemys;
    public GameObject[] e_bullet;
    public GameObject[] explosions;
    private List<Queue<E_bullet>> bulletobjectQueue = new List<Queue<E_bullet>>();
    private List<Queue<Enemy>> EnemyobjectQueue = new List<Queue<Enemy>>();
    private List<Queue<Explosion>> explosionobjectQueue = new List<Queue<Explosion>>();

    // Start is called before the first frame update
    public void Awake()
    {
        if(objectManager == null){
            objectManager = this;
        }else{
            Destroy(gameObject);
        }

        Initialize(50);
    }
    public void AwakeSetting(){
        
    }

    // 하나를 생성하는거다.
    private E_bullet CreateNewBullet(int i)
    {
        E_bullet newObj = Instantiate(e_bullet[i], transform).GetComponent<E_bullet>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    private Enemy CreateNewEnemy(int type)
    {
        Enemy newObj = Instantiate(enemys[type], transform).GetComponent<Enemy>();
        newObj.enemytype = type;
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    private Explosion CreateNewExplosion(int type)
    {
        Explosion newObj = Instantiate(explosions[type], transform).GetComponent<Explosion>();
        newObj.type = type;
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    // count개수를 생성해서 집ㅇ넣는다.
    private void Initialize(int count){
        for (int j = 0; j < e_bullet.Length;j++)
        {
            bulletobjectQueue.Add(new Queue<E_bullet>());
            for (int i = 0; i < count; i++)
            {
                bulletobjectQueue[j].Enqueue(CreateNewBullet(j));
            }
        }

        for(int j=0; j<enemys.Length;j++){
            EnemyobjectQueue.Add(new Queue<Enemy>());
            if(j<=1){
                for(int i =0; i< 15; i++){
                    EnemyobjectQueue[j].Enqueue(CreateNewEnemy(j));
                }
            } else{
                
                for (int i = 0; i < 5;i++){
                    EnemyobjectQueue[j].Enqueue(CreateNewEnemy(j));
                }

            }
            
        }
        for (int j = 0; j < explosions.Length;j++){
            explosionobjectQueue.Add(new Queue<Explosion>());
            int size;
            if(j == 0){
                size = 10;
            }else{
                size = 5;
            }
            for (int i = 0; i < size;i++){
                explosionobjectQueue[j].Enqueue(CreateNewExplosion(j));
            }
        }

    }
    //0 1
    //10 11
    //20 21
    //30 31
    //40 41
    public static E_bullet GetBulletObject(int type){
        switch(type){
            default:
                if(objectManager.bulletobjectQueue[type/10].Count > 0){
                    E_bullet obj = objectManager.bulletobjectQueue[type/10].Dequeue();
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    obj.bullettype = type%10;
                    obj.bulletkind = type/10;
                    return obj;
                }
                else{
                    E_bullet newobj = objectManager.CreateNewBullet(0);
                    newobj.transform.SetParent(null);
                    newobj.gameObject.SetActive(true);
                    newobj.bullettype = type%10;
                    newobj.bulletkind = type/10;
                    return newobj;
                }
        }
        
    }
    
    public static void ReturnBulletObject(E_bullet bullet){
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(objectManager.transform);
        objectManager.bulletobjectQueue[bullet.bulletkind].Enqueue(bullet);
    }
    public static Enemy GetEnemyObject(int type){
        Enemy obj = objectManager.EnemyobjectQueue[type].Dequeue();
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        obj.enemytype = type;
        return obj;
    }
    
    public static void ReturnEnemyObject(Enemy enemy){
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(objectManager.transform);
        objectManager.EnemyobjectQueue[enemy.enemytype].Enqueue(enemy);
        
    }
    public static Explosion GetExlposionObject(int type){
        Explosion obj = objectManager.explosionobjectQueue[type].Dequeue();
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        obj.type = type;
        return obj;
    }
    
    public static void ReturnExlposionObject(Explosion explosion){
        explosion.gameObject.SetActive(false);
        explosion.transform.SetParent(objectManager.transform);
        objectManager.explosionobjectQueue[explosion.type].Enqueue(explosion);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
