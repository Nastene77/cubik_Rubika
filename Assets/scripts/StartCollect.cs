using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCollect : MonoBehaviour
{
    private bool isTurn = false;
    private Animator animator;

    [SerializeField]
    private AudioSource audioSource;
    private Button _button;

    public bool isStart = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("fsdfd");
        animator.SetBool("isTurn", !isTurn);
        isTurn = !isTurn;

        if (isTurn == true)
        {
            isStart = true;
        }
        else
        {
            isStart = false;
            audioSource.Play();
        }
    }
}
