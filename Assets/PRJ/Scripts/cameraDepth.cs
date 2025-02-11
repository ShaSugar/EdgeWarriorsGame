using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ҪCamera���
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class cameraDepth : MonoBehaviour
{
    public Material cameraMaterial;
    void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        //����Camera��depthTextureMode,�������ͼ
        camera.depthTextureMode = DepthTextureMode.Depth;
    }

    //OnRenderImage�����е���Ⱦ��ɺ����
    //�ú����������Ǵ�����Ⱦ���ͼ������ԭͼ��source�������ͼ��desitination
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //���ָ����cameraMaterial  ����cameraMaterial���������ͼ�񣬷�����������ԭͼ���������
        if (cameraMaterial != null)
        {
            //�÷������ڽ������ͼ��ָ��material��shader pass �����
            Graphics.Blit(source, destination, cameraMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
