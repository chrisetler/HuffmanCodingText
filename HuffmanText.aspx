<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuffmanText.aspx.cs" Inherits="HuffmanCodingText.HuffmanText" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="huffmanCSS.css" />
</head>
<body>
    <div id="welcome">
            <h1>Huffman Coding</h1> Enter a string of Unicode text, and this page will compress it using a huffman coding algorithm, and display some statistics regarding the text and its compression.
    </div>

    <form id="form1" runat="server">
    <div id="everything">
        <div id="leftSide">
            <br />
        
            <div id="userInput">
                <asp:TextBox ID="enterText" runat="server" Width="800" Height="300" Wrap="true" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Button ID="go" runat="server" Text="GO" OnClick="go_Click"/>
                <br />
            </div>
        
            <div id="streams">
                <table border="1">
                    <tr>
                        <td>Uncompressed Input (utf-16)</td>
                        <td>Compressed Input</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="uncompressedBitStream" runat="server" Width="400" Height="200" Wrap="true" TextMode="MultiLine"></asp:TextBox></td>
                        <td><asp:TextBox ID="compressedBitStream" runat="server" Width="400" Height="200" Wrap="true" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="rightSide">
             <div id="statsBox">
                        <table border="1">
                <tr><td>Statistics</td></tr>
                <tr>
                    <td>Uncompressed Bits</td>
                    <td><asp:Label ID ="uncompressedBits" runat="server" Text="     "></asp:Label></td>
                
                </tr>
                <tr>
                    <td>Uncompressed Bits (ascii)</td>
                    <td><asp:Label ID ="uncompressedBitsAscii" runat="server" Text="     "></asp:Label></td>
                </tr>
                <tr>
                    <td>Compressed Bits</td>
                    <td><asp:Label ID ="compressedBits" runat="server" Text="     "></asp:Label></td>
                </tr>
                <tr>
                    <td>Compression Ratio</td>
                    <td><asp:Label ID ="compressionRatio" runat="server" Text="     "></asp:Label></td>
                </tr>
                <tr>
                    <td>Compression Ratio (ascii)</td>
                    <td><asp:Label ID ="compressionRatioAscii" runat="server" Text="     "></asp:Label></td>
                </tr>
                <tr>
                    <td>Entropy (minumum bits per character)</td>
                    <td><asp:Label ID ="entropy" runat="server" Text="     "></asp:Label></td>
                </tr>
                <tr>
                    <td>Theoretical minimum stream size</td>
                    <td><asp:Label ID="entropyTotal" runat="server" Text="    "></asp:Label></td>
                </tr>
            </table>
            </div>
            <br />
           
            <div id="dictionary">
                 <asp:Table ID="dictionaryTable" runat="server" BorderStyle="Solid"  ></asp:Table>
            </div>
        </div>
       
    
    </div>
    </form>
</body>
</html>
