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

//Our own namespacesa
using ConsoleTextRenderer.Systems;
using ConsoleTextRenderer.Render;

//Class for controlling graphics in all of its forms
namespace ConsoleTextRenderer
{
    class ConsoleTextRenderer
    {
        //OpenTK handle object
        private GameWindow window = null;
        //GlyphManager
        private GlyphManager glyphManager = null;
        //RenderQueue
        private RenderQueue renderQueue = null;
        //Render Engine
        private RenderEngine renderEngine = null;
        //Marker
        private Marker marker = null;
       
        public ConsoleTextRenderer(int x,int y, int width, int height,int refreshRate)
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
            this.window.KeyUp       += this.KeyboardKeyUp;
            this.window.Closed      += this.Close;

            //Create our Glyph Manager
            this.glyphManager = new GlyphManager(6,4);
            //Render Queue - Assemble!
            this.renderQueue = new RenderQueue();
            //Our Render Engine from which all rendering capabilities are derived from
            this.renderEngine = new RenderEngine(4096);
            //Our marker that exists where the next character ought to be
            this.marker = new Marker(0.0f, 0.0f, 2,ref this.glyphManager);

            //Now add our lovely Glyph Manager to the Render Queue - so that it shall be rendered
            this.renderQueue.AddEntity(this.glyphManager);
            //Add our Marker to the Render Queue!
            this.renderQueue.AddEntity(this.marker);

            //Start it up!
            this.window.Run(refreshRate);
        }

        //Called on window creation
        private void Load(object sender, object param)
        { 
            //Submit a nice Triangle to draw
            //DEBUG ONLY
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));

            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f));
        }

        //Called every frame; update logic here
        private void Update(object sender, object param)
        {
            this.marker.Update();
        }

        //Called every frame; its only purpose is to draw
        private void Draw(object sender,object param)
        {
            //Clear Screen
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);

            //Don't actually Render here, we just add data to our client_vbo_data
            this.renderQueue.RenderAll();
            //Map our client data to the server
            this.renderEngine.GetVBO().Map(this.renderEngine.client_vbo_data, 0, sizeof(float) * 9 * this.renderEngine.client_vbo_data.Count);
            //Draw
            Graphics.VertexBuffer.DrawAll(0, this.renderEngine.client_vbo_data.Count);
            //Render all of our objects
            //this.renderQueue.RenderAll();
            //Now clear our VBO aka server side memory
            this.renderEngine.GetVBO().Clear();
            //Clear our client-side memory
            this.renderEngine.client_vbo_data.Clear();
            //Wait for all OpenGL operations to complete
            GL.Finish();
            //Now swap buffers
            this.window.SwapBuffers();
        }

        //Called on a window resize
        private void Resize(object sender, object param)
        {
            //Change the viewport on resize
            GL.Viewport(new Rectangle(0, 0, this.window.Width, this.window.Height));
        }

        private void KeyboardKeyUp(object sender, KeyboardKeyEventArgs param)
        {
            switch(param.Key)
            {
                case Key.Escape:
                    {
                        this.window.Close();
                        break;
                    }

                case Key.AltLeft:
                    {
                        this.glyphManager.ClearGlpyhs();
                        break;
                    }

                case Key.A:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_A);
                        break;
                    }

                case Key.B:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_B);
                        break;
                    }

                case Key.C:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_C);
                        break;
                    }

                case Key.D:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_D);
                        break;
                    }

                case Key.E:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_E);
                        break;
                    }

                case Key.F:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_F);
                        break;
                    }

                case Key.G:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_G);
                        break;
                    }

                case Key.H:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_H);
                        break;
                    }

                case Key.I:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_I);
                        break;
                    }

                case Key.J:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_J);
                        break;
                    }

                case Key.K:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_K);
                        break;
                    }

                case Key.L:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_L);
                        break;
                    }

                case Key.M:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_M);
                        break;
                    }

                case Key.N:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_N);
                        break;
                    }

                case Key.O:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_O);
                        break;
                    }

                case Key.P:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_P);
                        break;
                    }

                case Key.Q:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_Q);
                        break;
                    }

                case Key.R:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_R);
                        break;
                    }

                case Key.S:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_S);
                        break;
                    }

                case Key.T:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_T);
                        break;
                    }

                case Key.U:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_U);
                        break;
                    }

                case Key.V:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_V);
                        break;
                    }

                case Key.W:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_W);
                        break;
                    }

                case Key.X:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_X);
                        break;
                    }

                case Key.Y:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_Y);
                        break;
                    }

                case Key.Z:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_Z);
                        break;
                    }

                default: break;
            }
        }

        private void Close(object sender,object args)
        {
            this.renderEngine.Cleanup();
        }
    }
}
