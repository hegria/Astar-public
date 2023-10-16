using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guide : MonoBehaviour
{
    Rigidbody2D m_rigid = null;
    Transform m_trans = null;
    bool check_trans;

    [SerializeField] float m_speed = 0f;
    float m_current = 0f;
    [SerializeField] LayerMask m_layermask = 0;
    [SerializeField] ParticleSystem my_psEffect = null;
    bool isnull;
    int Cnt;
    void search()
    {
        Collider2D[] mycol = Physics2D.OverlapCircleAll(transform.position, 30f, m_layermask);
        if(mycol.Length > 0)
        {
            m_trans = mycol[0].transform;
        }
        check_trans = true;
        isnull = false;
        Cnt++;
    }
    IEnumerator launchdelay()
    {
        //yield return new WaitUntil(() => m_rigid.velocity.y < 0);
        yield return new WaitForSeconds(0.1f);
        search();
        my_psEffect.Play();
    }
    Coroutine myco;
    public AudioSource audioSource;
    public void SetAwake(){
        Cnt = 0;
        isnull = false;
        m_current = 0;
        m_trans = null;
        check_trans = false;
        m_rigid = GetComponent<Rigidbody2D>();
        myco = StartCoroutine(launchdelay());
        audioSource.Play();
    }


    void Update()
    {
        if(m_trans != null&&m_trans.gameObject.activeSelf&&!isnull)
        {
            if (m_current <= m_speed)
                m_current += m_speed * Time.deltaTime;

            transform.position += transform.up * m_current * Time.deltaTime;

            Vector3 t_dir = (m_trans.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);
        }
        if(m_trans!= null&&!m_trans.gameObject.activeSelf&&check_trans&&Cnt<3){
            isnull = true;
            check_trans = false;
            // transform.rotation = Quaternion.identity;
            // GetComponent<Rigidbody2D>().velocity = Vector3.up*10f;
            search();
        }
        if(m_trans!= null&&!m_trans.gameObject.activeSelf&&check_trans&&Cnt>2){
            isnull = true;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody2D>().velocity = Vector3.up*10f;
            check_trans = false;
        }
        if(m_trans == null&&check_trans){
            isnull = true;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody2D>().velocity = Vector3.up*10f;
            check_trans = false;
        }
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(4,gameObject);
        }
        if(transform.position.y <= -5f){
            m_trans = null;
        }
    }
    float check_dis(Collider2D e_col)
    {
        return Vector3.Distance(transform.position, e_col.transform.position);
    }
}
