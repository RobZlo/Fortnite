using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour

{
    Ray ray;
    RaycastHit hit;
    public GameObject ground;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.distance < 10 && hit.collider.gameObject.tag == "Layout")
            {
                hit.collider.gameObject.GetComponent<Layout>().tracked = true;
            }

            if(Input.GetMouseButtonDown(0) && hit.distance < 10)
            {
                if(hit.collider.gameObject.tag == "Component")
                {

                }
                else if(hit.collider.gameObject.tag == "Layout")
                {
                    GameObject container;
                    container = Instantiate(ground, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(ground, hit.point, player.transform.rotation);
                }
            }
        }
        
    }
}
