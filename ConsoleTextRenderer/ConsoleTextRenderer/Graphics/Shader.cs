using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ConsoleTextRenderer.Graphics
{
    class Shader
    {
        //Shader Directory in which our shaders can be found
        private String shaderDirectory      = "";
        private String vertexShaderSource   = "";
        private String fragmentShaderSource = "";
        private int vertexShader            = 0;
        private int fragmentShader          = 0;
        private int program                 = 0;
        private bool loaded                 = false;
        private bool bound                  = false;

        public Shader(String _filePath)
        {
            this.shaderDirectory        = _filePath;
            this.vertexShaderSource     = "";
            this.fragmentShaderSource   = "";
            this.loaded                 = false;
            this.bound                  = false;
            this.vertexShader           = 0;
            this.fragmentShader         = 0;
            this.program                = 0;
            this.LoadShader(_filePath);
        }

        //We must read the shaders from files ; alternatively we can store them as strings
        //in here, but I don't quite like that idea very much. It is more proper to give them
        //their own files. Therfore, it will be done that way.
        private void LoadShader(String _filePath)
        {
            if(!(File.Exists(_filePath + "\\vertex_shader.glsl") && File.Exists(_filePath + "\\fragment_shader.glsl")) )
            {
                throw new Exception("Shaders do not exist.");
            }

            this.vertexShaderSource = File.ReadAllText(_filePath + "\\vertex_shader.glsl");
            this.fragmentShaderSource = File.ReadAllText(_filePath + "\\fragment_shader.glsl");

            //Grab pointers for our shaders
            this.vertexShader   = GL.CreateShader(ShaderType.VertexShader);
            this.fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            //Assign source
            GL.ShaderSource(this.vertexShader, this.vertexShaderSource);
            GL.ShaderSource(this.fragmentShader, this.fragmentShaderSource);

            //Compile
            //Why don't we use binaries? We can. But I prefer not to.
            GL.CompileShader(this.vertexShader);
            GL.CompileShader(this.fragmentShader);

            Console.WriteLine(GL.GetShaderInfoLog(this.vertexShader));
            Console.WriteLine(GL.GetShaderInfoLog(this.fragmentShader));

            //Grab a program pointer
            this.program = GL.CreateProgram();

            //Attach these shaders to our program
            GL.AttachShader(this.program, this.vertexShader);
            GL.AttachShader(this.program, this.fragmentShader);

            //Bind them, as it were, to our program
            GL.LinkProgram(this.program);

            //Now detach our shaders
            GL.DetachShader(this.program, this.vertexShader);
            GL.DetachShader(this.program, this.fragmentShader);

            //Return the compilation log. Used for debugging.
            String log = GL.GetProgramInfoLog(this.program);
            Console.WriteLine(log);

            //Did we do anything naughty?
            Misc.Misc.AssertGLError();

            //We have reaches this point, therefore we must've loaded
            this.loaded = true;
        }

        //Bind our shader; make it current
        public void Bind()
        {
            if(this.loaded)
            {
                GL.UseProgram(this.program);
                this.bound = true;
            }
        }

        //Upload a matrix
        public void UploadUniformMatrix(int slot,OpenTK.Matrix4 mat)
        {
            if (!this.bound) return;
            else
            {
                GL.UniformMatrix4(slot, false, ref mat);
            }
        }
        
        //Detaches ALL shaders! 
        //OpenGL is a state machine
        //The purpose of this function is to really set the 'bound' flag
        //Be careful. Be vigilant!
        public void Unbind()
        {
            if(this.loaded && this.bound)
            {
                GL.UseProgram(0);
                this.bound = false;
            }
        }

        //Release memory
        public void Release()
        {
            //First, unbind our shader
            this.Unbind();
            //Now, delete
            GL.DeleteProgram(this.program);
            GL.DeleteShader(this.vertexShader);
            GL.DeleteShader(this.fragmentShader);
        }

        //Self-expalantory
        public String GetShaderDirectory() { return this.shaderDirectory; }
    }
}
