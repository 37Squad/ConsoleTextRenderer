using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    class RenderManager
    {
        //List of RenderManagers
        public static RenderManager glyph_renderManager = new RenderManager(new RenderGlyphs());

        //This RenderManagers renderer
        private IRenderable renderer;

        public RenderManager(IRenderable _renderer)
        {
            this.renderer = _renderer;
        }
    }
}
