using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private bool pausado;
    public GameObject botonPausa;
    public GameObject menuPausa;
    private GameObject objetoMusica;
    public AudioClip audioClip;

    private void Awake()
    {
        pausado = false;
        menuPausa.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pausado)
        {
            ActivarPausa();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausado)
        {
            DesactivarPausa();
        }
    }
    public void PauseGame()
    {
        if (!pausado)
        {
            ActivarPausa();
        }
        else if (pausado)
        {
            DesactivarPausa();
        }

    }
    public void ResumeGame()
    {
        DesactivarPausa();
    }




    public void ActivarPausa()
    {
        Time.timeScale = 0;
        pausado = true;
        botonPausa.SetActive(false);
        menuPausa.SetActive(pausado);
        InstanciarMusicaPause();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().jugadorEnPausaMenu = true;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().entradaX = 0f;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().entradaY = 0f;

    }

    public void DesactivarPausa()
    {
        Time.timeScale = 1;
        pausado = false;
        botonPausa.SetActive(true);
        menuPausa.SetActive(pausado);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().jugadorEnPausaMenu = false;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().entradaX = Input.GetAxis("Horizontal");
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().entradaY = Input.GetAxis("Vertical");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void InstanciarMusicaPause()
    {
        if (GameObject.FindWithTag("Music") == null)
        {
            // Instanciar un objeto vacío
            objetoMusica = new GameObject("ObjetoMusica");
            objetoMusica.tag = "Music";

            // Añadir un componente AudioSource al objeto
            AudioSource audioSource = objetoMusica.AddComponent<AudioSource>();


            // Ahora puedes configurar las propiedades del AudioSource según tus necesidades
            audioSource.clip = audioClip; // Asigna tu AudioClip
            audioSource.volume = 0.8f; // Establece el volumen, ajusta según sea necesario
            audioSource.playOnAwake = true; // Configura si el audio se reproduce al inicio
            audioSource.loop = true;
            audioSource.Play();

            //Una vez creado y reproducido no se destruye mas
            objetoMusica.AddComponent<MusicController>();

        }
    }
}
