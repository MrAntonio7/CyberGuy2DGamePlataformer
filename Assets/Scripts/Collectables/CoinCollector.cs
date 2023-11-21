using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer sprite;
    private CapsuleCollider2D colliderCap;
    private Animator anim;
    private GameObject player;
    public AudioClip audioCoin;
    public int value;


    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        colliderCap = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        //Make Collider2D as trigger 
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }


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
        if (collision.CompareTag("Player")) {
            audioSource.PlayOneShot(audioCoin);
            player.GetComponent<Player>().SumaMoneda(value);

            Destroy(sprite);
            Destroy(colliderCap);
            Destroy(anim);
            
            //Destroy(gameObject);
        }
    }

}
