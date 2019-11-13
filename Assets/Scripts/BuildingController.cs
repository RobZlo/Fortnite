using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour

{
    Ray ray;
    RaycastHit hit;
    public GameObject ground;
    public GameObject player;
    public GameObject groundLayout;
    private GameObject layoutContrainer;
    private Vector3 layoutVector;
    private bool VectorSet = false;

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
            if(hit.distance >= 10)
            {
                Destroy(layoutContrainer);
                VectorSet = false;
            }

            if (layoutVector != hit.point && VectorSet)
            {
                layoutContrainer.transform.position = hit.point;
                layoutContrainer.transform.rotation = player.transform.rotation;
            }
            if(hit.distance < 10 && !VectorSet)
            {
                layoutContrainer = Instantiate(groundLayout, hit.point, player.transform.rotation);
                layoutVector = hit.point;
                VectorSet = true;
            }

            if(hit.collider.gameObject.tag == "Layout" && hit.distance < 10)
            {
                hit.collider.gameObject.GetComponent<Layout>().tracked = true;
                Destroy(layoutContrainer);
                VectorSet = false;
            }

            if(hit.distance < 10 && hit.collider.gameObject.tag == "Component")
            {
                Destroy(layoutContrainer);
                VectorSet = false;
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
