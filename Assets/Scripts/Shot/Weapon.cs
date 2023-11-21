using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform puntoDisparo;
    public GameObject bulletPrefab;
    private bool estaDisparando;
    private float timerDisparo = 0f;
    public float cadencia = 0.1f;
    private Player player;
    private AudioSource audioS;
    private bool coroutineRunning = false; 
    //Este bool evita que se dupliquen las balas al cambiar de estado

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Con este metodo y la courutina se evita que al pulsar rapido no instancia muy rapido

        if (player.tocandosuelo && !player.agachado && Input.GetButton("Fire1"))
        {
            if (!estaDisparando && !coroutineRunning)
            {
                estaDisparando = true;
                StartCoroutine(DispararConCadencia());
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            estaDisparando = false;
            timerDisparo = 0f; // Reinicia el temporizador al soltar el botón
        }
        else if (!player.tocandosuelo || player.agachado)
        {
                estaDisparando = false;
                timerDisparo = 0f; // Reinicia el temporizador al soltar el botón
        }
    }

    IEnumerator DispararConCadencia()
    {
        coroutineRunning = true; // Indica que la corrutina está en ejecución
        while (Input.GetButton("Fire1"))
        {
            if (timerDisparo >= cadencia)
            {
                // Disparar
                Shoot();
                audioS.pitch = 1.3f;
                audioS.PlayOneShot(player.audioShoot, 0.7f);
                timerDisparo = 0f; // Reinicia el temporizador después de disparar
            }

            timerDisparo += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.002f); // Espera 0.1 segundos
        coroutineRunning = false; // Indica que la corrutina ha terminado
    }

    private void Shoot()
    {
        GameObject instanciaPrefab = Instantiate(bulletPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Destroy(instanciaPrefab, 0.25f);
    }


}