using UnityEngine;

public class SetObjectEnabled : MonoBehaviour
{
    public GameObject toggleObject;

    [ContextMenu("ToggleObject")]
    public void ToggleObject()
    {
        toggleObject.SetActive(!toggleObject.activeSelf);
    }
}
