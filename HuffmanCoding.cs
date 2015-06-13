using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//should take in a string and produce an array of char values and coressponing appearences. t
//Then sort and run the algorithm
namespace HuffmanCodingText
{
    public class HuffmanCoding
    {
        String stream;
        private List<BTreeNode<char>> nodeCharList = new List<BTreeNode<char>>();
        private List<int> nodeP =  new List<int>();
        private List<String> encodingNumber;
        private double entropy;
        private double totalBits; //entropy * num of characters
        int charCount;
        public HuffmanCoding(String textStream)
        {
            stream = textStream;
            //textStream = the text to compress
            //for each uniqe character in the array, add it to the list
            List<char> textStreamArray = textStream.ToList();
            nodeCharList = new List<BTreeNode<char>>();
            nodeP = new List<int>();
            encodingNumber = new List<String>();
            charCount = 0;
            while (textStreamArray.Count > 0)
            {
                int count = 1;
                char charToCount = textStreamArray[0];
                textStreamArray.Remove(charToCount);
                int i = 0;
                while (i < textStreamArray.Count)
                {
                    if (textStreamArray[i] == charToCount)
                    {
                        count++;
                        textStreamArray.Remove(charToCount);
                        continue;
                    }
                    i++;
                }
                //Object[] nodeVal = {charToCount, count};
                BTreeNode<char> newNode = new BTreeNode<char>(charToCount);
                nodeCharList.Add(newNode);
                nodeP.Add(count);
                charCount++;
            }
            //sort the nodes
            sort();
            //on init, run the algorithm
            generateEncodingNumbers();
            //calculate entropy
            calculateEntropy();
        }
        public String genTable()
        {
            String s = "";
            for (int i = 0; i < charCount; i++)
            {
                s += nodeCharList[i].nodeValue.ToString() + " " + nodeP[i].ToString()+ " " +encodingNumber[i].ToString() + "\n";
            }
            return s;
        }

        //the crux of the algorithm
        //creates a tree generate the dictionary
        private void generateEncodingNumbers()
        {
            List<BTreeNode<char>> nodeListTemp = new List<BTreeNode<char>>();
            BTreeNode<char>[] nodeListArray = new BTreeNode<char>[charCount];
            nodeCharList.CopyTo(nodeListArray);
            nodeListTemp = nodeListArray.ToList<BTreeNode<char>>();
            List<int> nodePTemp = new List<int>();
            int[] nodePArray = new int[charCount];
            nodeP.CopyTo(nodePArray);
            nodePTemp = nodePArray.ToList<int>();

            //while there are still more than one node without a parent, add more nodes
            while (nodeListTemp.Count > 1)
            {
                //scan for smallest and second smallest nodeP
                int smallestNode = 2147483647;
                int indexOfSmallest =-1;
                int indexOfSecondSmallest = -1;
                //find the smallest node that doesn't have a parent
                for (int i = 0; i < nodePTemp.Count; i++)
                {
                    if (nodePTemp[i] < smallestNode)
                    {
                        smallestNode = nodePTemp[i];
                        indexOfSmallest = i;
                    }
                }
                //find the second largest node without a parent
                int secondSmallestNode = 2147483647;
                for (int i = 0; i < nodePTemp.Count; i++)
                {
                    if (nodePTemp[i] < secondSmallestNode && i != indexOfSmallest)
                    {
                        secondSmallestNode = nodePTemp[i];
                        indexOfSecondSmallest = i;
                    }
                }
                //add a new node to the list, and make it a parent of these two nodes. The one with the larger value goes on the right.
                BTreeNode<char> newNode = new BTreeNode<char>(' ');
                int newP = nodePTemp[indexOfSmallest] + nodePTemp[indexOfSecondSmallest];
                nodeListTemp.Add(newNode);
                nodePTemp.Add(newP);
                nodeListTemp[nodeListTemp.Count - 1].addLeftChild(nodeListTemp[indexOfSecondSmallest]);
                nodeListTemp[nodeListTemp.Count - 1].addRightChild(nodeListTemp[indexOfSmallest]);

                //remove the nodes that were added to the tree
                nodeListTemp.RemoveAt(indexOfSmallest);
                nodePTemp.RemoveAt(indexOfSmallest);
                indexOfSecondSmallest = (indexOfSmallest > indexOfSecondSmallest)? indexOfSecondSmallest:indexOfSecondSmallest-1;
                nodeListTemp.RemoveAt(indexOfSecondSmallest);
                nodePTemp.RemoveAt(indexOfSecondSmallest);
                
            }
            //now that the tree is generated, transverse it starting at each of the children (from 0..charCount), and determine which side they are at each point, the flip that string to get the encoding value.
            for (int i = 0; i < charCount; i++)
            {
                String s = "";
                
                BTreeNode<char> currentNode = nodeCharList[i];
                while (currentNode.hasParent)
                {
                    if (currentNode.onParentSide == BTreeNode<int>.LEFT_SIDE) s += "1";
                    else s += 0;
                    currentNode = currentNode.getParent();
                }
                //the string now has to be reversed
                char[] arr = s.ToArray<char>();
                Array.Reverse(arr);
                s = new String(arr);
                encodingNumber.Add(s);
            }
            


        }
        //The number of nodes WO parents - i.e. not in the tree
        private int NodesWOParents()
        {
            int nodesWOParents = 0;
            for (int i = 0; i < nodeCharList.Count; i++)
            {
                if (!nodeCharList[i].hasParent) nodesWOParents++;
            }
            return nodesWOParents;
        }

