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
            var buttonNumber = i;
            _stageButtons[i].onClick.AddListener((() => _stageHelperView.SetStageHelperData(buttonNumber)));
        }
    }
}