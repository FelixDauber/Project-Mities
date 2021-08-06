using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEditor;

public class SceneCameraDataDESTROYER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [ContextMenu("Destroy the fucker")]
    public void DestroyCamera()
    {
        DestroyImmediate(SceneView.currentDrawingSceneView.camera.gameObject);
    }
}
