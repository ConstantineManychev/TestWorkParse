using UnityEngine;
using System.Collections;
using SmartLocalization;

public class LanguageChangeScript : MonoBehaviour {

    public static string versionKey = "Version";
    public static string enterKey = "Enter";
    public static string cubeKey = "Cube";
    public static string scaleKey = "Scale";
    public static string exitKey = "Exit";
    public static string errorKey = "Error";
    public static string error2Key = "Error2";

    public AudioClip buttonSound;

    public void ToRussian()
    {
        LanguageManager.Instance.ChangeLanguage("ru");
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }
    public void ToEnglish()
    {
        LanguageManager.Instance.ChangeLanguage("en");
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }
    public void BackToMenu()
    {
        Application.LoadLevel(0);
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }
}
