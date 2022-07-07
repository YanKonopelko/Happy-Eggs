using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject lineCreator;
    GameObject currentCreator;
    public static bool onAllowedPosition = false;
    void Update()
    {
        Debug.Log(InkManager.inkAmount);
        if (Input.GetMouseButtonDown(0) && InkManager.inkAmount > 0 && onAllowedPosition)
        {
            CreateLineCreator(InkManager.currentColor);
        }
    }

    private void CreateLineCreator(int collor)
    {
        currentCreator = Instantiate(lineCreator);
        currentCreator.GetComponent<DrowLine>().startPos = GetWorldPosition(Input.mousePosition);
    }
    public static Vector2 GetWorldPosition(Vector3 mousePosition)
    {
        Vector2 mouseCoor = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mouseCoor);
    }
}
