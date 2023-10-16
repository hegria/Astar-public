using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public bool SubLaser;
    public AudioSource sources;
    public void GoLaser(){
        sources.Play();
        SubLaser = true;
    }
}
