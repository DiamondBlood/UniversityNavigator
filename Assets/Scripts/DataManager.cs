using UnityEngine;

public static class DataManager 
{
    public static void Delete()
    {
       PlayerPrefs.DeleteAll();
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
}
