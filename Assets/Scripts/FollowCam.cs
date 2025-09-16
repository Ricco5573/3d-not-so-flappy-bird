using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.GraphicsBuffer;

public class FollowCam : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField]
    private Transform followObject;


    private void Start()
    {
        offset = followObject.position - this.transform.position;
    }
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, followObject.position - offset, 0.19f);

    }


}
