using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        //Window Data
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
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
        }

        //Called every frame; update logic here
        private void Update(object sender, object param)
        {

        }

        //Called every frame; its only purpose is to draw
        private void Draw(object sender,object param)
        {
            //Wait for all OpenGL operations to complete
            GL.Finish();
        }

        //Called on a window resize
        private void Resize(object sender, object param)
        {

        }
    }
}
