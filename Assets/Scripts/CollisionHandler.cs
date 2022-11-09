using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have bumped into something friendly!");
                break;
            case "Finish":
                Debug.Log("You have finished the level!");
                break;
            default:
                RestartGame();
                break;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
