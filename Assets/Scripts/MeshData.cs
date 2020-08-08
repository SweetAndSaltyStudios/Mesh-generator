using System;
using UnityEngine;

public abstract class MeshData
{
    public Vector3[] Vertices
    {
        get;
        protected set;
    }
    public Vector3[] Normales
    {
        get;
        protected set;
    }
    public Vector2[] Uvs
    {
        get;
        protected set;
    }
    public int[] Triangles
    {
        get;
        protected set;
    }

    public abstract void ResetValues();
  
    public virtual void UpdateMeshData()
    {
        MeshGenerator.Instance.UpdateCreatedMesh(Vertices, Normales, Uvs, Triangles);
    }
}

[Serializable]
public class PlaneData : MeshData
{
    public float Lenght = 1f;
    public float Width = 1f;

    public override void ResetValues()
    {
        Lenght = 1f;
        Width = 1f;
    }

    public override void UpdateMeshData()
    {
        var resolutionX = 2; // 2 minimum
        var resolutionZ = 2;

        #region Vertices	

        Vertices = new Vector3[resolutionX * resolutionZ];

        for (int z = 0; z < resolutionZ; z++)
        {
            // [ -length / 2, length / 2 ]
            var zPos = ((float)z / (resolutionZ - 1) - 0.5f) * Lenght;

            for (int x = 0; x < resolutionX; x++)
            {
                // [ -width / 2, width / 2 ]
                var xPos = ((float)x / (resolutionX - 1) - .5f) * Width;
                Vertices[x + z * resolutionX] = new Vector3(xPos, 0f, zPos);
            }
        }

        #endregion

        #region Normales

        Normales = new Vector3[Vertices.Length];

        for (int n = 0; n < Normales.Length; n++)
            Normales[n] = Vector3.up;

        #endregion

        #region UVs		

        Uvs = new Vector2[Vertices.Length];

        for (int v = 0; v < resolutionZ; v++)
        {
            for (int u = 0; u < resolutionX; u++)
            {
                Uvs[u + v * resolutionX] = new Vector2((float)u / (resolutionX - 1), (float)v / (resolutionZ - 1));
            }
        }
        #endregion

        #region Triangles

        var facesNumber = (resolutionX - 1) * (resolutionZ - 1);

        Triangles = new int[facesNumber * 6];

        var t = 0;

        for (int face = 0; face < facesNumber; face++)
        {
            // Retrieve lower left corner from face ind
            int i = face % (resolutionX - 1) + (face / (resolutionZ - 1) * resolutionX);

            Triangles[t++] = i + resolutionX;
            Triangles[t++] = i + 1;
            Triangles[t++] = i;

            Triangles[t++] = i + resolutionX;
            Triangles[t++] = i + resolutionX + 1;
            Triangles[t++] = i + 1;
        }

        #endregion

        base.UpdateMeshData();
    }
}

[Serializable]
public class CubeData : MeshData
{
    public float Length = 1f;
    public float Width = 1f;
    public float Height = 1f;

    public override void ResetValues()
    {
        Length = Width = Height = 1f;
    }

    public override void UpdateMeshData()
    {
        #region Vertices

        var p0 = new Vector3(-Length * 0.5f, -Width * 0.5f,  Height * 0.5f);
        var p1 = new Vector3( Length * 0.5f, -Width * 0.5f,  Height * 0.5f);
        var p2 = new Vector3( Length * 0.5f, -Width * 0.5f, -Height * 0.5f);
        var p3 = new Vector3(-Length * 0.5f, -Width * 0.5f, -Height * 0.5f);

        var p4 = new Vector3(-Length * 0.5f, Width * 0.5f,  Height * 0.5f);
        var p5 = new Vector3( Length * 0.5f, Width * 0.5f,  Height * 0.5f);
        var p6 = new Vector3( Length * 0.5f, Width * 0.5f, -Height * 0.5f);
        var p7 = new Vector3(-Length * 0.5f, Width * 0.5f, -Height * 0.5f);

        Vertices = new Vector3[]
        {
	        // Bottom
	        p0, p1, p2, p3,
 
	        // Left
	        p7, p4, p0, p3,
 
	        // Front
	        p4, p5, p1, p0,
 
	        // Back
	        p6, p7, p3, p2,
 
	        // Right
	        p5, p6, p2, p1,
 
	        // Top
	        p7, p6, p5, p4
        };

        #endregion

        #region Normales

        var up = Vector3.up;
        var down = Vector3.down;
        var front = Vector3.forward;
        var back = Vector3.back;
        var left = Vector3.left;
        var right = Vector3.right;

        Normales = new Vector3[]
        {
	        // Bottom
	        down, down, down, down,
 
	        // Left
	        left, left, left, left,
 
	        // Front
	        front, front, front, front,
 
	        // Back
	        back, back, back, back,
 
	        // Right
	        right, right, right, right,
 
	        // Top
	        up, up, up, up
        };

        #endregion

        #region UVs

        var _00 = new Vector2(0f, 0f);
        var _10 = new Vector2(1f, 0f);
        var _01 = new Vector2(0f, 1f);
        var _11 = new Vector2(1f, 1f);

        Uvs = new Vector2[]
        {
	        // Bottom
	        _11, _01, _00, _10,
 
	        // Left
	        _11, _01, _00, _10,
 
	        // Front
	        _11, _01, _00, _10,
 
	        // Back
	        _11, _01, _00, _10,
 
	        // Right
	        _11, _01, _00, _10,
 
	        // Top
	        _11, _01, _00, _10,
        };
        #endregion

        #region Triangles

        Triangles = new int[]
        {
	        // Bottom
	        3, 1, 0,
            3, 2, 1,			
 
	        // Left
	        3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
            3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
 
	        // Front
	        3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
            3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
 
	        // Back
	        3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
            3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
 
	        // Right
	        3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
            3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
 
	        // Top
	        3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
            3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
        };

        #endregion

        base.UpdateMeshData();
    }
}

