using UnityEngine;
using UnityEngine.UI;

public class TopButtonsView : MonoBehaviour
{
    [SerializeField] private CubeManager _cubeManager;

    [Header("UI")]
    [SerializeField] private Button _shuffleCubeButton;
    [SerializeField] private Button _tableButton;
    [SerializeField] private Button _rotateCubeButton;

    [SerializeField] private GameObject _table;

    private void Awake()
    {
        _shuffleCubeButton.onClick.AddListener(OnShuffleCubeButtonClicked);
        _tableButton.onClick.AddListener(OnTableButtonClicked);
        _rotateCubeButton.onClick.AddListener(OnRotateCubeButtonClicked);
    }

    private void OnRotateCubeButtonClicked()
    {
        _cubeManager.RotateCube();
    }

    private void OnTableButtonClicked()
    {
        _table.SetActive(!_table.gameObject.activeSelf);
    }

    private void OnShuffleCubeButtonClicked()
    {
        _cubeManager.ShuffleCube();
    }
}