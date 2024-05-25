using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Data", fileName = "HelperData")]
public class HelperData : ScriptableObject
{
    [SerializeField] private List<Data> _data;

    public List<Data> Data => _data;
}