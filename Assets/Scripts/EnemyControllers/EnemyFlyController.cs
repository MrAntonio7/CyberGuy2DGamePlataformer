using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyFlyController : MonoBehaviour
{
    public Rigidbody2D fisica;
    public Animator anim;
    private BoxCollider2D hitbox;
    //private CapsuleCollider2D hitboxCrouch;
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

    private void Awake()
    {
        audioSourceRun = GetComponent<AudioSource>();
        audioSourceDead = GetComponent<AudioSource>();
        fisica = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<BoxCollider2D>();
        //hitboxCrouch = GetComponent<CapsuleCollider2D>();
        vidas = 5;
        enableOnCollision = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;
        posicionFin = new Vector2(posicionInicio.x + topeX, posicionInicio.y + topeY);
        moviendoAFin = true;
        //hitboxCrouch.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        MoverEnemigo();
    }

    public void MoverEnemigo()
    {
        Vector2 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicio;
        transform.position = Vector2.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        if (transform.position.x == posicionFin.x && transform.position.y == posicionFin.y)
        {
            moviendoAFin = false;
            

        }
        if (transform.position.x == posicionInicio.x && transform.position.y == posicionInicio.y)
        {
            moviendoAFin = true;
        }
    }

    public void Damage()
    {
        vidas--;
        sprite.color = Color.red;
        Invoke("ColorOriginal", 0.3f);
        if (vidas <= 0)
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
        audioSourceDead.pitch = 1.6f;
        audioSourceDead.volume = 0.4f;
        audioSourceDead.PlayOneShot(audioClipDead, 0.4f);
        //hitboxCrouch.enabled = true;
        //hitbox.enabled = false;
        Destroy(hitbox);
        fisica.gravityScale = 3f;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
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
}
