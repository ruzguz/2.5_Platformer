using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // Player variables
    [SerializeField]
    const float DEFAULT_SPEED = 7.0f;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    [SerializeField]
    private float _pushForce = 2f;
    
    // Aux Variables
    private float _yVelocity;
    [SerializeField]
    private bool _doubleJump = true;
    
    // Wall Jump Variables
    [SerializeField]
    private bool _canWallJump = false;
    private Vector3 _wallJumpDirection;
    private Vector3 _wallJumpBoost;
    [SerializeField]
    private float _wallJumpForce = 5.0f;

    // Game Variables
    [SerializeField]
    private int _coins;

    public int Coins 
    {
        set 
        {
            _coins = value;
        }
        get 
        {
            return _coins;
        }
    }

    [SerializeField]
    private int _lives = 3;

    public int Lives 
    {
        set;
        get;
    }

    [SerializeField]
    private float _deathZone = -8;
    private Vector3 _respawnPosition;


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

        UIManager.Instance.UpdateLivesDisplay(_lives);
        _respawnPosition = transform.position;
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
            SetWallJumpPhysics(false, true, Vector3.zero, DEFAULT_SPEED);

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                _yVelocity = _jumpHeight;
            }
        } else 
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                
                if (_canWallJump ==  true) 
                {
                    _yVelocity = _jumpHeight;
                    SetWallJumpPhysics(false, false, _wallJumpDirection * _wallJumpForce, 1.0f);
                }
                else if (_doubleJump == true) 
                {
                    _yVelocity = _jumpHeight;
                    _doubleJump = false;
                }
                
            } else 
            {
                _yVelocity -= _gravity;
            } 

        }

        velocity.y = _yVelocity;
        velocity += _wallJumpBoost;

        _controller.Move(velocity * Time.deltaTime);

        if (transform.position.y <= _deathZone) 
        {
            // Update lives
            _lives--;
            UIManager.Instance.UpdateLivesDisplay(_lives);
            
            // Respawn Player
            transform.position = _respawnPosition;

            if (_lives <= 0) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {

        Rigidbody body = hit.collider.attachedRigidbody;

        if (hit.transform.tag == "MovingBox") 
        {
            if (body == null || body.isKinematic) 
            {
                return;
            }

            if (hit.moveDirection.y < -0.3f) 
            {
                return;
            }

            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);

            body.velocity = pushDirection * _pushForce;
        }
        


        if (_controller.isGrounded == false && hit.transform.tag == "Wall") 
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _canWallJump = true;
            _wallJumpBoost = Vector3.zero;
            _wallJumpDirection = hit.normal;
        }
    }

    private void SetWallJumpPhysics(bool canWallJump, bool doubleJump, Vector3 wallJumpBoost, float speed)
    {
        _canWallJump = canWallJump;
        _doubleJump = doubleJump;
        _wallJumpBoost = wallJumpBoost;
        _speed = speed;
    }
}
