using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Language
{
    public class TreeNode<T>
    {
        private T Value;
        private LinkedList<TreeNode<T>> Children;

        public TreeNode(T value)
        {
            Value = value;
            Children = new LinkedList<TreeNode<T>>();
        }

        public void AddChild(T value)
        {
            Children.AddFirst(new TreeNode<T>(value));
        }

        public void Traverse(TreeNode<T> node, Action<T> previous)
        {
            previous(node.Value);
            foreach (TreeNode<T> child in node.Children)
                Traverse(child, previous);
        }
    }
}
