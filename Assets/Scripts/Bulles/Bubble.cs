﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bubble : MonoBehaviour
{

    float time;
    public float note;//stock la note jouée
    float volume;//puissance sonore de la source du son en mW
    float intensite;//calcule l'intensité sonore actuelle du son en mW
    public AudioClip son;//stock le chemin vers le son qui doit être joué
    public Renderer rend;
    public Color color;
    public AudioSource myAudioSource;
    //static float maxIntensite = 90f; //volume max en bD
    float[] freqData = new float[128];
    bool destroyed;
    // Start is called before the first frame update

    float origineScale;

    void Start()
    {
        destroyed = false;
        origineScale = Mathf.Max(transform.parent.transform.lossyScale.x, transform.parent.transform.lossyScale.y, transform.parent.transform.lossyScale.z);
        transform.localScale = new Vector3(1 / origineScale, 1 / origineScale, 1/ origineScale);

        time = transform.root.gameObject.GetComponent<Instrument>().time;
        volume = transform.root.gameObject.GetComponent<Instrument>().volume;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x> 1)
        {
            volume = volume / (4 * Mathf.PI * (float)Mathf.Pow((float)transform.localScale.x, 2f));
        }
        intensite = 10 * Mathf.Log10(volume);
        time = transform.root.gameObject.GetComponent<Instrument>().time;
        if (destroyed)
        {
            Debug.Log("coucou");
            transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localScale = new Vector3((transform.lossyScale.x + time) / origineScale, (transform.lossyScale.x + time) / origineScale, (transform.lossyScale.x + time) / origineScale);
        }
        if (((origineScale * transform.localScale.x) >= 250 || transform.lossyScale.x < 0.8) && !destroyed)
        {
            Debug.Log("local scale : " + (float)transform.localScale.x);
            Debug.Log("lossy scale : " + (float)transform.lossyScale.x);
            destroyed = true;
            Destroy(this.gameObject);
        }
        

        Color oldColor = rend.material.color;
        rend.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f - ((float)(origineScale * transform.localScale.x) /  500.0f));

    }

    private void OnTriggerEnter(Collider other)
    {
        myAudioSource.clip = son;
        myAudioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        myAudioSource.clip = son;
        myAudioSource.Play();
    }
}
