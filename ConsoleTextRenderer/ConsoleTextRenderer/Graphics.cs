using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

//OpenTK includes ; for everything we could ever want!
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Math;
using OpenTK.Input;

//Class for controlling graphics in all of its forms
namespace ConsoleTextRenderer
{
    class Graphics
    {
        //OpenTK handle object
        private GameWindow window = null;
        //GlyphManager
        private GlyphManager glyphManager;
        //RenderQueue
        private RenderQueue renderQueue;

       
        public Graphics(int x,int y, int width, int height,int refreshRate)
        {
            //Create new GameWindow object with specified width and height
            this.window             = new GameWindow(width, height);
            //Set window position on screen
            this.window.Location    = new Point(x, y);
            //Set refresh rate (Hz)
            this.window.TargetUpdateFrequency = refreshRate;
            //Create references to this classes methods
            //Function pointers!
            //I'll haunt you with pointers even in C#
            this.window.Load        += this.Load;
            this.window.UpdateFrame += this.Update;
            this.window.RenderFrame += this.Draw;
            this.window.Resize      += this.Resize;

            this.glyphManager = new GlyphManager(10, 40);
            this.renderQueue = new RenderQueue();
            this.renderQueue.AddEntity(this.glyphManager);

            //Start it up!
            this.window.Run(refreshRate);
        }

        //Called on window creation
        private void Load(object sender, object param)
        {
            //Load basic OpenGL state
            //Create an orthographic projection
            GL.MatrixMode(MatrixMode.Projection);
            /*
            Window Coordinate System
            (0.0D,0.0D)------------(1.0D,0.0D)
            |                                |
            |                                |
            |                                |
            (0.0D,1.0D)------------(1.0D,1.0D)
            */
            GL.Ortho(0.0D, 1.0D, 1.0D, 0.0D, 0.0D, 1.0D);

            //Turn on 2D texture capability
            GL.Enable(EnableCap.Texture2D);
            //Enable texture blending
            GL.Enable(EnableCap.Blend);
            //Use this blending function
            //It does not modify the output value in any way
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //Load our bitmap
            Bitmap glyphMap = new Bitmap("alphabet_production.bmp");
            BitmapData bmp_data = glyphMap.LockBits(new Rectangle(0, 0, glyphMap.Width, glyphMap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
           
            //OpenGL texture
            int id = GL.GenTexture();
            //Bind
            GL.BindTexture(TextureTarget.Texture2D, id);
            //Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            //Generate texture
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);
            //Unlock our bitmap
            glyphMap.UnlockBits(bmp_data);
            //Clear our Screen
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);
        }

        //Called every frame; update logic here
        private void Update(object sender, object param)
        {
            var keyboardState = OpenTK.Input.Keyboard.GetState();
            if (keyboardState[Key.Escape]) this.window.Close();
            if (keyboardState[Key.A])
            {
                this.glyphManager.WriteGlyph(Glyph.GLYPH_a);
            }
        }

        //Called every frame; its only purpose is to draw
        private void Draw(object sender,object param)
        {
            this.renderQueue.RenderAll();
            //Wait for all OpenGL operations to complete
            GL.Finish();
            //Now swap buffers
            this.window.SwapBuffers();
        }

        //Called on a window resize
        private void Resize(object sender, object param)
        {

        }
    }
}
