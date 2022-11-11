using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelRestartDelay = 2f;
    [SerializeField] float levelNextDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Do not do any other actions if the level is transitioning
        if (isTransitioning) return; 

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have bumped into something friendly!");
                break;
            case "Finish":
                if (!isTransitioning) StartFinishSequence();
                break;
            default:
                if (!isTransitioning) StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        // First stop the engine audio
        GetComponent<AudioSource>().Stop();
        // Then start playing the crash effect
        audioSource.PlayOneShot(crashAudio);
        // todo add particle effects
        isTransitioning = true;
        Invoke("RestartGame",levelRestartDelay);
        GetComponent<Movement>().enabled = false;
    }
    
    private void StartFinishSequence()
    {
        // First stop the engine audio
        GetComponent<AudioSource>().Stop();
        // Then start playing the crash effect
        audioSource.PlayOneShot(successAudio);
        // todo add particle effects
        isTransitioning = true;
        Invoke("NextLevel", levelNextDelay);
        GetComponent<Movement>().enabled = false;
    }

    private void RestartGame()
    {
        audioSource.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isTransitioning = false;
    }

    private void NextLevel()
    {
        isTransitioning = false;
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
