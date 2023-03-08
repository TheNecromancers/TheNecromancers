using UnityEngine;

public class FacePlayerLight : MonoBehaviour
{

    [SerializeField] Transform target;


    void Update()
    {
            if(target != null)
       {
            transform.LookAt(target);
       }
    }
}
