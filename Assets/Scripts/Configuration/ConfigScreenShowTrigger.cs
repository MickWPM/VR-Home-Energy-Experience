using UnityEngine;

public class ConfigScreenShowTrigger : MonoBehaviour
{
    public ConfigManagerControl configScreenManager;
    private void OnTriggerEnter(Collider other)
    {
        configScreenManager.Show();
    }

    private void OnTriggerExit(Collider other)
    {
        configScreenManager.Hide();
    }
}
