using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaGravedad : MonoBehaviour
{
    private BoxCollider2D hitbox;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (boss.GetComponent<BossController>().vidas <=0)
        //{
        //    canvaVidaBoss.SetActive(false);
        //}
        //else
        //{
        //    canvaVidaBoss.SetActive(true);
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().fisica.gravityScale = 1f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<Player>().fisica.gravityScale = 3.4f;
            }
        }
        catch
        {

        }

    }
}
