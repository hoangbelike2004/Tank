using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CacheObject
{
    private static Dictionary<Collider,Character> cacheColliders = new Dictionary<Collider,Character>();

    private static Dictionary<Character,Character> cacheCharacters = new Dictionary<Character,Character>();

    public static Character GetCharacterFromCollider(Collider collider)
    {
        if (!cacheColliders.ContainsKey(collider))
        {
            cacheColliders.Add(collider, collider.GetComponent<Character>());
        }
        return cacheColliders[collider];
    }

    public static Character GetCharacter(Character cha)
    {
        if (!cacheCharacters.ContainsKey(cha))
        {
            cacheCharacters.Add(cha, cha);
        }
        return cacheCharacters[cha];
    }
}
