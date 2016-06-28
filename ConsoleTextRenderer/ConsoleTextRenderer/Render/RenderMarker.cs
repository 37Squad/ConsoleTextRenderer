using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace ConsoleTextRenderer.Render
{
    class RenderMarker
    {
        //Cursor / Marker Renderer
        public static void Render_Marker_Object(ref Render.RenderEngine engine, object renderable)
        {
            Render_Marker(ref engine, (Systems.Marker)renderable);
        }

        private static void Render_Marker(ref Render.RenderEngine engine, Systems.Marker marker)
        {
            //Bind
            engine.GetMarkerShader().Bind();
            engine.GetVBO().Bind();

            engine.textureAtlas.MakeActive(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
            engine.textureAtlas.Bind();

            //Create a Quad
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 0.0f, -0.5f, 0.3125f, 0.0625f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, -0.5f, 0.34375f, 0.0625f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, -0.5f, 0.3125f, 0.09375f, 1.0f, 1.0f, 1.0f, 1.0f));

            engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, -0.5f, 0.3125f, 0.09375f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 1.0f, -0.5f, 0.34375f, 0.09375f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, -0.5f, 0.34375f, 0.0625f, 1.0f, 1.0f, 1.0f, 1.0f));

            //Translate our Marker based on where the next glyph will be placed
            float offsetX = 0.0f;
            float offsetY = 0.0f;

            offsetX = (float)(marker.GetGlyphManagerRef().GetPosition()) * Systems.GlyphManager.glyphWidth * 2;
            offsetY = -(float)(marker.GetGlyphManagerRef().GetLine()) * Systems.GlyphManager.glyphHeight * 2;
            
            //Push previous matrix - it should be identity
            engine.modelStack.Push();
            //Scale based on glyph width and height, even though this isn't really a glyph
            engine.modelStack.Multiply(OpenTK.Matrix4.CreateScale(Systems.GlyphManager.glyphWidth, Systems.GlyphManager.glyphHeight,1.0f));
            //Translate so that we are now in the upper left hand corner to start
            engine.modelStack.Multiply(OpenTK.Matrix4.CreateTranslation(-1.0f + Systems.GlyphManager.glyphWidth,1.0f - Systems.GlyphManager.glyphHeight,0.0f));
            //Translate based on character position and line position
            engine.modelStack.Multiply(OpenTK.Matrix4.CreateTranslation(offsetX,offsetY,0.0f));
            //Upload Uniforms
            engine.GetMarkerShader().UploadUniformMatrix(0, engine.modelStack.stack.Peek());
            engine.GetMarkerShader().UploadUniformMatrix(1, engine.viewStack.stack.Peek());
            engine.GetMarkerShader().UploadUniformMatrix(2, engine.projectionStack.stack.Peek());

            GL.Uniform1(engine.GetMarkerShader().GetUniformLocation("render"), Convert.ToInt32(marker.ShouldRender()));
            //Upload our texture atlas
            engine.textureAtlas.MakeActive(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
            engine.textureAtlas.Bind();
            engine.GetMarkerShader().UploadTexture("textureAtlas", 0);
            //Pop!
            engine.modelStack.stack.Pop();
            //Map
            engine.GetVBO().Map(engine.client_vbo_data, 0, sizeof(float) * 9 * engine.client_vbo_data.Count);
            //Draw
            Graphics.VertexBuffer.DrawAll(0, engine.client_vbo_data.Count);
            //Reset Client data
            engine.client_vbo_data.Clear();
            //Reset Server data
            engine.GetVBO().Clear();

            //Unbind
            engine.GetVBO().Unbind();
            engine.GetMarkerShader().Unbind();
        }
        //Cursor / Marker Renderer
    }
}
