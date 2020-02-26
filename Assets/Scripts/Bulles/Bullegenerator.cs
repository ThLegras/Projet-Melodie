using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullegenerator : MonoBehaviour
{
    public static List<SaveBubble> allBubblesCreated = new List<SaveBubble>();
    public static bool saveBubblesBool = true;

    private List<GameObject> objects;
    new Renderer renderer;
    Color temp;
    public AudioClip son;// son qui doit être joué
    public float note;
    public Material mat;
    float m_Hue;
    float m_Saturation;
    float m_Value;

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        renderer = GetComponent<Renderer>();
        temp = renderer.material.color;
        m_Hue = (float)note / transform.root.GetComponent<Instrument>().notes;
        //Debug.Log(m_Hue);
        m_Saturation = 0.8f;
        m_Value = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CreateBubble(SaveBubble save)
    {
        GameObject sphere = GameObject.Instantiate(GameObject.Find("BubbleExample"), save.sourceTransform);
        sphere.AddComponent<Bubble>();
        sphere.transform.SetParent(save.sourceTransform.root.transform);//on désigne le cube comme parent de l'objet
        sphere.transform.GetComponent<Renderer>().material.color = save.color;
        sphere.gameObject.GetComponent<Bubble>().myAudioSource = save.audio;
        sphere.gameObject.GetComponent<Bubble>().note = save.note;
        sphere.gameObject.GetComponent<Bubble>().son = save.son;
        sphere.transform.position = save.sourceTransform.position;
        //objects.Add(sphere);


        GameObject invSphere = GameObject.Instantiate(GameObject.Find("BubbleExampleReverse"), save.sourceTransform);
        invSphere.AddComponent<ReversedBubble>();
        invSphere.transform.SetParent(save.sourceTransform.root.transform);//on désigne le cube comme parent de l'objet
        invSphere.transform.GetComponent<Renderer>().material.color = save.color;
        invSphere.transform.position = save.sourceTransform.position;
        //objects.Add(invSphere);
    }

    private void OnMouseDown()
    {
        SaveBubble save = new SaveBubble(transform, Color.HSVToRGB(m_Hue, m_Saturation, m_Value), GetComponent<AudioSource>(), note, son, Time.time);
        if (Bullegenerator.saveBubblesBool)
            Bullegenerator.allBubblesCreated.Add(save);
        Bullegenerator.CreateBubble(save);
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveBubble save = new SaveBubble(transform, Color.HSVToRGB(m_Hue, m_Saturation, m_Value), GetComponent<AudioSource>(), note, son, Time.time);
        if (Bullegenerator.saveBubblesBool)
            Bullegenerator.allBubblesCreated.Add(save);
        Bullegenerator.CreateBubble(save);
    }

    /*private void OnHandOverBegin()
    {
        GameObject sphere = GameObject.Instantiate(GameObject.Find("Sphere"), transform);
        sphere.AddComponent<Bubble>();
        sphere.transform.SetParent(transform);//on désigne le cube comme parent de l'objet
        sphere.transform.GetComponent<Renderer>().material = mat;
        objects.Add(sphere);
        GameObject invSphere = GameObject.Instantiate(GameObject.Find("ReversedSphere"), transform);
        invSphere.AddComponent<ReversedBubble>();
        invSphere.transform.SetParent(transform);//on désigne le cube comme parent de l'objet
        invSphere.transform.GetComponent<Renderer>().material = mat;
        objects.Add(invSphere);
        Debug.Log("interaction");
    }*/


    private void OnMouseEnter()
    {
        transform.GetComponent<Renderer>().material.color = new Color(1 - temp.r, 1 - temp.g, 1 - temp.b, -1.0f);
    }

    private void OnMouseExit()
    {
        renderer.material.color = temp;
    }
}

/**
 * Used to save information about bubble created. Used in case of sound played history.
 */
public struct SaveBubble
{
    public Transform sourceTransform;
    public Color color;
    public AudioSource audio;
    public float note;
    public AudioClip son;
    public float time;

    public SaveBubble(Transform sourceTransform, Color color, AudioSource audio, float note, AudioClip son, float time)
    {
        this.sourceTransform = sourceTransform;
        this.color = color;
        this.audio = audio;
        this.note = note;
        this.son = son;
        this.time = time;
    }
}