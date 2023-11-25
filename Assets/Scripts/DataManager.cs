using UnityEngine;

public static class DataManager 
{
    public static void Delete()
    {
        PlayerPrefs.DeleteKey("PositionX");
        PlayerPrefs.DeleteKey("PositionY");
        PlayerPrefs.DeleteKey("PositionZ");
    }
    public static void SaveCoordinates(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("PositionX", x);
        PlayerPrefs.SetFloat("PositionY", y);
        PlayerPrefs.SetFloat("PositionZ", z);
    }
    public static float LoadPositionX()
    {
        return PlayerPrefs.GetFloat("PositionX", 0);
    }
    public static float LoadPositionY()
    {
        return PlayerPrefs.GetFloat("PositionY", -3);
    }
    public static float LoadPositionZ()
    {
        return PlayerPrefs.GetFloat("PositionZ", 0);
    }

    public static void SaveCredo(string role, string login, string password)
    {
        PlayerPrefs.SetString("AuthorizationRole", role);
        PlayerPrefs.SetString("AuthorizationLogin", login);
        PlayerPrefs.SetString("AuthorizationPassword", password);
        PlayerPrefs.Save();
    }
    public static string LoadCredoRole()
    {
        return PlayerPrefs.GetString("AuthorizationRole", "");
    }
    public static string LoadCredoLogin()
    {
        return PlayerPrefs.GetString("AuthorizationLogin", "");
    }
    public static string LoadCredoPassword()
    {
        return PlayerPrefs.GetString("AuthorizationPassword", "");
    }
}
