using UnityEngine;

public class ShieldToggle : MonoBehaviour
{
    public GameObject shieldObject;
    private bool isActive = false;

    void Start()
    {
        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isActive = !isActive;
            if (shieldObject != null)
                shieldObject.SetActive(isActive);
        }
    }
}
