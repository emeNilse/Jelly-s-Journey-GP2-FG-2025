using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Turner added this 2/11/2025
public class TransitionToScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 2;
    private void TransitionToMainGame(){
        SceneManager.LoadScene(sceneIndex);
    }
}