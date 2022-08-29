using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//using System.Drawing;

namespace VoxelMonger
{
    enum Shape
    {
        Sphere,
        Box,
        Grid
    }



    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        SkinnedEffect ef;
        int frame = 0;
        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;
    
        //float zangle = 0.0f;
        Camera camera;
        Vector3 cameraPosition = new Vector3(0, -0, 100);
        float yangle = 0;

        //Model earthModel;
        //Model rockModel;

        //Random rand = new Random();

        GeometryGenerator geometry = new GeometryGenerator();

        //Texture2D tex;
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        //Vector3 rockPosition = new Vector3(6, 0, 100);
        //Vector3 rockTrajectory = new Vector3(0.5f, 0, -0.1f);
        //float velocity = 1.0f;

        //Vector3 earthPosition = new Vector3(0, 0, 0);

        float displayWidth = 0;
        float displayHeight = 0;

        Vector2 ScrollOffsets = new Vector2(0, 0);

        Model faerieModel;
        Model Sphere;
        Model forestHexTile;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            device = _graphics.GraphicsDevice;
            displayWidth = device.Viewport.Width;
            displayHeight = device.Viewport.Height;

           // _graphics.PreferredBackBufferHeight = 512;
           // _graphics.PreferredBackBufferWidth = 640;
            // _graphics.IsFullScreen = true;
            //_graphics.SynchronizeWithVerticalRetrace = false;

            _graphics.ApplyChanges();

            IsFixedTimeStep = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(device);

            // TODO: use this.Content to load your game content here
            camera = new Camera(device, 1.0f, 1000.0f);            
            camera.MoveCamera(new Vector3(-5, 5, 5));
            camera.LookAt(new Vector3(0, 0, 0));

            faerieModel = Content.Load<Model>("Objects/castle");
            Sphere = CreateModel(Shape.Sphere, "earth");
            forestHexTile = Content.Load<Model>("Tiles/Hex/hex_forest");

            viewMatrix = camera.ViewMatrix;
            projectionMatrix = camera.ProjectionMatrix;

