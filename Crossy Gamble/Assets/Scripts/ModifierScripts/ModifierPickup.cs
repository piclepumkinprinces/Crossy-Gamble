using UnityEngine;

public class ModifierPickup : MonoBehaviour
{
    public ModifierData modifier;

    private void OnTriggerEnter(Collider other)
    {
        print("Collided");
        if (!other.CompareTag("Player"))
        {
            print("compared");
            return;
        }


        ModifierManager manager =
            other.GetComponent<ModifierManager>();

        if (manager == null)
        {
            print("managernull");
            return;
        }


        manager.AddModifier(modifier);

        Destroy(gameObject);
    }
}