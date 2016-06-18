﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    class GlyphManager
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
        public const float glyphUVWidth = 16.0f / 512.0f;
        public const float glyphUVHeight = 16.0f / 512.0f;
        //image dimension shouldn't be changed...
        public float glyphMapWidth = 512.0F;
        public float glyphMapHeight = 512.0F;
        public float glyphWidth  = 0.03125f;
        public float glyphHeight = 0.03125f;

        //Constructor
        public GlyphManager(int lines,int characters)
        {
            this.glyphs = new Glyph[lines,characters];
            this.maxCharacters  = characters;
            this.maxLines       = lines;
            //this.glyphWidth /= 2 * this.maxCharacters;
            //this.glyphHeight = this.glyphWidth;
            this.ClearGlpyhs();
        }

        //Update logic?
        public void Update()
        {

        }

        //Load GlyphMap
        public void LoadGlyphMap(String path)
        {

        }

        //Get current line
        public int GetLine() { return this.glyphLine; }
        //Get current pos
        public int GetPosition() { return this.glyphPos; }

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
            if (this.glyphPos >= this.maxCharacters)
            {
                glyphLine++;
                this.glyphPos = 0;

                if (this.glyphLine >= this.maxLines)
                {
                    this.ClearGlpyhs();
                    this.glyphPos = 0;
                    this.glyphLine = 0;
                }
            }
            else
            {
                this.glyphs[glyphLine, glyphPos] = glyph;
                glyphPos++;
            }
        }
    }
}
