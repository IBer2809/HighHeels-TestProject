using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _finishPointForCam;
    [SerializeField] private Camera _cameraMain;

    private float _targetCameraDistance;
    [SerializeField] private float _smoothDistancePosition;

    private void Start()
    {
        _targetCameraDistance = _cameraMain.transform.localPosition.z;
    }

    private void Update()
    {
        _cameraMain.transform.localPosition = Vector3.Lerp(_cameraMain.transform.localPosition, new Vector3(0, 0, _targetCameraDistance), _smoothDistancePosition * Time.deltaTime);

        if (_gm.CurrentState != GameManager.GameState.Menu)
            transform.position = Vector3.Lerp(transform.position, _player.position, 5f * Time.deltaTime);

        if (_gm.CurrentState == GameManager.GameState.Win)
        {
            transform.DOMove(_finishPointForCam.position, 5f).SetEase(Ease.Linear);
            transform.DOLookAt(_player.transform.position, 5f);
        }

    }

    public void ChangeDistanceForCamera(float pos)
    {
        _targetCameraDistance = _cameraMain.transform.localPosition.z + pos;
    }
}
