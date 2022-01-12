using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;
    private Vector3 _posA, _posB;
    [SerializeField]
    private bool _moveForward = false;
    [SerializeField]
    private float _speed = 8.0f;

    void Start() 
    {
        _posA = _pointA.position;
        _posB = _pointB.position;
    }

    public void MoveForward()
    {
        _moveForward = true;
    }

    public void MoveBackward() 
    {
        _moveForward = false;
    }

    private void FixedUpdate() 
    {
        if (_moveForward == false) 
        {
            transform.position = Vector3.MoveTowards(transform.position, _posA, _speed * Time.deltaTime);
        } else 
        {
            transform.position = Vector3.MoveTowards(transform.position, _posB, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            MoveBackward();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.parent = null;
        }
    }
}
