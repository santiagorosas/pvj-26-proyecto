using System;
using System.Collections.Generic;
using System.Linq;

public class CollectionSerializer
{
    // Serializes a dictionary<int, int> into a string
    public static string SerializeDictionary(Dictionary<int, int> dict)
    {
        return string.Join(";", dict.Select(kv => $"{kv.Key}:{kv.Value}"));
    }

    // Deserializes a string back into a dictionary<int, int>
    public static Dictionary<int, int> DeserializeDictionary(string data)
    {
        var dict = new Dictionary<int, int>();

        if (string.IsNullOrWhiteSpace(data))
            return dict;

        foreach (var pair in data.Split(';'))
        {
            var parts = pair.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[0], out int key) && int.TryParse(parts[1], out int value))
            {
                dict[key] = value;
            }
        }

        return dict;
    }

    // Serializes a List<List<int>> into a string
    public static string SerializeMatrix(List<List<int>> matrix)
    {
        //return string.Join(";", matrix.Select(innerList => string.Join(",", innerList)));

        List<string> rows = new List<string>();

        foreach (var row in matrix)
        {
            List<string> rowValues = new List<string>();
            foreach (var value in row)
            {
                rowValues.Add(value.ToString());
            }
            rows.Add(string.Join(",", rowValues));
        }

        return string.Join(";", rows);
    }

    // Deserializes a string back into a List<List<int>>
    public static List<List<int>> DeserializeMatrix(string data)
    {
        var matrix = new List<List<int>>();

        if (string.IsNullOrWhiteSpace(data))
            return matrix;

        foreach (var innerList in data.Split(';'))
        {
            matrix.Add(innerList.Split(',').Select(int.Parse).ToList());
        }

        return matrix;
    }
}
