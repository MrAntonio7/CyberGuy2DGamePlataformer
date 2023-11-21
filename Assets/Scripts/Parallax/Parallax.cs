using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float efectoParalax;
    private Transform camara;
    private Vector3 ultimaPosicionCamara;
    public bool enableAxisY;

    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main.transform;
        ultimaPosicionCamara = camara.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableAxisY)
        {

        Vector3 movimientoFondo = camara.position - ultimaPosicionCamara;
        transform.position += new Vector3(movimientoFondo.x * efectoParalax, movimientoFondo.y, 0);
        ultimaPosicionCamara = camara.position;
        }
        else
        {
            Vector3 movimientoFondo = camara.position - ultimaPosicionCamara;
            transform.position += new Vector3(movimientoFondo.x * efectoParalax, 0, 0);
            ultimaPosicionCamara = camara.position;
        }
    }
}
