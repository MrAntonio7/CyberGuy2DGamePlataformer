using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaController : MonoBehaviour
{

    private string myTag;
    public BoxCollider2D[] boxes;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        myTag = "Caida";
        boxes = GetComponentsInChildren<BoxCollider2D>();

        foreach (Transform hijo in transform)
        {
            hijo.gameObject.tag = myTag;
        }

        foreach (BoxCollider2D collider in boxes)
        {
            collider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
