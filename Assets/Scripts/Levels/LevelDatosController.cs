using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatosController : MonoBehaviour
{
    private static LevelDatosController instance;
    public int numeroNivel;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GuardarNumeroNivel(int numero)
    {
        numeroNivel = numero;
    }
}
