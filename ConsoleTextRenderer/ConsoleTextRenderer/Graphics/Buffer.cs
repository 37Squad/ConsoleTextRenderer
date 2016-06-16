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
    //This descriptor exists to define what type of Buffer we are creating and describve how it will be used
    struct BufferDescriptor
    {
        private BufferTarget buffer_target;
        private int slot;
        private int byte_width;
        private BufferUsageHint usage;

        public BufferDescriptor(BufferTarget _buffer_target,BufferUsageHint _usage,int _slot,int _byte_width)
        {
            this.buffer_target  = _buffer_target;
            this.slot           = _slot;
            this.byte_width     = _byte_width;
            this.usage          = _usage;
        }

        public BufferTarget GetTarget() { return this.buffer_target; }
        public int GetSlot() { return this.slot; }
        public int GetByteWidth() { return this.byte_width; }
        public BufferUsageHint GetUsage() { return this.usage; }
    }


    class BufferObject<T> where T : struct
    {
        //OpenGL pointer to our server-side buffer
        private int buffer_object           = 0;
        //Are we currently bound?
        private bool bound                  = false;
        //Descriptor, tells us what type of buffer we are making
        private BufferDescriptor descriptor;
        //How many elements exist in the buffer?
        private int elements                = 0;

        //Create a BufferObject
        public BufferObject(BufferDescriptor _descriptor,List<T> data)
        {
            //Grab a pointer to a buffer
            this.buffer_object  = GL.GenBuffer();
            this.descriptor     = _descriptor;
            this.bound          = false;
            if(data == null)
            {
                this.elements = 0;
            }
            else
            {
                this.elements = data.Count;
            }

            //Call BufferData initially to initialize our Vertex Buffer's actual server-side data
            this.Bind();
            if (data == null)
            {
                GL.BufferData(this.descriptor.GetTarget(), (IntPtr)this.descriptor.GetByteWidth(), IntPtr.Zero, this.descriptor.GetUsage());
            }
            else
            {
                GL.BufferData<T>(this.descriptor.GetTarget(), (IntPtr)this.descriptor.GetByteWidth(), data.ToArray(), this.descriptor.GetUsage());
            }
            this.Unbind();

            Misc.Misc.AssertGLError();
        }

        public void Release()
        {
            this.Unbind();
            GL.DeleteBuffer(this.buffer_object);
        }

        //Self-explanatory
        public BufferDescriptor GetDescriptor()
        {
            return this.descriptor;
        }

        //Call this to upload our data to OpenGL
        public bool Map(List<T> data,int startIdx,int byte_count)
        {
            if (!this.bound) return false;
            else
            {
                //Update either A) the entire buffer, or B) a portion of the buffer
                GL.BufferSubData<T>(this.descriptor.GetTarget(),(IntPtr)startIdx,(IntPtr)byte_count,data.ToArray());
                Misc.Misc.AssertGLError();

                return true;
            }
        }

        //Clear server-side data
        public bool Clear()
        {
            if (!this.bound) return false;
            else
            {
                GL.BufferData(this.descriptor.GetTarget(), (IntPtr)this.descriptor.GetByteWidth(), IntPtr.Zero, this.descriptor.GetUsage());
            }

            this.elements = 0;
            return true;
        }

        //Bind this buffer object
        public void Bind()
        {
            GL.BindBuffer(this.descriptor.GetTarget(), this.buffer_object);
            this.bound = true;
        }

        //Unbind ANY BufferObject bound to THIS BufferObject's target
        //Be careful
        public void Unbind()
        {
            GL.BindBuffer(this.descriptor.GetTarget(), 0);
            this.bound = false;
        }
    }
}
