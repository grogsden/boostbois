using UnityEngine;

public class CollisionHandler : MonoBehaviour
{  
    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You collided with Friendly");
                break;
            case "Finish":
                Debug.Log("You won");
                break;
            case "Fuel":
                Debug.Log("You hit some fuel");
                break;
            default:
                Debug.Log("You ded");
                break;
        }
    }
}
