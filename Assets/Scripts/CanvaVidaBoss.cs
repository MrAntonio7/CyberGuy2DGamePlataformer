using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaVidaBoss : MonoBehaviour
{
    public GameObject canvaVidaBoss;
    public GameObject boss;
    private BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
        canvaVidaBoss.SetActive(false);
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
        if (collision.gameObject.CompareTag("Player") && boss.GetComponent<BossController>().vidas > 0)
        {
            canvaVidaBoss.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player") || boss.GetComponent<BossController>().vidas <= 0)
            {
                canvaVidaBoss.SetActive(false);
            }
        }
        catch
        {
            
        }

    }
}
