using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour

{
    Ray ray;
    RaycastHit hit;
    public GameObject player;
    public GameObject buildingObject;
    public GameObject ground;
    public GameObject groundLayout;
    public GameObject ramp;
    public GameObject rampLayout;
    public GameObject wall;
    public GameObject wallLayout;
    public GameObject layout;
    public Image imageGround;
    public Image imageRamp;
    public Image imageWall;
    private GameObject layoutContainer;
    private Vector3 layoutVector;
    private bool VectorSet = false;
    private bool buildModeOn = false;
    private int mode = 0;

    public int bulletsExists = 0;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    //Takes care of the shooting mechanism

    

  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            buildModeOn = false;
            Destroy(layoutContainer);
            VectorSet = false;
            imageGround.color = Color.white;
            imageRamp.color = Color.white;
            imageWall.color = Color.white;

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            buildModeOn = false;
            Destroy(layoutContainer);
            VectorSet = false;
            imageGround.color = Color.white;
            imageRamp.color = Color.white;
            imageWall.color = Color.white;
        }

        // Condition which modus is activated to hitmark the image in the Panel
        if (Input.GetKey(KeyCode.F1))
        {
            buildModeOn = true;
            mode = 0;
            imageGround.color = Color.green;
            imageRamp.color = Color.white;
            imageWall.color = Color.white;
            buildingObject = ground;
            layout = groundLayout;
            Destroy(layoutContainer);
            VectorSet = false;
        }
        else if(Input.GetKey(KeyCode.F2))
        {
            buildModeOn = true;
            mode = 1;
            imageGround.color = Color.white;
            imageRamp.color = Color.green;
            imageWall.color = Color.white;
            buildingObject = ramp;
            layout = rampLayout;
            Destroy(layoutContainer);
            VectorSet = false;
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            buildModeOn = true;
            mode = 2;
            imageGround.color = Color.white;
            imageRamp.color = Color.white;
            imageWall.color = Color.green;
            buildingObject = wall;
            layout = wallLayout;
            Destroy(layoutContainer);
            VectorSet = false;
        }

   

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.distance >= 10)
            {
                Destroy(layoutContainer);
                VectorSet = false;
            }

            if (layoutVector != hit.point && VectorSet)
            {
                layoutContainer.transform.position = hit.point;
                layoutContainer.transform.rotation = player.transform.rotation;
            }
            if(hit.distance < 10 && !VectorSet  && bulletsExists == 0 && buildModeOn == true)
            {
                layoutContainer = Instantiate(layout, hit.point, player.transform.rotation);
                layoutVector = hit.point;
                VectorSet = true;
            }

            if(hit.collider.gameObject.tag == "Layout" && hit.distance < 10 && bulletsExists == 0 && buildModeOn == true)
            {
                hit.collider.gameObject.GetComponent<Layout>().tracked = true;
                hit.collider.gameObject.GetComponent<Layout>().buildingObject.mode = mode;
                Destroy(layoutContainer);
                VectorSet = false;
            }

            if(hit.distance < 10 && hit.collider.gameObject.tag == "BuildingObject")
            {
                if(hit.collider.gameObject.GetComponent<BuildingObject>() != null)
                {
                    hit.collider.gameObject.GetComponent<BuildingObject>().mode = mode;
                    Destroy(layoutContainer);
                    VectorSet = false;
                }
               
            }
            



            if(Input.GetMouseButtonDown(0) && hit.distance < 10)
            {
                if(hit.collider.gameObject.tag == "BuildingObject")
                {

                }
                else if(hit.collider.gameObject.tag == "Layout" && buildModeOn == true)
                {
                    GameObject container;
                    
                    if(mode == 2)
                    {
                        container = Instantiate(buildingObject, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                        if(!hit.collider.gameObject.transform.parent.gameObject.name.Equals("Wall"))
                        {
                            container.transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        container = Instantiate(buildingObject, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                    }

                    container.transform.SetParent(hit.collider.gameObject.transform.parent);
                }
                else
                {
                    if(mode == 1 && buildModeOn == true)
                    {
                        GameObject gameObjectInstance = Instantiate(buildingObject, hit.point, player.transform.rotation);
                        gameObjectInstance.transform.Rotate(0, 90, -45);
                        gameObjectInstance.transform.Translate(-2.5f, 0, 0);
                    }
                    else if(mode == 2 && buildModeOn == true)
                    {
                        GameObject gameObjectInstance = Instantiate(buildingObject, hit.point, player.transform.rotation);
                        gameObjectInstance.transform.Rotate(90, 0, 0);
                        gameObjectInstance.transform.Translate(0,0,-2.5f);
                    }
                    else
                    {
                        if(buildModeOn == true)
                        {
                            Instantiate(buildingObject, hit.point, player.transform.rotation);
                        }
                       
                    }
                }
            }
        }
    }
}
