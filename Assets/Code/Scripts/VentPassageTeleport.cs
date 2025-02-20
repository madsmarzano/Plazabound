using UnityEngine;

/// <summary>
/// By Mads:
/// 
/// Attached to vent passage entrance/exits (the small vent passages used to travel between rooms).
/// When player collides with the vent, it triggers a teleportation into the passage (and vice versa).
/// Trigger will only work if player is bug-sized.
/// 
/// </summary>

public class VentPassageTeleport : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //check if the player is bug-size 
            //if yes, proceed with teleportation
            ChangeScale sizeCheck = other.GetComponent<ChangeScale>();
            if (sizeCheck.currentSize == ChangeScale.Size.BUG)
            {
                other.transform.position = teleportPoint.position;
            }
            Player player = other.GetComponent<Player>();

            // If player is ENTERING the vent passage, change isInVent to TRUE
            // If player is EXITING the vent passage, change isInVent to FALSE
            if (!player.isInVent)
            {
                player.isInVent = true;
            }
            else
            {
                player.isInVent = false;
            }
        }
    }
}
