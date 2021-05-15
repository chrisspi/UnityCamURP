//Copyright (c) 2016  MHD Yamen Saraiji



using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Camera))]
public class UnityCam : MonoBehaviour
{

    internal const string DllName = "UnityWebcam";

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private System.IntPtr CreateTextureWrapper();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void DeleteTextureWrapper(System.IntPtr w);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private bool SendTexture(System.IntPtr w, System.IntPtr textureID);


    System.IntPtr _instance;

    public bool Flip = false;

    TextureWrapper _wrapper;

    OffscreenProcessor _BlitterProcessor;

    private RenderTexture source;
    private int WIDTH = 1280;
    private int HEIGHT = 720;


    void Start()
    {
        //Init UnityWebCamera plugin
        _instance = CreateTextureWrapper();

        _BlitterProcessor = new OffscreenProcessor("UnityCam/Image/Blitter");

        _wrapper = new TextureWrapper();

        source = new RenderTexture(WIDTH, HEIGHT, 24);
        gameObject.GetComponent<Camera>().targetTexture = source;
    }

    private void Update()
    {
        RenderImage(source);
    }

    public void RenderImage(RenderTexture source)
    {
        Texture tex = source;

        if (Flip)
            tex = _BlitterProcessor.ProcessTexture(tex, 0);
        else
            tex = _BlitterProcessor.ProcessTexture(tex, 1);

        _wrapper.ConvertTexture(tex);
        tex = _wrapper.WrappedTexture;

        //Send the rendered image to the plugin 
        SendTexture(_instance, tex.GetNativeTexturePtr());
    }

    void OnDestroy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            DestroyImmediate(obj);
        }

        gameObject.GetComponent<Camera>().targetTexture = null;
        source.Release();
    }
}