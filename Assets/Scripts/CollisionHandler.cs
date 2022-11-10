using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelRestartDelay = 2f;
    [SerializeField] float levelNextDelay = 1f;
    bool crashed = false;

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
        // todo add SFX
        // todo add particle effects
        crashed = true;
        Invoke("RestartGame",levelRestartDelay);
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }
    
    private void StartFinishSequence()
    {
        Invoke("NextLevel", levelNextDelay);
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        crashed = false;
    }

    private void NextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex + 1 == SceneManager.sceneCountInBuildSettings) 
        { 
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(sceneIndex + 1); 
        }
    }
}
