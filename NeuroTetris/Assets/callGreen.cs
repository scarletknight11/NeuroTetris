using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callGreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            TrafficLight.sendGreen();
        }

    }

    void OnMouseDown() {
     print("Clicked Green");
     TrafficLight.sendGreen();
   }
}
