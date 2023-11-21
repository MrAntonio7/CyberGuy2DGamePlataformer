using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BossController : MonoBehaviour
{
    public Rigidbody2D fisica;
    public Animator anim;
    private BoxCollider2D hitbox;
    private CapsuleCollider2D hitboxCrouch;
    private SpriteRenderer sprite;
    private AudioSource audioSourceRun;
    private AudioSource audioSourceDead;
    public AudioClip audioClipRun;
    public AudioClip audioClipDead;

    public int velocidad;
    public Vector2 posicionInicio;
    public Vector2 posicionFin;
    public int topeX, topeY;
    private bool moviendoAFin;
    private bool enableOnCollision;
    public int vidas;
    public bool enableShoot;
    public Transform puntoDisparo;
    public GameObject bullet;
    public bool noHayaAcertado;
    public AudioClip machineGun;
    private AudioSource sonidoMachineGun;
    private bool sonidoReproducido = false;
    private bool sePuedeReir = true;
    public float distanciaDisparo;

    private void Awake()
    {
        audioSourceRun = GetComponent<AudioSource>();
        audioSourceDead = GetComponent<AudioSource>();
        fisica = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<BoxCollider2D>();
        hitboxCrouch = GetComponent<CapsuleCollider2D>();
        enableOnCollision = true;
        noHayaAcertado = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;
        posicionFin = new Vector2(posicionInicio.x + topeX, posicionInicio.y + topeY);
        moviendoAFin = true;
        if (enableShoot)
        {
            InvokeRepeating("DispararConPausa", 0f, 6f);
            sonidoMachineGun = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        }
        hitboxCrouch.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        MoverEnemigo();

    }

    private void Dispara()
    {
        StartCoroutine(Shoot());
    }

    public void MoverEnemigo()
    {
        Vector2 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicio;
        transform.position = Vector2.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        if (transform.position.x == posicionFin.x && transform.position.y == posicionFin.y)
        {
            moviendoAFin = false;
            //sprite.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        if (transform.position.x == posicionInicio.x && transform.position.y == posicionInicio.y)
        {
            moviendoAFin = true;
            //sprite.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Damage()
    {
        
        vidas--;
        GameObject.FindGameObjectWithTag("BarraDeVida").GetComponent<BarraDeVida>().ActualizarBarraDeVida(vidas);
        sprite.color = Color.red;
        Invoke("ColorOriginal", 0.3f);
        if(vidas <= 0)
        {
            
            StartCoroutine(DestruirEnemigo());

            //Destroy(gameObject);
        }
    }
    void ColorOriginal()
    {
        sprite.color = Color.white;
    }
    IEnumerator DestruirEnemigo()
    {
        anim.SetBool("EstaMuerto", true);
        velocidad = 0;
        enableOnCollision = false;
        //Physics2D.IgnoreCollision(hitbox, GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>(), true);
        //Physics2D.IgnoreCollision(hitbox, GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), true);
        //Physics2D.IgnoreCollision(hitbox, GameObject.FindGameObjectWithTag("Bullet").GetComponent<CapsuleCollider2D>(), true);
        audioSourceRun.Stop();
        sePuedeReir = false;
        audioSourceDead.PlayOneShot(audioClipDead, 1f);
        //hitboxCrouch.enabled = true;
        //Destroy(audioSourceRun);
        fisica.constraints = RigidbodyConstraints2D.FreezeAll;
        hitbox.enabled = false;
        hitboxCrouch.enabled = false;
        yield return new WaitForSeconds(2f);
        //Destroy(sprite,0.5f);
        //Destroy(anim);
        //Destroy(audioSource);
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enableOnCollision)
        {
            Debug.Log("Jugador dañado");
            Vector2 normalDireccion = collision.contacts[0].normal;
            float direccionX = Mathf.Sign(normalDireccion.x);
            
            // Empujar el objeto en la dirección correspondiente
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerDamage(direccionX);

        }
    }
    IEnumerator Shoot()
    {
        sonidoReproducido = false;
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < 3f && !noHayaAcertado && !anim.GetBool("EstaMuerto"))
        {
            
            anim.SetBool("activaDisparo",true);
            if (anim.GetBool("activaDisparo") && !sonidoReproducido)
            {
               
                sonidoMachineGun.PlayOneShot(machineGun);
                sonidoReproducido = true;
            }
            // Crea un proyectil
            GameObject instanciaPrefab = Instantiate(bullet, puntoDisparo.position, puntoDisparo.rotation);
            instanciaPrefab.GetComponent<BulletBoss>().EstablecerEnemigoOrigen(this);
            Destroy(instanciaPrefab, distanciaDisparo);
            yield return new WaitForSeconds(0.15f);
        }


        // Pausa durante 3 segundos
        anim.SetBool("activaDisparo", false);
        if (sePuedeReir)
        {
            audioSourceRun.Play();
        }
        
        sonidoMachineGun.Stop();
        noHayaAcertado = false;
        yield return new WaitForSeconds(3f);
    }
    void DispararConPausa()
    {
        StartCoroutine(Shoot());
    }
}
