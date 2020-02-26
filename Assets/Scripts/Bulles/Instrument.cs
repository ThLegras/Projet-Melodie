using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Instrument : MonoBehaviour
{
    public float time;
    public int volume;//puissance sonore de la source du son en mW
    public int notes; //nombre de notes jouables sur l'instrument
    // Start is called before the first frame update
    void Start()
    {
        time = 1;

        volume = 0;
    }

    public SteamVR_Action_Vector2 touchPadAction;

    // Update is called once per frame
    void Update()
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        if(touchPadValue != Vector2.zero)
        {
            Debug.Log(touchPadValue);
        }
    }
}