[Serializable]
public class SphereData : MeshData
{
    public float Radius = 1f;
    public int Longitude = 24;
    public int Latitude = 16;

    public override void ResetValues()
    {
        Radius = 1f;
        Longitude = 24;
        Latitude = 16;
    }

    public override void UpdateMeshData()
    {
        #region Vertices

        Vertices = new Vector3[(Longitude + 1) * Latitude + 2];
        var pi = Mathf.PI;
        var pi2 = pi * 2f;

        Vertices[0] = Vector3.up * Radius;

        for (int lat = 0; lat < Latitude; lat++)
        {
            var a1 = pi * (lat + 1) / (Latitude + 1);
            var sin1 = Mathf.Sin(a1);
            var cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= Longitude; lon++)
            {
                var a2 = pi2 * (lon == Longitude ? 0 : lon) / Longitude;
                var sin2 = Mathf.Sin(a2);
                var cos2 = Mathf.Cos(a2);

                Vertices[lon + lat * (Longitude + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * Radius;
            }
        }

        Vertices[Vertices.Length - 1] = Vector3.up * -Radius;

        #endregion

        #region Normales	

        Normales = new Vector3[Vertices.Length];

        for (int n = 0; n < Vertices.Length; n++)
            Normales[n] = Vertices[n].normalized;

        #endregion

        #region UVs

        Uvs = new Vector2[Vertices.Length];

        Uvs[0] = Vector2.up;
        Uvs[Uvs.Length - 1] = Vector2.zero;
        for (int lat = 0; lat < Latitude; lat++)
            for (int lon = 0; lon <= Longitude; lon++)
                Uvs[lon + lat * (Longitude + 1) + 1] = new Vector2((float)lon / Longitude, 1f - (float)(lat + 1) / (Latitude + 1));

        #endregion

        #region Triangles

        var numberOfFaces = Vertices.Length;
        var numberOfTriangles = numberOfFaces * 2;
        var indexes = numberOfTriangles * 3;

        Triangles = new int[indexes];

        //Top Cap
        var i = 0;
        for (int lon = 0; lon < Longitude; lon++)
        {
            Triangles[i++] = lon + 2;
            Triangles[i++] = lon + 1;
            Triangles[i++] = 0;
        }

        //Middle
        for (int lat = 0; lat < Latitude - 1; lat++)
        {
            for (int lon = 0; lon < Longitude; lon++)
            {
                int current = lon + lat * (Longitude + 1) + 1;
                int next = current + Longitude + 1;

                Triangles[i++] = current;
                Triangles[i++] = current + 1;
                Triangles[i++] = next + 1;

                Triangles[i++] = current;
                Triangles[i++] = next + 1;
                Triangles[i++] = next;
            }
        }

        //Bottom Cap
        for (int lon = 0; lon < Longitude; lon++)
        {
            Triangles[i++] = Vertices.Length - 1;
            Triangles[i++] = Vertices.Length - (lon + 2) - 1;
            Triangles[i++] = Vertices.Length - (lon + 1) - 1;
        }

        #endregion

        base.UpdateMeshData();
    }
}

[Serializable]
public class ConeData : MeshData
{
    public float Height = 1f;
    public float BottomRadius = 0.25f;
    public float TopRadius = 0.05f;
    public int NumberOfSides = 18;

    public override void ResetValues()
    {
        Height = 1f;
        BottomRadius = 0.25f;
        TopRadius = 0.05f;
        NumberOfSides = 18;
    }

