using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    //An Entity is anything that can be rendered
    interface Entity
    {
        //return this objects corresponding RenderManager- so that it can be rendered
        RenderManager getRenderManager();
    }
}
