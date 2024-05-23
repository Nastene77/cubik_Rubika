using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePieceScr : MonoBehaviour
{
    public List<GameObject> Planes => _planes;
    public GameObject UpPlane, DownPlane,
        FrontPlane, BackPlane,
        LeftPlane, RightPlane;

    [SerializeField] private List<GameObject> _planes;

    public void SetColor(int x, int y, int z) 
    {
        if (y == 0) 
            _planes[0].SetActive(true);
        else if(y == -2)
            _planes[1].SetActive(true);
        
        if (z == 0)
            _planes[2].SetActive(true);
        else if (z == 2)
            _planes[3].SetActive(true);
        
        if (x == 0)
            _planes[4].SetActive(true);
        else if (x == -2)
            _planes[5].SetActive(true);
    }
}
