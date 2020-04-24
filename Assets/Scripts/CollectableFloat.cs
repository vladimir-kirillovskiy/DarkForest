using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFloat : MonoBehaviour
{
    // Start is called before the first frame update
     public float amplitude = 0.1f;          //Set in Inspector 
     public float speed = 5.0f;                  //Set in Inspector 
     private float tempVal;
     private Vector3 tempPos;
    void Start()
    {
        tempVal = transform.position.y;
        tempPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }
}
