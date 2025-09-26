using System;
using UnityEngine;

public static class PlayerPrefsUtil
{
    public static bool GetBool(string key, bool defaultValue = false)
    {
        if (!PlayerPrefs.HasKey(key))
            return defaultValue;

        return PlayerPrefs.GetInt(key) == 1;
    }
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static T GetEnum<T>(string key, T defaultValue = default) where T : Enum
    {
        if (!PlayerPrefs.HasKey(key))
            return defaultValue;

        int intValue = PlayerPrefs.GetInt(key);

        if (!Enum.IsDefined(typeof(T), intValue))
            return defaultValue;

        return (T)Enum.ToObject(typeof(T), intValue);
    }
    public static void SetEnum<T>(string key, T value) where T : Enum
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }
}