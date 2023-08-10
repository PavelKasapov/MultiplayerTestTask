using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CoinPatternCollector : MonoBehaviour
{
 #if UNITY_EDITOR
    const string coinPatternsFolderPath = "Assets/CoinsPatterns";
    const string nameSample = "coinPattern";
    [SerializeField] bool showButton;
    private void OnGUI()
    {
        if (showButton && GUI.Button(new Rect(0, 0, 200, 40), "CollectPattern"))
        {
            CollectPattern();
        }
    }

    void CollectPattern()
    {
        var newCoinPositions = new List<Vector3>();
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "Coin")
            {
                newCoinPositions.Add(child.position);
            }
        }
        var coinPattern = ScriptableObject.CreateInstance<CoinPattern>();
        coinPattern.CoinPositions = newCoinPositions.ToArray();

        var assetGuids = AssetDatabase.FindAssets("coinPattern?", new[] { coinPatternsFolderPath });
        int lastPathId = 0;
        if (assetGuids.Length > 0 )
        {
            var lastPath = AssetDatabase.GUIDToAssetPath(assetGuids[assetGuids.Length - 1]);
            lastPathId = int.Parse(lastPath.Substring(coinPatternsFolderPath.Length + 1 + nameSample.Length, 1));
        }

        AssetDatabase.CreateAsset(coinPattern, $"{coinPatternsFolderPath}/{nameSample}{lastPathId + 1}.asset");
        AssetDatabase.SaveAssets();
    }
#endif
}
