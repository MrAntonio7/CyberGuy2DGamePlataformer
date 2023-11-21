using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DatosSceneController : MonoBehaviour
{
    private GameObject datos;
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private TextMeshProUGUI textoVida2;
    [SerializeField] private TextMeshProUGUI textoCristal;
    [SerializeField] private TextMeshProUGUI textoCristal2;
    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoTiempo2;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Datos"))
        {
            datos = GameObject.FindGameObjectWithTag("Datos");
            textoVida.text = datos.GetComponent<DatosController>().vidas.ToString();
            textoVida2.text = datos.GetComponent<DatosController>().vidas.ToString();
            textoCristal.text = datos.GetComponent<DatosController>().puntos.ToString();
            textoCristal2.text = datos.GetComponent<DatosController>().puntos.ToString();
            textoTiempo.text = FormatearTiempo(datos.GetComponent<DatosController>().tiempo);
            textoTiempo2.text = FormatearTiempo(datos.GetComponent<DatosController>().tiempo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    string FormatearTiempo(float tiempoVar)
    {
        //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(tiempoVar / 60).ToString("00");
        string segundos = Mathf.Floor(tiempoVar % 60).ToString("00");

        //Devuelvo el string formateado con : como separador
        return minutos + ":" + segundos;
    }
}
