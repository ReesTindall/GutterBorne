using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RatHealthExt
{
    public static void OnDestroyCall(this RatHealth h, System.Action cb)
    {
        if (!h) return;
        h.gameObject.AddComponent<DestroyRelay>().callback = cb;
    }
}
public class DestroyRelay : MonoBehaviour
{
    public System.Action callback;
    void OnDestroy() => callback?.Invoke();
}
