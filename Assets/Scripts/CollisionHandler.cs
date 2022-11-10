using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelRestartDelay = 2f;
    [SerializeField] float levelNextDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;

    AudioSource audioSource;

    bool crashed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have bumped into something friendly!");
                break;
            case "Finish":
                if (!crashed) StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        audioSource.PlayOneShot(crashAudio);
        // todo add particle effects
        crashed = true;
        Invoke("RestartGame",levelRestartDelay);
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }
    
    private void StartFinishSequence()
    {
        audioSource.PlayOneShot(successAudio);
        // todo add particle effects
        Invoke("NextLevel", levelNextDelay);
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }

    private void RestartGame()
    {
        audioSource.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        crashed = false;
    }

    private void NextLevel()
    {
        audioSource.Stop();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex + 1 == SceneManager.sceneCountInBuildSettings) 
        { 
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(sceneIndex + 1); 
        }
    }
}
