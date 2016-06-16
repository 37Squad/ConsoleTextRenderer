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

namespace ConsoleTextRenderer.Graphics
{
    public struct VertexBufferLayout
    {
        int slot;
        int size;
        int stride;
        VertexAttribPointerType type;
        int offsetPosition;

        public VertexBufferLayout(int _slot, int _size, int _stride, int _offset, VertexAttribPointerType _type)
        {
            this.slot = _slot;
            this.size = _size;
            this.stride = _stride;
            this.offsetPosition = _offset;
            this.type = _type;
        }

        public int GetSlot() { return this.slot; }
        public int GetSize() { return this.size; }
        public int GetStride() { return this.stride; }
        public int GetOffset() { return this.offsetPosition; }
        public VertexAttribPointerType GetAttribType() { return this.type; }
    }

    //The functions found here deal with OpenGL state; and as such they are static
    public class VertexBuffer
    {
        //Do we have a layout?
        private static VertexBufferLayout[] currentLayout = null;

        public static VertexBufferLayout[] GetCurrentLayout()
        {
            return currentLayout;
        }

        //Assign a vertex data layout
        //Bind your vertex buffer before / unbind after
        public static void AssignLayout(VertexBufferLayout[] layout)
        {
            foreach(VertexBufferLayout vertex_buffer_layout in layout)
            {
                GL.EnableVertexAttribArray(vertex_buffer_layout.GetSlot());
                GL.VertexAttribPointer(vertex_buffer_layout.GetSlot(), vertex_buffer_layout.GetSize(), vertex_buffer_layout.GetAttribType(), false, vertex_buffer_layout.GetStride(), (IntPtr)vertex_buffer_layout.GetOffset());
                GL.DisableVertexAttribArray(vertex_buffer_layout.GetSlot());
            }

            currentLayout = layout;
        }

        public static void EnableAttribArrays()
        {
            if (currentLayout == null) return;
            else
            {
                foreach(VertexBufferLayout vertex_buffer_layout in currentLayout)
                {
                    GL.EnableVertexAttribArray(vertex_buffer_layout.GetSlot());
                }
            }
        }

        public static void DisableAttribArrays()
        {
            if (currentLayout == null) return;
            else
            {
                foreach (VertexBufferLayout vertex_buffer_layout in currentLayout)
                {
                    GL.DisableVertexAttribArray(vertex_buffer_layout.GetSlot());
                }
            }
        }

        public static void DrawAll(int startIdx,int count)
        {
            if (currentLayout == null) return;
            else
            {
                EnableAttribArrays();
                GL.DrawArrays(PrimitiveType.TriangleStrip, startIdx, count);
                DisableAttribArrays();
            }
        }
    }
}
