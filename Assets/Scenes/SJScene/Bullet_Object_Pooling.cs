using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Object_Pooling : MonoBehaviour
{
    public int Shot_case;
    public static Bullet_Object_Pooling instance;
    [SerializeField]
    private GameObject[] Bullet_Array = new GameObject[12];
    List<Queue<GameObject>> MyQueueList = new List<Queue<GameObject>>();
    private void Awake()
    {
        instance = this;
        for(int i = 0; i<Bullet_Array.Length;i++){
            Queue<GameObject> myqueue = new Queue<GameObject>();
            MyQueueList.Add(myqueue);
        }
        // for(int i = 0; i<MyQueueList.Count;i++){
        //     initialize(10,i+1);
        // }
    }
    private void initialize(int initCount,int BulletNum){
        for(int i = 0; i<initCount;i++){
            MyQueueList[BulletNum - 1].Enqueue(CreateNewObject(BulletNum));
        }
    }
    private GameObject CreateNewObject(int Bulletnum){
        var newobj = Instantiate(Bullet_Array[Bulletnum-1]);
        newobj.SetActive(false);
        newobj.transform.SetParent(transform);
        return newobj;
    }
    public static GameObject GetObject(int BulletNum){
        if(instance.MyQueueList[BulletNum - 1].Count > 0){
            var obj = instance.MyQueueList[BulletNum - 1].Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else{
            var Newobj = instance.CreateNewObject(BulletNum);
            Newobj.transform.SetParent(null);
            Newobj.SetActive(true);
            return Newobj;
        }
    }
    public static void ReturnObject(int BulletNum,GameObject myBullet){
        myBullet.SetActive(false);
        myBullet.transform.SetParent(instance.transform);
        instance.MyQueueList[BulletNum - 1].Enqueue(myBullet);
    }
}
