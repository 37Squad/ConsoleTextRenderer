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
                        GL.MatrixMode(MatrixMode.Modelview);
                        GL.PushMatrix();
                        GL.LoadIdentity();

                        //Offsets; for translation
                        float offsetX = (position * (2*glyphs.glyphWidth)) + glyphs.glyphWidth;
                        float offsetY = (line * (2*glyphs.glyphHeight)) + glyphs.glyphHeight;

                        //UV pairs
                        float u0, v0, u1, v1, u2, v2, u3, v3;

                        u0 = currentGlyph.U0;
                        v0 = currentGlyph.V0;

                        u1 = currentGlyph.U0 + GlyphManager.glyphUVWidth;
                        v1 = currentGlyph.V0;

                        u2 = currentGlyph.U0 + GlyphManager.glyphUVWidth;
                        v2 = currentGlyph.V0 + GlyphManager.glyphUVHeight;

                        u3 = currentGlyph.U0;
                        v3 = currentGlyph.V0 + GlyphManager.glyphUVHeight;

                        GL.Translate(offsetX, offsetY,0.0f);
                        GL.Scale(glyphs.glyphWidth, glyphs.glyphHeight, 1.0f);
                        GL.Rotate(-90.0f, 0.0f, 0.0f, 1.0f);

                        GL.Begin(PrimitiveType.Quads);

                        GL.Vertex3(-1.0f, -1.0f, 0.0f);
                        GL.TexCoord2(u0, v0);
                        //GL.TexCoord2(0.0f, 0.0f);

                        GL.Vertex3(1.0f, -1.0f, 0.0f);
                        GL.TexCoord2(u1, v1);
                        //GL.TexCoord2(1.0f, 0.0f);

                        GL.Vertex3(1.0f, 1.0f, 0.0f);
                        GL.TexCoord2(u2, v2);
                        //GL.TexCoord2(1.0f, 1.0f);

                        GL.Vertex3(-1.0f, 1.0f, 0.0f);
                        GL.TexCoord2(u3, v3);
                        //GL.TexCoord2(0.0f, 1.0f);

                        GL.End();
                        GL.PopMatrix();

                        ErrorCode e = GL.GetError();
                        if(e != 0)
                        {
                            throw new System.Exception(e.ToString());
                        }
                    }
                }
            }
        }
    }
}
