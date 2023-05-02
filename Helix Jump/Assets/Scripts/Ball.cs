using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 400f;
    public GameObject splitPrefab;
    public AudioSource bounceAudio;
    public AudioSource GameOverSoundAudio;
    public AudioSource LevelWinSoundAudio;


    public GameObject splash;
    ParticleSystem splashPs;


    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        splashPs = splash.GetComponent<ParticleSystem>();
        var emission = splashPs.emission;
        emission.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        splashPs = splash.GetComponent<ParticleSystem>();
        var emission = splashPs.emission;
        emission.enabled = true;
        StartCoroutine(stopSplash());

        bounceAudio.Play();
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        GameObject newsplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y - 0.1f, transform.position.z),
          transform.rotation);
        newsplit.transform.localScale = Vector3.one * Random.Range(0.4f, 0.8f);
        newsplit.transform.parent = other.transform;

        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        
        if(materialName == "Safe Color (Instance)")
        {
            anim.SetBool("temas etti", true);       
        }

        if (materialName == "Unsafe Color (Instance)")
        {
            GameManager.GameOver = true;
            GameOverSoundAudio.Play();
        }

        if (materialName == "Black_and_white_checkered_pattern (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
            LevelWinSoundAudio.Play();
        }
    }
    IEnumerator stopSplash()
    {
        var emission = splashPs.emission;
        yield return new WaitForSeconds(.1f);
        emission.enabled = false;
    }

}
