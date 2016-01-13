using UnityEngine;
using System.Collections;
using Parse;
using System.Threading;
using System.Threading.Tasks;
using SmartLocalization;

public class ParseScript : MonoBehaviour
{
    private static bool toGUI = false;

    static string key1;
    static string key2;
    static string key3;
    static string key;

    static string _userID;
    static string _tableName;

    public static bool exist1;
    public static bool exist2;
    public static bool exist3;

    public static float scaleA;
    public static float scaleB;
    public static float scaleC;
    public static float scaleD;

    private static bool check;

    //Is it User in the server
    public static void CheckUser(string tableName, string userID)
    {
        _userID = userID;

        exist1 = false;
        exist2 = false;
        exist3 = false;
        check = false;

        var user = ParseObject.GetQuery(tableName).WhereEqualTo("UserID", _userID);
        Task async = user.FindAsync().ContinueWith(u =>
        {
            var res = u.Result;
            check = true;
            foreach (var us in res)
            {
                if (us.Get<string>("UserID") == _userID)
                {
                    Debug.Log("In " + tableName + " user " + _userID);
                    switch (tableName)
                    {
                        case "Version_1":
                            key1 = us.ObjectId;
                            exist1 = true;
                            break;
                        case "Version_2":
                            key2 = us.ObjectId;
                            exist2 = true;
                            break;
                        case "Version_3":
                            key3 = us.ObjectId;
                            exist3 = true;
                            break;
                    }
                }
            }
        });
        
    }

    //Adding User
    public static void AddUser(string tableName)
    {
        _tableName = tableName;
        bool exist=false;
        ParseObject addUser;
        switch (_tableName)
        {
            case "Version_1":
                exist = exist1;
                break;
            case "Version_2":
                exist = exist2;
                break;
            case "Version_3":
                exist = exist3;
                break;
        }

        if (check == true)
        {
            if (exist)
            {
                Debug.Log("User already created in table"+_tableName);
                GetScale();
                switch (_tableName)
                {
                    case "Version_1":
                        key = key1;
                        break;
                    case "Version_2":
                        key = key2;
                        Debug.Log("key is " + key + " in Version_2");
                        break;
                    case "Version_3":
                        key = key3;
                        Debug.Log("key is "+key+" in Version_3");
                        break;
                }
            }
            else
            {
                
                addUser = new ParseObject(_tableName);
                addUser["UserID"] = _userID;
                addUser.SaveAsync();
                Debug.Log("Create User: " + addUser);
                key = addUser.ObjectId;
            }
            Application.LoadLevel(1);
        }
        else {
            toGUI = true;
        }
        
    }

    //To Server
    public static void SetScale(string box, string boxScale)
    {
        ParseObject addScale;
        ParseObject.GetQuery(_tableName).GetAsync(key).ContinueWith(scale =>
            {
                addScale = scale.Result;
                addScale[box]=boxScale;
                addScale.SaveAsync();
            });
    }

    //From server
    public static void GetScale()
    {
        var user = ParseObject.GetQuery(_tableName).WhereEqualTo("UserID", _userID);
        Task async = user.FindAsync().ContinueWith(u =>
        {
            var res = u.Result;
            foreach (var us in res)
            {
                switch (_tableName)
                {
                    case "Version_1":
                        scaleA = float.Parse(us.Get<string>("BoxAScale"));
                        scaleB = float.Parse(us.Get<string>("BoxBScale"));
                        break;
                    case "Version_2":
                        scaleA = float.Parse(us.Get<string>("BoxAScale"));
                        scaleB = float.Parse(us.Get<string>("BoxBScale"));
                        scaleC = float.Parse(us.Get<string>("BoxCScale"));
                        break;
                    case "Version_3":
                        scaleD = float.Parse(us.Get<string>("BoxDScale"));
                        scaleC = float.Parse(us.Get<string>("BoxCScale"));
                        break;
                }
            }
        });
    }

    //To copy from one table to another
    public static void CopyUser(string fromTable, string toTable)
    {
        ParseObject newUser;
        newUser = new ParseObject(toTable);
        newUser["UserID"] = _userID;
        newUser.SaveAsync().ContinueWith(t =>
            {
                key = newUser.ObjectId;
                key2 = key;
                key3 = key;
            });
        
        Debug.Log("Select key " + key);
        var user = ParseObject.GetQuery(fromTable).WhereEqualTo("UserID", _userID);
        Task async = user.FindAsync().ContinueWith(u =>
        {
            var res = u.Result;
            
            foreach (var us in res)
            {
                switch (toTable)
                {
                    case "Version_2":
                        _tableName = toTable;
                        scaleA = float.Parse(us.Get<string>("BoxAScale"));
                        scaleB = float.Parse(us.Get<string>("BoxBScale"));
                        scaleC = 1;
                        exist2 = true;
                        break;
                    case "Version_3":
                        if (fromTable == "Version_2")
                        {
                            Debug.Log("To Version_3");
                            _tableName = toTable;
                            scaleC = float.Parse(us.Get<string>("BoxCScale"));
                            scaleD = 1;
                            exist3 = true;
                        }
                        else
                        {
                            CopyUser(fromTable,"Version_2");
                            CopyUser("Version_2", toTable);
                        }
                        break;
                }
                
            }
        });

    }

    void OnGUI()
    {
        if (toGUI == true)
        {
            GUI.Label(new Rect((Screen.width / 2) - 25, (Screen.height / 2), 100, 30), LanguageManager.Instance.GetTextValue(LanguageChangeScript.error2Key));
            if (GUI.Button(new Rect((Screen.width / 2) - 25, (Screen.height / 2) + 30, 100, 30), "OK")) toGUI = false;
        }
    }

}
