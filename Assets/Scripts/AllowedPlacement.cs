using UnityEngine;

public class AllowedPlacement : MonoBehaviour
{
    private void OnMouseEnter()
    {
        InputManager.onAllowedPosition = true;
    }
    private void OnMouseExit()
    {
        InputManager.onAllowedPosition = false;
    }
}
