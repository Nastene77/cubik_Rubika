using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class About : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _viewQuestion;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _viewQuestion.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _viewQuestion.SetActive(false);
    }
}
