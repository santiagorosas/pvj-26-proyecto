using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public static class Utils
{
    public static void AddButtonClickListener(Transform parentTransform, string buttonName, UnityAction listener)
    {
        Button playButton = parentTransform.Find(buttonName).GetComponent<Button>();
        playButton.onClick.AddListener(listener);
    }


    /// <summary>
    /// Includes aInt1 and aInt2
    /// </summary>    
    public static int GetRandomIntBetween(int int1, int int2)
    {


        return int1 + (int)Mathf.Floor(UnityEngine.Random.value * (int2 - int1 + 1));
    }


    public static T GetRandomListElement<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            throw new ArgumentException("List is empty!");
        }

        return list[GetRandomIntBetween(0, list.Count - 1)];
    }


    public static T GetRandomArrayElement<T>(T[] array)
    {
        return array[GetRandomIntBetween(0, array.Length - 1)];
    }


    public static float GetRandomFloatBetween(float float1, float float2)
    {
        return float1 + UnityEngine.Random.value * (float2 - float1);
    }


    public static float GetSpriteHeight(Transform transform)
    {
        return transform.GetComponent<SpriteRenderer>().bounds.size.y;
    }


    public static float GetSpriteWidth(Transform transform)
    {
        return transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }


    public static float GetScreenBottom()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }


    public static float GetScreenTop()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }


    /*
    public static void SetVelocityX(Rigidbody2D rigidbody, float x)
    {
        Vector2 velocity = rigidbody.linearVelocity;
        velocity.x = x;
        rigidbody.linearVelocity = velocity;
    }


    public static void SetVelocityY(Rigidbody2D rigidbody, float y)
    {
        Vector2 velocity = rigidbody.linearVelocity;
        velocity.y = y;
        rigidbody.linearVelocity = velocity;
    }
    */


    public static void SetPositionX(Transform transform, float x)
    {
        Vector2 position = transform.position;
        position.x = x;
        transform.position = position;
    }


    public static void SetPositionY(Transform transform, float y)
    {
        Vector2 position = transform.position;
        position.y = y;
        transform.position = position;
    }


    public static bool FuzzyEquals(float a, float b, float fuzziness = 0.01f)
    {
        return Mathf.Abs(a - b) < fuzziness;
    }


    public static TextMeshProUGUI GetText(Transform transform, string path)
    {
        return Find(transform, path).GetComponent<TextMeshProUGUI>();
    }

    public static Button FindButton(string path)
    {
        return Find(path).GetComponent<Button>();
    }

    public static Button FindButton(Transform transform, string path)
    {
        return Find(transform, path).GetComponent<Button>();
    }

    public static GameObject FindGameObject(Transform transform, string path)
    {
        return Find(transform, path).gameObject;
    }

    public static Transform FindAnywhereUnder(Transform transform, string childName)
    {
        Transform found = FindAnywhereUnderRecursive(transform, childName);
        if (found == null)
        {
            throw new UnityException($"Couldn't find child {childName} anywhere under {transform.name}!");
        }
        else
        {
            return found;
        }
    }

    private static Transform FindAnywhereUnderRecursive(Transform transform, string childName)
    {
        foreach (Transform child in transform)
        {
            if (child.name == childName)
            {
                return child;
            }

            var result = FindAnywhereUnderRecursive(child, childName);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public static Transform Find(Transform transform, string path)
    {
        string[] names = path.Split('/');

        Transform currentTransform = transform;

        foreach (string name in names)
        {
            currentTransform = FindChildByName(currentTransform, (name));
            if (currentTransform == null)
            {
                throw new UnityException("Can't find " + name + " in path " + path + " in transform " + transform.name);
            }
        }

        return currentTransform;
    }


    public static Transform FindChildByName(Transform transform, string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name == name)
            {
                return child;
            }
        }

        throw new UnityException("Couldn't find child " + name + " in transform " + transform.name);
    }


    public static void SetText(Transform transform, string path, string text)
    {
        Find(transform, path).GetComponent<TextMeshProUGUI>().text = text;
    }


    public static T GetEnumFromString<T>(string aString)
    {
        return (T)System.Enum.Parse(typeof(T), aString);
    }

    public static List<T> GetEnumList<T>()
    {
        Array array = Enum.GetValues(typeof(T));
        List<T> list = new List<T>();

        foreach (T element in array)
        {
            list.Add(element);
        }

        return list;
    }

    public static T GetEnumFromInt<T>(int aInt)
    {
        return GetEnumList<T>()[aInt];
    }

    public static void ClampZ(Transform transform, float minZ, float maxZ)
    {
        if (transform.position.z < minZ)
        {
            Vector3 pos = transform.position;
            pos.z = maxZ;
            transform.position = pos;
        }
        else if (transform.position.z > maxZ)
        {
            Vector3 pos = transform.position;
            pos.z = maxZ;
            transform.position = pos;
        }
    }

    public static int RoundToInt(float aFloat)
    {
        return Mathf.RoundToInt(aFloat);
    }

    public static int CeilToInt(float aFloat)
    {
        return Mathf.CeilToInt(aFloat);
    }

    public static float GetScreenSpaceDistance(float worldDistance)
    {
        // Converts worldDistance to a screen space (camera) distance
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            UnityEngine.Debug.LogError("Main camera not found");
            return 0;
        }

        Vector3 worldPoint1 = new Vector3(0, 0, 0);
        Vector3 worldPoint2 = new Vector3(worldDistance, 0, 0);

        Vector3 screenPoint1 = mainCamera.WorldToScreenPoint(worldPoint1);
        Vector3 screenPoint2 = mainCamera.WorldToScreenPoint(worldPoint2);

        return Vector3.Distance(screenPoint1, screenPoint2);
    }

    public static void ResetAllFields(object obj)
    {
        if (obj == null) return;

        // Get all fields from the object's type and its base types
        var fields = obj.GetType().GetFields(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance
        );

        foreach (var field in fields)
        {
            // Skip readonly fields
            if (field.IsInitOnly) continue;

            // Get default value for the field's type
            object defaultValue = field.FieldType.IsValueType
                ? System.Activator.CreateInstance(field.FieldType)
                : null;

            // Set the field to its default value
            field.SetValue(obj, defaultValue);
        }
    }

    public static int CeilToMultiple(float number, int multipleOf)
    {
        return CeilToInt(number / multipleOf) * multipleOf;
    }

    //Assumes "button" has a child named Text that has a TextMeshProUGUI component.
    public static TextMeshProUGUI GetButtonText(Button button)
    {
        Transform textTransform = button.transform.Find("Text (TMP)");
        if (textTransform == null)
        {
            throw new UnityException("No child Text in button " + button.name);
        }

        TextMeshProUGUI text = textTransform.GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            throw new UnityException("Text object " + text.name + " doesn't have a TextMeshProUGUI component!");
        }

        return text;
    }

    public static void SetButtonText(Button button, string text)
    {
        GetButtonText(button).text = text;
    }

    public static bool IsMouseOverUiObject(string uiObjectName)
    {
        if (GameObject.Find(uiObjectName) == null)
        {
            return false;
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    return false;
                }
                else
                {
                    return EventSystem.current.currentSelectedGameObject.name == uiObjectName;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public static bool IsMouseOverUiButton()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return true;
            }
        }

        return false;
    }

    public static bool GetRandomBoolWeighted(float trueProbability)
    {
        float randomFloat = GetRandomFloatBetween(0, 1);
        return randomFloat < trueProbability;
    }

    [Serializable]
    public class WeightedInt
    {
        public int value;
        public float weight;
    }

    [Serializable]
    public class WeightedValue<T>
    {
        public T value;
        public float weight;
    }

    public static T GetRandomValueWeighted<T>(List<WeightedValue<T>> weightedValues)
    {
        float totalWeight = weightedValues.Sum(x => x.weight);
        float randomFloat = GetRandomFloatBetween(0, totalWeight);

        float currentWeight = 0;
        foreach (WeightedValue<T> weightedValue in weightedValues)
        {
            currentWeight += weightedValue.weight;
            if (randomFloat < currentWeight)
            {
                return weightedValue.value;
            }
        }

        throw new UnityException("Couldn't get a random weighted value");
    }

    public static int GetRandomIntWeighted(List<WeightedInt> weightedInts)
    {
        float totalWeight = 0;
        foreach (WeightedInt weightedInt in weightedInts)
        {
            totalWeight += weightedInt.weight;
        }

        float randomFloat = GetRandomFloatBetween(0, totalWeight);

        float currentWeight = 0;
        foreach (WeightedInt weightedInt in weightedInts)
        {
            currentWeight += weightedInt.weight;
            if (randomFloat < currentWeight)
            {
                return weightedInt.value;
            }
        }

        throw new UnityException("Couldn't get a random weighted int");
    }

    public static T TryFind<T>() where T : UnityEngine.Object
    {
        try
        {
            return Find<T>();
        }
        catch (UnityException)
        {
            return null;
        }
    }

    public static T Find<T>() where T : UnityEngine.Object
    {
        T theObject = GameObject.FindFirstObjectByType<T>(FindObjectsInactive.Include);
        return theObject;
    }

    public static T Find<T>(Transform transform, string name) where T : UnityEngine.Object
    {
        T theObject = Find(transform, name).GetComponent<T>();
        return theObject;
    }

    public static List<T> FindAllList<T>() where T : UnityEngine.Object
    {
        return FindAllArray<T>().ToList();
    }

    public static T[] FindAllArray<T>() where T : UnityEngine.Object
    {
        return GameObject.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public static GameObject TryFind(string name)
    {
        try
        {
            return Find(name);
        }
        catch (UnityException)
        {
            return null;
        }
    }

    public static GameObject Find(string name)
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>();
        var firstOrDefault = objects.FirstOrDefault(x => x.name == name);
        if (firstOrDefault == null)
        {
            throw new UnityException("No GameObject with name " + name);
        }
        return firstOrDefault;
    }

    public static T FindInScene<T>(string name) where T : MonoBehaviour
    {
        return FindAllList<T>().Find(x => x.name == name);
    }

    public static Vector3 GetWorldCenterOfScreen()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane);
        return Camera.main.ScreenToWorldPoint(screenCenter);
    }

    public static List<T> SelectRandomItemsFromList<T>(List<T> sourceList, int itemCount)
    {
        // Check if the input parameters are valid
        if (sourceList == null)
            throw new ArgumentNullException(nameof(sourceList), "Source list cannot be null");

        if (itemCount < 0)
            throw new ArgumentException("Item count cannot be negative", nameof(itemCount));

        if (itemCount > sourceList.Count)
            throw new ArgumentException("Item count cannot exceed source list length", nameof(itemCount));

        // Use Random class to shuffle and select items
        System.Random random = new System.Random();

        // Order by random value and take specified count
        return sourceList
            .OrderBy(x => random.Next())
            .Take(itemCount)
            .ToList();
    }

    public static Vector2 GetAnchoredPosition(this Transform transform)
    {
        return transform.GetComponent<RectTransform>().anchoredPosition;
    }

    public static void SetAnchoredPosition(this Transform transform, Vector2 anchoredPosition)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    public static void SetAnchoredX(this Transform transform, float x)
    {
        SetAnchoredPosition(transform, new Vector2(x, GetAnchoredPosition(transform).y));
    }

    public static float GetAnchoredX(this Transform transform)
    {
        return GetAnchoredPosition(transform).x;
    }

    public static bool IsInteger(float x)
    {
        return Math.Floor(x) == x;
    }

    public static string AddSpacesToString(this string aString)
    {
        string spacedString = Regex.Replace(aString, "(?<!^)([A-Z])", " $1");
        return spacedString;
    }

    public static void RandomizeList<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public static string GetClassName(object aObject)
    {
        string className = aObject.GetType().Name;
        return className;
    }

    public static object CreateInstanceFromClassName(string className, Assembly assembly)
    {
        System.Type type = GetTypeFromClassName(className, assembly);

        if (type != null)
        {
            UnityEngine.Debug.Log("creating instance of " + type);
            object instance = Activator.CreateInstance(type);
            return instance;
        }
        else
        {
            throw new UnityException($"Type {className} is null!");
        }
    }

    public static System.Type GetTypeFromClassName(string className, Assembly assembly)
    {
        System.Type type = assembly.GetType(className);
        return type;
    }


    /// <summary>
    /// The returned "bag" is a list with repeated quantities of each enum type, the quantities are based on the provided weights and the desired bag size.
    /// 
    /// This is for creating distributions of an enum (for example, types of balloons in Balloon Pop Punk) that are exact instead of probabilistic
    /// e.g. if I set weights W(A) = 10, W(B) = 5 and W(C) = 3 the exact quantities of each one will be multiples of those numbers
    /// e.g. with a bag size of 36, we have 20 A's, 10 B's and 6 C's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="weightedList"></param>
    /// <param name="bagSize"></param>
    /// <returns></returns>
    public static List<T> GetBagFromWeightedList<T>(List<WeightedValue<T>> weightedList, int bagSize)
    {
        List<T> bag = new();

        float weightsSum = weightedList.Sum(x => x.weight);
        if (weightsSum == 0)
        {
            throw new UnityException("Weights in weighted value list sum 0!");
        }

        foreach (WeightedValue<T> weightedType in weightedList)
        {
            int quantityOfThisType = Mathf.CeilToInt(bagSize * weightedType.weight / weightsSum);

            for (int i = 0; i < quantityOfThisType; i++)
            {
                bag.Add(weightedType.value);
            }
        }

        return bag;
    }

    public static List<int> GetIntBagFromWeightedList(List<WeightedInt> weightedList, int bagSize)
    {
        List<int> bag = new();

        foreach (WeightedInt weightedInt in weightedList)
        {
            int quantityOfThisInt = Mathf.CeilToInt(bagSize * weightedInt.weight);

            for (int i = 0; i < quantityOfThisInt; i++)
            {
                bag.Add(weightedInt.value);
            }
        }

        return bag;
    }

    public static void RandomizeArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            swap(array, i, UnityEngine.Random.Range(i, array.Length));
        }
    }


    public static List<T> RandomizeList<T>(List<T> list)
    {
        var array = list.ToArray();
        RandomizeArray(array);
        List<T> randomizedList = ArrayToList(array);
        return randomizedList;
    }


    public static List<T> CopyList<T>(List<T> list)
    {
        return new List<T>(list);
    }


    public static List<T> ConcatLists<T>(List<T> list1, List<T> list2)
    {
        return ConcatLists(new List<List<T>> { list1, list2 });
    }


    public static List<T> ConcatLists<T>(List<List<T>> lists)
    {
        List<T> concat = new List<T>();
        foreach (List<T> list in lists)
        {
            concat.AddRange(list);
        }
        return concat;
    }


    public static void swap<T>(T[] arr, int id1, int id2)
    {
        var temp = arr[id1];
        arr[id1] = arr[id2];
        arr[id2] = temp;
    }


    public static T mGetRandomListElement<T>(List<T> aList)
    {
        if (aList.Count == 0)
        {
            throw new Exception("The list can't be empty");
        }

        int vIndex = (int)(Mathf.Floor(aList.Count * UnityEngine.Random.value));
        return aList[Math.Min(vIndex, aList.Count - 1)];
    }

    public static T mGetRandomArrayElement<T>(T[] aList)
    {
        if (aList.Length == 0)
        {
            throw new Exception("The array can't be empty");
        }

        int vIndex = (int)(Mathf.Floor(aList.Length * UnityEngine.Random.value));
        return aList[Math.Min(vIndex, aList.Length - 1)];
    }


    /**
     * @param	aTime
     * @return time representation in format MM:SS
     */
    public static string mGetTimeString(float aTime)
    {
        int vMin = (int)(aTime / 60);
        int vSeg = (int)(aTime - 60 * vMin);
        string vSegStr;
        if (vSeg < 10)
        {
            vSegStr = "0" + vSeg;
        }
        else
        {
            vSegStr = "" + vSeg;
        }

        return "" + vMin + ":" + vSegStr;
    }


    //time in seconds
    public static string FormatTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string formattedTimeSpan = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        return formattedTimeSpan;
    }

    /**
 * @param	aFrequencies an array of frequencies (Numbers)
 * e.g. {0.3, 0.2, 0.5}
 * @return  A random int, that's an index of aFrequencies, randomized according to the
 * frequencies. 
 * @example If aFrequencies == {x} it will always return 0.
 * If aFrequencies == {0.1, 0.9}, it will return 0 with a frequency of 1/10, and
 * 1 with a frequency of 9/10.
 */
    public static int mGetRandomIntFromFrequencies(List<float> aFrequencies)
    {
        if (aFrequencies == null)
        {
            throw new ArgumentException("The frequency array can't be null.");
        }

        aFrequencies = mNormalizeFrequenciesList(aFrequencies);

        /*
    if (aFrequencies.Exists(x => x.ToString() == "NaN")) {			
        mPrintList(aFrequencies);
        throw new Exception("NaN at index " + aFrequencies.IndexOf(aFrequencies.Find(x => x.ToString() == "NaN")));;
    }
    */

        float vRandom = UnityEngine.Random.value;
        float vFrequencyAccumulator = 0;

        for (int i = 0; i < aFrequencies.Count; i++)
        {
            vFrequencyAccumulator += aFrequencies[i];

            if (aFrequencies[i].ToString() == "NaN")
            {
                throw new Exception("NaN");
            }

            if (vRandom < vFrequencyAccumulator)
            {
                return i;
            }
        }

        mPrintList(aFrequencies);
        throw new Exception("This shouldn't happen! The function is badly implemented!");
    }


    public static List<float> mNormalizeFrequenciesList(List<float> aFrequencies)
    {
        float vSumFrequencies = mGetListSum(aFrequencies);

        if (vSumFrequencies == 0)
        {
            mPrintList(aFrequencies);
            throw new Exception("Sum Frequencies = 0");
        }

        List<float> vNormalizedFrequencies = new List<float>();
        for (int i = 0; i < aFrequencies.Count; i++)
        {
            vNormalizedFrequencies.Add(aFrequencies[i] / vSumFrequencies);
        }

        return vNormalizedFrequencies;
    }


    /**
 * @param aArray an array of any type
 * @param	aFrequencies an array of frequencies (floats), whose sum is 1.0, e.g. {0.3f, 0.2f, 0.5f}
 * @return the element of aArray with index equal to mGetRandomIntFromFrequencies(aFrequencies)
 * @see mGetRandomIntFromFrequencies()
 * @throws ArgumentError if the lengths of aArray and aFrequencies are not equal.
 */
    public static T mGetRandomListElementFromFrequencies<T>(List<T> aList,
        List<float> aFrequencies)
    {
        if (mGetListSum(aFrequencies) == 0)
        {
            mPrintList(aList);
            throw new ArgumentException("The sum of the frequencies can't be 0");
        }

        if (aList.Count != aFrequencies.Count)
        {
            throw new ArgumentException("The lengths of aArray and aFrequencies must be equal.");
        }

        int vIndex = mGetRandomIntFromFrequencies(aFrequencies);
        return aList[vIndex];
    }


    public static float mDistance(float aNum1, float aNum2)
    {
        return Math.Abs(aNum1 - aNum2);
    }


    /**
 * Returns the sum of the elements of a list.	 
 */
    public static float mGetListSum(List<float> aList)
    {
        float vSum = 0;
        for (int i = 0; i < aList.Count; i++)
        {
            vSum += aList[i];
        }

        return vSum;
    }


    public static List<T> mGetRange<T>(this List<T> aList, int aFirstIndex, int aLastIndex)
    {
        List<T> vList = new List<T>();
        for (int i = aFirstIndex; i <= Math.Min(aLastIndex, aList.Count - 1); i++)
        {
            vList.Add(aList[i]);
        }

        return vList;
        //return aList.GetRange(aFirstIndex, aLastIndex - aFirstIndex + 1);
    }


    public static void mRemoveAllFromList<T>(this List<T> aGoodList, List<T> aBadList)
    {
        aGoodList.RemoveAll(x => aBadList.Contains(x));
    }


    public static T mGetRandomListElementDifferentFrom<T>(List<T> aSourceList, List<T> aExcludedList)
    {
        return mGetRandomListElement(mSubstractList(aSourceList, aExcludedList));
    }


    public static T mGetRandomListElementDifferentFrom<T>(List<T> aSourceList, T aExcludedElement)
    {
        List<T> vList = new List<T>();
        for (int i = 0; i < aSourceList.Count; i++)
        {
            if (!aSourceList[i].Equals(aExcludedElement))
            {
                vList.Add(aSourceList[i]);
            }
        }

        return mGetRandomListElement(vList);
    }


    public static T mGetRandomListElementFromFrequenciesDifferentFrom<T>(List<T> aSourceList,
        List<float> aFrequencies, List<T> aExcludedList)
    {
        List<T> vIncludedElements = mSubstractList(aSourceList, aExcludedList);
        //I want a new frequencies list (vIncludedFrequencies) that corresponds to vIncludedElements
        List<float> vIncludedFrequencies = new List<float>();
        foreach (T vIncludedElement in vIncludedElements)
        {
            vIncludedFrequencies.Add(mGetFrequency(vIncludedElement, aSourceList, aFrequencies));
        }

        if (mGetListSum(vIncludedFrequencies) == 0)
        {
            throw new ArgumentException("");
        }

        return mGetRandomListElementFromFrequencies(vIncludedElements, vIncludedFrequencies);
    }


    //Get the frequency of a element from the elements list and the frequencies list
    private static float mGetFrequency<T>(T aElement, List<T> aElements, List<float> aFrequencies)
    {
        int vIndexOfElement = aElements.IndexOf(aElement);
        float vFrequency;
        try
        {
            vFrequency = aFrequencies[vIndexOfElement];
        }
        catch (Exception e)
        {
            Debug.Log("aElement: " + aElement);
            Debug.Log("aElements:");
            mPrintList(aElements);
            Debug.Log("aFrequencies:");
            mPrintList(aFrequencies);
            throw e;
        }

        return vFrequency;
    }


    public static List<T> mGetCommonElements<T>(List<T> aList1, List<T> aList2)
    {
        List<T> vList = new List<T>();
        for (int i = 0; i < aList1.Count; i++)
        {
            if (aList2.Contains(aList1[i]))
            {
                vList.Add(aList1[i]);
            }
        }

        return vList;
    }


    public static int mGetNumCommonElements<T>(List<T> aList1, List<T> aList2)
    {
        return mGetCommonElements(aList1, aList2).Count;
    }


    //Get all the elements that are in aList1 but not in aList2
    public static List<T> mSubstractList<T>(List<T> aList1, List<T> aList2)
    {
        List<T> vList = new List<T>();
        for (int i = 0; i < aList1.Count; i++)
        {
            if (!aList2.Contains(aList1[i]))
            {
                vList.Add(aList1[i]);
            }
        }

        return vList;
    }


    public static int mGetNumNullsInList<T>(List<T> aList)
    {
        int vCount = 0;
        for (int i = 0; i < aList.Count; i++)
        {
            if (aList[i] == null)
            {
                vCount++;
            }
        }

        return vCount;
    }


    public static float mDegreesToRadians(float aAngleDegrees)
    {
        return aAngleDegrees / 360 * (2 * Mathf.PI);
    }


    public static float mRadiansToDegrees(float aAngleRadians)
    {
        return aAngleRadians * 360 / (2 * Mathf.PI);
    }


    public static TKey mFindKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> aDictionary, TValue aValue)
    {
        if (aDictionary == null)
        {
            throw new ArgumentNullException("dictionary");
        }

        foreach (KeyValuePair<TKey, TValue> vPair in aDictionary)
        {
            if (aValue.Equals(vPair.Value))
            {
                return vPair.Key;
            }
        }

        throw new Exception("The value is not found in the dictionary");
    }


    public static List<TValue> mGetValuesList<TKey, TValue>(this Dictionary<TKey, TValue> aDictionary)
    {
        List<TValue> vList = new List<TValue>();
        foreach (TKey vKey in aDictionary.Keys)
        {
            vList.Add(aDictionary[vKey]);
        }

        return vList;
    }


    public static float mGetRandomFloatBetween(float aNumber1, float aNumber2)
    {
        return aNumber1 + UnityEngine.Random.value * (aNumber2 - aNumber1);
    }


    /**
     * Includes aInt1 and aInt2
     */
    public static int mGetRandomIntBetween(int aInt1, int aInt2)
    {
        return aInt1 + (int)Mathf.Floor(UnityEngine.Random.value * (aInt2 - aInt1 + 1));
    }


    public static bool mGetRandomBool()
    {
        return UnityEngine.Random.value < 0.5f;
    }


    //Returns a color in RGB from 0 to 1 from RGB values from 0 to 256
    public static Color mGetColor(float aRed, float aGreen, float aBlue)
    {
        return new Color(aRed / 256f, aGreen / 256f, aBlue / 256f);
    }


    //Returns true iff aObject is equal to aObject2, aObject3, aObject4 or aObject5
    public static bool mIsAnyOf(this object aObject, object aObject2, object aObject3, object aObject4, object aObject5,
        object aObject6)
    {
        return aObject != null &&
               (aObject.mIsAnyOf(aObject2, aObject3, aObject4, aObject5) || aObject.Equals(aObject6));
    }


    //Returns true iff aObject is equal to aObject2, aObject3, aObject4 or aObject5
    public static bool mIsAnyOf(this object aObject, object aObject2, object aObject3, object aObject4, object aObject5)
    {
        return aObject != null && (aObject.mIsAnyOf(aObject2, aObject3, aObject4) || aObject.Equals(aObject5));
    }

    //Returns true iff aObject is equal to aObject2, aObject3 or aObject4
    public static bool mIsAnyOf(this object aObject, object aObject2, object aObject3, object aObject4)
    {
        return aObject != null && (aObject.mIsAnyOf(aObject2, aObject3) || aObject.Equals(aObject4));
    }


    public static bool mIsAnyOf(this object aObject, object aObject2, object aObject3)
    {
        return aObject != null && (aObject.Equals(aObject2) || aObject.Equals(aObject3));
    }


    public static string GetListString<T>(this List<T> aList)
    {
        string listString = "[";
        for (int i = 0; i < aList.Count; i++)
        {
            listString += aList[i].ToString();
            if (i < aList.Count - 1)
            {
                listString += ",";
            }
        }

        listString += "]";
        return listString;
    }


    public static void mPrintList<T>(List<T> aList)
    {
        Debug.Log(GetListString(aList));
    }


    public static string mGetStringFromFile(string aFilePath)
    {
        TextAsset vFile = Resources.Load<TextAsset>(aFilePath);
        if (vFile == null)
        {
            return null;
        }
        else
        {
            return vFile.text;
        }
    }


    public static List<string> GetTsvSpreadsheetColumn(string fileName, string columnName)
    {
        string tsvContent = mGetStringFromFile(fileName);
        int rowCount = new List<char>(tsvContent.ToCharArray()).FindAll(x => x == '\n').Count + 1;
        List<string> cells = GetTsvSpreadsheetCells(fileName);
        int columnCount = cells.Count / rowCount;
        int desiredColumnIndex = cells.IndexOf(columnName);

        List<string> columnCells = new List<string>();
        for (int i = desiredColumnIndex + columnCount; i < cells.Count; i += columnCount)
        {
            string cell = cells[i];
            columnCells.Add(cell);
        }

        return columnCells;
    }


    public static List<string> GetTsvSpreadsheetCells(string fileName)
    {
        string tsvContent = mGetStringFromFile(fileName);

        if (tsvContent == null)
        {
            throw new UnityException("String from file " + fileName + " is null");
        }

        tsvContent = tsvContent.Replace("\r\n", "\n").Replace("\r", "\n"); // Normalize newlines
        List<string> cells = new List<string>(tsvContent.Split('\n').SelectMany(line => line.Split('\t')));
        return cells;

        /*
        if (text == null)
        {
            throw new UnityException("Couldn't load file " + fileName + ".txt. Maybe it's not in the txt format!");
        }
        string[] tsvStrings = text.Split(new char[] { '\t', '\n' });

        return new List<string>(tsvStrings);
        */
    }


    /**		 
 * @param	aVector Vector to rotate
 * @param	aAngle Rotation angle in degrees
 * @return
 */
    public static Vector2 mRotateVector(Vector2 aVector, float aAngle)
    {
        if (aAngle == 0)
        {
            return aVector;
        }
        else
        {
            float vAngle = mDegreesToRadians(aAngle);
            float vX = aVector.x * Mathf.Cos(vAngle) + aVector.y * Mathf.Sin(vAngle);
            float vY = aVector.y * Mathf.Cos(vAngle) - aVector.x * Mathf.Sin(vAngle);
            return new Vector2(vX, vY);
        }
    }


    public static Vector2 mGetOppositeVector(Vector2 aVector)
    {
        return new Vector2(0, 0) - aVector;
    }


    public static List<T> ArrayToList<T>(T[] array)
    {
        List<T> list = new List<T>();
        list.AddRange(array);
        return list;
    }


    public static T[] ListToArray<T>(List<T> list)
    {
        return list.ToArray();
    }


    public static bool IsBetweenNumbers(float number, float min, float max)
    {
        return number >= min && number <= max;
    }


    public static List<T> GetDictionaryKeys<T, U>(Dictionary<T, U> dictionary)
    {
        List<T> keys = new List<T>();
        foreach (T key in dictionary.Keys)
        {
            keys.Add(key);
        }

        return keys;
    }


    public static List<U> GetDictionaryValues<T, U>(Dictionary<T, U> dictionary)
    {
        List<U> values = new List<U>();
        foreach (U value in dictionary.Values)
        {
            values.Add(value);
        }

        return values;
    }

    public static bool IsInScreen(Vector2 pos, float xTolerance = 0)
    {
        return IsBetweenNumbers(Camera.main.WorldToViewportPoint(pos).x, 0, 1 + xTolerance) &&
               IsBetweenNumbers(Camera.main.WorldToViewportPoint(pos).y, 0, 1);
    }


    public static Vector2 Vector3To2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    /// <summary>
    /// Returns a random point inside a circle lying in the XZ plane.
    /// </summary>
    public static Vector3 GetRandomPointInCircleXZ(Vector3 center, float radius)
    {
        var angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        var dist = Mathf.Sqrt(UnityEngine.Random.value) * radius;

        return new Vector3(
            center.x + Mathf.Cos(angle) * dist,
            center.y,
            center.z + Mathf.Sin(angle) * dist
        );
    }

    //http://mathworld.wolfram.com/DiskPointPicking.html
    public static Vector2 GetRandomPointInCircle2D(Vector2 center, float radius)
    {
        float angle = mGetRandomFloatBetween(0, 2 * Mathf.PI);
        float r = mGetRandomFloatBetween(0, radius);
        float x = Mathf.Sqrt(r) * Mathf.Cos(angle);
        float y = Mathf.Sqrt(r) * Mathf.Sin(angle);
        return new Vector2(center.x + x, center.y + y);
    }

    public static void Stretch(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition) * 6;
        _sprite.transform.localScale = scale;
    }


    public static Quaternion GetAngleToFaceDirection(Vector3 vector)
    {
        float angle = Vector2.Angle(Vector2.up, vector);
        if (vector.x > 0)
        {
            angle = 360 - angle;
        }

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }


    public static void SetBarWidth(GameObject bar, float value, float maxValue)
    {
        float div = value / maxValue;
        bar.transform.localScale = new Vector2(div, 1);
    }


    public static string GetSpaceSeparatedStringFromObject(object aObject)
    {
        return Regex.Replace(aObject.ToString(), "(\\B[A-Z])", " $1");
    }


    public static void SetY(Transform transform, float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }


    public static void SetX(Transform transform, float x)
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }


    public static void SetZ(Transform transform, float z)
    {
        Vector3 pos = transform.position;
        pos.z = z;
        transform.position = pos;
    }


    public static void SetAnchoredY(Transform transform, float y)
    {
        Vector3 position = (transform as RectTransform).anchoredPosition;
        position.y = y;
        (transform as RectTransform).anchoredPosition = position;
    }

    public static float GetTime(float speed, float distance)
    {
        //speed = distance / time
        //time = distance / speed
        return distance / speed;
    }

    public static FieldType GetFieldValue<FieldType>(object aObject, string fieldName)
    {
        Type objectType = aObject.GetType();
        FieldInfo fieldInfo = objectType.GetField(fieldName);
        return (FieldType)fieldInfo.GetValue(aObject);
    }

    /*
    public static void SetText(Transform transform, string path, string text)
    {
        transform.Find(path).GetComponent<TextMeshProUGUI>().text = text;
    }
    */

    public static int GetEnumIndex<T>(T enumValue)
    {
        return Array.IndexOf(Enum.GetValues(enumValue.GetType()), enumValue);
    }

    public static string ConvertStringToAllCaps(string aString)
    {
        return Regex.Replace(aString, @"(\p{Ll})(\p{Lu})", "$1_$2").ToUpper();
    }

    public static void Billboard(Transform transform, Camera camera)
    {
        transform.LookAt(camera.transform);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x * -1, 0, 0));
    }

    public static Vector3 CurveMove(Vector3 origin, Vector3 target, AnimationCurve curve, float elapsedTime, float maxTime, Action completeCallback)
    {
        float normalizedTime = elapsedTime / maxTime;

        if (normalizedTime >= 1 && completeCallback != null)
        {
            completeCallback();
        }
        float progress = curve.Evaluate(normalizedTime);
        Vector3 newPos = origin + (target - origin) * progress;
        return newPos;
    }

    public static float CurveNumber(float origin, float target, AnimationCurve curve, float time, float maxTime, Action completeCallback)
    {
        float normalizedTime = time / maxTime;

        if (normalizedTime >= 1 && completeCallback != null)
        {
            completeCallback();
        }
        float progress = curve.Evaluate(normalizedTime);
        float newNumber = origin + (target - origin) * progress;
        return newNumber;
    }
 
    public static void AddButtonClickListener(Transform buttonTransform, UnityEngine.Events.UnityAction listener)
    {
        buttonTransform.GetComponent<Button>().onClick.AddListener(listener);
    }

    public static void RemoveAllButtonClickListeners(Transform buttonTransform)
    {
        buttonTransform.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public static void ChangeButtonText(Transform buttonTransform, string newText)
    {
        buttonTransform.Find("Text").GetComponent<Text>().text = newText;
    }

    public static Button CreateButton(GameObject prefab, Transform parent, string text, UnityAction listener)
    {
        GameObject button = GameObject.Instantiate(prefab, parent: parent);
        button.transform.Find("Text").GetComponent<Text>().text = text;
        button.GetComponent<Button>().onClick.AddListener(listener);
        return button.GetComponent<Button>();
    }

    public static string FormatNumberUruguay(double number, int decimalDigitsCount)
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("es-UY");
        string numberString = number.ToString("N" + decimalDigitsCount, culture);
        return numberString;
    }

    public static double UnformatNumberUruguay(string formattedNumber)
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("es-UY");

        try
        {
            return double.Parse(formattedNumber, culture);
        }
        catch (Exception e)
        {
            Debug.LogError("Error trying to unformat number: " + formattedNumber);
            throw e;
        }

    }

    public static string GetStringFromList<T>(List<T> aList)
    {
        string vStr = "[";
        for (int i = 0; i < aList.Count; i++)
        {
            vStr += aList[i].ToString();
            if (i < aList.Count - 1)
            {
                vStr += ",";
            }
        }

        vStr += "]";
        return vStr;
    }


    /**
     * Finds an active or inactive object
     */
    public static T FindObject<T>() where T : UnityEngine.Object
    {
        return Resources.FindObjectsOfTypeAll<T>()[0];
    }

    /**
     * Finds all active or inactive objects
     */
    public static List<T> FindObjects<T>() where T : UnityEngine.Object
    {
        return new List<T>(Resources.FindObjectsOfTypeAll<T>());
    }

    /// 
    /// <param name=fileName>Without extension</param>               
    /// 
    public static string GetTsvSpreadsheetDictionaryValue(string fileName, string key)
    {
        List<string> cells = GetTsvSpreadsheetCells(fileName);
        int keyCellIndex = cells.FindIndex(cell => cell == key);

        if (keyCellIndex == -1)
        {
            throw new UnityException("Couldn't find key " + key + " in spreadsheet with file name " + fileName);
        }

        return cells[keyCellIndex + 1];
    }

    public static List<string> SeparateParagraphs(string paragraphsString, string separator)
    {
        List<string> paragraphs = new List<string>(paragraphsString.Split(separator));
        paragraphs.RemoveAt(0);
        return paragraphs;
    }

    public static string ChangeStringCharacter(string originalString, int index, char newChar)
    {
        char[] originalStringCharArray = originalString.ToCharArray();
        originalStringCharArray[index] = newChar;
        return new string(originalStringCharArray);
    }

    public static bool IsInLayer(GameObject @object, string layerName)
    {
        return @object.layer == LayerMask.NameToLayer(layerName);
    }

    // Converts any analog vector into 8 fixed directions.
    // By ChatGPT
    public static Vector2 GetDpadDirection(Vector2 analogDirection)
    {
        // Dead zone
        if (analogDirection.sqrMagnitude < 0.0001f)
            return Vector2.zero;

        // Calculate angle in degrees (0� = right, CCW)
        float angle = Mathf.Atan2(analogDirection.y, analogDirection.x) * Mathf.Rad2Deg;

        // Normalize to 0�360
        if (angle < 0)
            angle += 360f;

        // Each direction occupies a 45� sector
        // We offset by 22.5� so boundaries feel more natural
        float sector = Mathf.Floor((angle + 22.5f) / 45f) % 8;

        switch ((int)sector)
        {
            case 0: return new Vector2(1f, 0f);    // Right
            case 1: return new Vector2(1f, 1f).normalized; // Up-Right
            case 2: return new Vector2(0f, 1f);    // Up
            case 3: return new Vector2(-1f, 1f).normalized; // Up-Left
            case 4: return new Vector2(-1f, 0f);    // Left
            case 5: return new Vector2(-1f, -1f).normalized; // Down-Left
            case 6: return new Vector2(0f, -1f);    // Down
            case 7: return new Vector2(1f, -1f).normalized; // Down-Right
        }

        return Vector2.zero; // fallback
    }

    public static IEnumerator TweenAmountCoroutine(float targetAmount, float time, Func<float> getter, Action<float> setter, UnityAction endListener = null)
    {
        float diff = targetAmount - getter(); // can be negative
        float absDiff = Mathf.Abs(diff);
        float amountPerSecond = diff / time;
        float absTarget = getter() + absDiff;
        float timeStep = (float)1 / (float)30;
        int steps = Mathf.RoundToInt(time / timeStep);

        //while (Math.Abs(getter()) < absTarget)

        for (int i=0; i<steps; i++)
        {
            Debug.Log("get: " + getter());
            Debug.Log("abs target: " + absTarget);
            float increase = amountPerSecond * timeStep; // can be negative
            setter(getter() + increase);
            yield return new WaitForSeconds(timeStep);
        }

        setter(targetAmount);

        endListener?.Invoke();
    }

    public static void SetParticleEmission(ParticleSystem particleSystem, bool enabled)
    {
        var emission = particleSystem.emission;
        emission.enabled = enabled;
    }

    public static T FindAnywhereUnder<T>(Transform transform) where T : Component
    {
        foreach (Transform child in transform)
        {
            // Check the child itself
            if (child.TryGetComponent(out T component))
            {
                return component;
            }

            // Recursively search descendants
            T foundInChildren = FindAnywhereUnder<T>(child);
            if (foundInChildren != null)
            {
                return foundInChildren;
            }
        }

        return null;
    }

    public static bool RemoveFromMatrix<T>(T[,] matrix, T item) where T : Component
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (ReferenceEquals(matrix[row, col], item))
                {
                    matrix[row, col] = null;
                    return true;
                }
            }
        }

        return false;
    }

    // Returns the projection of v1 onto v2, i.e. the component of v1 that points in the same direction as v2.
    public static float ProjectVector(Vector3 v1, Vector3 v2)
    {
        if (v2.sqrMagnitude <= 0.000001f)
        {
            return 0f;
        }

        return Vector3.Dot(v1, v2.normalized);
    }

    public static IEnumerator Wait(float time, bool ignoreTimeScale)
    {
        if (ignoreTimeScale)
        {
            yield return new WaitForSecondsRealtime(time);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
    }

    /*
    public static void ScaleJump(Transform transform, float maxScale, float duration, float stopTime = 0, bool ignoreTimeScale = true, UnityAction stopCallback = null)
    {
        Vector3 originalScale = transform.localScale;
        transform.LeanScale(to: Vector3.one * maxScale, time: duration / 2).setOnComplete(() =>
        {
            stopCallback?.Invoke();
            LeanTween.delayedCall(stopTime, () =>
            {
                transform.LeanScale(to: originalScale, duration / 2).setIgnoreTimeScale(ignoreTimeScale);
            }).setIgnoreTimeScale(ignoreTimeScale);
        }).setIgnoreTimeScale(ignoreTimeScale);
    }
    */
    

    /// <summary>
    /// Returns a list of all objects of type T in the hierarchy below transform
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static List<T> FindAllRecursive<T>(Transform transform)
    {
        var results = new List<T>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out T component))
            {
                results.Add(component);
            }
            results.AddRange(FindAllRecursive<T>(child));
        }
        return results;
    }
}

