using UnityEngine;

namespace Workflow
{
    public abstract class Node
    {
        public int ExitCode { get; protected set; }

        public string Name { get; }
        
        public abstract int Execute(SharedVariables sharedVariables);

        public Node(string name)
        {
            Name = name;
        }

        public void ExitWithError(string errorMessage)
        {
            ExitCode = 1;
            Logger.Error(errorMessage);
        }

        public void Exit(string message)
        {
            ExitCode = 0;
            Logger.Info(message);
        }
    }
}