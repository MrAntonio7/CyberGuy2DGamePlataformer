using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowCamaraBaja : MonoBehaviour
{
    private GameObject targetPlayer;
    private float offsetX;

    public float limiteIzq;
    public float limiteDer;

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        offsetX = 9f;
    }


    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {


            if (targetPlayer.transform.position.x < limiteIzq || targetPlayer.transform.position.x > limiteDer)
            {
                if (targetPlayer.transform.position.x < limiteIzq)
                {
                    transform.SetParent(null);
                    transform.position = new Vector3(limiteIzq + offsetX, targetPlayer.transform.position.y +2f, -10f);
                }
                if (targetPlayer.transform.position.x > limiteDer)
                {
                    transform.SetParent(null);
                    transform.position = new Vector3(limiteDer + offsetX, targetPlayer.transform.position.y+2f, -10f);
                }
            }
            else
            {
                transform.SetParent(targetPlayer.transform);
                transform.localPosition = new Vector3(offsetX, 2f, -10f);
            }
            if (targetPlayer.transform.position.y < -20f)
            {
                transform.SetParent(null);
                transform.position = new Vector3(targetPlayer.transform.position.x, -20f, -10f);
            }
       
    }
}

        
