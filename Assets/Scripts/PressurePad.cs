using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        Debug.Log("We are in the trigger");
        if (other.CompareTag("MovingBox")) 
        {
            Debug.Log("MovingBox");
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if ( distance < 0.05f) 
            {
                other.transform.position = transform.position;
                other.attachedRigidbody.isKinematic = true;
                other.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }
}
