using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_manage : MonoBehaviour
{
    Queue<GameObject> LeapCollection = new Queue<GameObject>();
    public GameObject leap;
    public void SetAwake(int Leap_Num){
        if(transform.childCount > 0){
            for(int i = 0; i<transform.childCount;i++){
            LeapCollection.Enqueue(transform.GetChild(i).gameObject);
        }
        }
        float Divide = (float)Leap_Num;
        for(int i = 0; i< Leap_Num;i++){
            if(LeapCollection.Count > 0){
                GameObject myleap = LeapCollection.Dequeue();
                myleap.transform.SetParent(transform);
                myleap.transform.position = Character.chartrans.position;
                myleap.transform.localRotation = Quaternion.identity;
                myleap.GetComponent<Leap_Script>().SetAwake(2*Mathf.PI/Divide*i);
            }
            else{
                GameObject myleap = Instantiate(leap,Character.chartrans.position,Quaternion.identity);
                myleap.transform.SetParent(transform);
                myleap.GetComponent<Leap_Script>().SetAwake(2*Mathf.PI/Divide*i);
            }
        }
    }
    private void Update() {
        transform.position = Character.chartrans.position;
    }
}
