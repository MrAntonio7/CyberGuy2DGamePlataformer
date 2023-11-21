using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMusic : MonoBehaviour
{

    
    private AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoverCursor()
    {
        audioSource.PlayOneShot(audioClip, 0.7f);
    }
}
