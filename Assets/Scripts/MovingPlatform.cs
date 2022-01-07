using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private int _currentTargetIndex = 0;
    private Vector3[] waypoints;

    void Start() 
    {
        Transform[] transforms = transform.Cast<Transform>().ToArray();
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < transforms.Length; i++) 
        {
            positions.Add(new Vector3(transforms[i].position.x, transforms[i].position.y, transforms[i].position.z));
        }
        waypoints = positions.ToArray();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.childCount > 0) 
        {
            // Check for moving platform childs
            Vector3 currentTarget = waypoints[_currentTargetIndex];
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * _speed);

            if (transform.position == currentTarget) 
            {
                if (_currentTargetIndex == transform.childCount - 1) 
                {
                    _currentTargetIndex = 0;
                } else 
                {
                    _currentTargetIndex++;
                }
            }
        }
    }
}
