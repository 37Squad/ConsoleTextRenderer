using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Render
{
    class RenderFlicker
    {
        public static void Render_Flicker_Object(ref RenderEngine engine, object obj)
        {
            Render_Flicker(ref engine,(Systems.Flicker)obj);
        }

        private static void Render_Flicker(ref RenderEngine engine, Systems.Flicker flicker)
        {
            engine.GetFlickerShader().Bind();
            engine.GetVBO().Bind();

            engine.flickerMask.MakeActive(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
            engine.flickerMask.Bind();

            engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f));

            engine.client_vbo_data.Add(new Graphics.VertexBufferData(0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f));
            engine.client_vbo_data.Add(new Graphics.VertexBufferData(1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));

            OpenTK.Graphics.OpenGL.GL.Uniform1(engine.GetFlickerShader().GetUniformLocation("ticks"),flicker.GetTickCount());
            OpenTK.Graphics.OpenGL.GL.Uniform1(engine.GetFlickerShader().GetUniformLocation("scanlineY"), flicker.GetScanline());

            engine.modelStack.Push();

            engine.GetFlickerShader().UploadUniformMatrix(0, engine.modelStack.stack.Peek());
            engine.GetFlickerShader().UploadUniformMatrix(1, engine.viewStack.stack.Peek());
            engine.GetFlickerShader().UploadUniformMatrix(2, engine.projectionStack.stack.Peek());

            engine.modelStack.stack.Pop();

            engine.GetVBO().Map(engine.client_vbo_data, 0, sizeof(float) * 9 * engine.client_vbo_data.Count);

            //Draw
            Graphics.VertexBuffer.DrawAll(0, engine.client_vbo_data.Count);
            //Reset Client data
            engine.client_vbo_data.Clear();

            //Reset Server Memory
            engine.GetVBO().Clear();

            engine.GetVBO().Unbind();
            engine.GetFlickerShader().Unbind();
        }
    }
}
