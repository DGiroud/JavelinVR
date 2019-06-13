using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlider : MonoBehaviour
{
    public Collider[] Slidernode;

    public float targetTemp = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        targetTemp = 0;
        foreach (var collider in Slidernode)
        {
            targetTemp += 0.05f;

            if (collider == collision)                
                break;           
        }
    }
}
