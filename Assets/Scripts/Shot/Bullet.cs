using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Bullet : MonoBehaviour
{

    private CapsuleCollider2D hitbox;
    private Rigidbody2D rb;
    public int speed;
    private EnemyController enemigo;


    // Start is called before the first frame update
    void Start()
    {
        
        //el operador de nulabilidad (?.) accede al componente EnemyController y evita que se produzca una excepción si el objeto con la etiqueta "Enemigo" no existe o no tiene el componente EnemyController
        enemigo = GameObject.FindGameObjectWithTag("Enemigo")?.GetComponent<EnemyController>();

        rb = GetComponent<Rigidbody2D>();
        hitbox = rb.GetComponent<CapsuleCollider2D>();
        
        hitbox.isTrigger = true;

        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemigo") && enemigo != null && !enemigo.anim.GetBool("EstaMuerto"))
    //    {
    //        enemigo.Damage();
    //        Destroy(gameObject);
    //    }
    //    if (collision.gameObject.tag == "Suelo")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo") && collision != null && !collision.GetComponent<EnemyController>().anim.GetBool("EstaMuerto"))
        {
            collision.GetComponent<EnemyController>().Damage();
            Destroy(gameObject);
        }
        if (collision.CompareTag("EnemigoFly") && collision != null && !collision.GetComponent<EnemyFlyController>().anim.GetBool("EstaMuerto"))
        {
            collision.GetComponent<EnemyFlyController>().Damage();
            Destroy(gameObject);
        }
        if (collision.CompareTag("EvilRobot") && collision != null && !collision.GetComponent<EnemyController>().anim.GetBool("EstaMuerto"))
        {
            collision.GetComponent<EnemyController>().Damage();
            Destroy(gameObject);
        }
        if (collision.CompareTag("TheBoss") && collision != null && !collision.GetComponent<BossController>().anim.GetBool("EstaMuerto"))
        {
            collision.GetComponent<BossController>().Damage();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Suelo")
        {
            Destroy(gameObject);
        }
    }
}
