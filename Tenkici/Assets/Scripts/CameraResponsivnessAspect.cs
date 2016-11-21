using UnityEngine;
 using System.Collections;
 public class CameraResponsivnessAspect : MonoBehaviour {

	

	//aspect ratio igre:
	public float targetRatio = 16f/10f; 
	void Start()
	{
		Camera cam = GetComponent<Camera>();
		cam.aspect = targetRatio;
	} 
 }