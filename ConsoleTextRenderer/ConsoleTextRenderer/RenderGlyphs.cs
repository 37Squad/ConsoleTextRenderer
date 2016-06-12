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
            float glyphX = 0.0f, glyphY = 0.0f;
            
            for (int line = 0; line < glyphs.GetGlyphs().Length;line++)
            {
                for(int position = 0; position < glyphs.GetGlyphs()[0].Length;position++)
                {
                    Glyph currentGlyph = glyphs.GetGlyphs()[line][position];
                    if (currentGlyph == Glyph.GLYPH_NULL)
                    {
                        continue;
                    }
                    else
                    {
                        //glyphWidth ang glyphHeight will also work for the UV coords, coincidentally
                        GL.Begin(PrimitiveType.Quads);
                        GL.Vertex3(glyphX, glyphY, 1.0f);
                        GL.TexCoord2(currentGlyph.U0, currentGlyph.V0);
                        GL.Vertex3(glyphX + Glyph.glyphWidth, glyphY, 1.0f);
                        GL.TexCoord2(currentGlyph.U0 + Glyph.glyphWidth, currentGlyph.V0);
                        GL.Vertex3(glyphX, glyphY + Glyph.glyphHeight, 1.0f);
                        GL.TexCoord2(currentGlyph.U0, currentGlyph.V0 - Glyph.glyphHeight);
                        GL.Vertex3(glyphX + Glyph.glyphWidth, glyphY + Glyph.glyphHeight, 1.0f);
                        GL.TexCoord2(currentGlyph.U0 + Glyph.glyphWidth, currentGlyph.V0 - Glyph.glyphHeight);
                        GL.End();
                    }

                    glyphX += Glyph.glyphWidth;
                }

                glyphY += Glyph.glyphHeight;
            }
        }
    }
}
