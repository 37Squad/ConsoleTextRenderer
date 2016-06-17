using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Math;
using OpenTK.Input;

namespace ConsoleTextRenderer.Render
{
    class RenderGlyphs
    {
        //Font Renderer
        public static void Render_Font_Object(ref Render.RenderEngine engine, object renderable)
        {
            Render_Font(ref engine, (Systems.GlyphManager)renderable);
        }

        private static void Render_Font(ref Render.RenderEngine engine, Systems.GlyphManager glyphManager)
        {

        }
        //Font Renderer
    }
}
