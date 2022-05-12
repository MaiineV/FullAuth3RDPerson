using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    delegate void PlayerDelegate();
    PlayerDelegate buttonsDelegate;
    
    ButtonsController _buttons;

    public CameraController _camera;
    public MovementController _move;

    Rigidbody rb;

    public LayerMask layerMaskFloor;
    public float rcJumpDist;
    public float jumpFoce;

    public float speed;
    
    public float cameraSensitivity;

    public Transform pivotCamera;

    void Awake()
    {
        _move = new MovementController();
        _buttons = new ButtonsController(this, _camera);
        buttonsDelegate = delegate { };
        StartCoroutine(CheckTimerCamera());

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        buttonsDelegate();
    }

    public void CameraController(Vector3 rot)
    {
        pivotCamera.localRotation = Quaternion.Euler(rot * cameraSensitivity);
    }

    public void Movement(float x, float z)
    {
        Vector3 _movementVector = pivotCamera.right * x + pivotCamera.forward * z;
        _movementVector.y = 0;

        if (_movementVector.magnitude > 1)
            _movementVector.Normalize();

        transform.position += _movementVector * speed * Time.deltaTime;
    }

    public void Jump()
    {
        if(Physics.Raycast(transform.position, transform.up * -1, rcJumpDist, layerMaskFloor))
        {
            rb.AddForce(transform.up * jumpFoce, ForceMode.Impulse);
        }
    }

    IEnumerator CheckTimerCamera()
    {
        yield return new WaitForSeconds(1f);
        buttonsDelegate = _buttons.OnUpdate;
    }

}
