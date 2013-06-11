using UnityEngine;
using System.Collections;

/// <summary>
/// WIP - Trying to make camera viewport fix on the position of the game area rectangule...
/// </summary>
public class FixCameraViewPortOnPanel : MonoBehaviour
{
    public Camera Camera;

    private GameObject panel;

    void Awake()
    {
        //panel = GameObject.FindGameObjectWithTag(Tag.GameAreaCamera);
    }

	void Start() {
	
	}

    void Update()
    {
        //float targetaspect = 16.0f / 9.0f;
        //
        //// determine the game window's current aspect ratio
        //float windowaspect = (float)Screen.width / (float)Screen.height;
        //
        //// current viewport height should be scaled by this amount
        //float scaleheight = windowaspect / targetaspect;
        //
        //Rect rect = camera.rect;
        //
        //rect.width = 0.45f;
        //rect.height = 0.59f;
        //rect.x = 0.015f * scaleheight;
        //rect.y = 0.205f;
        //
        //camera.rect = rect;
        //Camera.p = new Rect(1, panel.transform.position.y, 10, 10);
    }

    void SetVanishingPoint(Camera cam, Vector2 perspectiveOffset) {
	    var m = cam.projectionMatrix;
	    var w = 2*cam.nearClipPlane / m.m00;
	    var h = 2*cam.nearClipPlane / m.m11;
 
	    var left = -w/2 - perspectiveOffset.x;
	    var right = left+w;
	    var bottom = -h/2 - perspectiveOffset.y;
	    var top = bottom+h;
 
	    cam.projectionMatrix = PerspectiveOffCenter(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
    }
 
    static Matrix4x4 PerspectiveOffCenter(
	    float left, float right,
	    float bottom, float top,
	    float near, float far)
    {
	    float x =  (2.0f * near)		/ (right - left);
	    float y =  (2.0f * near)		/ (top - bottom);
	    float a =  (right + left)		/ (right - left);
	    float b =  (top + bottom)		/ (top - bottom);
	    float c = -(far + near)		    / (far - near);
	    float d = -(2.0f * far * near)  / (far - near);
	    float e = -1.0f;

        Matrix4x4 m = new Matrix4x4();
	    m[0,0] =    x;  m[0,1] = 0.0f;  m[0,2] = a;   m[0,3] = 0.0f;
	    m[1,0] = 0.0f;  m[1,1] =    y;  m[1,2] = b;   m[1,3] = 0.0f;
	    m[2,0] = 0.0f;  m[2,1] = 0.0f;  m[2,2] = c;   m[2,3] =    d;
	    m[3,0] = 0.0f;  m[3,1] = 0.0f;  m[3,2] = e;   m[3,3] = 0.0f;
	    return m;
    }
}