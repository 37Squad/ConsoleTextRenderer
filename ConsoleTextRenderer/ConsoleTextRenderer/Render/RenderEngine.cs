using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Render
{
    class RenderEngine
    {
        //Our vertex buffer
        private Graphics.BufferObject<Graphics.VertexBufferData> vbo = null;
        private Graphics.VertexBufferLayout[] vertex_layout;

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
            //-----VERTEX----------/--UV--/--COLOR---/
            // 4        8      12   16 20 24 28 32 36
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
            

            this.vertex_layout = new Graphics.VertexBufferLayout[]
            {
                new Graphics.VertexBufferLayout(0,3,sizeof(float)*6,0,OpenTK.Graphics.OpenGL.VertexAttribPointerType.Float),
                new Graphics.VertexBufferLayout(1,2,sizeof(float)*7,sizeof(float)*3,OpenTK.Graphics.OpenGL.VertexAttribPointerType.Float),
                new Graphics.VertexBufferLayout(2,4,sizeof(float)*5,sizeof(float)*5,OpenTK.Graphics.OpenGL.VertexAttribPointerType.Float)
            };

            this.vbo.Bind();
            Graphics.VertexBuffer.AssignLayout(this.vertex_layout);
            this.vbo.Unbind();

            Misc.Misc.AssertGLError();
        }

        //Cleanup time, cleanup time, everybody clean up
        //Don't tell me what to do ^
        public void Cleanup()
        {
            this.vbo.Release();
        }

        //Get our Vertex Buffer Object (but really its just a BufferObject)
        public Graphics.BufferObject<Graphics.VertexBufferData> GetVBO() { return this.vbo; }
    }
}
