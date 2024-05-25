using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageHelperView : MonoBehaviour
{
    [SerializeField] private HelperData _helperData;
    [SerializeField] private CameraRotation _cameraRotation;

    [Header("UI")]
    [SerializeField] private Button _helperButton;
    [SerializeField] private Button _resultButton;
    [SerializeField] private TMP_Text _instructionText;
    [SerializeField] private TMP_Text _helperText;
    [SerializeField] private Image _firstHelperImage;
    [SerializeField] private Image _secondHelperImage;
    [SerializeField] private Image _resultImage;

    [Header("Popups")]
    [SerializeField] private GameObject _helperPopup;
    [SerializeField] private GameObject _resultPopup;

    private void Awake()
    {
        _helperButton.onClick.AddListener(OnHelperButtonClicked);
        _resultButton.onClick.AddListener(OnResultButtonClicked);
    }

    public void SetStageHelperData(int buttonNumber)
    {
        _instructionText.text = _helperData.Data[buttonNumber].InstructionText;
        _helperText.text = _helperData.Data[buttonNumber].HelperText;
        _firstHelperImage.sprite = _helperData.Data[buttonNumber].FirstHelperImage;
        _secondHelperImage.sprite = _helperData.Data[buttonNumber].SecondHelperImage;
        _resultImage.sprite = _helperData.Data[buttonNumber].ResultImage;
    }

    private void OnHelperButtonClicked()
    {
        _helperPopup.SetActive(!_helperPopup.gameObject.activeSelf);
        _cameraRotation.IsRotationEnabled = !_helperPopup.gameObject.activeSelf;
    }

    private void OnResultButtonClicked()
    {
        _resultPopup.SetActive(!_resultPopup.gameObject.activeSelf);
        _cameraRotation.IsRotationEnabled = !_resultPopup.gameObject.activeSelf;
    }
}