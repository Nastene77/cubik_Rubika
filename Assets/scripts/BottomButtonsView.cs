using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomButtonsView : MonoBehaviour
{
    [SerializeField] private StageHelperView _stageHelperView;
    [SerializeField] private List<Button> _stageButtons;

    private void Awake()
    {
        for (var i = 0; i < _stageButtons.Count; i++)
        {
            int buttonNumber = i;
            _stageButtons[i].onClick.AddListener((() => _stageHelperView.SetStageHelperData(buttonNumber)));
        }
    }

    public void SetVisibleStageButton(int toggleNumber, bool isOn)
    {
        _stageButtons[toggleNumber].interactable = isOn;

        float newAlpha = isOn ? 0.5f : 0f;
        Color currentColor = _stageButtons[toggleNumber].image.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        _stageButtons[toggleNumber].image.color = newColor;
    }
}