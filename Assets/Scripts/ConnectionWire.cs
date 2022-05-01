using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionWire : MonoBehaviour
{
    public Transform connectionWireEnd;
    public Lever connectedLever;
    ConnectionPin connectedPin;
    bool isHovered;
    bool isGrabbed;
    Camera cam;
    const float VERTICAL_OFFSET = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //On met en cache la ref a la camera car l'opération camera.main est lourde.
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Detecte un click gauche pour attraper
        if (isHovered && Input.GetMouseButtonDown(0))
        {
            isGrabbed = true;

            //Annule la connection a un pin si il y a
            if (connectedPin != null)
            {
                connectedPin.ReleaseWire();
                connectedPin = null;
            }
        }

        //Relache l'objet si on relache le bouton
        if (!Input.GetMouseButton(0) && isGrabbed)
            ReleaseGrab();

        if (isGrabbed)
        {
            //On envoie une ray depuis le curseur dans le monde
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(camRay, out RaycastHit hit))
            {
                //Suit le curseur avec une léger décale en hauteur
                connectionWireEnd.position = hit.point + Vector3.up * VERTICAL_OFFSET;
            }
        }
    }

    void OnMouseEnter()
    {
        //Note that the mouse is hovering the object
        isHovered = true;
    }

    void OnMouseExit()
    {
        //Note that the mouse stopped hovering the object
        isHovered = false;
    }

    void ReleaseGrab()
    {
        isGrabbed = false;
        connectedPin = null;

        //On envoie une ray depuis le curseur dans le monde
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit hit))
        {
            //Si on touche un connectionPin, on se connecte au pin
            ConnectionPin hitPin = hit.collider.GetComponent<ConnectionPin>();
            if(hitPin != null)
            {
                connectedPin = hitPin;
                connectionWireEnd.position = hitPin.transform.position;
                connectedPin.ConnectWire(this);
            }
        }

        //On replace le cable à l'origine si on ne se connecte pas a un pin
        if(connectedPin == null)
            connectionWireEnd.localPosition = Vector3.zero;
    }
}
