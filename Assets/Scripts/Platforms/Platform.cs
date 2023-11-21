using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public int velocidad;
    public Vector2 posicionInicio;
    public Vector2 posicionFin;
    public int topeX, topeY;
    private bool moviendoAFin;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;
        posicionFin = new Vector2(posicionInicio.x + topeX, posicionInicio.y + topeY);
        moviendoAFin = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoverPlataforma();
    }

    public void MoverPlataforma()
    {
        Vector2 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicio;
        transform.position = Vector2.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        if (transform.position.x == posicionFin.x && transform.position.y == posicionFin.y)
        {
            moviendoAFin = false;
        }
        if (transform.position.x == posicionInicio.x && transform.position.y == posicionInicio.y)
        {
            moviendoAFin |= true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.transform.parent = null;
    }
}

