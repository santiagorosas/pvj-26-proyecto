using System.Collections.Generic;


public class Persistence : ReusablePersistence
{
    // Bools
    public static bool IsMovementTutorialCompleted { get => GetBool(); set => SetBool(aValue: value); }
    public static bool IsNextWorldTutorialCompleted { get => GetBool(); set => SetBool(aValue: value); }
    public static bool HasCreatedLevelsDictionary { get => GetBool(); set => SetBool(aValue: value); }
    public static bool IsDropTutorialCompleted { get => GetBool(); set => SetBool(aValue: value); }
    
    // Ints
    public static int Money { get => GetInt(); set => SetInt(aValue: value); }
    public static int LastUnlockedLevelNumber { get => GetInt(); set => SetInt(aValue: value); }

    // Strings
    public static string ExampleString { get => GetString(); set => SetString(aValue: value); }

    // Dictionaries
    public static Dictionary<int, int> ExampleDictionary { get => GetDictionary(); set => SetDictionary(aValue: value); }

    public const int TRUE = 1;
    public const int FALSE = 0;
    
    public static Dictionary<int, int> IsLevelMastered { get => GetDictionary(); set => SetDictionary(aValue: value); }


    public static void Reset()
    {
        DeleteAll();
    }



}

