using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullegenerator : MonoBehaviour
{
    private List<GameObject> objects;
    new Renderer renderer;
    Color temp;
    public AudioClip son;// son qui doit être joué
    public float note;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        renderer = GetComponent<Renderer>();
        temp = renderer.material.color;
        note = 50;        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject sphere = GameObject.Instantiate(GameObject.Find("BubbleExample"), transform);
        sphere.AddComponent<Bubble>();
        sphere.transform.SetParent(transform.root.transform);//on désigne le cube comme parent de l'objet
        sphere.transform.GetComponent<Renderer>().material = mat;
        sphere.gameObject.GetComponent<Bubble>().myAudioSource = GetComponent<AudioSource>();
        sphere.gameObject.GetComponent<Bubble>().note = note;
        sphere.gameObject.GetComponent<Bubble>().son = son;
        sphere.transform.position = transform.position;
        objects.Add(sphere);




        GameObject invSphere = GameObject.Instantiate(GameObject.Find("BubbleExampleReverse"), transform);
        invSphere.AddComponent<ReversedBubble>();
        invSphere.transform.SetParent(transform.root.transform);//on désigne le cube comme parent de l'objet
        invSphere.transform.GetComponent<Renderer>().material = mat;
        invSphere.transform.position = transform.position;
        objects.Add(invSphere);
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
