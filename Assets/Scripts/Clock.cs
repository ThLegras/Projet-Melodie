using UnityEngine;
using System.Collections;
using Valve.VR;

public class Clock : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //
    //  Simple Clock Script / Andre "AEG" Bürger / VIS-Games 2012
    //  Update / Théo Legras 
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------

    //-- set start time 00:00
    public int minutes = 0;
    public int hour = 0;

    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    int seconds;
    float msecs;
    GameObject pointerMinutes;
    float LastTime;
    public Vector2 previous_pos;//acienne position du doigt sur le touchpad
    float number;
    float tempsMesure = 0.05f;
    float lastSpeed = 1.0f;
    bool pushing = false;
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        pointerMinutes = transform.Find("rotation_axis_pointer_minutes").gameObject;

        msecs = 0.0f;
        seconds = 0;
        LastTime = Time.time;
        previous_pos = new Vector2(1.0f, 0.0f);//position initiale de notre doigt
        number = 0.0f;
    }


    public SteamVR_Action_Vector2 touchPadAction;

    void Update()
    {

        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        if (touchPadAction.GetAxis(SteamVR_Input_Sources.RightHand) != Vector2.zero && touchPadAction.GetAxis(SteamVR_Input_Sources.LeftHand) != Vector2.zero)
        {
            if (clockSpeed != 0)
            {
                lastSpeed = clockSpeed;
                clockSpeed = 0;
                pushing = true;
            }
            else if (!pushing)
            {
                clockSpeed = lastSpeed;
            }
        }
        else if (LastTime - Time.time < 0.1f)
        {
            pushing = false;
            if (touchPadValue != Vector2.zero)
            {
                if (((touchPadValue[0] - previous_pos[0]) < 0 && touchPadValue[1] > 0) || ((touchPadValue[0] - previous_pos[0]) > 0 && touchPadValue[1] < 0))
                {
                    clockSpeed -= 0.005f;
                    previous_pos = touchPadValue;
                }
                if (((touchPadValue[0] - previous_pos[0]) > 0 && touchPadValue[1] > 0) || ((touchPadValue[0] - previous_pos[0]) < 0 && touchPadValue[1] < 0))
                {
                    clockSpeed += 0.005f;
                    previous_pos = touchPadValue;
                }
            }
            lastSpeed = clockSpeed;
        }
        number = Input.GetAxis("Mouse ScrollWheel");
        clockSpeed += number;
        //-- calculate time
        msecs += Time.deltaTime * clockSpeed;
        if (msecs >= tempsMesure)
        {
            msecs -= tempsMesure;
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
            }
        }
        else if (msecs <= -tempsMesure)
        {
            msecs += tempsMesure;
            seconds--;
            if (seconds <= 0)
            {
                seconds = 60;
            }
        }


        //-- calculate pointer angles
        float rotationMinutes = (360.0f / 60.0f) * seconds;

        //-- draw pointers
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);

    }

}
