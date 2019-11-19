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
    private GameObject layoutContrainer;
    private Vector3 layoutVector;
    private bool VectorSet = false;
    private int modus = 0;

    // Start is called before the first frame update
    void Start()
    {
        buildingObject = ground;
        layout = groundLayout;
        modus = 0;
        imageGround.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        // Condition which modus is activated to hitmark the image in the Panel
        if(Input.GetKey(KeyCode.F1))
        {
            modus = 0;
            imageGround.color = Color.green;
            imageRamp.color = Color.white;
            imageWall.color = Color.white;
            buildingObject = ground;
            layout = groundLayout;
            Destroy(layoutContrainer);
            VectorSet = false;
        }
        else if(Input.GetKey(KeyCode.F2))
        {
            modus = 1;
            imageGround.color = Color.white;
            imageRamp.color = Color.green;
            imageWall.color = Color.white;
            buildingObject = ramp;
            layout = rampLayout;
            Destroy(layoutContrainer);
            VectorSet = false;
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            modus = 2;
            imageGround.color = Color.white;
            imageRamp.color = Color.white;
            imageWall.color = Color.green;
            buildingObject = wall;
            layout = wallLayout;
            Destroy(layoutContrainer);
            VectorSet = false;
        }

   

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
                layoutContrainer = Instantiate(layout, hit.point, player.transform.rotation);
                layoutVector = hit.point;
                VectorSet = true;
            }

            if(hit.collider.gameObject.tag == "Layout" && hit.distance < 10)
            {
                hit.collider.gameObject.GetComponent<Layout>().tracked = true;
                hit.collider.gameObject.GetComponent<Layout>().buildingObject.modus = modus;
                Destroy(layoutContrainer);
                VectorSet = false;
            }

            if(hit.distance < 10 && hit.collider.gameObject.tag == "BuildingObject")
            {
                hit.collider.gameObject.GetComponent<BuildingObject>().modus = modus;
                Destroy(layoutContrainer);
                VectorSet = false;
            }



            if(Input.GetMouseButtonDown(0) && hit.distance < 10)
            {
                if(hit.collider.gameObject.tag == "BuildingObject")
                {

                }
                else if(hit.collider.gameObject.tag == "Layout")
                {
                    GameObject container;
                    container = Instantiate(buildingObject, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                }
                else
                {
                    if(modus == 1)
                    {
                        GameObject gameObjectInstance = Instantiate(buildingObject, hit.point, player.transform.rotation);
                        gameObjectInstance.transform.Rotate(0, 90, -45);
                    }
                    else if(modus == 2)
                    {
                        GameObject gameObjectInstance = Instantiate(buildingObject, hit.point, player.transform.rotation);
                        gameObjectInstance.transform.Rotate(90, 0, 0);
                    }
                    else
                    {
                        Instantiate(buildingObject, hit.point, player.transform.rotation);
                    }
                }
            }
        }
    }
}
