using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class ConnectionManager : MonoBehaviour
{
    public static string user;

    [SerializeField] string _user;
    private static ConnectionManager instance = null;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _user = user;
    }

    private void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            user = PlayGamesPlatform.Instance.GetUserDisplayName();
        }
        else
        {
            user = "Invitado";
        }
    }
}
