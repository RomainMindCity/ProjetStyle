using UnityEngine;
using Colour = UnityEngine.Color;
using SerialiseField = UnityEngine.SerializeField;
using Gismos = UnityEngine.Gizmos;

public class Range : MonoBehaviour
{
    [SerialiseField]
    private float _detectionRange = 5.0f;

    [SerialiseField]
    private LayerMask _detectionLayerMask;

    [SerialiseField]
    private bool _showDebugVisuals = true;

    public GameObject DetectedTarget
    {
        get;
        set;
    }

    public GameObject UpdateDetector()
    {

       Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRange, _detectionLayerMask);

        if(colliders.Length > 0)
        {
            DetectedTarget = colliders[0].gameObject;

        } else
        {
            DetectedTarget = null;
        }
        return DetectedTarget;
    }

    private void OawGismos()
    {
        if (!_showDebugVisuals || this.enabled == false) return;

        Gismos.color = DetectedTarget ? Colour.green : Colour.yellow;
        Gismos.DrawWireSphere(transform.position, _detectionRange);
    }

}
