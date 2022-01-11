using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    private MeshRenderer _callButtonMeshRenderer;

    void Start() 
    {
        _callButtonMeshRenderer = transform.Find("Call_Button").GetComponent<MeshRenderer>();

        if (_callButtonMeshRenderer == null) 
        {
            Debug.LogError("Call Button Mesh Renderer is null");
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                _callButtonMeshRenderer.material.SetColor("_Color", Color.green);
            }
        }
    }
}
