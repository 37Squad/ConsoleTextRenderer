﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Main
{
    class Application
    {
        //STAThread exists to explicitly declare the entry point of a program
        [STAThread]
        static void Main(string[] args)
        {
            ConsoleTextRenderer graphics = new ConsoleTextRenderer(0, 0, 768, 768,60);
        }
    }
}
