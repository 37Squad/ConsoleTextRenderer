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

        private static void Render_Font(ref Render.RenderEngine engine,Systems.GlyphManager glyphManager)
        {
            //Bind shader
            engine.GetFontShader().Bind();
            //Bind VBO
            engine.GetVBO().Bind();

            engine.textureAtlas.MakeActive(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
            engine.textureAtlas.Bind();
            engine.GetFontShader().UploadTexture("textureAtlas", 0);

            float u0, v0, u1, v1, u2, v2, u3, v3;

            Systems.Glyph currentGlyph = null;

            for (int line = 0; line < glyphManager.GetMaxLines(); line++)
            {
                for (int position = 0; position < glyphManager.GetMaxCharacters(); position++)
                {
                    currentGlyph = glyphManager.GetGlyphs()[line, position];

                    if (currentGlyph == Systems.Glyph.GLYPH_NULL || currentGlyph == null)
                    {
                        
                    }
                    else
                    {
                        u0 = currentGlyph.U0;
                        v0 = currentGlyph.V0;

                        u1 = currentGlyph.U0 + Systems.GlyphManager.glyphUVWidth;
                        v1 = currentGlyph.V0;

                        u2 = currentGlyph.U0;
                        v2 = currentGlyph.V0 + Systems.GlyphManager.glyphUVHeight;

                        u3 = currentGlyph.U0 + Systems.GlyphManager.glyphUVWidth;
                        v3 = currentGlyph.V0 + Systems.GlyphManager.glyphUVHeight;

                        //Create a Quad
                        engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 0.0f, 0.0f, u0, v0, 1.0f, 1.0f, 1.0f, 1.0f));
                        engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, 0.0f, u1, v1, 1.0f, 1.0f, 1.0f, 1.0f));
                        engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, 0.0f, u2, v2, 1.0f, 1.0f, 1.0f, 1.0f));

                        engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, 0.0f, u2, v2, 1.0f, 1.0f, 1.0f, 1.0f));
                        engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 1.0f, 0.0f, u3, v3, 1.0f, 1.0f, 1.0f, 1.0f));
                        engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, 0.0f, u1, v1, 1.0f, 1.0f, 1.0f, 1.0f));

                        //Translate our Marker based on where the next glyph will be placed
                        float offsetX = 0.0f;
                        float offsetY = 0.0f;

                        offsetX = position * Systems.GlyphManager.glyphWidth * 2;
                        offsetY = -line * Systems.GlyphManager.glyphHeight * 2;

                        //Push previous matrix - it should be identity
                        engine.modelStack.Push();

                        //Scale based on glyph width and height, even though this isn't really a glyph
                        //engine.modelStack.Multiply(OpenTK.Matrix4.CreateRotationZ(OpenTK.MathHelper.DegreesToRadians(-90.0f)));

                        engine.modelStack.Multiply(OpenTK.Matrix4.CreateScale(Systems.GlyphManager.glyphWidth, Systems.GlyphManager.glyphHeight, 1.0f));

                        //Translate so that we are now in the upper left hand corner to start
                        engine.modelStack.Multiply(OpenTK.Matrix4.CreateTranslation(-1.0f + Systems.GlyphManager.glyphWidth, 1.0f - Systems.GlyphManager.glyphHeight, 0.0f));

                        //Translate based on character position and line position
                        engine.modelStack.Multiply(OpenTK.Matrix4.CreateTranslation(offsetX, offsetY, 0.0f));

                        //Upload Uniforms
                        engine.GetFontShader().UploadUniformMatrix(0, engine.modelStack.stack.Peek());
                        engine.GetFontShader().UploadUniformMatrix(1, engine.viewStack.stack.Peek());
                        engine.GetFontShader().UploadUniformMatrix(2, engine.projectionStack.stack.Peek());

                        engine.modelStack.stack.Pop();

                        //Map
                        engine.GetVBO().Map(engine.client_vbo_data, 0, sizeof(float) * 9 * engine.client_vbo_data.Count);

                        //Draw
                        Graphics.VertexBuffer.DrawAll(0, engine.client_vbo_data.Count);
                        //Reset Client data
                        engine.client_vbo_data.Clear();
                    }
                }
            }

            //Reset Server data
            engine.GetVBO().Clear();
            //Unbind VBO
            engine.GetVBO().Unbind();
            //Unbind shader
            engine.GetFontShader().Unbind();
        }
        //Font Renderer
    }
}
