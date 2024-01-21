using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChessGameLogic : MonoBehaviour
{
    GameObject clickedPiece;
    GameObject WhiteClock;
    GameObject BlackClock;
    
    // Start is called before the first frame update
    void Start()
    {
        WhiteClock = GameObject.FindWithTag("Wclock");
        BlackClock = GameObject.FindWithTag("Bclock");
        WhiteClock.GetComponent<Clock>().clockactive = true;
        BlackClock.GetComponent<Clock>().clockactive = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                string color = clickedObject.tag;
                string type = clickedObject.name;
                UnityEngine.Debug.Log("Clicked on " + color + " " + type);

                if (color == "White" || color == "Black") {
                    clickedPiece = clickedObject;
                    UnityEngine.Debug.Log("Clicked on " + clickedPiece.tag + " " + clickedPiece.name);
                }

                if (type == "Chess Board" && clickedPiece != null)
                {
                    // Move the clicked piece to the position where the mouse was clicked
                    Vector3 newPosition = hit.point;
                    newPosition.y = clickedPiece.transform.position.y; // Keep the same y-coordinate
                    clickedPiece.transform.position = newPosition;
                    UnityEngine.Debug.Log("Move " + clickedPiece.tag + " " + clickedPiece.name + " to new position: " + newPosition);
                    if (clickedPiece.tag == "White")
                    {
                        WhiteClock.GetComponent<Clock>().clockactive = false;
                        BlackClock.GetComponent<Clock>().clockactive = true;
                    }
                    else
                    {
                        WhiteClock.GetComponent<Clock>().clockactive = true;
                        BlackClock.GetComponent<Clock>().clockactive = false;
                    }
                    clickedPiece = null;
                }
            }
        }
    }
}
