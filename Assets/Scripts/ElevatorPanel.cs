using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    private MeshRenderer _callButtonMeshRenderer;
    [SerializeField]
    private int _coinsRequired = 8;
    [SerializeField]
    private Elevator _elevator;

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
            Player player = other.GetComponent<Player>();
            if (Input.GetKeyDown(KeyCode.E) && player.Coins >= _coinsRequired) 
            {
                _callButtonMeshRenderer.material.SetColor("_Color", Color.green);
                _elevator.MoveForward();
            }
        }
    }
}
