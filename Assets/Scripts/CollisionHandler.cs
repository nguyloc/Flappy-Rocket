using System;
using UnityEngine;
    
public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision detected.");
                break;
            
            case "Finish":
                Debug.Log("Finish collision detected.");
                break;
            
            case "Fuel":
                Debug.Log("Fuel collision detected.");
                break;
            
            default:
                Debug.Log("Unknown collision detected.");
                break;
        }
    }
}
