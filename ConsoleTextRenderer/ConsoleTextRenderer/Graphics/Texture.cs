using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace ConsoleTextRenderer.Graphics
{
    class Texture
    {
        private String filePath     = "";
        private int texture_pointer = 0;
        private bool bound          = false;

        public Texture(String _filePath, System.Drawing.Imaging.PixelFormat format, PixelInternalFormat internalFormat, OpenTK.Graphics.OpenGL.PixelFormat openGLFormat)
        {
            this.filePath           = _filePath;
            this.texture_pointer    = GL.GenTexture();
            this.bound              = false;
            this.LoadTexture(format,internalFormat,openGLFormat);
        }

        //Called in constructor
        private void LoadTexture(System.Drawing.Imaging.PixelFormat format, PixelInternalFormat internalFormat, OpenTK.Graphics.OpenGL.PixelFormat openGLFormat)
        {
            Bitmap bitmap = new Bitmap(this.filePath);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, format);

            this.Bind();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.TexImage2D(TextureTarget.Texture2D, 0, internalFormat, bmpData.Width, bmpData.Height, 0, openGLFormat, PixelType.UnsignedByte, bmpData.Scan0);

            this.Unbind();

            bitmap.UnlockBits(bmpData);
        }

        //Bind this texture
        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, this.texture_pointer);
            this.bound = true;
        }

        public void MakeActive(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
        }

        public void Release()
        {
            this.Unbind();
            GL.DeleteTexture(this.texture_pointer);
        }

        //Unbind ALL textures
        public void Unbind()
        {
            if (this.bound)
            {
                GL.BindTexture(TextureTarget.Texture2D, 0);
                this.bound = false;
            }
        }
    }
}
