using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SelectAllAndTrackNew
{
    private const string PrefKey = "SelectAllAndTrackNew_Enabled";
    private const string MenuItemPath = "Tools/Select All And Track New";

    private static HashSet<int> knownInstanceIDs = new HashSet<int>();

    private static bool IsEnabled
    {
        get => EditorPrefs.GetBool(PrefKey, true);
        set => EditorPrefs.SetBool(PrefKey, value);
    }

    static SelectAllAndTrackNew()
    {
        EditorApplication.delayCall += Initialize;
    }

    private static void Initialize()
    {
        RefreshKnownObjects();

        if (IsEnabled)
            SelectAllInHierarchy();

        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private static void OnHierarchyChanged()
    {
        var allObjects = GetAllSceneGameObjects();
        var newObjects = allObjects.Where(go => !knownInstanceIDs.Contains(go.GetInstanceID())).ToList();

        if (IsEnabled && newObjects.Count > 0)
        {
            var updatedSelection = new List<Object>(Selection.objects);
            updatedSelection.AddRange(newObjects.Cast<Object>());
            Selection.objects = updatedSelection.ToArray();
        }

        RefreshKnownObjects();
    }

    private static void RefreshKnownObjects()
    {
        knownInstanceIDs = new HashSet<int>(GetAllSceneGameObjects().Select(go => go.GetInstanceID()));
    }

    private static GameObject[] GetAllSceneGameObjects()
    {
        return Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    [MenuItem(MenuItemPath)]
    private static void ToggleEnabled()
    {
        IsEnabled = !IsEnabled;
        Menu.SetChecked(MenuItemPath, IsEnabled);
    }

    [MenuItem(MenuItemPath, true)]
    private static bool ToggleEnabledValidate()
    {
        Menu.SetChecked(MenuItemPath, IsEnabled);
        return true;
    }

    [MenuItem("Edit/Select All And Track New/Select All Now %#A")]
    private static void SelectAllInHierarchy()
    {
        Selection.objects = GetAllSceneGameObjects();
    }
}