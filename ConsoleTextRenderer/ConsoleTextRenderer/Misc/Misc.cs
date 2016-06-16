using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace ConsoleTextRenderer.Misc
{
    class Misc
    {
        //Determines if we will assert GL errors
        private const bool DEBUG_MODE = true;

        public static void AssertGLError()
        {
            if (DEBUG_MODE)
            {
                ErrorCode e = GL.GetError();
                if (e != ErrorCode.NoError)
                {
                    throw new Exception(e.ToString());
                }
            }
        }
    }
}
