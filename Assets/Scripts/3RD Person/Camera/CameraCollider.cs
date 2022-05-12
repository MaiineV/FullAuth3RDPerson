using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public CameraController _cameraController;
    private int _ammountOfColliders = 0;

    public LayerMask enviromentMask;

    bool _isMoving = false;
    public bool closeCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _ammountOfColliders++;
            if (!_isMoving && closeCollider)
            {
                _isMoving = true;
                _cameraController.UpdateMovement(0);
            }
            else if (_isMoving && !closeCollider)
            {
                _isMoving = false;
                _cameraController.UpdateMovement(2);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _ammountOfColliders--;
            if(_ammountOfColliders <= 0 && !closeCollider && !_isMoving)
            {
                _ammountOfColliders = 0;
                _isMoving = true;
                _cameraController.UpdateMovement(1);
            }
            else if (_ammountOfColliders <= 0 && _isMoving && closeCollider)
            {
                _ammountOfColliders = 0;
                _isMoving = false;
                _cameraController.UpdateMovement(2);
            }
        }
    }
}
