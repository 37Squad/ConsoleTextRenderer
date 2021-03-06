﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ConsoleTextRenderer.Render
{
    class RenderEngine
    {
        //Our vertex buffer
        private Graphics.BufferObject<Graphics.VertexBufferData> vbo = null;
        private Graphics.VertexBufferLayout[] vertex_layout;
        private Graphics.Shader fontShader = null;
        private Graphics.Shader markerShader = null;
        private Graphics.Shader flickerShader = null;

        public Graphics.MatrixStack modelStack      = null;
        public Graphics.MatrixStack projectionStack = null;
        public Graphics.MatrixStack viewStack       = null;

        public Graphics.Texture textureAtlas        = null;
        public Graphics.Texture flickerMask         = null;

        public List<Graphics.VertexBufferData> client_vbo_data;

        public RenderEngine(int vbo_byte_width)
        {
            //The 'null' just means that there is no initial data stored in the buffer
            //If we made a STATIC_DRAW vbo then it is imperative that we make it non-null
            //But we don't use STATIC_DRAW so it doesn't matter
            this.vbo = new Graphics.BufferObject<Graphics.VertexBufferData>
                (
                new Graphics.BufferDescriptor(
                        OpenTK.Graphics.OpenGL.BufferTarget.ArrayBuffer,
                        OpenTK.Graphics.OpenGL.BufferUsageHint.DynamicDraw,
                        0,
                        vbo_byte_width),
                null
                );

            //This isn't regular software anymore
            //Bytes baby
            //-----VERTEX----------/--UV--/--COLOR---/  //---VERTEX-------/
            // 4        8      12   16 20 24 28 32 36   40      44      48
            // vertex,vertex,vertex,uv,uv,r ,g ,b ,a // repeat
            //previous floats * sizeof(floats) = offset
            //This vertex data layout is interleaved
            //Format is: 3 floats for x y z coordinates, 2 floats for UV coordinates for texturing, and 4 (FOUR) floats
            //9 floats in total, for an entire 'set' of data (ONE VERTICE), implies 9*4 byte width per datum, which is
            //*duh* 36 bytes
            //36 + 1???
            //37
            //for colors(including the spooky alpha channel)
            //Please understand that we have the ability to NOT waste memory on floating point decimals, but
            //I don't think we will encounter major performance tweaking with this lovely program.
            //36 is the size of our VertexBufferData 

            this.vertex_layout = new Graphics.VertexBufferLayout[]
            {
                new Graphics.VertexBufferLayout(0,3,36,0,OpenTK.Graphics.OpenGL.VertexAttribPointerType.Float),
                new Graphics.VertexBufferLayout(1,2,36,sizeof(float)*3,OpenTK.Graphics.OpenGL.VertexAttribPointerType.Float),
                new Graphics.VertexBufferLayout(2,4,36,sizeof(float)*5,OpenTK.Graphics.OpenGL.VertexAttribPointerType.Float)
            };

            this.vbo.Bind();
            Graphics.VertexBuffer.AssignLayout(this.vertex_layout);
            this.vbo.Unbind();

            //CHANGE THIS per user
            //One day we will make this nice and clean
            this.fontShader = new Graphics.Shader("C:\\Users\\Nick\\Source\\Repos\\ConsoleTextRenderer\\ConsoleTextRenderer\\ConsoleTextRenderer\\Graphics\\Shaders\\Font");

            //Marker Shader
            this.markerShader = new Graphics.Shader("C:\\Users\\Nick\\Source\\Repos\\ConsoleTextRenderer\\ConsoleTextRenderer\\ConsoleTextRenderer\\Graphics\\Shaders\\Marker");

            //Flicker Shader
            this.flickerShader = new Graphics.Shader("C:\\Users\\Nick\\Source\\Repos\\ConsoleTextRenderer\\ConsoleTextRenderer\\ConsoleTextRenderer\\Graphics\\Shaders\\ScreenFlicker");

            //Create a list which we can use to upload our client data to the server
            this.client_vbo_data = new List<Graphics.VertexBufferData>();

            //Load our matrices
            this.modelStack         = new Graphics.MatrixStack(OpenTK.Matrix4.Identity);
            this.projectionStack    = new Graphics.MatrixStack(OpenTK.Matrix4.CreateOrthographicOffCenter(0.0f,1.0f,1.0f,0.0f,0.0f,10.0f));
            this.viewStack          = new Graphics.MatrixStack(OpenTK.Matrix4.Identity);

            //Load basic OpenGL state
            //Bind the font shader
            this.fontShader.Bind();
            //Bind our VBO
            this.vbo.Bind();

            this.textureAtlas = new Graphics.Texture("C:\\Users\\Nick\\Source\\Repos\\ConsoleTextRenderer\\ConsoleTextRenderer\\ConsoleTextRenderer\\bin\\Debug\\alphabet_production_32bpp.bmp",System.Drawing.Imaging.PixelFormat.Format32bppArgb,PixelInternalFormat.Rgba8,PixelFormat.Bgra);
            this.textureAtlas.MakeActive(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
            this.textureAtlas.Bind();
            this.fontShader.UploadTexture("textureAtlas", 0);

            this.flickerMask = new Graphics.Texture("C:\\Users\\Nick\\Source\\Repos\\ConsoleTextRenderer\\ConsoleTextRenderer\\ConsoleTextRenderer\\bin\\Debug\\ScreenNoise.bmp",System.Drawing.Imaging.PixelFormat.Format8bppIndexed,PixelInternalFormat.Luminance8,PixelFormat.Luminance);
            this.flickerMask.MakeActive(TextureUnit.Texture0);
            this.flickerMask.Bind();
            this.flickerShader.Bind();
            this.flickerShader.UploadTexture("flickerMask", 0);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.AlphaTest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //Debugging incoming
            Misc.Misc.AssertGLError();
        }

        //Cleanup time, cleanup time, everybody clean up
        //Don't tell me what to do ^
        public void Cleanup()
        {
            this.vbo.Release();
            this.fontShader.Release();
            this.textureAtlas.Release();
        }

        //Get our Vertex Buffer Object (but really its just a BufferObject)
        public Graphics.BufferObject<Graphics.VertexBufferData> GetVBO() { return this.vbo; }
        //Get our font shader
        public Graphics.Shader GetFontShader() { return this.fontShader; }
        //Get Marker shader
        public Graphics.Shader GetMarkerShader() { return this.markerShader; }
        //Get Flicker Shader
        public Graphics.Shader GetFlickerShader() { return this.flickerShader; }
    }
}
