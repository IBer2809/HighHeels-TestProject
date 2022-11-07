using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private CollChecker _colChecker;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _finishEndPoint;
    [SerializeField] private Animator[] _crowdAnim;
    private bool _isLevelCompleted;
    private void Start()
    {
        _isLevelCompleted = false;
    }

    private void Update()
    {
        switch (_gameManager.CurrentState)
        {
            case GameManager.GameState.Play:
                if (!_colChecker.OnCylinderBridge && !_colChecker.OnBridge)
                {
                    _playerAnimator.SetTrigger("Running");
                    _playerAnimator.SetBool("OnCylBridge", false);
                    _playerAnimator.SetBool("OnBridge", false);
                }
                else if (_colChecker.OnCylinderBridge)
                    _playerAnimator.SetBool("OnCylBridge", true);
                else if (_colChecker.OnBridge)
                    _playerAnimator.SetBool("OnBridge", true);
                break;
            
            case GameManager.GameState.Loose:
                    _playerAnimator.SetBool("IsDie", true);
                break;

            case GameManager.GameState.WinOnStairs:
                _playerAnimator.SetBool("IsDance", true);
                break;
            
            case GameManager.GameState.Win:
                _playerAnimator.SetTrigger("IsFinish");
                for (int i = 0; i < _crowdAnim.Length; i++)
                {
                    _crowdAnim[i].SetTrigger("Win");
                }
                if (_isLevelCompleted)
                {
                    _playerAnimator.SetBool("IsDance", true);
                }
                break;
        }
    }


    public void SetLevelCompletedBool()
    {
        _isLevelCompleted = true;
    }

}
