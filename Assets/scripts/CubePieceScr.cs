using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePieceScr : MonoBehaviour
{
    public GameObject UpPlane, DownPlane,
        FrontPlane, BackPlane,
        LeftPlane, RightPlane;
    
    public void SetColor(int x, int y, int z) 
    {
        if (y == 0) 
            UpPlane.SetActive(true);
        else if(y == -2)
            DownPlane.SetActive(true);

        if (z == 0)
            LeftPlane.SetActive(true);
        else if (z == 2)
            RightPlane.SetActive(true);

        if (x == 0)
            FrontPlane.SetActive(true);
        else if (x == -2)
            BackPlane.SetActive(true);
    }
}
