using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //originn + (travel * parallax)
    // Start is called before the first frame update

    public Camera cam;

    [SerializeField]
    private Transform player;

    private Vector2 startPos;

    private float startZ;
    private float startY;
    private float disFromSub => transform.position.z - player.position.z;
    //clipping plane
    private float clippingPlanne => (cam.transform.position.z + (disFromSub > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //Lambda Expression
    private Vector2 travel => (Vector2)cam.transform.position - startPos;
    private float parallaxFactor => Mathf.Abs(disFromSub) / clippingPlanne;

    private Vector2 newPos;

    /*infinite
    private Sprite sprite;
    private Texture2D texture;
    private float textureUniteSizeX;
    private float offsetPositionX;
    */
    void Start()
    {
        startPos = transform.position;
        startZ = transform.position.z;
        startY = transform.position.y;

        //infinite background
        //sprite = GetComponent<SpriteRenderer>().sprite;
        //texture = sprite.texture;
        //textureUniteSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        newPos = startPos + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, startY, startZ);
    }
    /*
    private void LateUpdate()
    {
        if(Mathf.Abs(cam.transform.position.x - transform.position.x) >= textureUniteSizeX)
        {
            offsetPositionX = (cam.transform.position.x - transform.position.x) % textureUniteSizeX;
            transform.position = new Vector3(cam.transform.position.x + offsetPositionX, transform.position.y);
        }
    }
    */
}
