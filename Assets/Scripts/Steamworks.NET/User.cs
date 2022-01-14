using UnityEngine;
using Steamworks;

public class User
{
    public CSteamID SteamID;
    public AvatarType AvatarType { get; private set; }
    public int AvatarID;
    public Texture2D SteamAvatarImage { get; private set; }


    public string SteamUsername { get; private set; }
    public delegate void NameLoadedSuccessHandler(string username);
    public delegate void AvatarLoadedSuccessHandler(Texture2D user);

    Callback<PersonaStateChange_t> callPersona;
    Callback<AvatarImageLoaded_t> callAvatar;
    Callback<PersonaStateChange_t> personaState;

    public User(CSteamID id, NameLoadedSuccessHandler nameLoadedFunc = null, AvatarLoadedSuccessHandler avatarLoadedFunc = null, AvatarType avatarType = AvatarType.Medium)
    {
        AvatarID = -1;
        SteamID = id;

        SteamUsername = SteamFriends.GetFriendPersonaName(SteamID);
        if (SteamUsername == "" || SteamUsername == "[unknown]")
            LoadName(nameLoadedFunc);
        else
            nameLoadedFunc?.Invoke(SteamUsername);

        GetUserAvatar(avatarType, avatarLoadedFunc);
    }

    private void LoadName(NameLoadedSuccessHandler nameLoadedFunc)
    {
        personaState = Callback<PersonaStateChange_t>.Create((cb) =>
        {
            if (SteamID == (CSteamID)cb.m_ulSteamID)
            {
                SteamUsername = SteamFriends.GetFriendPersonaName(SteamID);
                if (SteamUsername == "" || SteamUsername == "[unknown]") {
                    LoadName(nameLoadedFunc);
                }
                else {
                    nameLoadedFunc?.Invoke(SteamUsername);
                }
                personaState?.Dispose();
            }
        });
    }

    private void GetUserAvatar(AvatarType avatarType, AvatarLoadedSuccessHandler avatarLoadedFunc)
    {
        GetUserAvatar(SteamID, avatarType, avatarLoadedFunc);
    }

    private void GetUserAvatar(CSteamID steamID, AvatarType avatarType, AvatarLoadedSuccessHandler avatarLoadedFunc)
    {
        int handler = (avatarType == AvatarType.Medium)
            ? SteamFriends.GetMediumFriendAvatar(steamID)
            : SteamFriends.GetLargeFriendAvatar(steamID);

        switch (handler)
        {
            case -1:
                callAvatar = Callback<AvatarImageLoaded_t>.Create((cb) => {
                    if (steamID == cb.m_steamID)
                        GetUserAvatar(cb.m_steamID, avatarType, avatarLoadedFunc);
                });
                break;

            case 0:
                if (SteamFriends.RequestUserInformation(steamID, false))
                {
                    Debug.Log($"RequestUserInformation true");

                    callPersona = Callback<PersonaStateChange_t>.Create((cb) => {
                        if (steamID == (CSteamID)cb.m_ulSteamID)
                        {
                            Debug.Log($"GetUserAvatar in PersonaStateChange_t {(CSteamID)cb.m_ulSteamID}");
                            GetUserAvatar((CSteamID)cb.m_ulSteamID, avatarType, avatarLoadedFunc);
                        }
                    });
                }
                break;

            default:
                Debug.Log($"Avatar {SteamUsername} Handle Default: {handler}");
                
                avatarLoadedFunc?.Invoke(SteamAvatarImage = GetTex(AvatarID = handler));
                break;
        }
    }
    
    private Texture2D GetTex(int handler)
    {
        uint width, height;

        if (SteamUtils.GetImageSize(handler, out width, out height))
        {
            byte[] data = new byte[width * height * 4];
            if (SteamUtils.GetImageRGBA(handler, data, data.Length))
            {
                Texture2D tex = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                tex.LoadRawTextureData(data);
                tex.Apply();
                return tex;
            }
        }
        return null;
    }
}

public enum AvatarType
{
    Large,
    Medium
}