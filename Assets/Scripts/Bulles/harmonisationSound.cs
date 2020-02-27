using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harmonisationSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetComponentInChildren<AudioSource>().volume = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
