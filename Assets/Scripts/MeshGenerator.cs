using UnityEditor;
using UnityEngine;

public class MeshGenerator : Singelton<MeshGenerator>
{
    public MESH_TYPE MeshType { get; set; }
    public PlaneData PlaneData { get; set; }
    public CubeData CubeData { get; set; }
    public SphereData SphereData{ get; set; }
    public ConeData ConeData { get; set; }

    public Transform MeshGraphics;
    public Transform MeshParent;
    public Mesh Mesh;

    public bool IsEditorPlaying
    {
        get
        {
            return EditorApplication.isPlaying;
        }
    }

    [Header("Mesh Visuals")]
    public Material Material;
    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;

    [Header("Variables")]
    public float MoveSpeed = 1f;
    public float RotationSpeed = 1f;
    public float CameraScrollSpeed = 1f;

    [Header("Roation Input")]
    public KeyCode UpRotationKey = KeyCode.Q;
    public KeyCode DownRotationKey = KeyCode.E;
    public KeyCode LeftRotationKey = KeyCode.Z;
    public KeyCode RightRotationKey = KeyCode.C;
    public KeyCode ResetRotationKey = KeyCode.Space;


    private void Update()
    {
        if (MeshParent == null)
            return;

        RotateCreatedObject();
    }

    private void OnValidate()
    {
        //if (IsEditorPlaying)
        //{
        //    return;
        //}

        if(Mesh == null)
        {
            Initialize();
        }

        UpdateMesh(MeshType);
    }

    private void Initialize()
    {
        //var meshInstance = Instantiate(MeshFilter.sharedMesh) as Mesh;
        Mesh = MeshFilter.mesh = /*meshInstance*/ new Mesh();
    }

    private void RotateCreatedObject()
    {
        if (Input.GetKey(UpRotationKey))
        {
            MeshParent.Rotate(Vector3.up * RotationSpeed);
        }

        if (Input.GetKey(DownRotationKey))
        {
            MeshParent.Rotate(Vector3.down * RotationSpeed);
        }

        if (Input.GetKey(LeftRotationKey))
        {
            MeshParent.Rotate(Vector3.left * RotationSpeed);
        }

        if (Input.GetKey(RightRotationKey))
        {
            MeshParent.Rotate(Vector3.right * RotationSpeed);
        }

        if (Input.GetKeyDown(ResetRotationKey))
        {
            MeshParent.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        }
    }

    public void UpdateMesh(MESH_TYPE newMeshType)
    {
        MeshType = newMeshType;

        switch (MeshType)
        {
            case MESH_TYPE.PLANE:

                PlaneData.UpdateMeshData();

                break;

            case MESH_TYPE.CUBE:

                CubeData.UpdateMeshData();

                break;

            case MESH_TYPE.SPHERE:

                SphereData.UpdateMeshData();

                break;

            case MESH_TYPE.CONE:

                ConeData.UpdateMeshData();

                break;

            default:


                break;

        }
    }

    public void UpdateCreatedMesh(Vector3[] vertices, Vector3[] normales, Vector2[] uvs, int[] triangles)
    {
        if (Mesh == null)
        {
            return;
        }

        Mesh.Clear();
        Mesh.vertices = vertices;
        Mesh.normals = normales;
        Mesh.uv = uvs;
        Mesh.triangles = triangles;

        Mesh.RecalculateBounds();
    }
}
