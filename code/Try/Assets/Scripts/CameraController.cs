using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    //初始观察距离
    public float Distance = 1F;
    //旋转速度
    public float SpeedX = 240;
    public float SpeedY = 120;
    //角度限制
    public float MinLimitY = 5;
    public float MaxLimitY = 180;

    //旋转角度
    private float mX = 0.0F;
    private float mY = 0.0F;

    //鼠标缩放距离最值
    public float MaxDistance = 3;
    public float MinDistance = 0.2F;
    //鼠标缩放速率
    private float ZoomSpeed = 2F;
    //速度
    public float Damping = 10F;

    private Quaternion mRotation;
    private List<GameObject> collideredObjects;//本次射线hit到的GameObject
    private List<GameObject> bufferOfCollideredObjects;//上次射线hit到的GameObject
    void Start()
    {
        mX = transform.eulerAngles.x;
        mY = transform.eulerAngles.y;
        collideredObjects = new List<GameObject>();
        bufferOfCollideredObjects = new List<GameObject>();
    }


    void LateUpdate()
    {
        CameraSet();//相机设置
        //定义一条射线
        /*
        RaycastHit hit;
        if (Physics.Linecast(player.transform.position, this.transform.position, out hit))
        {
            GameObject item = hit.collider.gameObject;
            string name = item.name;
            if (name != "Main Camera")
            {
                //如果射线碰撞的不是相机，那么就取得射线碰撞点到玩家的距离
                float currentDistance = Vector3.Distance(hit.point, player.transform.position);
                //如果射线碰撞点小于玩家与相机本来的距离，就说明角色身后是有东西，为了避免穿墙，就把相机拉近
                
                if (currentDistance < m_distanceAway)
                {
                    this.transform.position = hit.point;
                }
            }
        }*/


        bufferOfCollideredObjects.Clear();
        for (int temp = 0; temp < collideredObjects.Count; temp++)
        {
            bufferOfCollideredObjects.Add(collideredObjects[temp]);//得到上次的
        }
        collideredObjects.Clear();

        //发射射线
        Vector3 dir = -(player.transform.position - transform.position).normalized;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.transform.position, dir, Vector3.Distance(player.transform.position, transform.position));

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name != "Plane" && hits[i].collider.gameObject.name != "Cube")
            {
                collideredObjects.Add(hits[i].collider.gameObject);//得到现在的
            }
        }
        //把上次的还原，这次的透明
        for (int i = 0; i < bufferOfCollideredObjects.Count; i++)
        {
            SetMaterialsColor(bufferOfCollideredObjects[i].GetComponent<Renderer>(), false);
        }
        for (int i = 0; i < collideredObjects.Count; i++)
        {
            SetMaterialsColor(collideredObjects[i].GetComponent<Renderer>(), true);
        }
    }

    //是否搞透明
    void SetMaterialsColor(Renderer r, bool isClear)
    {
        if (isClear)
        {
            int materialsNumber = r.sharedMaterials.Length;
            for (int i = 0; i < materialsNumber; i++)
            {
                r.materials[i].shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = r.materials[i].color;
                tempColor.a = 0.4f;
                r.materials[i].color = tempColor;

            }
        }
        else
        {
            int materialsNumber = r.sharedMaterials.Length;
            for (int i = 0; i < materialsNumber; i++)
            {
                r.materials[i].shader = Shader.Find("Legacy Shaders/Diffuse");
                Color tempColor = r.materials[i].color;
                tempColor.a = 1f;
                r.materials[i].color = tempColor;

            }
        }

    }

    void CameraSet()
    {
        mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
        mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;
        //范围限制
        mY = ClampAngle(mY, MinLimitY, MaxLimitY);
        //计算旋转
        //我们可以通过Quaternion.Euler()方法将一个Vector3类型的值转化为一个四元数, 进而通过修改Transform.Rotation来实现相同的目的
        mRotation = Quaternion.Euler(mY, mX, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, mRotation, Time.deltaTime * Damping);

        //鼠标滚轮缩放
        Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);

        //重新计算位置
        Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + player.transform.position;

        transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * Damping);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}