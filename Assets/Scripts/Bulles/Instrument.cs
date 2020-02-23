using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public float time;
    public int volume;//puissance sonore de la source du son en mW
    // Start is called before the first frame update
    void Start()
    {
        time = 1;

        volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
