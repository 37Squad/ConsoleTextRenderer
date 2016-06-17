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

        public Texture(String _filePath)
        {
            this.filePath           = _filePath;
            this.texture_pointer    = GL.GenTexture();
            this.bound              = false;
            this.LoadTexture();
        }

        //Called in constructor
        private void LoadTexture()
        {
            Bitmap bitmap = new Bitmap(this.filePath);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            this.Bind();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, bmpData.Width, bmpData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

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
