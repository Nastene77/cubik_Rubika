using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CollectCube _collectCube;
    [SerializeField] private Transform _parent;
    [SerializeField] private Material _whiteSideMaterial;

    private bool isStart = false;
    private float timer;

    public GameObject CubePiecePref;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubecenterPieces;
    bool canRotate = true;

    public AudioSource rotationSound;

    List<GameObject> UpPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> DownPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -2);
        }
    }
    List<GameObject> FrontPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    List<GameObject> BackPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -2);
        }
    }
    List<GameObject> LeftPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    List<GameObject> RightPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 2);
        }
    }

    void Start()
    {
        CreateCube();
    }

    void Update()
    {
        if (canRotate) 
        CheckInput();

        if (isStart && _collectCube.IsRaised)
        {
            timer += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            _text.text = string.Format("{0:mm\\:ss\\.fff}", timeSpan); ;
        }
    }
    public void StartTimer()
    {
        isStart = true;
        timer = 0f;
        _text.text = timer.ToString();
    }

    public void RotateCube()
    {
        transform.RotateAround(CubecenterPieces.transform.position, new Vector3(0,0,1), 180);
    }

    void CreateCube()
    {
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++) 
                {
                    GameObject go = Instantiate(CubePiecePref, _parent, false);
                    go.transform.localPosition = new Vector3(-x, -y, z);
                    go.GetComponent<CubePieceScr>().SetColor(-x, -y, z);
                    AllCubePieces.Add(go);
                }
        CubecenterPieces = AllCubePieces[13];
    }

    void CheckInput()
    {
        if (!_collectCube.IsRaised)
            return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.B))
        {
            if(isStart == false)
            {
                StartTimer();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                StartCoroutine(Rotate(UpPieces, new Vector3(0, -1, 0))); 
            else
                StartCoroutine(Rotate(UpPieces, new Vector3(0, 1, 0)));
            PlayRotationSound();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                StartCoroutine(Rotate(DownPieces, new Vector3(0, 1, 0))); 
            else
                StartCoroutine(Rotate(DownPieces, new Vector3(0, -1, 0)));
            PlayRotationSound();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, 1))); 
            else
                StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, -1)));
            PlayRotationSound();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, -1))); 
            else
                StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, 1)));
            PlayRotationSound();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                StartCoroutine(Rotate(FrontPieces, new Vector3(-1, 0, 0))); 
            else
                StartCoroutine(Rotate(FrontPieces, new Vector3(1, 0, 0)));
            PlayRotationSound();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                StartCoroutine(Rotate(BackPieces, new Vector3(1, 0, 0))); 
            else
                StartCoroutine(Rotate(BackPieces, new Vector3(-1, 0, 0)));
            PlayRotationSound();
        }

    }

    IEnumerator Rotate(List<GameObject> pieces, Vector3 rotationVec)
    {
        canRotate = false;
        int angle = 0;

        while(angle < 90)
        {
            foreach (GameObject go in pieces) 
                go.transform.RotateAround(CubecenterPieces.transform.position, rotationVec, 5);
            angle += 5;
            yield return null;
        }
        canRotate = true;
        CheckFirstStageComplete();
        CheckComplete();
    }

    void PlayRotationSound()
    {
        if (rotationSound != null)
            rotationSound.Play();
    }

    private void CheckComplete()
    {
        if (IsSideComplete(UpPieces) &&
            IsSideComplete(DownPieces) &&
            IsSideComplete(LeftPieces) &&
            IsSideComplete(RightPieces) &&
            IsSideComplete(FrontPieces) &&
            IsSideComplete(BackPieces)) 
            Debug.Log("Seventh Stage Completed");
    }

    private void CheckFirstStageComplete()
    {
        if (IsWhiteCrossCompleted(UpPieces) ||
            IsWhiteCrossCompleted(DownPieces) ||
            IsWhiteCrossCompleted(LeftPieces) ||
            IsWhiteCrossCompleted(RightPieces) ||
            IsWhiteCrossCompleted(FrontPieces) ||
            IsWhiteCrossCompleted(BackPieces)) 
            Debug.Log("First Stage Completed");
    }

    private bool IsWhiteCrossCompleted(List<GameObject> pieces)
    {
        int centerPlaneIndex = pieces[4].GetComponent<CubePieceScr>().Planes.FindIndex(x => x.activeInHierarchy);
        Color centerPlaneColor = pieces[4].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material.color;
        if (_whiteSideMaterial.color != centerPlaneColor)
        {
            Debug.Log($"Not white center plane");
            return false;
        }
        // for (int i = 0; i < pieces.Count; i++)
        // {
        //     GameObject currentPiecePlane = pieces[i].GetComponent<CubePieceScr>().Planes[centerPlaneIndex];
        //     if (!currentPiecePlane.activeInHierarchy)
        //     {
        //         Debug.Log($"Center plane is not active");
        //         return false;
        //     }
        // }
        if (!(pieces[1].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].activeInHierarchy &&
            pieces[3].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].activeInHierarchy &&
            pieces[4].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].activeInHierarchy &&
            pieces[5].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].activeInHierarchy &&
            pieces[7].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].activeInHierarchy))
        {
            Debug.Log($"Center plane is not active");
            return false;
        }
        if (pieces[1].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material.color == _whiteSideMaterial.color &&
            pieces[3].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material.color == _whiteSideMaterial.color &&
            pieces[4].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material.color == _whiteSideMaterial.color &&
            pieces[5].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material.color == _whiteSideMaterial.color &&
            pieces[7].GetComponent<CubePieceScr>().Planes[centerPlaneIndex].GetComponent<Renderer>().material.color == _whiteSideMaterial.color)
        {
            return true;
        }
        else
        {
            Debug.Log($"Cross is not constructed");
            return false;
        }
    }

    private bool IsSideComplete(List<GameObject> pieces)
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

    public void StopTimer()
    {
        isStart = false;
    }
}

