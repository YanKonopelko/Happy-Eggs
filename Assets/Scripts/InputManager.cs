using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject lineCreator;
    static public float inkAmount = 100;
    private int currentColor = 0;
    static public float currentPrice = 10;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inkAmount > 0)
        {
            CreateLineCreator(currentColor);
        }
    }

    private void CreateLineCreator(int collor)
    {
        var obj = Instantiate(lineCreator);
        obj.GetComponent<DrowLine>().startPos = GetWorldPosition(Input.mousePosition);
    }
    public static Vector2 GetWorldPosition(Vector3 mousePosition)
    {
        Vector2 mouseCoor = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mouseCoor);
    }
}
