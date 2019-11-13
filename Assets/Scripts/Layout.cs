using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout : MonoBehaviour
{
    public Material material1;
    public Material material2;
    public bool tracked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tracked)
        {
            this.GetComponent<Renderer>().material = material1;
        }
        else
        {
            this.GetComponent<Renderer>().material = material2;
        }

        tracked = false;
    }
}
