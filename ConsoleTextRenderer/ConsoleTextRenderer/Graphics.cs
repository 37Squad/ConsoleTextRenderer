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
        public Graphics(int x,int y, int width, int height)
        {
            //Create new GameWindow object with specified width and height
            this.window             = new GameWindow(width, height);
            //Set window position on screen
            this.window.Location    = new Point(x, y);
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

        }

        //Called every frame; update logic here
        private void Update(object sender, object param)
        {

        }

        //Called every frame; its only purpose is to draw
        private void Draw(object sender,object param)
        {

        }

        //Called on a window resize
        private void Resize(object sender, object param)
        {

        }
    }
}
