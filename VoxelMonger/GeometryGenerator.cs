using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using System;

namespace VoxelMonger
{
    
    class GeometryGenerator
    {
        public class MeshData
        {
            public List<VertexPositionNormalTexture> Vertices = new List<VertexPositionNormalTexture>();
            public List<short> Indices = new List<short>();

        }

        public MeshData CreateBox(float width, float height, float depth)
        {
            MeshData meshData = new MeshData();
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[24];
            float w2 = 0.5f * width;
            float h2 = 0.5f * height;
            float d2 = 0.5f * depth;

            //front face
            vertices[0] = new VertexPositionNormalTexture(new Vector3(-w2, -h2, -d2), new Vector3(0, 0, -1), new Vector2(0, 1));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(-w2, +h2, -d2), new Vector3(0, 0, -1), new Vector2(0, 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(+w2, +h2, -d2), new Vector3(0, 0, -1), new Vector2(-1, 0));
            vertices[3] = new VertexPositionNormalTexture(new Vector3(+w2, -h2, -d2), new Vector3(0, 0, -1), new Vector2(-1, 1));

            //back face
            vertices[4] = new VertexPositionNormalTexture(new Vector3(-w2, -h2, +d2), new Vector3(0, 0, 1), new Vector2(-1, 1));
            vertices[5] = new VertexPositionNormalTexture(new Vector3(+w2, -h2, +d2), new Vector3(0, 0, 1), new Vector2(0, 1));
            vertices[6] = new VertexPositionNormalTexture(new Vector3(+w2, +h2, +d2), new Vector3(0, 0, 1), new Vector2(0, 0));
            vertices[7] = new VertexPositionNormalTexture(new Vector3(-w2, +h2, +d2), new Vector3(0, 0, 1), new Vector2(-1, 0));

            //top face
            vertices[8] = new VertexPositionNormalTexture(new Vector3(-w2, +h2, -d2), new Vector3(0, 1, 0), new Vector2(0, 1));
            vertices[9] = new VertexPositionNormalTexture(new Vector3(-w2, +h2, +d2), new Vector3(0, 1, 0), new Vector2(0, 0));
            vertices[10] = new VertexPositionNormalTexture(new Vector3(+w2, +h2, +d2), new Vector3(0, 1, 0), new Vector2(-1, 0));
            vertices[11] = new VertexPositionNormalTexture(new Vector3(+w2, +h2, -d2), new Vector3(0, 1, 0), new Vector2(-1, 1));

            //bottom face
            vertices[12] = new VertexPositionNormalTexture(new Vector3(-w2, -h2, -d2), new Vector3(0, -1, 0), new Vector2(-1, 1));
            vertices[13] = new VertexPositionNormalTexture(new Vector3(+w2, -h2, -d2), new Vector3(0, -1, 0), new Vector2(0, 1));
            vertices[14] = new VertexPositionNormalTexture(new Vector3(+w2, -h2, +d2), new Vector3(0, -1, 0), new Vector2(0, 0));
            vertices[15] = new VertexPositionNormalTexture(new Vector3(-w2, -h2, +d2), new Vector3(0, -1, 0), new Vector2(-1, 0));

            //left face
            vertices[16] = new VertexPositionNormalTexture(new Vector3(-w2, -h2, +d2), new Vector3(-1, 0, 0), new Vector2(0, 1));
            vertices[17] = new VertexPositionNormalTexture(new Vector3(-w2, +h2, +d2), new Vector3(-1, 0, 0), new Vector2(0, 0));
            vertices[18] = new VertexPositionNormalTexture(new Vector3(-w2, +h2, -d2), new Vector3(-1, 0, 0), new Vector2(-1, 0));
            vertices[19] = new VertexPositionNormalTexture(new Vector3(-w2, -h2, -d2), new Vector3(-1, 0, 0), new Vector2(-1, 1));

            //right face
            vertices[20] = new VertexPositionNormalTexture(new Vector3(+w2, -h2, -d2), new Vector3(1, 0, 0), new Vector2(0, 1));
            vertices[21] = new VertexPositionNormalTexture(new Vector3(+w2, +h2, -d2), new Vector3(1, 0, 0), new Vector2(0, 0));
            vertices[22] = new VertexPositionNormalTexture(new Vector3(+w2, +h2, +d2), new Vector3(1, 0, 0), new Vector2(-1, 0));
            vertices[23] = new VertexPositionNormalTexture(new Vector3(+w2, -h2, +d2), new Vector3(1, 0, 0), new Vector2(-1, 1));

            meshData.Vertices.AddRange(vertices);

            short[] i = new short[36];
            //front face
            i[0] = 0; i[1] = 1; i[2] = 2;
            i[3] = 0; i[3] = 2; i[5] = 3;
            //back face
            i[6] = 4; i[7] = 5; i[8] = 6;
            i[9] = 4; i[10] = 6; i[11] = 7;
            // top face 
            i[12] = 8; i[13] = 9; i[14] = 10;
            i[15] = 8; i[16] = 10; i[17] = 11;
            //  bottom face 
            i[18] = 12; i[19] = 13; i[20] = 14;
            i[21] = 12; i[22] = 14; i[23] = 15;
            // left face 
            i[24] = 16; i[25] = 17; i[26] = 18;
            i[27] = 16; i[28] = 18; i[29] = 19;
            // right face
            i[30] = 20; i[31] = 21; i[32] = 22;
            i[33] = 20; i[34] = 22; i[35] = 23;


            meshData.Indices.AddRange(i);

            return meshData;

        }
        public MeshData CreateSpehere(float radius, int sliceCount, int stackCount)
        {
            MeshData meshData = new MeshData();

            VertexPositionNormalTexture topVertex = new VertexPositionNormalTexture(
                new Vector3(0, +radius, 0), new Vector3(0, 1, 0), new Vector2(0, 0));
            VertexPositionNormalTexture bottomVertex = new VertexPositionNormalTexture(
                new Vector3(0, -radius, 0), new Vector3(0, -1, 0), new Vector2(0, 1));

            meshData.Vertices.Add(topVertex);

            float phiStep = MathHelper.Pi / stackCount;
            float thetaStep = 2.0f * MathHelper.Pi / sliceCount;

            for(int i = 1; i <= stackCount-1; ++i)
            {
                float phi = i * phiStep;
                for (int j = 0; j <= sliceCount; ++j)
                {
                    float theta = j * thetaStep;
                    //spherical cartesion
                    VertexPositionNormalTexture vertex = new VertexPositionNormalTexture();
                    vertex.Position.X = radius * (float)Math.Sin(phi) * (float)Math.Cos(theta);
                    vertex.Position.Y = radius * (float)Math.Cos(phi);
                    vertex.Position.Z = radius * (float)Math.Sin(phi) * (float)Math.Sin(theta);

                    vertex.Normal = vertex.Position;
                    vertex.Normal.Normalize();

                    vertex.TextureCoordinate.X = -(theta / MathHelper.TwoPi);
                    vertex.TextureCoordinate.Y = (phi / MathHelper.Pi);

                    meshData.Vertices.Add(vertex);
                }
            }
            meshData.Vertices.Add(bottomVertex);

            //indices for top stack.
            for(int i = 1; i <= sliceCount; ++i)
            {
                meshData.Indices.Add(0);
                meshData.Indices.Add((short)(i + 1));
                meshData.Indices.Add((short)i);
            }

            //indices for innter stack
            int baseIndex = 1;
            int ringVertextCount = sliceCount + 1;
            for(int i = 0; i < stackCount-2; ++i)
            {
                for (int j = 0; j < sliceCount; ++j)
                {
                    meshData.Indices.Add((short)(baseIndex + i * ringVertextCount + j));
                    meshData.Indices.Add((short)(baseIndex + i * ringVertextCount + j + 1));
                    meshData.Indices.Add((short)(baseIndex + (i+1) * ringVertextCount + j));

                    meshData.Indices.Add((short)(baseIndex + (i+1) * ringVertextCount + j));
                    meshData.Indices.Add((short)(baseIndex + i * ringVertextCount + j + 1));
                    meshData.Indices.Add((short)(baseIndex + (i+1) * ringVertextCount + j + 1));
                }
            }

            //indices for bottom stack
            int southPoleIndex = meshData.Vertices.Count - 1;
            baseIndex = southPoleIndex - ringVertextCount;
            for (int i = 0; i < sliceCount; ++i)
            {
                meshData.Indices.Add((short)southPoleIndex);
                meshData.Indices.Add((short)(baseIndex + i));
                meshData.Indices.Add((short)(baseIndex + i + 1));
            }

            return meshData;
        }
        public MeshData CreateGrid(float width, float depth, int m, int n)
        {
            MeshData meshData= new MeshData();
            VertexPositionNormalTexture[] vertices;
            int vertexCount = m * n;
            int faceCount = (m - 1) * (n - 1) * 2;

            //create vertices
            float halfWidth = 0.5f * width;
            float halfdepth = 0.5f * depth;

            float dx = width / (n - 1);
            float dz = depth / (m - 1);
            float du = 1.0f / (n - 1);
            float dv = 1.0f / (m - 1);

            vertices = new VertexPositionNormalTexture[vertexCount];
            for(int i = 0; i < m; ++i)
            {
                float z = halfdepth - i * dz;
                for(int j = 0; j < n; ++j)
                {
                    float x = -halfWidth + j * dx;
                    vertices[i * n + j].Position = new Vector3(x, 0, z);
                    vertices[i * n + j].Normal = new Vector3(0, 1, 0);
                    vertices[i * n + j].TextureCoordinate.X = -(j * du);
                    vertices[i * n + j].TextureCoordinate.Y = i * dv;
                }
            }
            meshData.Vertices.AddRange(vertices);

            short[] indices = new short[faceCount * 3];
            int k = 0;
            for(int i = 0; i < m-1; ++i)
            {
                for(int j = 0; j < n-1; ++j)
                {
                    indices[k] = (short)(i * n + j);
                    indices[k + 1] = (short)(i * n + j + 1);
                    indices[k + 2] = (short)((i + 1) * n + j);
                    indices[k + 3] = (short)((i + 1) * n + j);
                    indices[k + 4] = (short)(i * n + j + 1);
                    indices[k + 5] = (short)((i + 1) * n + j + 1);

                    //next quad
                    k += 6;
                 }
            }
            meshData.Indices.AddRange(indices);

            return meshData;
        }
    }
}
