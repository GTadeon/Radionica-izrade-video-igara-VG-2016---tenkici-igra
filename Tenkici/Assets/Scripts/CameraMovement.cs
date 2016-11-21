using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float smoothY;
	public float smoothX;

	public float odmakX;
	public float odmakY;

    public float constrainXmax;
    public float constrainXmin;
    public float constrainYmax;
    public float constrainYmin;


    public GameObject player;

	void FixedUpdate()
	{
		Vector2 V = this.gameObject.GetComponent<Rigidbody2D> ().velocity;
		float posx = Mathf.SmoothDamp (transform.position.x, Mathf.Clamp (player.transform.position.x, constrainXmin,constrainXmax), ref V.x, smoothX);
		float posy = Mathf.SmoothDamp (transform.position.y, Mathf.Clamp(player.transform.position.y,constrainYmin,constrainYmax), ref V.y, smoothY);

		transform.position = new Vector3 (posx+odmakX,posy+odmakY,transform.position.z);
	}
}
