using UnityEngine;

public enum ModifierType
{
    Reusable,
    SingleUse
}

public enum ModifierEffect
{
    PrintReusable,
    PrintSingleUse
}

[CreateAssetMenu(menuName = "Modifiers/Modifier")]
public class ModifierData : ScriptableObject
{
    public string modifierName;
    public ModifierType modifierType;
    public ModifierEffect effect;

    public void Apply()
    {
        switch (effect)
        {
            case ModifierEffect.PrintReusable:
                Debug.Log("Reusable modifier activated!");
                break;

            case ModifierEffect.PrintSingleUse:
                Debug.Log("Single-use modifier activated!");
                break;
        }
    }
}