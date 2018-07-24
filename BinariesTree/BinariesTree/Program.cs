using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinariesTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();
            Random rd = new Random();

            for (int i = 0; i < 10; i++)
            {
                tree.AddValue(rd.Next(1,100));
            }

            Console.WriteLine("Root: " + tree.Root.Value);

            tree.InOrder(tree.Root);

            Console.WriteLine("================================");
            tree.IsRNLTraversal = true;
            foreach (var node in tree)
            {
                Console.WriteLine(node.Value);
            }
        }
    }

    class BinaryTree : IEnumerable<Node>
    {
        public Node Root;
        public bool IsRNLTraversal { get; set; }

        /// <summary>
        /// Add Value
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(int value)
        {
            Node newNode = new Node { Value = value };
            Node current = Root, prev = null;

            while (current != null)
            {
                prev = current;
                if (newNode.CompareTo(current) < 0)
                {
                    current = current.LeftNode;
                }
                else
                {
                    current = current.RightNode;
                }
            }

            if (Root == null)
            {
                Root = newNode;
            }
            else if (newNode.CompareTo(prev) < 0)
            {
                prev.LeftNode = newNode;
            }
            else
            {
                prev.RightNode = newNode;
            }
        }

        public void InOrder(Node c)
        {
            if (c != null)
            {
                InOrder(c.LeftNode);
                Console.WriteLine(c.Value);
                InOrder(c.RightNode);
            }
        }

        public IEnumerator<Node> GetEnumerator()
        {
            if (IsRNLTraversal)
            {
                return new RNLEnumerator(this);
            }
            return new LNREnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (IsRNLTraversal)
            {
                return new RNLEnumerator(this);
            }
            return new LNREnumerator(this);
        }

        class LNREnumerator : IEnumerator<Node>
        {
            private BinaryTree tree;
            private Stack<Node> treeStack = new Stack<Node>();

            public LNREnumerator(BinaryTree tree)
            {
                this.tree = tree;
                pushToStack(tree.Root);
            }

            private void pushToStack(Node c)
            {
                if (c != null)
                {
                    pushToStack(c.RightNode);
                    treeStack.Push(c);
                    pushToStack(c.LeftNode);
                }
            }

            public object Current
            {
                get
                {
                    return treeStack.Pop();
                }
            }

            Node IEnumerator<Node>.Current
            {
                get
                {
                    return treeStack.Pop();
                }
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (treeStack.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                
            }
        }
        class RNLEnumerator : IEnumerator<Node>
        {
            private BinaryTree tree;
            private Stack<Node> treeStack = new Stack<Node>();

            public RNLEnumerator(BinaryTree tree)
            {
                this.tree = tree;
                pushToStack(tree.Root);
            }

            private void pushToStack(Node c)
            {
                if (c != null)
                {
                    pushToStack(c.LeftNode);
                    treeStack.Push(c);
                    pushToStack(c.RightNode);
                }
            }

            public object Current
            {
                get
                {
                    return treeStack.Pop();
                }
            }

            Node IEnumerator<Node>.Current
            {
                get
                {
                    return treeStack.Pop();
                }
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (treeStack.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public void Reset()
            {

            }
        }
    }

    class Node : IComparable
    {
        public int Value { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is Node)
            {
                return this.Value.CompareTo((obj as Node).Value);
            }
            else
            {
                return -1;
            }
        }

    }
}
