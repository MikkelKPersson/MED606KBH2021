using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class AutomaticDOF : MonoBehaviour
{
    DepthOfField dofComponent;
    public Transform tracker;
    public Transform cam;
    
    void Start()
    {
        Volume volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            dofComponent = tmp;
        }
    }

    void Update()
    {
        float focusDist = (tracker.localPosition.z + cam.localPosition.z);
        dofComponent.focusDistance.value = focusDist;
    }
}
