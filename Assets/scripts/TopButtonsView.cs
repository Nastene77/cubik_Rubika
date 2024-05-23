using UnityEngine;
using UnityEngine.UI;

public class TopButtonsView : MonoBehaviour
{
    [SerializeField] private Button _mixCubeSidesButton;
    [SerializeField] private Button _takeUpCubeButton;
    [SerializeField] private Button _tableButton;
    [SerializeField] private Button _rotateCubeButton;
    [SerializeField] private CubeManager _cubeManager;
    private void Awake()
    {
        // _mixCubeSidesButton.onClick.AddListener();
        // _takeUpCubeButton.onClick.AddListener();
        // _tableButton.onClick.AddListener();
        _rotateCubeButton.onClick.AddListener(OnRotateCubeButtonClicked);
    }

    private void OnRotateCubeButtonClicked()
    {
        _cubeManager.RotateCube();
    }
}