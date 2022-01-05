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
    [SerializeField]
    private bool _doubleJump = true;


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
            _doubleJump = true;
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                _yVelocity = _jumpHeight;
            }
        } else 
        {
            if (Input.GetKeyDown(KeyCode.Space) && _doubleJump == true) 
            {
                _yVelocity = _jumpHeight;
                _doubleJump = false;
            } else 
            {
                _yVelocity -= _gravity;
            } 

        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
}