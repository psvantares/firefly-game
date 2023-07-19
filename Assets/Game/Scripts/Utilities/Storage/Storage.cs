using UnityEngine;

namespace Game.Utilities
{
    public static class Storage
    {
        public static void SetBool(string key, bool value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static void SetInt(string key, int value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            PlayerPrefs.SetInt(key, value);
        }

        public static void SetString(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            PlayerPrefs.SetString(key, value);
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key) != 0;
            }

            return defaultValue;
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;
        }

        public static string GetString(string key, string defaultValue = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            return !PlayerPrefs.HasKey(key) ? defaultValue : PlayerPrefs.GetString(key);
        }
    }
}