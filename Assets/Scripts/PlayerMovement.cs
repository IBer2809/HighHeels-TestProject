using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField]  private SplineComputer _spline;
    private Vector3 _posMouseOne;
    private Vector3 _posMouseTwo;
    private Vector3 _delta, _currentDelta;


    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CollChecker _colChecker;



    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_gameManager.CurrentState == GameManager.GameState.Play || _gameManager.CurrentState == GameManager.GameState.PreWin)
        {
            _splineFollower.follow = true;

            if (Input.GetMouseButtonDown(0))
            {
                _posMouseOne = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                _posMouseTwo = Input.mousePosition;
                _delta = -(_posMouseOne - _posMouseTwo).normalized;
                if (!_colChecker.OnBridge)
                {
                    _currentDelta = Vector3.Lerp(_currentDelta, _delta * 0.2f, 5f * Time.deltaTime);
                    _splineFollower.motion.offset += new Vector2(_currentDelta.x, 0);
                    ClampPersonMovementX(-4f, 4f);
                }
                else
                {
                    _currentDelta = Vector3.Lerp(_currentDelta, _delta, 30f * Time.deltaTime);
                    _splineFollower.motion.rotationOffset += new Vector3(0, 0, _currentDelta.x);
                    ClampPersonRotationtZ(-30, 30);
                }
                _posMouseOne = Input.mousePosition;
            }
        }
        else if (_gameManager.CurrentState == GameManager.GameState.Win)
        {
            _splineFollower.followSpeed = 2f;

        }
    }

    private void ClampPersonMovementX(float clampXMIN, float clampXMAX)
    {
        Vector3 pos = _splineFollower.motion.offset;
        pos.x = Mathf.Clamp(_splineFollower.motion.offset.x, clampXMIN, clampXMAX);
        _splineFollower.motion.offset = pos;
    }

    private void ClampPersonRotationtZ(float clampZMIN, float clampZMAX)
    {
        Vector3 rot = _splineFollower.motion.rotationOffset;
        rot.z = Mathf.Clamp(_splineFollower.motion.rotationOffset.z, clampZMIN, clampZMAX);
        _splineFollower.motion.rotationOffset = rot;
    }

    

    public IEnumerator ResetRotationOffset()
    {
        while (_splineFollower.motion.rotationOffset != Vector3.zero)
        {
            _splineFollower.motion.rotationOffset = Vector3.Lerp(_splineFollower.motion.rotationOffset, Vector3.zero, 5f * Time.deltaTime);
            yield return null;
        }
    }





}
