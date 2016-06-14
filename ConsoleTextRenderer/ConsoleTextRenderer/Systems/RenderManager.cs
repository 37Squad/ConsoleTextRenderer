using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    class RenderManager
    {
        //List of RenderManagers
        public static RenderManager glyph_renderManager = new RenderManager(new Render.RenderGlyphs());

        //This RenderManagers renderer
        public Render.IRenderable renderer;

        public RenderManager(Render.IRenderable _renderer)
        {
            this.renderer = _renderer;
        }
    }
}
