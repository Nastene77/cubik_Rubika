using UnityEngine;
using UnityEngine.UI;

public class CollectCube : MonoBehaviour
{
    public bool IsRaised => isRaised;

    public GameObject cube;
    private bool isRaised = false;
    private Vector3 initialPosition;

    public AudioSource audioSource;

    void Start()
    {
        initialPosition = cube.transform.position;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (isRaised)
        {
            cube.transform.position = initialPosition;
            isRaised = false;
            PlayRotationSound();
        }
        else
        {
            Vector3 raisedPosition = new Vector3(initialPosition.x, initialPosition.y + 8f, initialPosition.z);
            cube.transform.position = raisedPosition;
            isRaised = true;
        }
    }
    void PlayRotationSound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
