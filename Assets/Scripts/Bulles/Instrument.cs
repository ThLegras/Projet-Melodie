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
    float LastTime;
    float number;
    // Start is called before the first frame update
    void Start()
    {
        time = 1;
        number = 0.0f;
        previous_pos = new Vector2(1.0f, 0.0f);//position initiale de notre doigt
        volume = 0;
        LastTime = Time.time;
    }

    public SteamVR_Action_Vector2 touchPadAction;

    // Update is called once per frame
    void Update()
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        if(LastTime - Time.time < 0.1f)
        {
            if (touchPadValue != Vector2.zero)
            {
                if (((touchPadValue[0] - previous_pos[0]) < 0 && touchPadValue[1] > 0) || ((touchPadValue[0] - previous_pos[0]) > 0 && touchPadValue[1] < 0))
                {
                    time -= 0.005f;
                }
                if (((touchPadValue[0] - previous_pos[0]) > 0 && touchPadValue[1] > 0) || ((touchPadValue[0] - previous_pos[0]) < 0 && touchPadValue[1] < 0))
                {
                    time += 0.005f;
                }
                Debug.Log(touchPadValue);
            }
        }
        number = Input.GetAxis("Mouse ScrollWheel");
        time += number;
    }
}