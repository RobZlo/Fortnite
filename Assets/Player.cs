using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform hand;
    public GameObject cubePrefab;
    public GameObject ballPrefab;
    public float throwForce;
    public Camera cam;
    public LayerMask interactionMask;
    public float maxRange;
    public GameObject bombPrefab;

    private GameObject objInHand;

    public static GameObject instance;
    // Start is called before the first frame update


    void Awake()
    {
        Player.instance = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Instantiate(cubePrefab, hand.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject ball = Instantiate(ballPrefab, hand.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject bomb = Instantiate(bombPrefab, hand.position, Quaternion.identity);
            bomb.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);

        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(maxRange));
            if (Physics.Raycast(ray, out hit, maxRange, interactionMask))
            {
                if (objInHand == null)
                {
                    objInHand = hit.transform.gameObject;
                    objInHand.GetComponent<Rigidbody>().isKinematic = true;
                    objInHand.transform.position = hand.position;
                    objInHand.transform.parent = hand;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objInHand != null)
            {
                objInHand.transform.parent = null;
                objInHand.GetComponent<Rigidbody>().isKinematic = false;
                objInHand.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                objInHand = null;

            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0.33f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

    }
}
