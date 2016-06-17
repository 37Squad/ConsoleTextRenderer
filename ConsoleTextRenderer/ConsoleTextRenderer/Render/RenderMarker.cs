using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //Create a Quad
            engine.client_vbo_data.Add(new Graphics.VertexBufferData());
            engine.client_vbo_data.Add(new Graphics.VertexBufferData());
            engine.client_vbo_data.Add(new Graphics.VertexBufferData());

            engine.client_vbo_data.Add(new Graphics.VertexBufferData());
            engine.client_vbo_data.Add(new Graphics.VertexBufferData());
            engine.client_vbo_data.Add(new Graphics.VertexBufferData());

            //Upload Uniforms
            engine.GetMarkerShader().UploadUniformMatrix(0, engine.modelStack.stack.Peek());
            engine.GetMarkerShader().UploadUniformMatrix(0, engine.modelStack.stack.Peek());
            engine.GetMarkerShader().UploadUniformMatrix(0, engine.modelStack.stack.Peek());

            //Unbind
            engine.GetMarkerShader().Unbind();
        }
        //Cursor / Marker Renderer
    }
}
