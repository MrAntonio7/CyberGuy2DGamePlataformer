using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class BulletBoss : MonoBehaviour
{
    private CapsuleCollider2D hitbox;
    private Rigidbody2D rb;
    public int speed;

    private BossController enemigoOrigen;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = rb.GetComponent<CapsuleCollider2D>();
        hitbox.isTrigger = true;
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision != null && !collision.gameObject.GetComponent<Player>().gamerover)
        {
            enemigoOrigen.noHayaAcertado = true;
            rb.velocity = Vector2.zero;
            
            foreach(GameObject bala in GameObject.FindGameObjectsWithTag("bulletBoss"))
            {
                Destroy(bala);
            }
            float direccionX = 1f;

            // Empujar el objeto en la dirección correspondiente
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerDamage(direccionX);
            Destroy(gameObject);
        }
    }
    public void EstablecerEnemigoOrigen(BossController enemigo)
    {
        enemigoOrigen = enemigo;
    }
}

