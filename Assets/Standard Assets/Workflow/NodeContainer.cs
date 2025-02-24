using System.Collections.Generic;

namespace Workflow
{
    public class NodeContainer : Node
    {
        private List<Node> _nodes = new List<Node>();

        public NodeContainer(string name = "[container]")
            : base(name)
        {
        }
        
        public NodeContainer AddNode(Node node)
        {
            _nodes.Add(node);
            return this;
        }

        public override int Execute(SharedVariables sharedVariables)
        {
            foreach (var node in _nodes)
            {
                var code = node.Execute(sharedVariables);
                if (code != 0)
                {
                    return code;
                }
            }

            return 0;
        }

    }
}