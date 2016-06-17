using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Render
{
    //Interface for rendering Entities
    //'obj' is the object you want to render
    class Renderable
    {
        //Function Pointer Define
        public delegate void RenderObject(ref Render.RenderEngine engine,object renderable);
    }
}
