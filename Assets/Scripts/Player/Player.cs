using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Canvas textoPuntos;

    [SerializeField]
    private GameObject prefabVida;

    [SerializeField]
    private Camera cam;

    public Rigidbody2D fisica;
    private Animator anim;
    private CapsuleCollider2D hitbox;
    private BoxCollider2D hitboxCrouch;
    private SpriteRenderer sprite;
    //private RigidbodyConstraints2D originalConstraints;
    private DatosController datos;
    public Transform posicionJugador;
    public Transform puntoDisparo;
    private AudioSource audioSourceDisparo;
    private AudioSource audioSourceCaida;
    private AudioSource audioSourceSalto;

    public AudioClip audioShoot;
    public AudioClip audioCaida;
    public AudioClip audioSalto;
    public Vector3 ultimaVezSueloTocado;
    public float entradaX;
    public float entradaY;

    public int velocidad;
    public int fuerzaSalto;
    public int puntos;
    public int vidas;
    public int vidasPosX;

    public bool tocandosuelo;
    public bool agachado;
    public int numeroNivel;

    private bool flipSpriteShoot;
    public bool sePuedeMover;
    public GameObject prefabDatosNivel;
    private bool animacionMuerto;
    private GameObject[] textosGameOver;
    public bool jugadorEnPausaMenu;
    public bool gamerover;
    public int limiteVidas;
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        limiteVidas = 5;
        gamerover = false;
        jugadorEnPausaMenu = false;
        textosGameOver = GameObject.FindGameObjectsWithTag("GameOverText");
        foreach (GameObject texto in textosGameOver)
        {
            texto.SetActive(false);
        }
        
        if (GameObject.FindGameObjectWithTag("DatosNivel"))
        {
            numeroNivel = GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().numeroNivel;
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1Scene":
                    ConfigurarDatosNivel(1);
                    break;

                case "Level2Scene":
                    ConfigurarDatosNivel(2);
                    break;

                case "Level3Scene":
                    ConfigurarDatosNivel(3);
                    break;

                    // Agrega más casos según sea necesario

                    //default:
                    // Manejo para otras escenas si es necesario
                    //break;
            }
        }
        if (GameObject.FindGameObjectWithTag("Music"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Music"));
        }

        audioSourceDisparo = GetComponent<AudioSource>();
        audioSourceCaida = GetComponent<AudioSource>();
        audioSourceSalto = GetComponent<AudioSource>();
        posicionJugador = GetComponent<Transform>();
        fisica = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitbox = GetComponent<CapsuleCollider2D>();
        hitboxCrouch = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (GameObject.FindGameObjectWithTag("Datos")) 
        {
            datos = GameObject.FindGameObjectWithTag("Datos").GetComponent<DatosController>();
            puntos = datos.puntos;
            vidas = datos.vidas;
        }
        else
        {
            puntos = 0;
            vidas = 3;
        }
        

        velocidad = 13;
        fuerzaSalto = 22;



        vidasPosX = -15;

        //Variables de entrada para agacharse
        agachado = false;
        anim.SetBool("Estaagachado", false);
        hitboxCrouch.enabled = false;

        //originalConstraints = fisica.constraints;
        flipSpriteShoot = false;
        sePuedeMover = true;
        animacionMuerto = false;

        CalcularVidasUI();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sePuedeMover)
        {
            entradaX = Input.GetAxis("Horizontal");
            entradaY = Input.GetAxis("Vertical");
        }

        if (animacionMuerto)
        {
            sePuedeMover = false;
            sprite.color = Color.red;
            GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, 0.8f);
            foreach (GameObject texto in textosGameOver)
            {
                texto.SetActive(true);
            }
            entradaX = 0;
            entradaY = 0;
            anim.SetBool("Estaherido", true);
        }


        Salto();
        Agacharse();
        Disparar();
        Animacion();

        //Debug.Log(entradaX);

    }

    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);
        }
        //Debug.Log(anim.GetFloat("VelocidadY"));
        
    }



    private void Animacion()
    {
        //Animar
        anim.SetFloat("VelocidadX", Mathf.Abs(fisica.velocity.x));
        anim.SetFloat("VelocidadY", fisica.velocity.y);

        Volteo();

    }

    public void Volteo()
    {
        if (fisica.velocity.x > 0.1f)
        {
            sprite.flipX = false;
            RotacionDisparo(false);
        }
        else if (fisica.velocity.x < -0.1f)
        {
            sprite.flipX = true;
            RotacionDisparo(true);
        }
    }
    private void Salto()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space) && tocandosuelo)
        {
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            tocandosuelo=false;
        }
        */

        EstaEnElSuelo();

        if (Input.GetButtonDown("Jump") && tocandosuelo && !agachado && !jugadorEnPausaMenu)
        {
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            audioSourceSalto.PlayOneShot(audioSalto, 1f);
            
        }
    }


    private void EstaEnElSuelo()
    {

        /* //Funcion con un raycast en el centro
         * 
        RaycastHit2D raycast = Physics2D.Raycast(transform.position + new Vector3(0, -5f, 0), Vector2.down, 2f);

        Debug.DrawLine(transform.position + new Vector3(0, -5f, 0), transform.position + new Vector3(0, -7f, 0), Color.green);
        if(raycast)
        {
            Debug.Log(tocandosuelo);
            tocandosuelo = true;
        }
        else
        {
            Debug.Log(tocandosuelo);
            tocandosuelo = false;
        }

        */

        /*-----------------------*/

        //Funcion con 2 raycast, uno a cada extremo del personaje
        Debug.DrawLine(transform.position + new Vector3(0.8f, -5f, 0), transform.position + new Vector3(0.8f, -5.2f, 0), Color.green);
        Debug.DrawLine(transform.position + new Vector3(-1.4f, -5f, 0), transform.position + new Vector3(-1.4f, -5.2f, 0), Color.green);

        RaycastHit2D raydownleft = Physics2D.Raycast(transform.position + new Vector3(-1.4f, -5f, 0), Vector2.down, 0.2f);
        RaycastHit2D raydownright = Physics2D.Raycast(transform.position + new Vector3(0.8f, -5f, 0), Vector2.down, 0.2f);

        if (raydownleft || raydownright)
        {

             //Debug.Log(tocandosuelo);
             tocandosuelo = true;
             anim.SetBool("Estaenelsuelo", true);
             anim.SetFloat("VelocidadY", 0f);
             ultimaVezSueloTocado = transform.position;
             //Debug.Log(ultimaVezSueloTocado);

        }
        else
        {
            //Debug.Log(tocandosuelo);
            anim.SetBool("Estaenelsuelo", false);
            tocandosuelo = false;
        }

    }

    public void Agacharse()
    {
        if (Input.GetButton("Crouch") && tocandosuelo && !jugadorEnPausaMenu)
        {
            entradaX = 0f;
            anim.SetBool("Estaagachado", true);
            hitbox.enabled = false;
            hitboxCrouch.enabled = true;
            agachado = true;
        }
        else
        {
            anim.SetBool("Estaagachado", false);
            hitbox.enabled = true;
            hitboxCrouch.enabled = false;
            agachado = false;
            entradaX = Input.GetAxis("Horizontal");
        }
    }

    public void Disparar()
    {
        if (Input.GetButton("Fire1") && tocandosuelo && !jugadorEnPausaMenu)
        {
            //Guarda la ultima direccion del sprite
            flipSpriteShoot = sprite.flipX;

            if (entradaX > 0)
            {
                //Animacion
                anim.SetBool("Estadisparando", true);

                //Bloquea el movimiento mientras dispara hacia adelante
                entradaX = 0f;
                //fisica.constraints = RigidbodyConstraints2D.FreezePosition;

                //No voltea el sprite, mira hacia la derecha
                sprite.flipX = false;

                //Rota la posicion de disparo
                RotacionDisparo(false);

            }
            else if (entradaX < 0)
            {
                anim.SetBool("Estadisparando", true);

                //Bloquea el movimiento mientras dispara hacia atras
                entradaX = 0f;
                //fisica.constraints = RigidbodyConstraints2D.FreezePosition;

                //Voltea el sprite, mira hacia la izquierda
                sprite.flipX = true;

                //Rota la posicion de disparo
                RotacionDisparo(true);

            }
            else if (entradaX == 0)
            {
                //Animacion
                anim.SetBool("Estadisparando", true);

                //Bloque el moviento al disparar quieto
                entradaX = 0f;
                //fisica.constraints = RigidbodyConstraints2D.FreezePosition;

                //Pone la ultima direccion del sprite guardada anteriormente
                sprite.flipX = flipSpriteShoot;

                //Rota la posicion de disparo segun el sprite
                if (flipSpriteShoot)
                {
                    RotacionDisparo(flipSpriteShoot);
                }
                else
                {
                    RotacionDisparo(flipSpriteShoot);
                }
            }
        }
        else
        {
            //Animacion
            anim.SetBool("Estadisparando", false);

            //Desbloquea el movimiento, restableciendo las constraints originales
            //entradaX = Input.GetAxis("Horizontal");
            //fisica.constraints = originalConstraints;
            //fisica.constraints = RigidbodyConstraints2D.None;
        }
    }

    public void RotacionDisparo(bool flipBool)
    {
        if (flipBool)
        {
            Quaternion nuevaRotacion = Quaternion.Euler(0, 180, 0);
            puntoDisparo.rotation = nuevaRotacion;
            puntoDisparo.transform.position = new Vector3(posicionJugador.position.x - 3.35f, posicionJugador.position.y + 0.6f, 0);
        }
        else
        {
            Quaternion nuevaRotacion = Quaternion.Euler(0, 0, 0);
            puntoDisparo.rotation = nuevaRotacion;
            puntoDisparo.transform.position = new Vector3(posicionJugador.position.x + 3.35f, posicionJugador.position.y + 0.6f, 0);
        }
    }
    public void SumaMoneda(int valor)
    {
        puntos = puntos + valor;
        textoPuntos.GetComponent<ActualizaPuntos>().Actualiza(puntos);
        Debug.Log(puntos);

    }
    public void RenderVidas(int offSet)
    {
        GameObject nuevaVida = Instantiate(prefabVida, new Vector3(vidasPosX + offSet, 8.75f, 25f), Quaternion.identity);
        nuevaVida.transform.SetParent(cam.transform);
        nuevaVida.transform.localPosition = new Vector3(vidasPosX + offSet, 8.75f, 25f);
    }
    public void CalcularVidasUI()
    {

        for (int i = 0; i < vidas; i++)
        {
            RenderVidas(i * 2);
        }
    }
    public void SumarORestarVidas(bool sum_rest)
    {
        GameObject[] arrayCorazonesAnteriores = GameObject.FindGameObjectsWithTag("VidaUI");
        for (int i = 0; i < arrayCorazonesAnteriores.Length; i++)
        {
            Destroy(arrayCorazonesAnteriores[i]);
        }
        if (sum_rest)
        {
            if (vidas < limiteVidas)
            {
                vidas++;
            }
            
        }
        else
        {
            vidas--;
            ComprobarSiHaMuerto();
        }
        CalcularVidasUI();
    }

    public void PlayerDamage(float direccion)
    {
        StartCoroutine(PlayerDamageCourutine(direccion));
    }

    IEnumerator PlayerDamageCourutine(float direccion)
    {
        anim.SetBool("Estaherido", true);
        SumarORestarVidas(false);
        sprite.color = Color.red;
        

        if(direccion > 0)
        {
            sePuedeMover = false;
            //Debug.Log(direccion + " Izquierda");
            fisica.AddForce(new Vector3(-1,1, 0) * 15,  ForceMode2D.Impulse);
        }
        else if (direccion < 0)
        {
            sePuedeMover = false;
            //Debug.Log(direccion + " Derecha");
            fisica.AddForce(new Vector3(1, 1, 0) * 15, ForceMode2D.Impulse);

        }
        yield return new WaitForSeconds(0.9f);
        sePuedeMover = true; 
        anim.SetBool("Estaherido", false);
        sprite.color = Color.white;
        
    }

    public void GameOver()
    {
        Debug.Log("Ha muerto");

        //if(transform.position.x >= ultimaVezSueloTocado.x)
        //{
        //    transform.position = ultimaVezSueloTocado + new Vector3(-10, 5, 0);
        //}
        //else if (transform.position.x <= ultimaVezSueloTocado.x) 
        //{
        //    transform.position = ultimaVezSueloTocado + new Vector3(10, 5, 0);
        //}
        //StartCoroutine(RojoABlanco(1f));

        
        Invoke("PlayAudioCaida", 0.8f);
        animacionMuerto = true;
        StartCoroutine(EscenaReintentar(GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().numeroNivel));

    }
    public void ComprobarSiHaMuerto()
    {
        if (vidas <= 0)
        {
            GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Caida")&&!gamerover)
        {
            gamerover = true;
            GameOver();

        }
    }

    IEnumerator EscenaReintentar(int numeroNivel)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ReintentarScene");
        //SceneManager.LoadScene("ReintentarScene"+numeroNivel);
    }
    public void ConfigurarDatosNivel(int nivel)
    {
        numeroNivel = nivel;
        GameObject datosLevel = new GameObject ("DatosNivel");
        datosLevel.tag = "DatosNivel";
        datosLevel.AddComponent<LevelDatosController>();
        datosLevel.GetComponent<LevelDatosController>().GuardarNumeroNivel(nivel);

        Debug.Log("Nivel " + nivel);
    }
    public void PlayAudioCaida()
    {
        audioSourceCaida.pitch = 1;
        audioSourceCaida.volume = 0.6f;
        audioSourceCaida.PlayOneShot(audioCaida, 0.6f);

    }

}

