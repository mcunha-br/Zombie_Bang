using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDGameMenu : MonoBehaviour {
    
    public void LoadGame(string scene) => SceneManager.LoadScene(scene);
    
}