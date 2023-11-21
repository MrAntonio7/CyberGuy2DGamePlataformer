using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReintentarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (GameObject.FindGameObjectWithTag("DatosNivel"))
        {
            Debug.Log(GameObject.FindGameObjectWithTag("DatosNivel").GetComponent<LevelDatosController>().numeroNivel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