            ef = new SkinnedEffect(device);
           
        }

        

        private Model CreateModel(Shape shape, string texture)
        {
            GeometryGenerator.MeshData meshData = new GeometryGenerator.MeshData();
            
            switch (shape)
            {
                case Shape.Sphere:
                    meshData = geometry.CreateSpehere(1, 40, 40);
                    break;
                case Shape.Box:
                    meshData = geometry.CreateBox(1, 1, 1);
                    break;
                case Shape.Grid:
                    meshData = geometry.CreateGrid(1, 1, 2, 2);
                    break;
            }

            ModelMeshPart meshPart = new ModelMeshPart();
            meshPart.IndexBuffer = new IndexBuffer(device, IndexElementSize.SixteenBits, meshData.Indices.Count, BufferUsage.None);
            meshPart.IndexBuffer.SetData(meshData.Indices.ToArray());
            meshPart.VertexBuffer = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration, meshData.Vertices.Count, BufferUsage.None);
            meshPart.VertexBuffer.SetData(meshData.Vertices.ToArray());
            meshPart.NumVertices = meshData.Vertices.Count;
            meshPart.PrimitiveCount = meshData.Indices.Count / 3;
            meshPart.StartIndex = 0;
            meshPart.VertexOffset = 0;

            BasicEffect specialEffect = new BasicEffect(device);
            List<ModelMeshPart> parts = new List<ModelMeshPart>();
            List<ModelMesh> meshes = new List<ModelMesh>();
            List<ModelBone> bones = new List<ModelBone>();
            ModelBone bone = new ModelBone();

            parts.Add(meshPart);
            meshes.Add(new ModelMesh(device, parts));
            bone.AddMesh(meshes[0]);
            bone.Transform = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            bone.ModelTransform = Matrix.CreateTranslation(0, 0, 0) * Matrix.CreateRotationX(MathHelper.ToRadians(0));
            meshes[0].ParentBone = bone;
            bones.Add(bone);

            Model geoModel = new Model(device, bones, meshes);

            geoModel.Root = bone;

            Texture2D tex = Content.Load<Texture2D>(texture);

            specialEffect.EnableDefaultLighting();
            specialEffect.TextureEnabled = true;
            specialEffect.Texture = tex;
            specialEffect.SpecularPower = 64;
            specialEffect.DirectionalLight0.Enabled = true;
            specialEffect.DirectionalLight0.SpecularColor = new Vector3(0, 0, 0);
            specialEffect.DirectionalLight0.Direction = new Vector3(-1, 0, -1);
            specialEffect.DirectionalLight1.Enabled = false;
            specialEffect.DirectionalLight2.Enabled = false;
            specialEffect.AmbientLightColor = new Vector3(0, 0, 0);

            meshPart.Effect = specialEffect;

            return geoModel;
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
            }

            if(Keyboard.GetState().IsKeyUp(Keys.Up))
            {

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {

            }


            if (((int)gameTime.TotalGameTime.Ticks & 0x7) == 0x7)
                frame ^= 1;

            yangle += 0.01f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.CullClockwiseFace;
            rs.CullMode = CullMode.None;
            rs.FillMode = FillMode.Solid;
            GraphicsDevice.RasterizerState = rs;
            device.BlendState = BlendState.AlphaBlend;


            //worldMatrix = earthModel.Root.ModelTransform *Matrix.CreateScale(5,5,5)* Matrix.CreateFromYawPitchRoll(zangle, 0, 0) * Matrix.CreateTranslation(earthPosition);
            //earthModel.Draw(worldMatrix, camera.ViewMatrix, camera.ProjectionMatrix);

            //worldMatrix = rockModel.Root.ModelTransform * Matrix.CreateScale(1, 1, 1) * Matrix.CreateTranslation(rockPosition);
            //rockModel.Draw(worldMatrix, camera.ViewMatrix, camera.ProjectionMatrix);
            //worldMatrix = faerieModel.Root.ModelTransform * Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateTranslation(0, 0, 0);
          
            worldMatrix = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateTranslation(0, -0.9f, 0) *Matrix.CreateRotationY(MathHelper.ToRadians(0));
            foreach(ModelMesh mesh in faerieModel.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                }
            }
            faerieModel.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(0, -1, 0) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            foreach (ModelMesh mesh in forestHexTile.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                }
            }
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(1, -1, 1.75f) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(-1, -1, 1.75f) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(-2, -1, 0) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(2, -1, 0) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(1, -1, -1.75f) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            worldMatrix = Matrix.CreateScale(1f, 0.1f, 1f) * Matrix.CreateTranslation(-1, -1, -1.75f) * Matrix.CreateRotationY(MathHelper.ToRadians(0));
            forestHexTile.Draw(worldMatrix, viewMatrix, projectionMatrix);
            // Sphere.Draw(worldMatrix, camera.ViewMatrix, camera.ProjectionMatrix);


            base.Draw(gameTime);
        }

        public int[] ReadHeightMap(string fileName)
        {
            System.Drawing.Bitmap bm = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(fileName);
            List<int> map = new List<int>();
            for(int y = 128; y < 256; y++)
            {
                for(int x = 0; x < 128; x++)
                {
                    int height = bm.GetPixel(x, y).R / 16;
                    map.Add(height);
                }
            }
            return map.ToArray<int>();
        }

        public static Ray GetMouseRay(Vector2 mousePosition, Viewport viewport, Matrix ProjectionMatrix, Matrix ViewMatrix)
        {
            Vector3 nearPoint = new Vector3(mousePosition, 0);
            Vector3 farPoint = new Vector3(mousePosition, 1);

            nearPoint = viewport.Unproject(nearPoint, ProjectionMatrix, ViewMatrix, Matrix.Identity);
            farPoint = viewport.Unproject(farPoint, ProjectionMatrix, ViewMatrix, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }
    }
}
