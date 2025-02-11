using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//需要Camera组件
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class cameraDepth : MonoBehaviour
{
    public Material cameraMaterial;
    void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        //设置Camera的depthTextureMode,生成深度图
        camera.depthTextureMode = DepthTextureMode.Depth;
    }

    //OnRenderImage在所有的渲染完成后调用
    //该函数允许我们处理渲染后的图像，输入原图像source，输出的图像desitination
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //如果指定了cameraMaterial  就用cameraMaterial处理输出的图像，否则就用输入的原图像用作输出
        if (cameraMaterial != null)
        {
            //该方法用于将输入的图像指定material和shader pass 后输出
            Graphics.Blit(source, destination, cameraMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
