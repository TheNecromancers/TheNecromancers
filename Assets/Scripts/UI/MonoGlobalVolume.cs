using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class MonoGlobalVolume : MonoSingleton<MonoGlobalVolume>
{

    Volume GlobalVolume;
    
    public void ActivateBlur(bool _value)
    {
        GlobalVolume.profile.TryGet(out DepthOfField depthOfField);
        depthOfField.active = _value;
    }
}
