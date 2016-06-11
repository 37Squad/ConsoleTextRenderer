using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    class GlyphManager
    {
        //holds file path for the glyph-containing texture
        private String filePath = String.Empty;
        //holds current glpyh position in the current line
        private int glphyPos = 0;
        //holds current glyph line
        private int glyphLine = 0;
        //this is the array that contains the glpyh data
        private char[][] glpyhs = null;

        public GlyphManager(String glyphFilePath,int lines,int characters)
        {
            this.filePath = glyphFilePath;
        }

        public String GetFilePath()
        {
            return this.filePath;
        }

        public void ClearGlpyhs()
        {

        }

    }
}
