using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;


public class firstSceneMenuScript : MonoBehaviour {

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public InputField InputField;
    public Button exit;

    public static int versionNumber;

    private bool error=false;

    public AudioClip buttonSound;

    void Start()
    {


    }

    void Update()
    {
        if (Button1.GetComponentInChildren<Text>().text != LanguageManager.Instance.GetTextValue(LanguageChangeScript.versionKey) + " 1")
        {
            Button1.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.versionKey) + " 1";
            Button2.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.versionKey) + " 2";
            Button3.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.versionKey) + " 3";
            InputField.placeholder.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.enterKey) + " ID...";
            exit.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.exitKey);
        }
    }

    public void GoToVersion1()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);

        Debug.Log("Go to v.1");
        versionNumber = 1;
        if (ParseScript.exist2 || ParseScript.exist3)
        {
            error = true;
        }
        else
        {
            ParseScript.AddUser("Version_1");
        }
    }
    public void GoToVersion2()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);

        Debug.Log("Go to v.2");
        versionNumber = 2;
        if (ParseScript.exist3)
        {
            error = true;
        }
        else if (!ParseScript.exist2 && ParseScript.exist1)
        {
            ParseScript.CopyUser("Version_1", "Version_2");
        }
        else
        {
            ParseScript.AddUser("Version_2");
        }
    }
    public void GoToVersion3()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);

        Debug.Log("Go to v.3");
        versionNumber = 3;
        if (!ParseScript.exist3 && !ParseScript.exist2 && ParseScript.exist1)
        {
            error = true;
        }
        else if (!ParseScript.exist3 && ParseScript.exist2)
        {
            ParseScript.CopyUser("Version_2", "Version_3");
        }
        else
        {
            ParseScript.AddUser("Version_3");
        }
    }
    public void GoToExit()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);

        Debug.Log("Exit");
        Application.Quit();
    }

    public void Check()
    {
        ParseScript.CheckUser("Version_1", InputField.text);
        ParseScript.CheckUser("Version_2", InputField.text);
        ParseScript.CheckUser("Version_3", InputField.text);
    }

    void OnGUI()
    {
        if (error == true)
        {
            GUI.Label(new Rect((Screen.width / 2) - 25, (Screen.height / 2), 100, 30), LanguageManager.Instance.GetTextValue(LanguageChangeScript.errorKey));
            if (GUI.Button(new Rect((Screen.width / 2) - 25, (Screen.height / 2) + 30, 100, 30), "OK")) error = false;
        }
    }

}
