using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HuffmanCodingText
{
    public partial class HuffmanText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initTable();
        }

        protected void go_Click(object sender, EventArgs e)
        {
            //The text entered by the user
            String inputText = enterText.Text;
            //The huffman coding class. Performs the coding algorithm on init
            HuffmanCoding h = new HuffmanCoding(inputText);

            //generate the dictionary
            //populate the dictionary table
            String[,] characterDictionary = h.generateDictionary();
            populateTable(characterDictionary);
            int compressed = h.compressedBits();
            int uncompressed = h.uncompressedBits();
            int uncompressedASCII = uncompressed / 2;
            uncompressedBitStream.Text = h.uncompressedStream();
            compressedBitStream.Text = h.compressedStream();
            uncompressedBits.Text = uncompressed.ToString();
            uncompressedBitsAscii.Text = (uncompressedASCII).ToString(); //ASCII requires half the bits - 8 vs 16
            compressedBits.Text = compressed.ToString();
            compressionRatio.Text = (1.0 - (double)compressed / uncompressed).ToString();
            compressionRatioAscii.Text = (1.0 - (double)compressed / uncompressedASCII).ToString();
            entropy.Text = h.getEntropy().ToString();
            entropyTotal.Text = h.getTotalBits().ToString();
        }

        //add titles to the table
        protected void initTable()
        {
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            cell1.Text = "Char";
            cell2.Text = "Count";
            cell3.Text = "Encoding";
            TableRow row = new TableRow();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            dictionaryTable.Rows.Add(row);
        }

        //populate the table with the dictionary
        protected void populateTable(String[,] dict)
        {
            for (int i = 0; i < dict.GetLength(1); i++ )
            {
                TableRow row = new TableRow();
                TableCell charCell = new TableCell();
                charCell.Text = dict[0,i];
                TableCell encodingCell = new TableCell();
                encodingCell.Text = dict[2,i];
                TableCell countCell = new TableCell();
                countCell.Text = dict[1, i];
                row.Cells.Add(charCell);
                row.Cells.Add(countCell);
                row.Cells.Add(encodingCell);
                dictionaryTable.Rows.Add(row);
            }
        }
    }
}