using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTools : MonoBehaviour
{
    [MenuItem("Tools/OpenPersistentDataPath")]
    static void OpenPersistent()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    [MenuItem("Tools/ClearPreferences")]
    static void ClearPreference()
    {
        PlayerPrefs.DeleteAll();
    }
}
