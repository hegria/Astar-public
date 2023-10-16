using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToFrame : MonoBehaviour
{
    private void Start() {
        StartCoroutine(timetoframe(Vector3.up,3f,5f));
    }
    IEnumerator timetoframe(Vector3 dir, float Distance, float moveTime){
        float fixtime = moveTime;
        Vector3 fixpos = transform.position;
        while(moveTime > 0){
            moveTime -= Time.deltaTime;
            transform.position = fixpos + dir.normalized*Distance*(1-moveTime/fixtime); // 방향*거리*(0부터~1까지 movetime동안 움직이는 실수값)
            yield return new WaitForFixedUpdate();
        }
    }
}
