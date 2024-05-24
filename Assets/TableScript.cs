using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TableScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _table;
    private bool isActive;

    private void Start()
    {
        isActive = false;
        _table.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isActive)
        {
            _table.gameObject.SetActive(false);
            isActive = false;
        }
        else
        {
            _table.gameObject.SetActive(true);
            isActive = true;
        }
    }

}