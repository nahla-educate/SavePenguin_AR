using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;

public class pieceScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    
    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        //position x, position y
        transform.position = new Vector3(Random.Range(4f, 10f), Random.Range(-6f, 2f));
    }

    // Update is called once per frame
    void Update()
    {
        //snaps
        if(Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (Selected)
            {
                if(InRightPosition == false)
                {

                    transform.position = RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
           
        }
        
    }
}
