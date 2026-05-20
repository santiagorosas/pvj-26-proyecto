
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class ReusablePersistence
{
    //Save array as string: PlayerPrefs.SetString("title", string.Join("###", anArray));
    //Read array from string: var anArray = PlayerPrefs.SetString("title").Split(new[] { "###" }, StringSplitOptions.None);

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }


    protected static string GetStringByName(string name)
    {
        return PlayerPrefs.GetString(key: name);
    }


    protected static void SetStringByName(string name, string value)
    {
        PlayerPrefs.SetString(key: name, value);
    }


    public static int GetIntByName(string name)
    {
        return PlayerPrefs.GetInt(key: name);
    }


    public static void SetIntByName(string name, int value)
    {
        PlayerPrefs.SetInt(key: name, value);
    }


    protected static void SetIntByNameIfHigher(string name, int value)
    {
        if (value > GetIntByName(name))
        {
            SetIntByName(name: name, value: value);
        }
    }

    protected static bool GetBoolByName(string key)
    {
        return PlayerPrefs.GetInt(key) == 1;
    }

    protected static void SetBoolByName(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value ? 1 : 0);
    }

    protected static Dictionary<int, int> GetDictionaryByName(string key)
    {
        string serializedDictionary = PlayerPrefs.GetString(key);
        return CollectionSerializer.DeserializeDictionary(data: serializedDictionary);
    }

    protected static void SetDictionaryByName(string name, Dictionary<int, int> value)
    {
        string serializedDictionary = CollectionSerializer.SerializeDictionary(value);        
        PlayerPrefs.SetString(key: name, value: serializedDictionary);
    }

    protected static List<List<int>> GetMatrixByName(string name)
    {
        string serializedMatrix = PlayerPrefs.GetString(key: name);        
        return CollectionSerializer.DeserializeMatrix(data: serializedMatrix);
    }

    protected static void SetMatrixByName(string name, List<List<int>> value)
    {        
        string serializedMatrix = CollectionSerializer.SerializeMatrix(matrix: value);        
        PlayerPrefs.SetString(key: name, value: serializedMatrix);
    }

    protected static bool GetBool([CallerMemberName] string propertyName = null)
    {
        return GetBoolByName(propertyName);
    }

    protected static void SetBool(bool aValue, [CallerMemberName] string propertyName = null)
    {
        SetBoolByName(name: propertyName, value: aValue);
    }

    protected static int GetInt([CallerMemberName] string propertyName = null)
    {
        return GetIntByName(propertyName);
    }

    protected static void SetInt(int aValue, [CallerMemberName] string propertyName = null)
    {
        SetIntByName(name: propertyName, value: aValue);
    }

    protected static string GetString([CallerMemberName] string propertyName = null)
    {
        return GetStringByName(propertyName);
    }

    protected static void SetString(string aValue, [CallerMemberName] string propertyName = null)
    {
        SetStringByName(name: propertyName, value: aValue);
    }

    protected static Dictionary<int, int> GetDictionary([CallerMemberName] string propertyName = null)
    {
        return GetDictionaryByName(propertyName);
    }

    protected static void SetDictionary(Dictionary<int, int> aValue, [CallerMemberName] string propertyName = null)
    {
        SetDictionaryByName(name: propertyName, value: aValue);
    }

    protected static List<List<int>> GetMatrix([CallerMemberName] string propertyName = null)
    {
        return GetMatrixByName(propertyName);
    }

    protected static void SetMatrix(List<List<int>> aValue, [CallerMemberName] string propertyName = null)
    {
        SetMatrixByName(name: propertyName, value: aValue);
    }
}