    public override void UpdateMeshData()
    {
        var numberOfVerticesCap = NumberOfSides + 1;

        #region Vertices

        // bottom + top + sides
        Vertices = new Vector3[numberOfVerticesCap + numberOfVerticesCap + NumberOfSides * 2 + 2];

        var vert = 0;
        var pi2 = Mathf.PI * 2f;

        // Bottom cap
        Vertices[vert++] = new Vector3(0f, 0f, 0f);
        while (vert <= NumberOfSides)
        {
            float rad = (float)vert / NumberOfSides * pi2;
            Vertices[vert] = new Vector3(Mathf.Cos(rad) * BottomRadius, 0f, Mathf.Sin(rad) * BottomRadius);
            vert++;
        }

        // Top cap
        Vertices[vert++] = new Vector3(0f, Height, 0f);
        while (vert <= NumberOfSides * 2 + 1)
        {
            float rad = (float)(vert - NumberOfSides - 1) / NumberOfSides * pi2;
            Vertices[vert] = new Vector3(Mathf.Cos(rad) * TopRadius, Height, Mathf.Sin(rad) * TopRadius);
            vert++;
        }

        // Sides
        var v = 0;
        while (vert <= Vertices.Length - 4)
        {
            float rad = (float)v / NumberOfSides * pi2;
            Vertices[vert] = new Vector3(Mathf.Cos(rad) * TopRadius, Height, Mathf.Sin(rad) * TopRadius);
            Vertices[vert + 1] = new Vector3(Mathf.Cos(rad) * BottomRadius, 0, Mathf.Sin(rad) * BottomRadius);
            vert += 2;
            v++;
        }
        Vertices[vert] = Vertices[NumberOfSides * 2 + 2];
        Vertices[vert + 1] = Vertices[NumberOfSides * 2 + 3];

        #endregion

        #region Normales

        // bottom + top + sides
        Normales = new Vector3[Vertices.Length];
        vert = 0;

        // Bottom cap
        while (vert <= NumberOfSides)
        {
            Normales[vert++] = Vector3.down;
        }

        // Top cap
        while (vert <= NumberOfSides * 2 + 1)
        {
            Normales[vert++] = Vector3.up;
        }

        // Sides
        v = 0;
        while (vert <= Vertices.Length - 4)
        {
            float rad = (float)v / NumberOfSides * pi2;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);

            Normales[vert] = new Vector3(cos, 0f, sin);
            Normales[vert + 1] = Normales[vert];

            vert += 2;
            v++;
        }
        Normales[vert] = Normales[NumberOfSides * 2 + 2];
        Normales[vert + 1] = Normales[NumberOfSides * 2 + 3];

        #endregion

        #region UVs

        Uvs = new Vector2[Vertices.Length];

        // Bottom cap
        var u = 0;
        Uvs[u++] = new Vector2(0.5f, 0.5f);
        while (u <= NumberOfSides)
        {
            float rad = (float)u / NumberOfSides * pi2;
            Uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
            u++;
        }

        // Top cap
        Uvs[u++] = new Vector2(0.5f, 0.5f);
        while (u <= NumberOfSides * 2 + 1)
        {
            float rad = (float)u / NumberOfSides * pi2;
            Uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
            u++;
        }

        // Sides
        var u_sides = 0;

        while (u <= Uvs.Length - 4)
        {
            float t = (float)u_sides / NumberOfSides;
            Uvs[u] = new Vector3(t, 1f);
            Uvs[u + 1] = new Vector3(t, 0f);
            u += 2;
            u_sides++;
        }

        Uvs[u] = new Vector2(1f, 1f);
        Uvs[u + 1] = new Vector2(1f, 0f);

        #endregion

        #region Triangles

        var numberOfTriangles = NumberOfSides + NumberOfSides + NumberOfSides * 2;
        Triangles = new int[numberOfTriangles * 3 + 3];

        // Bottom cap
        var tri = 0;
        var i = 0;

        while (tri < NumberOfSides - 1)
        {
            Triangles[i] = 0;
            Triangles[i + 1] = tri + 1;
            Triangles[i + 2] = tri + 2;
            tri++;
            i += 3;
        }

        Triangles[i] = 0;
        Triangles[i + 1] = tri + 1;
        Triangles[i + 2] = 1;
        tri++;
        i += 3;

        // Top cap
        //tri++;
        while (tri < NumberOfSides * 2)
        {
            Triangles[i] = tri + 2;
            Triangles[i + 1] = tri + 1;
            Triangles[i + 2] = numberOfVerticesCap;
            tri++;
            i += 3;
        }

        Triangles[i] = numberOfVerticesCap + 1;
        Triangles[i + 1] = tri + 1;
        Triangles[i + 2] = numberOfVerticesCap;
        tri++;
        i += 3;
        tri++;

        // Sides
        while (tri <= numberOfTriangles)
        {
            Triangles[i] = tri + 2;
            Triangles[i + 1] = tri + 1;
            Triangles[i + 2] = tri + 0;
            tri++;
            i += 3;

            Triangles[i] = tri + 1;
            Triangles[i + 1] = tri + 2;
            Triangles[i + 2] = tri + 0;
            tri++;
            i += 3;
        }

        #endregion

        base.UpdateMeshData();
    }
}
