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
    class RenderGlyphs : IRenderable
    {
        public RenderGlyphs()
        {

        }

        public void Render(object obj)
        {
            this.Render_Glyphs((Systems.GlyphManager)obj);
        }

        //Render all glyphs, from a glyph manager
        private void Render_Glyphs(Systems.GlyphManager glyphs)
        {

        }
    }
}
