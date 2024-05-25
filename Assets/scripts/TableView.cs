using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableView : MonoBehaviour
{
    [SerializeField] private CubeManager _cubeManager;
    [SerializeField] private CheckStages _checkStages;
    [SerializeField] private BottomButtonsView _bottomButtonsView;

    [Header("UI")]
    [SerializeField] private List<Toggle> _toggles;
    [SerializeField] private List<TMP_Text> _rotatesCounter;
    [SerializeField] private List<TMP_Text> _currentTime;
    [SerializeField] private List<TMP_Text> _stagesCostTime;
    
    private event Action<int> _stageCompleted;
    private List<float> _allStagesCostTime = new();
    private int _currentStageNumber;
    private bool _isFirstStageCompleted;

    private void Awake()
    {
        for (var i = 0; i < _toggles.Count; i++)
        {
            _allStagesCostTime.Add(0f);
            int toggleNumber = i;
            _toggles[i].onValueChanged.AddListener(((isOn) =>
            {
                if (isOn)
                {
                    _stageCompleted?.Invoke(toggleNumber);
                }
            }));
        }

        _stageCompleted += OnStageCompleted;
        _checkStages.FirstStageCompleted += OnFirstStageCompleted;
        _checkStages.SeventhStageCompleted += OnSeventhStageCompleted;
    }

    private void OnEnable()
    {
        if (_toggles.Count >= _currentStageNumber)
            _rotatesCounter[_currentStageNumber].text = _cubeManager.RotatesCounter.ToString();
    }

    private void OnStageCompleted(int toggleNumber)
    {
        if (_currentStageNumber == 0)
        {
            _stagesCostTime[0].text = _currentTime[0].text;
        }
        else
        {
            if (toggleNumber - 1 >= 0)
            {
                var temp = _cubeManager.Timer - _allStagesCostTime[toggleNumber - 1];
                TimeSpan timeSpan = TimeSpan.FromSeconds(temp);
                _stagesCostTime[toggleNumber].text = string.Format("{0:mm\\:ss\\.fff}", timeSpan);
            }
        }
        _currentStageNumber += 1;
        _cubeManager.RotatesCounter = 0;
        _toggles[toggleNumber].isOn = true;
        _toggles[toggleNumber].interactable = false;
        _bottomButtonsView.SetVisibleStageButton(toggleNumber, true);
        if (_toggles.Count - 1 >= toggleNumber + 1)
        {
            _toggles[toggleNumber + 1].interactable = true;
        }
    }

    private void Update()
    {
        if (_currentTime.Count - 1 < _currentStageNumber) return;

        TimeSpan timeSpan = TimeSpan.FromSeconds(_cubeManager.Timer);
        _currentTime[_currentStageNumber].text = string.Format("{0:mm\\:ss\\.fff}", timeSpan);
        _allStagesCostTime[_currentStageNumber] = _cubeManager.Timer;
    }

    private void OnFirstStageCompleted()
    {
        //todo: change values on the table
        _checkStages.FirstStageCompleted -= OnFirstStageCompleted;
    }

    private void OnSeventhStageCompleted()
    {
        //todo: change values on the table
        _checkStages.SeventhStageCompleted -= OnSeventhStageCompleted;
    }
}