        //return a double array of Strings of characters and corresponding encoding numbers
        public String[,] generateDictionary()
        {
            String[,] dictionary = new String[3 , charCount];
            for(int i=0; i<charCount; i++){
                dictionary[0,i] = nodeCharList[i].nodeValue.ToString();
                dictionary[1, i] = nodeP[i].ToString();
                dictionary[2,i] = encodingNumber[i];
            }
            return dictionary;
        }

        //selection sort on both nodeP and charList (should be using a comparator this is fine for now).
        private void sort()
        {
            int sorted = 0;
           
            while (sorted < charCount)
            {
                int greatest = 0;
                int greatestIndex=0;
                //find the greatest and move it to the front
                for (int current = sorted; current < charCount; current++)
                {
                    if (nodeP[current] > greatest)
                    {
                        greatest = nodeP[current];
                        greatestIndex = current;
                    }
                }
                nodeP.Insert(sorted, nodeP[greatestIndex]);
                nodeP.RemoveAt(greatestIndex + 1);
                nodeCharList.Insert(sorted, nodeCharList[greatestIndex]);
                nodeCharList.RemoveAt(greatestIndex + 1);
                sorted++;
            }
        }
        public int uncompressedBits()
        {
            return stream.Length * 16; //16 bits per char in utf-16
        }
        public int compressedBits()
        {
            return compressedStream().Length;
        }
        public String uncompressedStream()
        {
            Char[] textArray = stream.ToArray<Char>();
            String output = "";
            foreach (char x in textArray){
                int unicodeVal = (int)x;
                String binaryCode = binStr(unicodeVal);
                output += binaryCode;
            }
            return output;
        }
        public String compressedStream()
        {
            Char[] textArray = stream.ToArray<Char>();
            String output = "";
            int index = -1;
            foreach (char x in textArray)
            {
                for (int i = 0; i < charCount; i++)
                {
                    if (x.Equals(nodeCharList[i].nodeValue)){
                        index = i;
                        output += encodingNumber[index];
                        break;
                    }
                }
                
            }
            return output;
        }

        //return a 16-bit (for utf16) binary string of an integer
        private String binStr(int x)
        {
            String output = "";
            while (x != 0)
            {
                if ((x & 1) == 1)
                {
                    output += "1";
                }
                else output += "0";
                x = x >> 1;
            }
            while (output.Length < 16)
            {
                output += "0";
            }
            //now reverse it
            char[] arr = output.ToArray<char>();
            Array.Reverse(arr);
            output = new String(arr);
            return output;
        }
        //calculate entropy
        private void calculateEntropy()
        {
            int totalChars = stream.Length;
            double ent = 0;
            for (int i = 0; i < charCount; i++)
            {
                double P = (double)nodeP[i] / totalChars;
                ent += P * Math.Log(P,2);
            }
            entropy = -ent;
        }
        public double getEntropy()
        {
            return entropy;
        }
        public double getTotalBits()
        {
            return entropy * stream.Length;
        }
    }
}