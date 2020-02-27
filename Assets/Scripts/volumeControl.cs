using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var childs = gameObject.GetComponentsInChildren<AudioSource>();
        foreach(var c in childs)
        {
            c.volume = 0.15f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
