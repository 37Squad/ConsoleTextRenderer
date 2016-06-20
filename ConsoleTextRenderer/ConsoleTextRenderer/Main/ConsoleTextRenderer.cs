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
            this.glyphManager = new GlyphManager(16,30);
            //Our Render Engine from which all rendering capabilities are derived from
            this.renderEngine = new RenderEngine(4096);
            //Render Queue - Assemble!
            this.renderQueue = new RenderQueue(ref this.renderEngine);
            //Our marker that exists where the next character ought to be
            this.marker = new Marker(0.0f, 0.0f, 2, ref this.glyphManager);
            //Now add our lovely Glyph Manager to the Render Queue - so that it shall be rendered
            this.renderQueue.AddPair(this.glyphManager, Render.RenderGlyphs.Render_Font_Object);
            //And now add our Marker
            this.renderQueue.AddPair(this.marker, Render.RenderMarker.Render_Marker_Object);

            //Start it up!
            this.window.Run(refreshRate);
        }

        //Called on window creation
        private void Load(object sender, object param)
        { 
            //Submit a nice Triangle to draw
            //DEBUG ONLY
            /*
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, -1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));

            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, -1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f));
            this.renderEngine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, -1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f));

            //Map our client data to the server
            //DEBUG ONLY
            this.renderEngine.GetVBO().Map(this.renderEngine.client_vbo_data, 0, sizeof(float) * 9 * this.renderEngine.client_vbo_data.Count);

            //Upload uniforms
            this.renderEngine.GetFontShader().UploadUniformMatrix(0, this.renderEngine.modelStack.stack.Peek());
            this.renderEngine.GetFontShader().UploadUniformMatrix(1, this.renderEngine.viewStack.stack.Peek());
            this.renderEngine.GetFontShader().UploadUniformMatrix(2, this.renderEngine.projectionStack.stack.Peek());
            */
        }

        //Called every frame; update logic here
        private void Update(object sender, object param)
        {
            //Update Maker Logic
            this.marker.Update();
        }

        //Called every frame; its only purpose is to draw
        private void Draw(object sender,object param)
        {
            //Clear Screen
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Black);

            //Render our things...
            this.renderQueue.RenderQueued();
            //DEBUG ONLY
            //Draw ALL contents
            //Graphics.VertexBuffer.DrawAll(0, this.renderEngine.client_vbo_data.Count);
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

        //Deprecated in favor of State-based keyboard input
        //LOL NEVERMIND
        private void KeyboardKeyUp(object sender, KeyboardKeyEventArgs param)
        {
           switch(param.Key)
           {
                case Key.Escape:
                    {
                        this.window.Close();
                        break;
                    }

                case Key.BackSpace:
                    {
                        this.glyphManager.GlyphBackspace();
                        break;
                    }

                case Key.Space:
                    {
                        this.glyphManager.WriteGlyph(Glyph.GLYPH_EMPTY);
                        break;
                    }

                case Key.Delete:
                    {
                        this.glyphManager.ClearGlpyhs();
                        break;
                    }

                case Key.Tab:
                    {
                        for(int i = 0; i < 5;i++)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_EMPTY);
                        }

                        break;
                    }

                case Key.Enter:
                    {
                        this.glyphManager.GlyphEnter();
                        break;
                    }
                 
                case Key.A:
                    {
                        if(param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_A);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_a);
                        }
                        break;
                    }

                case Key.B:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_B);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_b);
                        }
                        break;
                    }

                case Key.C:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_C);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_c);
                        }
                        break;
                    }

                case Key.D:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_D);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_d);
                        }
                        break;
                    }

                case Key.E:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_E);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_e);
                        }
                        break;
                    }

                case Key.F:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_F);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_f);
                        }
                        break;
                    }

                case Key.G:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_G);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_g);
                        }
                        break;
                    }

                case Key.H:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_H);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_h);
                        }
                        break;
                    }

                case Key.I:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_I);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_i);
                        }
                        break;
                    }

                case Key.J:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_J);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_j);
                        }
                        break;
                    }

                case Key.K:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_K);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_k);
                        }
                        break;
                    }

                case Key.L:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_L);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_l);
                        }
                        break;
                    }

                case Key.M:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_M);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_m);
                        }
                        break;
                    }

                case Key.N:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_N);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_n);
                        }
                        break;
                    }

                case Key.O:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_O);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_o);
                        }
                        break;
                    }

                case Key.P:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_P);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_p);
                        }
                        break;
                    }

                case Key.Q:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_Q);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_q);
                        }
                        break;
                    }

                case Key.R:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_R);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_r);
                        }
                        break;
                    }

                case Key.S:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_S);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_s);
                        }
                        break;
                    }

                case Key.T:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_T);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_t);
                        }
                        break;
                    }

                case Key.U:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_U);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_u);
                        }
                        break;
                    }

                case Key.V:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_V);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_v);
                        }
                        break;
                    }

                case Key.W:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_W);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_w);
                        }
                        break;
                    }

                case Key.X:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_X);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_x);
                        }
                        break;
                    }

                case Key.Y:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_Y);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_y);
                        }
                        break;
                    }

                case Key.Z:
                    {
                        if (param.Shift)
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_Z);
                        }
                        else
                        {
                            this.glyphManager.WriteGlyph(Glyph.GLYPH_z);
                        }
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
