using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckStages : MonoBehaviour
{
    public event Action FirstStageCompleted;
    public event Action SeventhStageCompleted;

    [SerializeField] private Material _whiteSideMaterial;

    private Vector3[] _firstStageVectors =
    {
        new Vector3(0, -2, 1), new Vector3(-1, -2, 0),
        new Vector3(-1, -2, 1), new Vector3(-1, -2, 2),
        new Vector3(-2, -2, 1)
    };

    public void CheckStagesComplete(List<GameObject> upPieces, List<GameObject> downPieces, List<GameObject> leftPieces,
        List<GameObject> rightPieces, List<GameObject> frontPieces, List<GameObject> backPieces)
    {
        if (IsSeventhStageComplete(upPieces) &&
            IsSeventhStageComplete(downPieces) &&
            IsSeventhStageComplete(leftPieces) &&
            IsSeventhStageComplete(rightPieces) &&
            IsSeventhStageComplete(frontPieces) &&
            IsSeventhStageComplete(backPieces))
        {
            SeventhStageCompleted?.Invoke();
            Debug.Log("Seventh Stage Completed");
        }

        if (IsWhiteCrossCompleted(upPieces) ||
            IsWhiteCrossCompleted(downPieces) ||
            IsWhiteCrossCompleted(leftPieces) ||
            IsWhiteCrossCompleted(rightPieces) ||
            IsWhiteCrossCompleted(frontPieces) ||
            IsWhiteCrossCompleted(backPieces))
        {
            FirstStageCompleted?.Invoke();
            Debug.Log("First Stage Completed");
        }
    }

    private bool IsSeventhStageComplete(List<GameObject> pieces)
    {
        int mainPlaneIndex = pieces[4].GetComponent<CubePieceScr>().Planes.FindIndex(x => x.activeInHierarchy);
        for (int i = 0; i < pieces.Count; i++)
        {
            if (!pieces[i].GetComponent<CubePieceScr>().Planes[mainPlaneIndex].activeInHierarchy ||
                pieces[i].GetComponent<CubePieceScr>().Planes[mainPlaneIndex].GetComponent<Renderer>().material.color !=
                pieces[4].GetComponent<CubePieceScr>().Planes[mainPlaneIndex].GetComponent<Renderer>().material.color)
                return false;
        }

        return true;
    }

    private bool IsWhiteCrossCompleted(List<GameObject> pieces)
    {
        int centerPlaneIndex = pieces[4].GetComponent<CubePieceScr>().Planes.FindIndex(x => x.activeInHierarchy);
        GameObject centerPiece = pieces[4].GetComponent<CubePieceScr>().Planes[centerPlaneIndex];
        Color centerPlaneColor = centerPiece.GetComponent<Renderer>().material.color;

        if (_whiteSideMaterial.color != centerPlaneColor)
        {
            Debug.Log($"Not white center plane");
            return false;
        }

        foreach (Vector3 vector in _firstStageVectors)
        {
            GameObject cubePiece = null;
            foreach (var piece in pieces)
            {
                if (piece.transform.localPosition == vector)
                {
                    cubePiece = piece;
                    break;
                }
            }

            if (!(cubePiece != null &&
                  cubePiece.GetComponent<CubePieceScr>().Planes[centerPlaneIndex].activeInHierarchy &&
                  cubePiece.GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material
                      .color == _whiteSideMaterial.color))
            {
                Debug.Log($"Cross is not constructed");
                return false;
            }

            // // Проверяем, лежат ли два кубика в одной гране
            // if (AreCubesOnSameFace(centerPiece, cubePiece.GetComponent<CubePieceScr>().Planes[centerPlaneIndex]))
            // {
            //     // Debug.Log("Кубики лежат в одной гране!");
            //     // return true;
            // }
            // else
            // {
            //     Debug.Log($"Rotation is not constructed");
            //     // Debug.Log("Кубики не лежат в одной гране!");
            //     return false;
            // }
        }

        return true;
    }

    private bool AreCubesOnSameFace(GameObject cube1, GameObject cube2)
    {
        // Получаем позицию кубиков в глобальной системе координат
        Vector3 cube1Position = cube1.transform.position;
        Vector3 cube2Position = cube2.transform.position;
        //todo: try on one axis
        // Проверяем, совпадают ли координаты по двум осям
        if (Mathf.Abs(cube1Position.x) - Mathf.Abs(cube2Position.x) <= 1.1f &&
            Mathf.Abs(cube1Position.y) - Mathf.Abs(cube2Position.y) <= 1.1f ||
            Mathf.Abs(cube1Position.x) - Mathf.Abs(cube2Position.x) <= 1.1f &&
            Mathf.Abs(cube1Position.z) - Mathf.Abs(cube2Position.z) <= 1.1f ||
            Mathf.Abs(cube1Position.y) - Mathf.Abs(cube2Position.y) <= 1.1f &&
            Mathf.Abs(cube1Position.z) - Mathf.Abs(cube2Position.z) <= 1.1f)
        {
            return true; // Кубики лежат в одной гране
        }

        return false; // Кубики не лежат в одной гране
    }
}