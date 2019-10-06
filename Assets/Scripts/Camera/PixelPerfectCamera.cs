// Reference : https://blogs.unity3d.com/2015/06/19/pixel-perfect-2d/
// Orthographic size = ((Vertical Resolution)/(PPUScale * PPU)) * 0.5

using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof (Camera))]
public class PixelPerfectCamera : MonoBehaviour 
{
	public Vector2 referenceResolution = new Vector2(800, 600);
	public float pixelPerUnit = 1f;
	public bool keepVerticalSizeRatio = true;
	
	void Awake () 
	{
		SetOrthographicSize();
	}
	
	private void SetOrthographicSize() 
	{
		Camera camera = GetComponent<Camera>();
		if (camera.orthographic) 
		{
			float scale = keepVerticalSizeRatio ? Screen.height / referenceResolution.y : 1f;
			camera.orthographicSize = (Screen.height / 2) / (pixelPerUnit * scale);
		}
	}
}