using System;
using System.Collections.Generic;

namespace Domain.Models
{
    [Serializable]
    public class CommandPanel
    {

        public List<Command> commands = new List<Command>();

        public int selectedIndex = 0;

        public CommandPanel()
        {
            foreach (Command command in Enum.GetValues(typeof(Command)))
            {
                commands.Add(command);
            }
        }

        public Command GetSelectedCommand()
        {
            return commands[selectedIndex];
        }


        public enum Command
        {
            MoveCommand = 0,
            AttackCommand,
            TurnEndCommand,
        }


    }
}
