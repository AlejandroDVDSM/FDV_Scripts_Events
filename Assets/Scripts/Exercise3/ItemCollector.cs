using System;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public static event Action OnItemCollected;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag.Equals("Item"))
        {
            OnItemCollected?.Invoke();
            Debug.Log("[!] ITEM COLLECTED [!]");
        }
    }
}
