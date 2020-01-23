using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Extencions  {

    public static void Polling(this List<GameObject> list, GameObject prefab, int amount, Transform parent = null) {
        for(int i = 0; i < amount ; i++) {
            var tempPrefab = GameObject.Instantiate(prefab);
            tempPrefab.transform.SetParent(parent);
            tempPrefab.SetActive(false);
            list.Add(tempPrefab);
        }
    }

    public static GameObject GetPolling(this List<GameObject> list, Vector3 position, Quaternion rotation) {
        var tempPrefab = list.Where(x => !x.activeSelf).First();
        tempPrefab.transform.position = position;
        tempPrefab.transform.rotation = rotation;
        tempPrefab.SetActive(true);
        return tempPrefab;
    }
    
}