using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Instrument : MonoBehaviour
{
    public float time;
    public int volume;//puissance sonore de la source du son en mW
    public int notes; //nombre de notes jouables sur l'instrument
    public Vector2 previous_pos;//acienne position du doigt sur le touchpad

    // Start is called before the first frame update
    void Start()
    {
        time = 1;
        previous_pos = new Vector2(1.0f, 0.0f);//position initiale de notre doigt
        volume = 0;
    }

    public SteamVR_Action_Vector2 touchPadAction;

    // Update is called once per frame
    void Update()
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        if(touchPadValue != Vector2.zero)
        {
            if(((touchPadValue[0]- previous_pos[0])<0 && touchPadValue[1] > 0) || ((touchPadValue[0] - previous_pos[0]) > 0 && touchPadValue[1] < 0))
            {
                time -= 0.01f;
            }
            else if (((touchPadValue[0] - previous_pos[0]) > 0 && touchPadValue[1] > 0) || ((touchPadValue[0] - previous_pos[0]) < 0 && touchPadValue[1] < 0))
            {
                time += 0.01f;
            }
            Debug.Log(touchPadValue);
        }
    }
}