using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Math;
using OpenTK.Input;

namespace ConsoleTextRenderer.Graphics
{
    class MatrixStack
    {
        //Our actual stack
        //You can use it!
        public Stack<OpenTK.Matrix4> stack = null;

        public MatrixStack(OpenTK.Matrix4 initial)
        {
            this.stack = new Stack<OpenTK.Matrix4>();

            this.stack.Push(initial);
        }

        //Helper function
        //Multiply top of Matrix
        //Too bad we can't reference the top element
        //Shame
        public void Multiply(OpenTK.Matrix4 mat)
        {
            OpenTK.Matrix4 copy = this.stack.Pop();
            this.stack.Push(OpenTK.Matrix4.Mult(copy, mat));
        }
    }
}
