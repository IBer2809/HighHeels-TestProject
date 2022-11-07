using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CollChecker : MonoBehaviour
{
    [SerializeField] private Stack _stack;
    [SerializeField] private CameraFollow _camFoolow;
    [SerializeField] private Transform _lastMovingPointForDiamond;
    [SerializeField] private GameObject _diamondIconObject;
    [SerializeField] private LayerMask _roadLayer;
    private bool _isRemovingHeels = false;

    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private AnimationManager _animationManager;

    public bool OnCylinderBridge { get; private set; }
    public bool OnBridge;

    private void Update()
    {
        Ray ray = new Ray(_playerMovement.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _roadLayer))
        {

            transform.position = new Vector3(_playerMovement.transform.position.x, hit.point.y, _playerMovement.transform.position.z);
        }
        else
        {
            transform.position = _playerMovement.transform.position;
        }

        if (OnBridge == true)
        {
            if (_splineFollower.motion.rotationOffset.z == 30f || _splineFollower.motion.rotationOffset.z == -30f)
            {
                OnBridge = false;
                _gameManager.CurrentState = GameManager.GameState.Loose;
            }
        }
    }

    private IEnumerator ChangeIsRemovingHeels(bool value, float delay)
    {
        yield return new WaitForSeconds(delay);
        _isRemovingHeels = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shoes")
        {
            other.gameObject.SetActive(false);
            _stack.lastActiveIndexForLeftStack++;
            _stack.lastActiveIndexForRightStack++;
            _stack.LeftHeelsOnPlayer[_stack.lastActiveIndexForLeftStack].gameObject.SetActive(true);
            _stack.RightHeelsOnPlayer[_stack.lastActiveIndexForRightStack].gameObject.SetActive(true);
            _camFoolow.ChangeDistanceForCamera(-1f);
        }

        else if (other.TryGetComponent(out Obstacle obstacle) && _isRemovingHeels == false)
        {
            _isRemovingHeels = true;
            StartCoroutine(ChangeIsRemovingHeels(false, 0.5f));
            _stack.RemoveHeels(obstacle.GetRemovedHeelsCount());
            _camFoolow.ChangeDistanceForCamera(1f);
        }
        else if (other.TryGetComponent(out Diamond diamond))
        {
            diamond.ExplodeDiamond();
            GameObject currentIcon = Instantiate(_diamondIconObject, other.transform.position, Quaternion.identity);
            StartCoroutine(currentIcon.GetComponent<DiamondIcon>().MoveDiamondUp(_lastMovingPointForDiamond));
            other.gameObject.SetActive(false);

        }

        else if (other.tag == "CylBridge")
        {
            OnCylinderBridge = true;
            _rigidbody.AddForce(Vector3.down * 300f, ForceMode.VelocityChange);
            _splineFollower.followSpeed = 10f;

        }
        else if (other.tag == "OnRoad")
        {
            _rigidbody.AddForce(Vector3.down * 25f, ForceMode.VelocityChange);
            _splineFollower.followSpeed = 7f;

            OnCylinderBridge = false;
        }
        else if(other.tag == "DefeatPlatform")
        {
            OnCylinderBridge = false;
            _gameManager.CurrentState = GameManager.GameState.Loose;
        }

        else if (other.tag == "Bridge")
        {
            _splineFollower.followSpeed = 3f;
            OnBridge = true;
            Debug.Log("OnBridge");


        }
        else if (other.tag == "PreFinish")
        {
            _gameManager.CurrentState = GameManager.GameState.PreWin;
        }
        else if (other.tag == "Finish")
        {
            _gameManager.CurrentState = GameManager.GameState.Win;
        }
        else if (other.tag == "ParticlesFlash")
        {
            _gameManager.FlashParticles();
            other.GetComponent<BoxCollider>().enabled = false;
        }

        else if (other.tag == "LevelComplete")
        {
            _animationManager.SetLevelCompletedBool();
            _uiManager.OpenWinMenu();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CylBridge")
        {
            Debug.Log("Yes");
            _rigidbody.AddForce(Vector3.up * 25f, ForceMode.VelocityChange);
        }
        else if (other.tag == "Bridge")
        {
            _splineFollower.followSpeed = 7f;
            StartCoroutine(_playerMovement.ResetRotationOffset());
            OnBridge = false;
        }
    }
}
