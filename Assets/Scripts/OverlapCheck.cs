using UnityEngine;

public sealed class OverlapCheck : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Mesh" && other.gameObject.name != "RoomManager" && other.gameObject.name != "CorridorGenerator")
            Destroy(other.gameObject);
    }
}
