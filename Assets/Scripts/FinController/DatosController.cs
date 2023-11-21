using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosController : MonoBehaviour
{
    private static DatosController instance;
    public int puntos;
    public int vidas;
    public float tiempo;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
            puntos = 0;
            vidas = 3;
            tiempo = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuardarDatos()

    {
        tiempo = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Cronometro>().tiempo;
        puntos =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().puntos;
        vidas =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().vidas;
  
    }


}
