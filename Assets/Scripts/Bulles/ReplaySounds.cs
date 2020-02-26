using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bullegenerator;

public class ReplaySounds : MonoBehaviour
{
    bool alreadyTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!this.alreadyTrigger)
        {
            StartCoroutine(ReplayAllBubbles());
            this.alreadyTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        this.alreadyTrigger = false;
    }

    public void OnMouseDown()
    {
        StartCoroutine(ReplayAllBubbles());
    }

    // Play every bubble saved in allBubblesCreated
    private IEnumerator ReplayAllBubbles()
    {
        Bullegenerator.saveBubblesBool = false;
        if(Bullegenerator.allBubblesCreated.Count != 0)
        {
            float lastTime = Bullegenerator.allBubblesCreated[0].time;
            foreach (SaveBubble s in Bullegenerator.allBubblesCreated)
            {
                yield return new WaitForSeconds(s.time - lastTime);
                Bullegenerator.CreateBubble(s);
                lastTime = s.time;
            }
            Bullegenerator.allBubblesCreated.Clear();
        }
        Bullegenerator.saveBubblesBool = true;
    }
}
