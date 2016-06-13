using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Math;
using OpenTK.Input;

namespace ConsoleTextRenderer
{
    class RenderGlyphs : IRenderable
    {
        public RenderGlyphs()
        {

        }

        public void Render(object obj)
        {
            this.Render_Glyphs((GlyphManager)obj);
        }

        //Render all glyphs, from a glyph manager
        private void Render_Glyphs(GlyphManager glyphs)
        {
            //Remember, screenspace is 0.0f -> 1.0f left to right, and 0.0f -> 1.0f up to down
            
            for (int line = 0; line < glyphs.GetMaxLines();line++)
            {
                for(int position = 0; position < glyphs.GetMaxCharacters();position++)
                {
                    Glyph currentGlyph = glyphs.GetGlyphs()[line,position];
                    if (currentGlyph == Glyph.GLYPH_NULL || currentGlyph == null)
                    {
                        continue;
                    }
                    else
                    {
                        GL.Begin(PrimitiveType.Quads);
                        GL.MatrixMode(MatrixMode.Modelview);
                        GL.LoadIdentity();
                       
                        float offsetX = position * glyphs.glyphWidth;
                        float offsetY = line * glyphs.glyphHeight;

                        GL.Translate(offsetX, offsetY, 0.0f);
                       
                        GL.Begin(PrimitiveType.Quads);

                        GL.Color3(1.0f, 0.0f, 0.0f);
                        GL.Normal3(0.0f, 0.0f, -1.0f);

                        GL.Vertex3(-1.0f, -1.0f, 0.0f);
                        GL.Vertex3(1.0f, -1.0f, 0.0f);
                        GL.Vertex3(1.0f, 1.0f, 0.0f);
                        GL.Vertex3(-1.0f, 1.0f, 0.0f);

                        GL.End();
                    }
                }
            }
        }
    }
}
