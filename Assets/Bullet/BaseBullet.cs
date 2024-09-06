using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float Speed = 1;
    public GameObject ExpFX;  // ???
    protected Rigidbody2D rb;
    protected Transform m_tansform;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();  // ???
        rb.gravityScale = 0;
        rb.drag = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_tansform = transform;
    }
    protected virtual void Update()
    {
        m_tansform.position = m_tansform.position + m_tansform.right * Speed * Time.deltaTime;
        //m_tansform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
    public void Explode()
    {
        Destroy(Instantiate(ExpFX, m_tansform.position, Quaternion.identity), 2f);
        Destroy(gameObject);
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
