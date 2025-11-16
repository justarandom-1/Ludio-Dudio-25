using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System.Linq;

// [System.Serializable]
// public class SaveData
// {
//     public int playerScore;
//     public string playerName;
//     public List<float> itemPositions;
// }

// [System.Serializable]
// public class IterationData
// {
//     public int playerScore;
//     public string playerName;
//     public List<float> itemPositions;
// }

public class DataStorage : MonoBehaviour
{
    public string[] stringVars;

    public string[] intVars;

    public string[] boolVars;

    static Dictionary<string, string> stringData;

    static Dictionary<string, int> intData;

    static Dictionary<string, bool> boolData;


    private static InMemoryVariableStorage variableStorage;

    void Awake()
    {
        if(stringData == null)
            stringData = new Dictionary<string, string>();

        if(intData == null)
            intData = new Dictionary<string, int>();

        if(boolData == null)
            boolData = new Dictionary<string, bool>();


        foreach (string key in stringVars)
        {
            stringData[key] = PlayerPrefs.GetString(key, "");
        }

        foreach (string key in intVars)
        {
            intData[key] = PlayerPrefs.GetInt(key, -1);
        }

        foreach (string key in boolVars)
        {
            boolData[key] = PlayerPrefs.GetInt(key, 0) != 0;
        }
    }

    void Start()
    {
        variableStorage = FindFirstObjectByType<InMemoryVariableStorage>();

        if (variableStorage == null)
        {
            Debug.Log("No memory storage");
            return;
        }

        foreach (string key in stringVars)
        {
            variableStorage.SetValue("$" + key, stringData[key]);
        }

        foreach (string key in intVars)
        {
            variableStorage.SetValue("$" + key, intData[key]);
        }

        foreach (string key in boolVars)
        {
            variableStorage.SetValue("$" + key, boolData[key]);
        }
    }

    public static void setInt(string key, int i)
    {
        if (intData == null) return;

        intData[key] = i;

        Debug.Log("Set \'" + key + "\' to " + i);

        if (variableStorage != null)
            variableStorage.SetValue("$" + key, i);
    }

    public void setInt(string input)
    {
        int i = input.IndexOf(':');

        string key = input.Substring(0, i);

        int value = int.Parse(input.Substring(i + 1));

        setInt(key, value);

        if (variableStorage == null) return;
    }

    public static int getInt(string key)
    {
        if (intData == null) return -1;

        return intData[key];
    }

    public static void setString(string key, string s)
    {
        if (stringData == null) return;

        stringData[key] = s;

        Debug.Log("Set \'" + key + "\' to " + s);

        if (variableStorage != null)
            variableStorage.SetValue("$" + key, s);
    }

    public void setString(string input)
    {
        int i = input.IndexOf(':');

        string key = input.Substring(0, i);

        string value = input.Substring(i + 1);

        setString(key, value);

        if (variableStorage == null) return;
    }

    public static string getString(string key)
    {
        if (stringData == null) return "";

        return stringData[key];
    }

    public static void setBool(string key, bool b)
    {
        if (boolData == null) return;

        boolData[key] = b;

        Debug.Log("Set \'" + key + "\' to " + b);

        if (variableStorage != null)
            variableStorage.SetValue("$" + key, b);
    }

    public void setBool(string input)
    {
        int i = input.IndexOf(':');

        string key = input.Substring(0, i);

        bool value = int.Parse(input.Substring(i + 1)) == 0;

        setBool(key, value);
    }

    public static bool getBool(string key)
    {
        if (boolData == null)
        {
            Debug.Log("No key " + key);
            return false;
        }

        return boolData[key];
    }

    public static void pushData()
    {
        if (stringData == null) return;

        foreach (string key in stringData.Keys)
        {
            PlayerPrefs.SetString(key, stringData[key]);
        }

        foreach (string key in intData.Keys)
        {
            PlayerPrefs.SetInt(key, intData[key]);
        }

        foreach (string key in boolData.Keys)
        {
            PlayerPrefs.SetInt(key, boolData[key] ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    public static void ResetLoopData()
    {
        int numResets = Mathf.Max(getInt("numPreviousResets") + 1, 0);

        if(numResets == 2)
        {
            setInt("numTimesShaeAbandonedEarly", getInt("numTimesShaeAbandoned"));
        }

        setInt("numPreviousResets", numResets);

        string lastRoute = getString("firstPartnerChoice");

        if (lastRoute != "")
        {
            setInt("numTimesStudiedWith" + lastRoute, Mathf.Max(getInt("numTimesStudiedWith" + lastRoute) + 1, 1));
        }       

        if (getBool("hasStudiedThisLoop"))
        {
            setInt("numTimesStudied", Mathf.Max(getInt("numTimesStudied") + 1, 1));
        }

        if (getBool("hasResearchedThisLoop"))
        {
            setInt("numTimesResearched", Mathf.Max(getInt("numTimesResearched") + 1, 1));
        }

        setString("firstPartnerChoice", "");
        setString("dinnerOrLibrary", "");
        setString("thoughtsOnMagic", "");
        setString("nayeliResponseDay0", "");
        setString("postDebateChoice", "");
        setString("shaeResponseDay0", "");
        setString("dinnerChoice", "");
        setString("libraryChoice", "");
        setString("dormChoice", "");
        setString("lukeDormResponseDay1", "");
        setString("shunStudyDay1", "");
        setString("shunResponseDay1", "");
        setString("shunDormResponseDay1", "");
        setString("shaeQuestionDay1", "");
        setString("historyQuestion1", "");
        setString("historyQuestion2", "");
        setString("historyQuestion3", "");
        setString("agentResponseDay2", "");
        setString("activityDay2", "");
        setString("postStudyDay1", "");
        setString("shunResponseDay2", "");
        setString("shunDormResponseDay2", "");

        setInt("fursuitChoice", 0);

        setBool("isDead", false);
        setBool("hasAskedDormThisLoop", false);
        setBool("hasStudiedThisLoop", false);
        setBool("hasResearchedThisLoop", false);
        setBool("isNayeliRouteActive", false);
        setBool("hasCompletedProjectThisLoop", false);
        setBool("isGoingOnDateThisLoop", false);
        setBool("askedDormThisLoop", false);
        setBool("beatMinigameThisLoop", false);
        setBool("wentToDormDay2ThisLoop", false);
        setBool("killedBinLadenThisLoop", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.I))
        {
            setInt("numPreviousResets", 5);
            setInt("numTimesStudied", 5);
            setInt("numTimesResearched", 5);
            setInt("numTimesStudiedWithLuke", 5);
            setInt("numTimesStudiedWithShun", 5);
            setInt("numTimesStudiedWithShae", 5);
            setBool("knowsShunDorm", true);
            setBool("hasGoneToDebate", true);

        }
    }
}
