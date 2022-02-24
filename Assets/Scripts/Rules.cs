using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{

    public float speed = 5.0f;

    // If use "checkSphere"
    float sphereRadius = 0.5f;
    float rando = 0;

    public int checkRadius;

    bool hasMovedPass = false;

    GameObject self;
    GameObject person;
    GameObject otherPerson;
    GameObject[] allPersons;

    Vector3 selfPos = new Vector3(0,0,0);
    Vector3 randMoveX = new Vector3(0,0,0);
    Vector3 randMoveZ = new Vector3(0,0,0);
    Vector3[] persPos;



    // Start is called before the first frame update
    void Start()
    {
        allPersons = GameObject.FindGameObjectsWithTag("person");

        persPos = new Vector3[allPersons.Length];
        //int i = 0;

        foreach (GameObject person in allPersons)
        {
            //Debug.Log("minus: " + (person.transform.position - transform.position));

            if (person.transform.position - transform.position == selfPos)
            {
                //Debug.Log("minus: " + (person.transform.position - transform.position));
                self = person;
                Debug.Log("Self Name = " + self.name + ", self position = " + self.transform.position);
            }
            //if (person != self)
            //{
            //    //for (int i = 0; i < persPos.Length; i++) {
            //    persPos[i] = transform.position;
            //    Debug.Log(persPos[i] + ", " + persPos.Length);
            //    //}
            //    i++;

            //}
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        CheckPerson();
    }

    void getPersonPositions()
    {
        for (int i = 0; i < persPos.Length; i++) {
            persPos[i] = transform.position;
            //Debug.Log(persPos[i] + ", " + persPos.Length);
        }
    }

    void CheckPerson()
    {

        getPersonPositions();

        foreach (GameObject person in allPersons)
        {
            if (person.transform.position == self.transform.position)
            {

            }
            else if (person.transform.position.x - self.transform.position.x < checkRadius
                && person.transform.position.y - self.transform.position.y < checkRadius
                && person.transform.position.z - self.transform.position.z < checkRadius
                && person.transform.position.x - self.transform.position.x > -checkRadius
                && person.transform.position.y - self.transform.position.y > -checkRadius
                && person.transform.position.z - self.transform.position.z > -checkRadius)
            {
                //if (person.transform.position != self.transform.position
                //    || person.transform.position - self.transform.position == selfPos)
                //{
                //Debug.Log(person.transform.position + ", " + self.transform.position);
                //Debug.Log("x: " + person.transform.position.x + ", " + self.transform.position.x);
                //Debug.Log("y: " + person.transform.position.y + ", " + self.transform.position.y);
                //Debug.Log("z: " + person.transform.position.z + ", " + self.transform.position.z);
                otherPerson = person;
                MoveAway();
                //}
            }
            else 
            //if ((person.transform.position.x - self.transform.position.x > 1
            //    && person.transform.position.y - self.transform.position.y > 1
            //    && person.transform.position.z - self.transform.position.z > 1)
            //    || (person.transform.position.x - self.transform.position.x < -1
            //    && person.transform.position.y - self.transform.position.y < -1
            //    && person.transform.position.z - self.transform.position.z < -1))
            {
                hasMovedPass = false;
            }
        }

        // Need to disable colliders?
        //if (Physics.CheckSphere(self.transform.position, sphereRadius))
        //{
        //    //Debug.Log("Person Check.");
        //    //if (transform.position != self.transform.position)
        //        MoveAway();


        //}

        // Need to enable colliders?
        //Collider[] hitColliders = Physics.OverlapSphere(person.transform.position, sphereRadius);
        //foreach (var hitCollider in hitColliders)
        //{
        //    Debug.Log("Person Collider Check.");
        //}


        // Debug.Log("Person Check.");



    }

    void MoveAway()
    {
        transform.Find("Eyes-Nuetral-FBX").gameObject.SetActive(false);
        transform.Find("Eyes-Happy-FBX").gameObject.SetActive(false);
        transform.Find("Eyes-Angry-FBX").gameObject.SetActive(true);

        if (!hasMovedPass)
        {
            MoveRandom();
        }

        transform.position = Vector3.MoveTowards(self.transform.position, self.transform.position + randMoveX + randMoveZ, speed * Time.deltaTime);
        Debug.Log(self.name + " " + self.transform.position + " is running away from " + transform.name + " " + transform.position + " randMove: " + randMoveX + randMoveZ);
        hasMovedPass = true;
    }

    void MoveRandom()
    {
        //rando = Random.value;
        
        //if (rando < 0.25)
        //{
        //    randMove = Vector3.back;
        //}
        //else if (rando > 0.25 && rando < 0.5)
        //{
        //    randMove = Vector3.right;
        //}
        //else if (rando > 0.5 && rando < 0.75)
        //{
        //    randMove = Vector3.left;
        //}
        //else
        //{
        //    randMove = Vector3.forward;
        //}

        if (otherPerson.transform.position.z - self.transform.position.z < 0)
        {
            randMoveZ = Vector3.Scale(Vector3.forward, new Vector3(0,0,10));
        }        
        else if (otherPerson.transform.position.z - self.transform.position.z > 0)
        {
            randMoveZ = Vector3.Scale(Vector3.back, new Vector3(0, 0, 10));
        }        

        if (otherPerson.transform.position.x - self.transform.position.x < 0)
        {
            randMoveX = Vector3.Scale(Vector3.right, new Vector3(10, 0, 0));
        }
        else if (otherPerson.transform.position.x - self.transform.position.x > 0)
        {
            randMoveX = Vector3.Scale(Vector3.left, new Vector3(10, 0, 0));
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
        //Gizmos.DrawWireCube(transform.position, new Vector3(2f,2f,2f));
    }



}
