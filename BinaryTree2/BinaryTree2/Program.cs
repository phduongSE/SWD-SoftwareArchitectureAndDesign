using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree2
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();

            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                tree.AddValue(rd.Next(1, 100));
            }

            tree.InOrder(tree);

            Console.WriteLine("==================================");
            //tree.IsRNLEnumerator = true;
            foreach (Node node in tree)
            {
                Console.WriteLine(node.Value);
            }


        }

        class BinaryTree : IEnumerable, IComparable
        {
            private BinaryTree Left;
            private BinaryTree Right;
            private Node Root;

            public bool IsRNLEnumerator { get; set; }

            public void AddValue(int value)
            {
                BinaryTree newNode = new BinaryTree
                {
                    Root = new Node(value)
                };
                BinaryTree current = this, prev = null;

                while (current != null)
                {
                    prev = current;
                    if (newNode.CompareTo(current) < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (Root == null)
                {
                    this.Root = newNode.Root;
                }
                else if (newNode.CompareTo(prev) < 0)
                {
                    prev.Left = newNode;
                }
                else
                {
                    prev.Right = newNode;
                }
            }

            public int CompareTo(object obj)
            {
                if (obj is BinaryTree)
                {
                    return this.Root.CompareTo((obj as BinaryTree).Root);
                }
                else
                {
                    return -1;
                }
            }

            public void InOrder(BinaryTree c)
            {
                if (c != null)
                {
                    InOrder(c.Left);
                    Console.WriteLine(c.Root.Value);
                    InOrder(c.Right);
                }
            }

            public IEnumerator GetEnumerator()
            {
                if (IsRNLEnumerator)
                {
                    return new RNLEnumerator(this);
                }
                return new LNREnumerator(this);
                //if (this.Left != null)
                //{
                //    foreach (Node node in this.Left)
                //    {
                //        yield return node;
                //    }
                //}

                //yield return this.Root;

                //if (this.Right != null)
                //{
                //    foreach (Node node in this.Right)
                //    {
                //        yield return node;
                //    }
                //}
            }

            class LNREnumerator : IEnumerator
            {
                private BinaryTree tree;
                private Stack<Node> treeStack = new Stack<Node>();
                public LNREnumerator(BinaryTree tree)
                {
                    this.tree = tree;
                    pushToStack(tree);
                }

                private void pushToStack(BinaryTree c)
                {
                    if (c != null)
                    {
                        pushToStack(c.Right);
                        treeStack.Push(c.Root);
                        pushToStack(c.Left);
                    }

                }

                public object Current
                {
                    get
                    {
                        return treeStack.Pop();
                    }
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
                    throw new NotImplementedException();
                }
            }

            class RNLEnumerator : IEnumerator
            {
                private BinaryTree tree;
                private Stack<Node> treeStack = new Stack<Node>();
                public RNLEnumerator(BinaryTree tree)
                {
                    this.tree = tree;
                    pushToStack(tree);
                }

                private void pushToStack(BinaryTree c)
                {
                    if (c != null)
                    {
                        pushToStack(c.Left);
                        treeStack.Push(c.Root);
                        pushToStack(c.Right);
                    }

                }

                public object Current
                {
                    get
                    {
                        return treeStack.Pop();
                    }
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
                    throw new NotImplementedException();
                }
            }
        }

        class Node : IComparable
        {
            public int Value { get; set; }

            public Node(int value)
            {
                Value = value;
            }

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
}
