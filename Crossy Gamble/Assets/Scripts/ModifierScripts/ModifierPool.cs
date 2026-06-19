using System.Collections.Generic;
using UnityEngine;
using static ModifierData;

public class ModifierPool : MonoBehaviour
{
    public List<ModifierData> availableModifiers = new();

    public ModifierData GetRandomModifier()
    {
        int index = Random.Range(0, availableModifiers.Count);

        ModifierData selected = availableModifiers[index];

        if (selected.modifierType == ModifierType.SingleUse)
        {
            availableModifiers.RemoveAt(index);
        }

        return selected;
    }
}