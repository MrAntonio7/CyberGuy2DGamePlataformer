using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MenuButtonsController : MonoBehaviour
{
    public Button[] arrayButtons;

    //public GameObject panelCreditos;

    private GameObject cursor;

    private int indexBoton;

    public AudioClip audioClip;

    private GameObject objetoMusica;

    public GameObject datosNivelPrefab;

    private void Awake()
    {
        cursor = GameObject.FindWithTag("Cursor");


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
    
    private void Start()
    {
        

        if (cursor != null)
        {
            // Hacer algo con el objetoPorTag
            //Debug.Log("Se encontró el objeto 'Cursor'");
        }
        else
        {
            // Manejar el caso en que no se encuentra el objeto
            //Debug.Log("No se encontró ningún objeto 'Cursor'");
        }

        indexBoton = 0;

        // Asegura de que haya al menos un botón en el arreglo

        if (arrayButtons.Length > 0)
        {
            //Debug.Log(arrayButtons.Length);
            //Debug.Log(arrayButtons[indexBoton].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            EventSystem.current.SetSelectedGameObject(arrayButtons[indexBoton].gameObject);
        }

    }

    private void Update()
    {
        for (int i = 0; i < arrayButtons.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == arrayButtons[i].gameObject)
            {
                indexBoton = i;

                //Posicion de un Objeto UI con RectTransform
                //Debug.Log(arrayButtons[i].gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                //Debug.Log(arrayButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text + " seleccionado");
                Vector2 posicionFlecha = new Vector2 (arrayButtons[i].gameObject.GetComponent<RectTransform>().anchoredPosition.x -90f, arrayButtons[i].gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                cursor.GetComponent<RectTransform>().anchoredPosition = posicionFlecha;


            }
        }
    }

    public void ClickOut()
    {
        EventSystem.current.SetSelectedGameObject(arrayButtons[indexBoton].gameObject);

    }

    public void ClickIn()
    {
        //Debug.Log("hola");
    }
    public void CargarNivel(int numeroNivel)
    {
        //Destroy(objetoMusica.GetComponent<MusicController>());
        //Destroy(objetoMusica);

        GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().GuardarNumeroNivel(numeroNivel);
        SceneManager.LoadScene("Level" + numeroNivel + "Scene");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("CreditsScene");
        //panelCreditos.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(panelCreditos.GetComponent<MenuButtonsController>().arrayButtons[0].gameObject);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuScene");
        //EventSystem.current.SetSelectedGameObject(panelMenu.GetComponent<MenuButtonsController>().arrayButtons[4].gameObject);
    }
    public void Reintentar()
    {
        //Destroy(objetoMusica.GetComponent<MusicController>());
        //Destroy(objetoMusica);
        CargarNivel(GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().numeroNivel);
    }
    public void SiguienteNivel()
    {
        //Destroy(objetoMusica.GetComponent<MusicController>());
        //Destroy(objetoMusica);


        //////////PREVENTIVO
        if(GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().numeroNivel + 1 > 3)
        {

            CargarNivel(3);
        }
        else { 
        ///////////



        CargarNivel(GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().numeroNivel + 1);
        }
    }

    public void SalirDelJuego()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            SalirDeLaAplicacion();
        #endif
    }


    private void SalirDeLaAplicacion()
    {
        // En una compilación, usar Application.Quit() para cerrar la aplicación
        Application.Quit();
    }
}
