using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpCube : MonoBehaviour
{
    public Animator animator;
    private bool isTurn = false;

    [SerializeField]
    private AudioSource audioSource;
    public bool isStart = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "but1")
        {
            animator.Play("UpCube");
        }
    }*/
    private void OnMouseDown()
    {
        Debug.Log("fsdfd");
        animator.SetBool("UpCube", !isTurn);
        isTurn = !isTurn;

        if (isTurn == true)
        {
            isStart = true;
            audioSource.Play();
        }
        else
        {
            isStart = false;
            //FindObjectOfType<SetupTimer>().Rebing();
            audioSource.Play();
        }
    }
}
