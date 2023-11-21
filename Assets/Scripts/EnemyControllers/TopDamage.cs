using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision != null && !gameObject.transform.parent.GetComponent<EnemyController>().anim.GetBool("EstaMuerto"))
        {
            gameObject.transform.parent.GetComponent<EnemyController>().Damage();
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().fisica.AddForce(new Vector3(0, 1, 0), ForceMode2D.Impulse);
        }
    }
}
