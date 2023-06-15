
using UnityEngine;

public class Land : MonoBehaviour
{
    public bool isDestoyed = true;
    // Start is called before the first frame update

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
