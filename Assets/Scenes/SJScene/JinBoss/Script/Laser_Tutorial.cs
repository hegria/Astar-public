using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Tutorial : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform LaserFirePnt;
    public LineRenderer _lineRenderer;
    Transform m_Transform;
    private void Awake() {
        m_Transform = GetComponent<Transform>();
    }
    private void Update() {
        ShootLaser();
    }
    void ShootLaser(){
        if(Physics2D.Raycast(m_Transform.position, transform.right)){
            RaycastHit2D _hit = Physics2D.Raycast(m_Transform.position, transform.right);
        }
        else{
            Draw2Ray(LaserFirePnt.position,LaserFirePnt.transform.right * defDistanceRay);
        }
    }
    void Draw2Ray(Vector2 startpos, Vector2 endpos){
        _lineRenderer.SetPosition(0,startpos);
        _lineRenderer.SetPosition(1,endpos);
    }
}
