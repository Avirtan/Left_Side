using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCountPlayer : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int count;
    IEnumerator Start()
    {
        var x = -5f;
        var z = 0f;
        for (int i = 0; i < count; i++)
        {
            z += 0.2f;
            if (x > 5) x = -5f;
            x += 0.5f;
            Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity);
            yield return null;
        }
    }
}
