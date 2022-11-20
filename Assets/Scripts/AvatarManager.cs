using System;
using System.Threading.Tasks;
using ReadyPlayerMe;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private AvatarConfig highConfig;
    [SerializeField] private AvatarConfig lowConfig;
    [SerializeField] private RuntimeAnimatorController menuAnimatorController;
    [SerializeField] private RuntimeAnimatorController gameAnimatorController;

    private GameObject highAvatar;

    public GameObject HighAvatar
    {
        get
        {
            if (lowAvatar)
            {
                lowAvatar.SetActive(false);
                lowAvatar.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            if (highAvatar)
            {
                highAvatar.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            return highAvatar;
        }
        private set => highAvatar = value;
    }

    private GameObject lowAvatar;
    public GameObject LowAvatar{
        get
        {
            if(highAvatar)
            {
                highAvatar.SetActive(false);
                highAvatar.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            if(lowAvatar)
            {
                lowAvatar.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            return lowAvatar;
        }
        private set => highAvatar = value;
    }
    
    public static AvatarManager Instance { get; private set; }

    public Action OnAvatarsLoaded;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
        
        DontDestroyOnLoad(Instance);
    }

    public void LoadAvatars(string url)
    {
        AvatarLoader loaderHigh = new AvatarLoader();
        loaderHigh.AvatarConfig = highConfig;
        loaderHigh.LoadAvatar(url);
        loaderHigh.OnCompleted += (sender, args) =>
        {
            highAvatar = args.Avatar;
            highAvatar.name += "_HIGH";
            highAvatar.SetActive(false);
            highAvatar.GetComponent<Animator>().runtimeAnimatorController = menuAnimatorController;
            
            DontDestroyOnLoad(HighAvatar);
            
            AvatarLoader loaderLow = new AvatarLoader();
            loaderLow.AvatarConfig = lowConfig;
            loaderLow.LoadAvatar(url);
            loaderLow.OnCompleted += (sender, args) =>
            {
                args.Avatar.name += "_LOW";
                args.Avatar.GetComponent<Animator>().runtimeAnimatorController = gameAnimatorController;
                args.Avatar.AddComponent<Move>();
                
                lowAvatar = new GameObject("Player");
                lowAvatar.SetActive(false);

                var controller = lowAvatar.AddComponent<CharacterController>();
                controller.center = Vector3.up;
                args.Avatar.transform.SetParent(lowAvatar.transform);
                
                DontDestroyOnLoad(lowAvatar);
                
                OnAvatarsLoaded?.Invoke();
            };
        };
    }
}
