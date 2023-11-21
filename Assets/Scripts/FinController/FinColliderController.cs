using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinColliderController : MonoBehaviour
{
    private BoxCollider2D colliderFin;
    private AudioSource audioSource;
    private GameObject player;
    private GameObject datos;
    public AudioClip clipVictory;


    private void Awake()
    {
        colliderFin = GetComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Datos"))
        {
            datos = GameObject.FindGameObjectWithTag("Datos");
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Canvas").GetComponent<PauseController>());
            Destroy(GameObject.FindGameObjectWithTag("BotonPausa"));
            player.GetComponent<Player>().sePuedeMover = false;

            if (GameObject.FindGameObjectWithTag("Datos"))
            {
                datos.GetComponent<DatosController>().GuardarDatos();
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute = true;
            audioSource.PlayOneShot(clipVictory);

            ////////////SOLO PARA LOS TRES NIVELES SI NO: Invoke("CargarDatosScene", 2f);
            if (player.GetComponent<Player>().numeroNivel < 3)
            {
                Invoke("CargarDatosScene", 2f);
            }
            else if(player.GetComponent<Player>().numeroNivel >= 3)
            {
                Invoke("CargarFinScene", 2f);
            }
            
        }
        
    }

    public void CargarDatosScene()
    {
        SceneManager.LoadScene("DatosScene");

    }
    public void CargarFinScene()
    {
        SceneManager.LoadScene("FinScene");


    }
}
