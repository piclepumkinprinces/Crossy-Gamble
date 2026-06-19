using System.Collections.Generic;
using UnityEngine;
using static ModifierData;

public class ModifierManager : MonoBehaviour
{
    public List<ModifierData> activeModifiers = new();

    public void AddModifier(ModifierData modifier)
    {
        if (modifier.modifierType == ModifierType.SingleUse)
        {
            bool alreadyOwned =
                activeModifiers.Contains(modifier);

            if (alreadyOwned)
                return;
        }



        activeModifiers.Add(modifier);

        Debug.Log("Added modifier: " + modifier.modifierName);
    }
}
