using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class Noeud : IComparable
{
    public List<int> restant;
    public String equation = " ";
    public int total;
    public int numberOfNodesExploredDFS = 0;
    public int numberOfNodesExploredBFS = 0;
    List<int> numbers = new List<int>();

    public Noeud(List<int> list, String equation, int total)
    {
        this.restant = list;
        this.equation = equation;
        this.total = total;
    }

    public int CompareTo(object obj)
    {
        Noeud tmp = (Noeud)obj;
        if (numbers.Count < tmp.numbers.Count)
            return -1;
        if (numbers.Count > tmp.numbers.Count)
            return 1;
        //tmp: //[2,8,20,5,4,2]
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] < tmp.numbers[i])
                return -1;
            if (numbers[i] > tmp.numbers[i])
                return 1;
        }
        return 0;
    }

    public int GetNodeDFS()
    {
        return numberOfNodesExploredDFS;
    }

    public String DFS(int goal)
    {
        Stack<Noeud> stack = new Stack<Noeud>();
        Noeud closeNode = new Noeud(new List<int>(), "", 0);

        //SortedSet<Noeud> visited = new SortedSet<Noeud>();
        HashSet<Noeud> visited = new HashSet<Noeud>();

        int min_diff = goal;
        int actual_diff = 0;
        int numberOfNodesExplored1 = 0;
        stack.Push(this);

        while (stack.Count != 0)
        {
            Noeud node = stack.Pop();

            if (!visited.Contains(node))
            {
                visited.Add(node);
                numberOfNodesExplored1 = numberOfNodesExplored1 + 1;

                actual_diff = Math.Abs(goal - node.total);

                if (node.IsGoal(goal))
                {
                    numberOfNodesExploredDFS = numberOfNodesExplored1;
                    return "Les equations sont : \r\n" + node.equation; //+ "   / " + numberOfNodesExploredDFS;
                }
                else
                {
                    if (actual_diff < min_diff)
                    {
                        min_diff = actual_diff;
                        closeNode = node;
                    }

                    foreach (Noeud n in node.Successeur())
                    {
                        stack.Push(n);
                    }
                }
            }
        }

        numberOfNodesExploredDFS = numberOfNodesExplored1;

        return "Pas d'equation trouver. \r\nLe numero le plus proche qu'on a pu vise est : " + closeNode.total + " avec les equations suivante:\r\n" + closeNode.equation; // + " / " + numberOfNodesExploredDFS;
    }
