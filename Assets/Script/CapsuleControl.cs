using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour
{
    public Trash.Type type;
    struct InsideObj
    {
        public Transform _transform;
        public Vector3 _locate;
    }
    private List<InsideObj> objList = new();
    [SerializeField]
    private float changeTimeout = 1.0f;
    [SerializeField]
    private float changeTimeoutDelta = 0.0f;
    [SerializeField]
    private PointHandler pointHandler;
    [SerializeField]
    private Material[] materialList;

    private void Awake()
    {
        pointHandler = FindAnyObjectByType<PointHandler>();
        UpdateMaterial((int)type);
    }

    public void OnTriggerEnter(Collider other)
    {
        var p = other.transform.parent;
        if (p != null && p.gameObject.CompareTag("trash"))
        {
            p.GetComponent<Rigidbody>().useGravity = false;
            p.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p.GetComponentInChildren<Collider>().enabled = false;
            p.GetComponentInChildren<Trash>().enabled = false;

            var trash = new InsideObj();
            trash._transform = p;
            var circle = Random.insideUnitCircle * 0.27f;
            trash._locate = transform.position + new Vector3(circle.x, 0, circle.y);
            objList.Add(trash);

            if (p.GetComponent<Trash>()._type == type)
            {
                pointHandler.UpdatePoint(p.GetComponent<Trash>()._point);
            }
            else
            {
                StartCoroutine(DisableCapsuleIn(5.0f));
            }    

        }
    }

    public void Update()
    {
        if (objList.Count > 0)
        {
            if (objList.Count > 7) DecomposedFirst();

            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i]._transform == null)
                {
                    objList.RemoveAt(i);
                    continue;
                }
                objList[i]._transform.position =
                    Vector3.Slerp(objList[i]._transform.position, objList[i]._locate, 2.0f * Time.deltaTime);
            }

            changeTimeoutDelta -= Time.deltaTime;
            if (changeTimeoutDelta < 0.0f)
            {
                changeTimeoutDelta = changeTimeout;
                for (int i = 0; i < objList.Count; i++)
                {
                    var newLocate = new Vector3(objList[i]._locate.x,
                        transform.position.y + Random.Range(-0.7f, 0.7f), objList[i]._locate.z);
                    var obj = new InsideObj();
                    obj._transform = objList[i]._transform;
                    obj._locate = newLocate;
                    objList[i] = obj;
                }
            }
        }
    }

    public void SetUpCapsuleType(Trash.Type type)
    {
        this.type = type;
        UpdateMaterial((int)type);
    }    

    /// <summary>
    /// Cập nhật Material của capsule
    /// </summary>
    private void UpdateMaterial(int id)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.GetComponent<Renderer>().material = materialList[id];
        }
    }

    /// <summary>
    /// Dừng capsule trong một khoảng thời gian
    /// </summary>
    private IEnumerator DisableCapsuleIn(float minute)
    {
        BreakCapsule();
        yield return new WaitForSeconds(minute);
        RestoreCapsule();
    }

    /// <summary>
    /// Khôi phục nhưng chưa cập nhật màu
    /// </summary>
    private void RestoreCapsule()
    {
        transform.GetComponent<Collider>().enabled = true;
        UpdateMaterial((int)type);
        //Hiệu ứng khôi phục
    }

    /// <summary>
    /// Tạo hiệu ứng nổ và tạm dừng capsule
    /// </summary>
    private void BreakCapsule()
    {
        transform.GetComponent<Collider>().enabled = false;
        DestroyAll();
        UpdateMaterial(6);
        //Hiệu ứng nổ
    }

    public void HardDisableCapsule()
    {
        StopAllCoroutines();
        BreakCapsule();
    }
    public void HardEnableCapsule()
    {
        StopAllCoroutines();
        RestoreCapsule();
    }

    /// <summary>
    /// Hủy tất cả object bên trong capsule
    /// </summary>
    private void DestroyAll()
    {
        while (objList.Count > 0)
        {
            var trash = objList[0]._transform;
            objList.RemoveAt(0);
            Destroy(trash.gameObject);
        }
    }

    /// <summary>
    /// Hiệu ứng tiêu hóa 1 object 
    /// </summary>
    private void DecomposedFirst()
    {
        var trash = objList[0]._transform;
        objList.RemoveAt(0);
        Destroy(trash.gameObject);

        //Hiệu ứng bùm chíu
    }
}
