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
            this.shaderDirectory    = _filePath;
            this.loaded             = false;
            this.vertexShader       = 0;
            this.fragmentShader     = 0;
            this.program            = 0;
            this.LoadShader(_filePath);
        }

        //We must read the shaders from files ; alternatively we can store them as strings
        //in here, but I don't quite like that idea very much. It is more proper to give them
        //their own files. Therfore, it will be done that way.
        private void LoadShader(String _filePath)
        {
            try
            {
                //Read files
                using (FileStream file = new FileStream(_filePath + "//vertex.glsl", FileMode.Open))
                {
                    //Read the raw bytes
                    byte[] fileData = new byte[256];
                    file.Read(fileData, 0, 256);

                    //Turn the bytes into a String
                    //characters are just bytes, right guys?
                    //so is everything...
                    // :I
                    this.vertexShaderSource = Convert.ToString(fileData);
                }

                using (FileStream file = new FileStream(_filePath + "//fragment.glsl", FileMode.Open))
                {
                    byte[] fileData = new byte[256];
                    file.Read(fileData, 0, 256);

                    this.fragmentShaderSource = Convert.ToString(fileData);
                }
            }
            //If we can't read these files, throw an icky exception
            catch(IOException)
            {
                throw new Exception("Could not open GLSL shaders.");
            }

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

            //Grab a program pointer
            this.program = GL.CreateProgram();

            //Attach these shaders to our program
            GL.AttachShader(this.program, this.vertexShader);
            GL.AttachShader(this.program, this.fragmentShader);

            //Bind them, as it were, to our program
            GL.LinkProgram(this.program);

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

        //Self-expalantory
        public String GetShaderDirectory() { return this.shaderDirectory; }
    }
}
