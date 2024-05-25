using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private CheckStages _checkStages;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CollectCube _collectCube;
    [SerializeField] private Transform _parent;

    private bool isStart = false;
    private float timer;

    public GameObject CubePiecePref;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubecenterPieces;
    bool canRotate = true;
    private bool _canShuffle = true;

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

    private Vector3[] RotationVectors =
    {
        new Vector3(0, 1, 0), new Vector3(0, -1, 0),
        new Vector3(0, 0, -1), new Vector3(0, 0, 1),
        new Vector3(1, 0, 0), new Vector3(-1, 0, 0)
    };

    private void Start()
    {
        CreateCube();
    }

    private void Update()
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

    public void RotateCube() => transform.RotateAround(CubecenterPieces.transform.position, new Vector3(0,0,1), 180);

    public void ShuffleCube()
    {
        if (!_canShuffle) return;
        StartCoroutine(Shuffle());
    }

    private void CreateCube()
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

    private void CheckInput()
    {
        if (!_collectCube.IsRaised || !_canShuffle) return;

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

    private IEnumerator Rotate(List<GameObject> pieces, Vector3 rotationVec, int speed = 5)
    {
        canRotate = false;
        int angle = 0;

        while(angle < 90)
        {
            foreach (GameObject go in pieces) 
                go.transform.RotateAround(CubecenterPieces.transform.position, rotationVec, speed);
            angle += speed;
            yield return null;
        }
        canRotate = true;
        _checkStages.CheckStagesComplete(UpPieces, DownPieces, LeftPieces, RightPieces, FrontPieces, BackPieces);
    }

    private IEnumerator Shuffle()
    {
        _canShuffle = false;
        for (int moveCount = 10; moveCount >= 0; moveCount--)
        {
            int edge = Random.Range(0, 6);
            List<GameObject> edgePieces = new List<GameObject>();
            switch (edge)
            {
                case 0:
                    edgePieces = UpPieces;
                    break;
                case 1:
                    edgePieces = DownPieces;
                    break;
                case 2:
                    edgePieces = LeftPieces;
                    break;
                case 3:
                    edgePieces = RightPieces;
                    break;
                case 4:
                    edgePieces = FrontPieces;
                    break;
                case 5:
                    edgePieces = BackPieces;
                    break;
            }
            
            StartCoroutine(Rotate(edgePieces, RotationVectors[edge], 15)); 
            yield return new WaitForSeconds(.3f);
        }
        
        _canShuffle = true;
    }

    void PlayRotationSound()
    {
        if (rotationSound != null)
            rotationSound.Play();
    }

    public void StopTimer()
    {
        isStart = false;
    }
}