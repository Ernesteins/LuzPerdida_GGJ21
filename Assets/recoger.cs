using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoger : MonoBehaviour
{
    public float pickupRange = 5;
    public float MoveForce = 350;
    [SerializeField] KeyCode key = KeyCode.E;
    public Transform holdParent;
    private GameObject heldObject;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }

                if (heldObject != null)
                {
                    MoveObject();
                }
            }
            else
            {
                DropObject();
            }
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObject.transform.position, holdParent.position)> 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * MoveForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;
            objRig.transform.parent = holdParent;
            heldObject = pickObj;
        }

    }
    
    void DropObject()
    {
        Rigidbody heldRig = heldObject.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;
        heldRig.transform.parent = null;
        heldObject = null;
    }
}
