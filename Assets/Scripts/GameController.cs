using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private string url = "https://api.readyplayer.me/v1/avatars/62ea68a28a6d28ec13499997.glb";
    
    private void Start()
    {
        AvatarManager.Instance.LowAvatar.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
