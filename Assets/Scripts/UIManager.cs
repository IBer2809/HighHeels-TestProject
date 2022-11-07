using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject[] _uiMainPanels;
    [SerializeField] private GameObject[] _uiMainImages;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _tapFinger;
    [SerializeField] private Text _diamondText;
    [SerializeField] private Text _levelText;
    private int _diamondScore;
    private int _levelCount;
    [SerializeField] private Image _diamondImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _diamondScore = PlayerPrefs.GetInt("Diamonds");
        _levelCount = PlayerPrefs.GetInt("LevelCount") + 1;
    }
    private void Start()
    {
        _diamondText.text = _diamondScore.ToString();
        _levelText.text = "Level " + _levelCount;
    }
    private void Update()
    {
    }

    public void StartGameMenu()
    {
        _uiMainPanels[0].SetActive(false);
        _uiMainPanels[2].SetActive(true);
        _gameManager.CurrentState = GameManager.GameState.Play;
    }

    public void OpenSettings()
    {
        _uiMainPanels[1].SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        _uiMainPanels[1].SetActive(false);
    }

    public void OpenLooseMenu()
    {
        _uiMainPanels[3].SetActive(true);
    }

    public void OpenWinMenu()
    {
        _uiMainPanels[1].SetActive(false);
        _uiMainPanels[4].SetActive(true);

    }

    public void SetMusicActiveFalse()
    {
        _uiMainImages[0].SetActive(false);
        _uiMainImages[1].SetActive(true);
        _uiMainImages[1].transform.DOShakeScale(1, 0.1f);
    }
    public void SetMusicActiveTrue()
    {
        _uiMainImages[0].SetActive(true);
        _uiMainImages[1].SetActive(false);
        _uiMainImages[0].transform.DOShakeScale(1, 0.1f);
    }

    public void SetSoundActiveFalse()
    {
        _uiMainImages[2].SetActive(false);
        _uiMainImages[3].SetActive(true);
        _uiMainImages[3].transform.DOShakeScale(1, 0.1f);
    }
    public void SetSoundActiveTrue()
    {
        _uiMainImages[2].SetActive(true);
        _uiMainImages[3].SetActive(false);
        _uiMainImages[2].transform.DOShakeScale(1, 0.1f);
    }

    public void SetVibrationActiveFalse()
    {
        _uiMainImages[4].SetActive(false);
        _uiMainImages[5].SetActive(true);
        _uiMainImages[5].transform.DOShakeScale(1, 0.1f);
    }

    public void SetVibrationActiveTrue()
    {
        _uiMainImages[4].SetActive(true);
        _uiMainImages[5].SetActive(false);
        _uiMainImages[4].transform.DOShakeScale(1, 0.1f);
    }

    public void ChangeSliderValue()
    {
        if (_slider.value > 0 && _slider.value < 0.5f)
        {
            _slider.value = 0.5f;
        }
        else if (_slider.value > 0.5f && _slider.value < 1f)
        {
            _slider.value = 1f;
        }
    }

    public void LevelCountUpdate()
    {
        _levelCount++;
        PlayerPrefs.SetInt("LevelCount", _levelCount - 1);
    }

    public void UpdateDiamondScore()
    {
        _diamondImage.transform.DOShakeScale(1, 10);
        _diamondScore++;
        PlayerPrefs.SetInt("Diamonds", _diamondScore);
        _diamondText.text = _diamondScore.ToString();
    }
}
