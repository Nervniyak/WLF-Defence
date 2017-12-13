using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTriggerEnter : MonoBehaviour
{

    public List<GameObject> GameObjects;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && GameObjects.Count > 0)
        {
            foreach (var gObject in GameObjects)
            {
                if (gObject)
                {
                    
                
                gObject.SetActive(true);
                }
            }
        }
    }
}
