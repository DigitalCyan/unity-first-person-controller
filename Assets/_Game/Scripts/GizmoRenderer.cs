using UnityEngine;

public class GizmoRenderer : MonoBehaviour
{
    [SerializeField]
    private Mesh gizmoMesh;

    private void OnDrawGizmos(){
        Gizmos.DrawMesh(gizmoMesh, 0, transform.position + Vector3.up * 1);
    }
}
