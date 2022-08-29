using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoxelMonger
{
    public class Camera
    {
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix worldMatrix = Matrix.Identity;

        public Vector3 CameraPosition { get; set; }
        public Vector3 Target { get; set; }
        public Vector3 Up { get; set; }

        public Matrix ViewMatrix { get { return viewMatrix; } }
        public Matrix ProjectionMatrix { get { return projectionMatrix; } }
        public Matrix WorldMatrix { get { return worldMatrix; } }

        Vector3 facingVector = new Vector3();

        private GraphicsDevice device;
        float near;
        float far;

        public Camera(GraphicsDevice device, float near, float far)
        {
            CameraPosition = Vector3.Zero;
            Target = Vector3.Zero;
            Up = Vector3.Up;
            this.device = device;
            this.near = near;
            this.far = far;
            CreateView();
            
        }
        public void MoveCamera(Vector3 cameraPosition)
        {
            CameraPosition = cameraPosition;
            CreateView();
        }
        public void MoveRight(float r)
        {
          //  float test = 1.0f - Math.Abs(facingVector.X);
          //  float test2 = 1.0f - Math.Abs(facingVector.Y);
          //  CameraPosition = new Vector3(CameraPosition.X + test, CameraPosition.Y+test2, CameraPosition.Z);
          //  MoveCamera(CameraPosition);

        }
        public void MoveLeft(float l)
        {

        }
        public void Orbit(float angle)
        {
            CameraPosition = Vector3.Transform(CameraPosition - Target, Matrix.CreateRotationZ(angle)) + Target;
            CreateView();
        }
        public void LookAt(Vector3 target)
        {
            Target = target;
            facingVector = CameraPosition - target;
            facingVector.Normalize();

            CreateView();
        }
        private void CreateView()
        {
            viewMatrix = Matrix.CreateLookAt(
               CameraPosition,
               Target,
               Up);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                device.Viewport.AspectRatio,
                near,
                far);
        }
    }
}
