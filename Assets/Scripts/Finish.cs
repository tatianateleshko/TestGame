using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _cinemachineVirtualCamera;
    [SerializeField] private GameObject _cryptSprite;
    public bool IsFinished;

    private void Start()
    {
        IsFinished = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Spawner")
        {
            IsFinished = true;
            _cinemachineVirtualCamera.SetActive(false);
            _cryptSprite.SetActive(false);
        }
        else if(collision.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("You win!");
        }
    }
}
