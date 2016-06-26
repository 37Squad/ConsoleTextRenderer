using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    //The Command Queue holds a list of commands and their parameters
    //This should be processed in ConsoleTextRenderer, under the 'Update' function
    //Command Layout

    // COMMAND *SPACE* N *SPACE* N+1 *SPACE*...
    // Where 'N' is a series of characters within a String
    // N is a parameter

    class CommandQueue
    {
        //Function pointer for a command
        public delegate void CommandHook(String[] parameters);

        //Defines
        const char PARAMETER_DELIMITER = ' ';
        //Overwrite existing commands?
        const bool doOverwrite = true;
        //Parameter max
        const int MAX_PARAMETERS = 2;

        //Internal list
        //I think we can do without
        //private List<CommandParameterPair> commandQueue;
        //Internal Command Dictionary
        private Dictionary<String, CommandHook> commandDictionary;

        public CommandQueue()
        {
            //this.commandQueue = new List<CommandParameterPair>();
            this.commandDictionary = new Dictionary<String, CommandHook>();
        }

        //Add a line to the Command Queue
        //More like TRY to add
        //Check if this command exists
        public void TryProcessLine(String line)
        {
            //First element is command
            String[] parameters = new String[3];
            String dummyChar = String.Empty;
            int idx = 0;
          
            foreach(char character in line)
            {
                if(character == PARAMETER_DELIMITER)
                {
                    idx++;
                    dummyChar = String.Empty;
                }
                else
                {
                    dummyChar += character;
                    parameters[idx] = dummyChar;
                }
            }

            if (this.commandDictionary.ContainsKey(parameters[0]))
            {
                this.commandDictionary[parameters[0]](parameters);
            }
            else
            {
                ;
            }
        }

        //We shall revisit and make this safer in a little bit
        public void AddCommand(String name,CommandHook function)
        {
            this.commandDictionary[name] = function;
        }

    }

}
