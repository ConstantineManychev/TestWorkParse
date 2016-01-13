using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;

public class secondSceneVers1Script : MonoBehaviour {

    public GameObject version1;
    public GameObject version2;
    public GameObject version3;

    public InputField aField;
    public InputField bField;
    public Scrollbar cScrollbar;
    public Scrollbar dScrollbar;

    public Transform boxA;
    public Transform boxB;
    public Transform boxC;
    public Transform boxD;

    public Button back;

    private bool set = false;



    void Start()
    {
        switch (firstSceneMenuScript.versionNumber)
        {
            case 1:
                Debug.Log("Version 1 set");
                DeactivateChildrens.DeactivateChildren(version1, true);
                DeactivateChildrens.DeactivateChildren(version2, false);
                DeactivateChildrens.DeactivateChildren(version3, false);
                break;
            case 2:
                Debug.Log("Version 2 set");
                DeactivateChildrens.DeactivateChildren(version1, true);
                DeactivateChildrens.DeactivateChildren(version2, true);
                DeactivateChildrens.DeactivateChildren(version3, false);
                break;
            case 3:
                Debug.Log("Version 3 set");
                DeactivateChildrens.DeactivateChildren(version1, false);
                DeactivateChildrens.DeactivateChildren(version2, true);
                DeactivateChildrens.DeactivateChildren(version3, true);
                break;
        }
    }

    void Update()
    {
        back.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.exitKey);
        if (firstSceneMenuScript.versionNumber == 1 && ParseScript.exist1 && !set && ParseScript.scaleA!=0)
        {
            boxA.localScale = new Vector3(ParseScript.scaleA, ParseScript.scaleA, ParseScript.scaleA);
            boxB.localScale = new Vector3(ParseScript.scaleB, ParseScript.scaleB, ParseScript.scaleB);
            ParseScript.SetScale("BoxAScale", ParseScript.scaleA + "");
            ParseScript.SetScale("BoxBScale", ParseScript.scaleB + "");
            set = true;
        }
        else if (firstSceneMenuScript.versionNumber == 2 && ParseScript.exist2 && !set && ParseScript.scaleA != 0)
        {
            boxA.localScale = new Vector3(ParseScript.scaleA, ParseScript.scaleA, ParseScript.scaleA);
            boxB.localScale = new Vector3(ParseScript.scaleB, ParseScript.scaleB, ParseScript.scaleB);
            ParseScript.SetScale("BoxAScale", ParseScript.scaleA + "");
            ParseScript.SetScale("BoxBScale", ParseScript.scaleB + "");
            boxC.localScale = new Vector3(ParseScript.scaleC, ParseScript.scaleC, ParseScript.scaleC);
            ParseScript.SetScale("BoxCScale", ParseScript.scaleC + "");
            set = true;
        }
        else if (firstSceneMenuScript.versionNumber == 3 && ParseScript.exist3 && !set && ParseScript.scaleC != 0)
        {
            boxC.localScale = new Vector3(ParseScript.scaleC, ParseScript.scaleC, ParseScript.scaleC);
            ParseScript.SetScale("BoxCScale", ParseScript.scaleC + "");
            boxD.localScale = new Vector3(ParseScript.scaleD, ParseScript.scaleD, ParseScript.scaleD);
            ParseScript.SetScale("BoxDScale", ParseScript.scaleD + "");
            set = true;
        }
        aField.placeholder.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.enterKey) + " " + LanguageManager.Instance.GetTextValue(LanguageChangeScript.cubeKey) + " A " + LanguageManager.Instance.GetTextValue(LanguageChangeScript.scaleKey);
        bField.placeholder.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue(LanguageChangeScript.enterKey) + " " + LanguageManager.Instance.GetTextValue(LanguageChangeScript.cubeKey) + " B " + LanguageManager.Instance.GetTextValue(LanguageChangeScript.scaleKey);
    }


    public void EnterA()
    {
        Debug.Log("Scale A box set to "+aField.text);
        float scale = float.Parse(aField.text);

        boxA.localScale = new Vector3(scale, scale, scale);
        ParseScript.SetScale("BoxAScale",scale+"");
    }
    public void EnterB()
    {
        Debug.Log("Scale B box set to " + bField.text);
        float scale = float.Parse(bField.text);
        boxB.localScale = new Vector3(scale, scale, scale);
        ParseScript.SetScale("BoxBScale", scale + "");
    }
    public void EnterC()
    {
        Debug.Log("Scale A box set to " + cScrollbar.value);
        float scale = cScrollbar.value*2;
        boxC.localScale = new Vector3(scale, scale, scale);
        ParseScript.SetScale("BoxCScale", scale + "");
    }
    public void EnterD()
    {
        Debug.Log("Scale B box set to " + dScrollbar.value);
        float scale = dScrollbar.value*3;
        boxD.localScale = new Vector3(scale, scale, scale);
        ParseScript.SetScale("BoxDScale", scale + "");
    }

}
