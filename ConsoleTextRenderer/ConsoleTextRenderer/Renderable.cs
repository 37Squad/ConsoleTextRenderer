using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    //Interface for rendering Entities
    //'obj' is the object you want to render
    interface IRenderable
    {
        void Render(object obj);
    }
}
