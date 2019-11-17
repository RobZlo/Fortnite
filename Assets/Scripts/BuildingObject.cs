using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public int modus = 0;
    public GameObject layoutGround1;
    public GameObject layoutGround2;
    public GameObject layoutGround3;
    public GameObject layoutGround4;

    public GameObject layoutRamp1;
    public GameObject layoutRamp2;
    public GameObject layoutRamp3;
    public GameObject layoutRamp4;

    public GameObject layoutWall1;
    public GameObject layoutWall2;
    public GameObject layoutWall3;
    public GameObject layoutWall4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(modus)
        {
            case 0: layoutGround1.SetActive(true);
                    layoutGround2.SetActive(true);
                    layoutGround3.SetActive(true);
                    layoutGround4.SetActive(true);
                    layoutRamp1.SetActive(false);
                    layoutRamp2.SetActive(false);
                    layoutRamp3.SetActive(false);
                    layoutRamp4.SetActive(false);
                    layoutWall1.SetActive(false);
                    layoutWall2.SetActive(false);
                    layoutWall3.SetActive(false);
                    layoutWall4.SetActive(false);
                break;
            case 1:
                    layoutGround1.SetActive(false);
                    layoutGround2.SetActive(false);
                    layoutGround3.SetActive(false);
                    layoutGround4.SetActive(false);
                    layoutRamp1.SetActive(true);
                    layoutRamp2.SetActive(true);
                    layoutRamp3.SetActive(true);
                    layoutRamp4.SetActive(true);
                    layoutWall1.SetActive(false);
                    layoutWall2.SetActive(false);
                    layoutWall3.SetActive(false);
                    layoutWall4.SetActive(false);
                break;
            case 2:
                layoutGround1.SetActive(false);
                layoutGround2.SetActive(false);
                layoutGround3.SetActive(false);
                layoutGround4.SetActive(false);
                layoutRamp1.SetActive(false);
                layoutRamp2.SetActive(false);
                layoutRamp3.SetActive(false);
                layoutRamp4.SetActive(false);
                layoutWall1.SetActive(true);
                layoutWall2.SetActive(true);
                layoutWall3.SetActive(true);
                layoutWall4.SetActive(true);
                break;
        }
    }
}
