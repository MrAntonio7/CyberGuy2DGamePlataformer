using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActualizaPuntos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntos;
    [SerializeField] private TextMeshProUGUI puntos2;

    private DatosController datos;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Datos"))
        {
            datos = GameObject.FindGameObjectWithTag("Datos").GetComponent<DatosController>();
            puntos.text = datos.puntos.ToString();
            puntos2.text = datos.puntos.ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Actualiza(int monedas)
    {
        puntos.text = monedas.ToString();
        puntos2.text = monedas.ToString();
    }
}
