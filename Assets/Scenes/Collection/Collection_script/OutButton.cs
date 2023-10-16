using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutButton : MonoBehaviour
{
    public void OutSite(){
        UIsound.uIsound.Clicked();
        transform.parent.gameObject.SetActive(false);
    }
}
