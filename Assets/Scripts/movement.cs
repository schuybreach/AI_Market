using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* -------------To Add-------------/
 * > More people
 * > Detailed models
 * > People follow only the paths
 * > More POI (benches and stuff)
 * > People pause at poi's
 * > People move at different speeds
 * > People entering and exiting area
 * > Vendors / people at stalls
 * > ...
 *--------------------------------*/



public class Movement : MonoBehaviour
{

    public float speed = 20.0f;

    // If use "checkSphere"
    float sphereRadius = 0.5f;


    // poi = point of interest (food stall, bench, etc.) 
    GameObject[] poi;

    int stallCount = 0;
    int randomNumber = 0;

    Vector3 poiPosition = new Vector3(0,0,0);
    float poiPosX = 0f;
    float poiPosY = 0f;
    float poiPosZ = 0f;

    public float yHeight;

    float floatingHeight = 1f;

    //Vector3 personPos = new Vector3(0f, 13.5f, 0f);
    //float personPosX = 0f;
    //float personPosY = 13.5f;
    //float personPosZ = 0f;

    Vector3 personPos = new Vector3(0f, 0f, 0f);
    float personPosX = 0f;
    float personPosY = 0f;
    float personPosZ = 0f;


    bool atPos = false;


    public int damp = 5; // we can change the slerp velocity here


    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Eyes-Angry-FBX").gameObject.SetActive(false);
        transform.Find("Eyes-Happy-FBX").gameObject.SetActive(false);
        transform.Find("Eyes-Nuetral-FBX").gameObject.SetActive(true);

        poi = GameObject.FindGameObjectsWithTag("stall");

        stallCount = poi.Length;

        RandomNumber();


        GetPersonPos();
        GetPoiPos();


        //foreach (GameObject p in poi)
        //{
        //    Debug.Log("Stall position: " + p.transform.position);

        //}


    }

    // Update is called once per frame
    void Update()
    {


        GetPersonPos();

        transform.position = Vector3.MoveTowards(personPos, poiPosition, speed * Time.deltaTime);

        Quaternion rotationAngle = Quaternion.LookRotation(poiPosition - transform.position); // we get the angle has to be rotated
        rotationAngle *= Quaternion.Euler(0, -75, 0); // this adds a -75 degrees Y rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * damp); // we rotate the rotationAngle 


        //Debug.Log("poi[]: " + poi[randomNumber] + " | poiPosition: " + poiPosition);


        if (transform.position == poiPosition)
        {
            transform.Find("Eyes-Nuetral-FBX").gameObject.SetActive(false);
            transform.Find("Eyes-Angry-FBX").gameObject.SetActive(false);
            transform.Find("Eyes-Happy-FBX").gameObject.SetActive(true);
            RandomNumber();
            GetPoiPos();
        }


    }

    void RandomNumber()
    {

        randomNumber = Random.Range(0, stallCount);

        //Debug.Log("stallCount: " + stallCount + " | randomNumber: " + randomNumber);

    }

    void GetPersonPos ()
    {
        //personPos = transform.position;

        personPosX = transform.position.x;
        //personPosY = 13.5f; //transform.position.y;
        //personPosY = transform.position.y;
        personPosY = yHeight;
        personPosZ = transform.position.z;

        personPos = new Vector3(personPosX, personPosY, personPosZ);

    }

    void GetPoiPos ()
    {

        poiPosX = poi[randomNumber].transform.position.x;
        //poiPosY = 13.5f; //poi[randomNumber].transform.position.y;
        //poiPosY = poi[randomNumber].transform.position.y;
        poiPosY = yHeight;
        poiPosZ = poi[randomNumber].transform.position.z;

        poiPosition = new Vector3(poiPosX, poiPosY, poiPosZ);


    }


}
