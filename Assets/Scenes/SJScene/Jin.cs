using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jin : MonoBehaviour
{
    public static Jin mypl;
    public static Transform mytr;
    // Start is called before the first frame update
    private void Awake()
    {
        mypl = this;
        mytr = this.transform;
    }
}
