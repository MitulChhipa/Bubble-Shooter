using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float _rotationMultiplier;
    [SerializeField] private Transform _transform;
    [SerializeField] private GameObject _shootingBall;
    [SerializeField] private Rigidbody _shootingBallRigidbody;
    [SerializeField] private Transform _ballTransform;

    private RaycastHit _hit;
    private Ray _ray;
    private float _horizontalInput;
    private Vector3 _rotation;
    public bool shoot;

    private void Start()
    {
        _rotation = new Vector3(0,0,0);
        lineRenderer.positionCount = 2;
    }


    private void Update()
    {
        DrawDirection();
        _horizontalInput = Input.GetAxis("Mouse X");
        _rotation.y = _rotation.y + (_horizontalInput * _rotationMultiplier * Time.deltaTime);
        _rotation.y = Mathf.Clamp(_rotation.y, -60f, 60f);
        _transform.rotation = Quaternion.Euler(_rotation);
        SetDirectionOfBall();
    }
    
    private void DrawDirection()
    {
        _ray.origin = _transform.position;
        _ray.direction = _transform.forward;
        if (Physics.Raycast(_ray, out _hit, 50f))
        {
            lineRenderer.SetPosition(0,_ray.origin);
            lineRenderer.SetPosition(1,_hit.point);
            
            if (Input.GetMouseButtonDown(0))
            {
                _shootingBallRigidbody.isKinematic = false;
                _shootingBall.transform.rotation = Quaternion.Euler(_rotation);
                _shootingBallRigidbody.AddRelativeForce(Vector3.forward * 1000f);
            }
        }
    }

    private void SetDirectionOfBall()
    {
        if (shoot)
        {
            return;
        }
        _ballTransform.rotation = _transform.rotation;
    }

    public void ResetBall()
    {
        shoot = false;
        _shootingBallRigidbody.isKinematic = true;
        _ballTransform.position = _transform.position;
        _shootingBall.SetActive(true);
    }
}
