using UnityEngine;
using UnityEngine.Rendering;

public class PipelineQualityControl : MonoBehaviour
{
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }    
}
