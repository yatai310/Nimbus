using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TriggerGameOver();
        }
    }
    void TriggerGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

