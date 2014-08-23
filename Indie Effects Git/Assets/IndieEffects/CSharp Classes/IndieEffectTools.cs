using UnityEngine;
using System.Collections;

public static class IndieEffectTools
{
    public static readonly Vector3 VEC3_ZERO = Vector3.zero;
    public static readonly Vector3 VEC3_UP = Vector3.up;
    public static readonly Vector3 VEC3_RIGHT_UP = Vector3.right + Vector3.up;
    public static readonly Vector3 VEC3_RIGHT = Vector3.right;

    public static void FullScreenQuad(Material renderMat)
    {
        FullScreenQuad(renderMat, renderMat.passCount, 0f, 1f);
    }

    public static void FullScreenQuad(Material renderMat, int pass)
    {
        FullScreenQuad(renderMat, pass);
    }

    public static void FullScreenQuad(Material renderMat, float min, float max)
    {
        FullScreenQuad(renderMat, renderMat.passCount, min, max);
    }

    public static void FullScreenQuad(Material renderMat, int pass, float min, float max)
    {
        GL.PushMatrix();

        for (var i = 0; i < pass; ++i)
        {
            renderMat.SetPass(i);

            GL.LoadOrtho();

            GL.Begin(GL.QUADS);
            {
                GL.Color(Color.white);

                GL.MultiTexCoord(0, VEC3_ZERO);
                GL.Vertex3(-min, -min, 0);

                GL.MultiTexCoord(0, VEC3_UP);
                GL.Vertex3(-min, max, 0);

                GL.MultiTexCoord(0, VEC3_RIGHT_UP);
                GL.Vertex3(max, max, 0);

                GL.MultiTexCoord(0, VEC3_RIGHT);
                GL.Vertex3(max, -min, 0);
            }
            GL.End();
        }

        GL.PopMatrix();
    }


    public static Texture2D ScreenGrab(Texture2D rt, Rect camRect)
    {
        float asp = Camera.current.pixelWidth / Camera.current.pixelHeight;

        GameObject dom = new GameObject("capture", typeof(Camera));
        dom.camera.aspect = asp;
        dom.camera.pixelRect = camRect;
        dom.transform.position = Camera.current.transform.position;
        dom.transform.rotation = Camera.current.transform.rotation;
        dom.camera.Render();

        rt.ReadPixels(camRect, 0, 0);
        rt.Apply();

        GameObject.Destroy(dom);

        return rt;
    }
}
