using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    int orderP = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
           
            if (hit.transform != null && hit.transform.CompareTag("Puzzle"))
            {
                if (!hit.transform.GetComponent<pieceScript>().InRightPosition)
                {
                    Debug.Log("select and make it in the right position");
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<pieceScript>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = orderP;
                    orderP++;

                }

            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(SelectedPiece != null)
            {
                SelectedPiece.GetComponent<pieceScript>().Selected = false;
                SelectedPiece = null;
            }

        }
        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }
    }
}
