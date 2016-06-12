using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    class GlyphManager : Entity
    {
        //'glyphPos' and 'glyphLine' are indexes for the 'glyphs' array
        //holds current glpyh position in the current line
        private int glyphPos = 0;
        //holds current glyph line
        private int glyphLine = 0;
        //this is the array that contains the glpyh data
        private Glyph[,] glyphs = null;
        //this is a glpyh lookup table- convert chars into its equivalent glyph
        //Not used currently - not sure if we need it
        private Dictionary<char, Glyph> glyphLookupTable = new Dictionary<char, Glyph>();
        //max lines
        private int maxLines = 0;
        //max characters per line
        private int maxCharacters = 0;

        //Pixels / image dimension
        public const float glyphUVWidth = 16.0F / glyphMapWidth;
        public const float glyphUVHeight = 16.0F / glyphMapHeight;
        //image dimension shouldn't be changed...
        public const float glyphMapWidth = 512.0F;
        public const float glyphMapHeight = 512.0F;
        public float glyphWidth  = 1.0f;
        public float glyphHeight = 1.0f;

        //Constructor
        public GlyphManager(int lines,int characters)
        {
            this.glyphs = new Glyph[lines,characters];
            this.maxCharacters  = characters;
            this.maxLines       = lines;
            this.glyphWidth /= this.maxCharacters;
            this.glyphHeight = this.glyphWidth;
            this.ClearGlpyhs();
        }

        //Return maximum lines
        public int GetMaxLines()
        {
            return this.maxLines;
        }

        //Return maximum characters per line
        public int GetMaxCharacters()
        {
            return this.maxCharacters;
        }
        
        public Glyph[,] GetGlyphs()
        {
            return this.glyphs;
        }

        //Reset all glyphs
        public void ClearGlpyhs()
        {
            //Iterate over 'glyphs'
            for(int x = 0; x < this.maxLines;x++)
            {
                for(int y = 0; y < this.maxCharacters;y++)
                {
                    //this is the NULL character - we will use it to indicate that there is no glyph stored here
                    this.glyphs[x,y] = Glyph.GLYPH_NULL;
                }
            }

            //Reset indexes too!
            this.glyphLine  = 0;
            this.glyphPos   = 0;
        }

        //Write a glyph to the list
        public void WriteGlyph(Glyph glyph)
        {
            if (glyphPos >= this.maxCharacters)
            {
                glyphLine++;
                glyphPos = 0;
            }
            else
            {
                this.glyphs[glyphLine, glyphPos] = glyph;
            }
            if (glyphLine >= this.maxLines)
            {
                //What do we do with an overflow? I don't know!
                //Clear it!
                //For now...
                this.ClearGlpyhs();
            }
            glyphPos++;
        }

        public RenderManager getRenderManager()
        {
            return RenderManager.glyph_renderManager;
        }
    }
}
