using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Player variables
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;


    // Handlers
    private CharacterController _controller;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null) 
        {
            Debug.LogError("Character Controller is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        float h = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(h, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded) 
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                _yVelocity = _jumpHeight;
            }
        } else 
        {
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
}
