using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{    
    [SerializeField] private InputField avatarUrlField;
    [SerializeField] private Button avatarLoadButton;
    [SerializeField] private GameObject dummyAvatar;
    [SerializeField] private RuntimeAnimatorController animatorController;

    [SerializeField] private GameObject avatarInputs;
    [SerializeField] private Button startButton;
    
    private void Start()
    {
        avatarLoadButton.onClick.AddListener(OnButtonClicked);
        startButton.onClick.AddListener(GoToGame);
        
        if (AvatarManager.Instance.HighAvatar != null)
        {
            OnAvatarLoaded();
        }
    }

    public void OnButtonClicked()
    { 
        AvatarManager.Instance.OnAvatarsLoaded = OnAvatarLoaded;
        AvatarManager.Instance.LoadAvatars(avatarUrlField.text);
    }

    private void OnAvatarLoaded()
    {
        avatarInputs.SetActive(false);
        startButton.gameObject.SetActive(true);
        dummyAvatar.SetActive(false);
        AvatarManager.Instance.HighAvatar.SetActive(true);
    }

    private void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
}
