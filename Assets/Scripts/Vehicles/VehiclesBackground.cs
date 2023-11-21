using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesBackground : MonoBehaviour
{
    private float posIniX;

    private float posIniY;

    public float speed;

    private Rigidbody2D rb;

    private GameObject target;
    public bool alreves;
    // Start is called before the first frame update
    void Start()
    {
        
        posIniY = transform.position.y;
        target = GameObject.FindGameObjectWithTag("Player");
        rb= GetComponent<Rigidbody2D>();
        transform.localPosition = new Vector3(posIniX,posIniY, transform.localPosition.z);
        if (alreves)
        {
            posIniX = target.transform.position.x + 50;
            
        }
        else
        {
            posIniX = target.transform.position.x - 50;
            
        }
        transform.position = new Vector3(posIniX, posIniY, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (speed > 0)
        {
            if (transform.position.x < target.transform.position.x+50f)
            {
                rb.velocity = new Vector3(speed, posIniY, 0f);
            }
            else
            {
                transform.position = new Vector3(target.transform.position.x - 50f, posIniY, 10f);
            }
        }
        else if (speed < 0) 
        {
            if (transform.position.x > target.transform.position.x - 50f)
            {
                rb.velocity = new Vector3(speed, posIniY, 0f);
            }
            else
            {
                transform.position = new Vector3(target.transform.position.x + 50f, posIniY, 10f);
            }
        }



    }

    
}
