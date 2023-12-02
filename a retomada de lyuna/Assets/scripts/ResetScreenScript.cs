using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScreenScript : MonoBehaviour
{

public void ResetTheGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Debug.Log("reiniciou");
}

}
