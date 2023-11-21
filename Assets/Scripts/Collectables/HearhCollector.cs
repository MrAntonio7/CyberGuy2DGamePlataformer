using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearhCollector : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer sprite;
    private CircleCollider2D colliderCap;
    private Animator anim;
    private GameObject player;
    public AudioClip audioCoin;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        colliderCap = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        //Make Collider2D as trigger 
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<Player>().vidas < player.GetComponent<Player>().limiteVidas)
        {
            audioSource.PlayOneShot(audioCoin);
            player.GetComponent<Player>().SumarORestarVidas(true);

            Destroy(sprite);
            Destroy(colliderCap);
            Destroy(anim);

            //Destroy(gameObject);
        }
    }
}
