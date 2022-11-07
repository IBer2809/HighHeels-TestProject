using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dreamteck.Splines;

public class GameManager : MonoBehaviour
{
    private static bool _loaded = false;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SplineFollower _splineFollower;
    public enum GameState { Menu, Play, Loose, PreWin, WinOnStairs, Win}
    public GameState CurrentState;
    [SerializeField] private ParticleSystem[] _finishParticles;
    [SerializeField] private int _levelIndex;

    private void Awake()
    {
        _levelIndex = PlayerPrefs.GetInt("LevelIndex");
    }
    private void Start()
    {
        if (!_loaded)
        {
            _loaded = true;
            SceneManager.LoadScene(_levelIndex);
        }
    }

    private void Update()
    {
        if (CurrentState == GameState.Loose)
        {
            _splineFollower.follow = false;
            _uiManager.OpenLooseMenu();
        }
            
        else if(CurrentState == GameState.WinOnStairs)
        {
            _splineFollower.follow = false;
            _uiManager.OpenWinMenu();
        }

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_levelIndex);
    }

    public void NextLevel()
    {
        if(_levelIndex < 2)
            PlayerPrefs.SetInt("LevelIndex", _levelIndex += 1);
        else
            PlayerPrefs.SetInt("LevelIndex", _levelIndex = 0);
        SceneManager.LoadScene(_levelIndex);
    }

    public void FlashParticles()
    {
        for (int i = 0; i < _finishParticles.Length; i++)
        {
            _finishParticles[i].Play();
        }
    }
}
