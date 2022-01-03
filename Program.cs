
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Xml;
using System.Windows;
using System.Globalization;
using System.Xml.Serialization;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Pinyin
{
    unsafe class Program
    {
        //static unsafe void RetrieveT(XmlReader xr_)
        //{
        //    sb_.Length = 0;
        //    bool flag_HasPreserved = false;
        //    LastElement LE = LastElement.None;
        //    string appendage = "";
        //    int index = 0;
        //    string accumulated = "";
        //    while (xr_.Read() && !(xr_.NodeType == XmlNodeType.EndElement && xr_.LocalName == "p") )
        //    {
        //
        //        switch (xr_.NodeType)
        //        {
        //            case XmlNodeType.Text:
        //                if (xr_.Value.StartsWith(" HY")) continue;
        //                if (LOGIC_TOBECHANGED(val, appendage))
        //                {
        //                    //if (xr_.Value[xr_.Value.Length - 1] == '經')
        //                   //     System.Windows.Forms.MessageBox.Show(xr_.Value);
        //                    if (flag_HasPreserved)
        //                    {
        //                        sb_.Append(string.Concat(LE == LastElement.PlainText?"</w:t></w:r>":"", "<w:r><w:rPr>" , appendage, CurrentrPr, "<w:t xml:space=\"preserve\">", xr_.Value, "</w:t></w:r>"));
        //                        LE = LastElement.Run;
        //                        flag_HasPreserved = false;
        //                        break;
        //                    }
        //                    sb_.Append(string.Concat(LE == LastElement.Run || LE == LastElement.None ? string.Concat("<w:r><w:rPr>", CurrentrPr, "<w:t>"):"", xr_.Value));
        //                    LE = LastElement.PlainText;
        //                    break;
        //                }
        //                if (LE == LastElement.PlainText)
        //                {
        //                    sb_.Append("</w:t></w:r>");
        //                }
        //                fixed (char* c = accumulated + xr_.Value)
        //                {
        //                    int length = *((int*)c - 1);
        //                    for (int i = 0; i < length; i++)
        //                    {
        //                        if (!PinyinC.PinyinC.IsValid(*(c + i)))
        //                        {
        //                            if (LE != LastElement.Invalid)
        //                            {
        //                                if (flag_HasPreserved)
        //                                {
        //                                    sb_.Append(string.Concat(LE == LastElement.Ruby ? "</w:r>" : "", "<w:r><w:rPr>", appendage, CurrentrPr, "<w:t xml:space=\"preserve\">"));
        //                                    flag_HasPreserved = false;
        //                                }
        //                                else
        //                                {
        //                                    sb_.Append(string.Concat(LE == LastElement.Ruby ? "</w:r>" : "", "<w:r><w:rPr>", CurrentrPr, "<w:t>"));
        //                                }
        //                            }
        //                            sb_.Append(*(c + i));
        //                            LE = LastElement.Invalid;
        //                            continue;
        //                        }
        //                        isHigh = char.IsHighSurrogate(*(c + i));
        //                        if (Decide(c, length - 1 == i, i, &index, ref accumulated))
        //                        {
        //                            continue;
        //                            //break;
        //                        }
        //                        if (LE == LastElement.Invalid)
        //                        {
        //                            sb_.Append("</w:t></w:r>");
        //                        }
        //
        //                        if (flag_HasPreserved)
        //                        {
        //                            sb_.Append(string.Concat(LE != LastElement.Ruby ? "<w:r>" : "", "<w:ruby><w:rubyPr><w:rubyAlign w:val=\"center\"/><w:hps w:val=\"10\"/><w:hpsRaise w:val=\"", val - 2, "\"/><w:hpsBaseText w:val=\"", val, //<w:color w:val=\"000000\"/>
        //                                "\"/><w:lid w:val=\"zh - TW\"/></w:rubyPr><w:rt><w:r><w:rPr><w:rStyle w:val=\"byline1\"/><w:rFonts w:ascii=\"Malgun Gothic\" w:eastAsia=\"Malgun Gothic\" w:hAnsi=\"Malgun Gothic\" w:cs=\"Malgun Gothic\"/><w:b/><w:bCs/><w:w w:val=\"75\"/><w:sz w:val=\"10\"/><w:lang w:eastAsia=\"zhTW\"/></w:rPr><w:t>"
        //                                , isHigh ? PinyinC.PinyinC.C2P(*(c + i), *(c + i + 1), false, ref i, index) : PinyinC.PinyinC.C2P(*(c + i), false, index), "</w:t></w:r></w:rt><w:rubyBase><w:r><w:rPr>", appendage, CurrentrPr,
        //                                "<w:t xml:space=\"preserve\">", isHigh ? xr_.Value : (*(c + i)).ToString(), "</w:t></w:r></w:rubyBase></w:ruby>"));
        //                            flag_HasPreserved = false;
        //                        }
        //                        else
        //                        {
        //                            sb_.Append(string.Concat(LE != LastElement.Ruby ? "<w:r>" : "", "<w:ruby><w:rubyPr><w:rubyAlign w:val=\"center\"/><w:hps w:val=\"10\"/><w:hpsRaise w:val=\"", val - 2, "\"/><w:hpsBaseText w:val=\"", val, //<w:color w:val=\"000000\"/>
        //                                "\"/><w:lid w:val=\"zh - TW\"/></w:rubyPr><w:rt><w:r><w:rPr><w:rStyle w:val=\"byline1\"/><w:rFonts w:ascii=\"Malgun Gothic\" w:eastAsia=\"Malgun Gothic\" w:hAnsi=\"Malgun Gothic\" w:cs=\"Malgun Gothic\"/><w:b/><w:bCs/><w:w w:val=\"75\"/><w:sz w:val=\"10\"/><w:lang w:eastAsia=\"zhTW\"/></w:rPr><w:t>"
        //                                , isHigh ? PinyinC.PinyinC.C2P(*(c + i), *(c + i + 1), false, ref i, index) : PinyinC.PinyinC.C2P(*(c + i), false, index), "</w:t></w:r></w:rt><w:rubyBase><w:r><w:rPr>", CurrentrPr,
        //                                "<w:t>", isHigh? xr_.Value : (*(c + i)).ToString(), "</w:t></w:r></w:rubyBase></w:ruby>"));
        //                        }
        //                        LE = LastElement.Ruby;
        //                    }
        //                }
        //                break;
        //            case XmlNodeType.Element:
        //             //   MessageBox.Show(xr_.LocalName);
        //                switch (xr_.LocalName)
        //                {
        //                    case "rPr":
        //                        CurrentrPr = xr_.ReadOuterXml();
        //                        //MessageBox.Show(CurrentrPr);
        //                        //MessageBox.Show(xr_.ReadOuterXml());
        //                        //<w:color w:val=\"000000\"/>
        //                        CurrentrPr = string.Concat(CurrentrPr.Substring(CurrentrPr.IndexOf("<w:pPr>", 20) + 1));
        //                        val = RetrieveVal(CurrentrPr, 'z', ' ', 8, 0);
        //                        if (CurrentrPr.Contains("foot"))
        //                            MessageBox.Show("Yes");
        //                        //MessageBox.Show(CurrentrPr);
        //                        break;
        //                    case "footnoteReference":
        //                        MessageBox.Show("Yes");
        //                        // Expecting any comments or annotations to be for ruby text only.
        //                        sb_.Append(string.Concat("</w:r><w:r><w:rPr><w:rStyle w:val=\"FootnoteReference\" /><w:sz w:val=\"", val, "\" /><w:szCs w:val=\"", val, "\" /></w:rPr><w:footnoteReference w:id=\"", xr_.GetAttribute(0), "\" /></w:r>"));
        //                        LE = LastElement.None;
        //                        appendage = "";
        //                        break;
        //                    case "br":
        //                        MessageBox.Show("Yes");
        //                        sb_.Append(string.Concat(LE == LastElement.Ruby ? "</w:r>" : LE == LastElement.PlainText ||  LE == LastElement.Invalid ? "</w:t></w:r>" : "", "<w:r><w:rPr><w:sz w:val=\"", val, "\" /><w:szCs w:val=\"", val, "\" /><w:lang w:eastAsia=\"zh - TW\" /></w:rPr><w:br /></w:r>"));
        //                        LE = LastElement.None;
        //                        break;
        //                    case "t":
        //                        MessageBox.Show("Yes");
        //                        if (xr_.AttributeCount > 0 && xr_.GetAttribute(0) == "preserve")
        //                        {
        //                            flag_HasPreserved = true;
        //                        }
        //                        break;
        //                    case "b":
        //                        MessageBox.Show("Yes");
        //                        appendage += "<w:b/><w:bCs/>";
        //                        break;
        //                    case "rStyle":
        //                        MessageBox.Show("Yes");
        //                        appendage = string.Concat("<w:rStyle w:val=\"", xr_.GetAttribute(0), "\"/>");
        //                        break;
        //                    default:
        //                     //   appendage = ""
        //                        break;
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    switch (LE)
        //    {
        //        case LastElement.Invalid:
        //        case LastElement.PlainText:
        //            sb_.Append("</w:t></w:r>");
        //            break;
        //        case LastElement.Ruby:
        //            sb_.Append("</w:r>");
        //            break;
        //    }
        //}


        //******************TOTALLY NEW ALGORITHM BEYOND THIS LINE*******************
        //Code reference here : https://referencesource.microsoft.com/#system.xml/System/Xml/Core/XmlWriter.cs,4a6f6354dccc895f,references

        //AIM : Less instructions in every loop -> Higher performance and efficiency; 

        public static void WriteAttributes(XmlReader reader, XmlWriter writer, bool defattr, bool getjc = false)
        // public static void WriteAttributes(XmlReader reader, XmlWriter writer, bool defattr)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
            {
                if (reader.MoveToFirstAttribute())
                {
                    WriteAttributes(reader, writer, defattr, getjc);
                    reader.MoveToElement();
                }
            }
            else if (reader.NodeType != XmlNodeType.Attribute)
            {
                MessageBox.Show("Attribute error : WriteAttributes();");
            }
            else
            {
                do
                {
                    // we need to check on both XmlReader.IsDefault and XmlReader.SchemaInfo.IsDefault. 
                    // If either of these is true and defattr=false, we should not write the attribute out
                    if (defattr || !(reader.IsDefault || reader.SchemaInfo.IsDefault))
                    {
                        writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                        while (reader.ReadAttributeValue())
                        {
                            if (reader.NodeType == XmlNodeType.EntityReference)
                            {
                                writer.WriteEntityRef(reader.Name);
                            }
                            else
                            {
                                writer.WriteString(reader.Value);
                                if (getjc)
                                {
                                    jc = reader.Value;
                                    getjc = false;
                                }
                            }
                        }
                        writer.WriteEndAttribute();
                    }
                }
                while (reader.MoveToNextAttribute());
            }
        }

        public static string GetAttributes(XmlReader reader, XmlWriter writer)
        {
            string buffer = null;
            while (reader.MoveToNextAttribute())
            {
                buffer += " " + reader.Name;
                while (reader.ReadAttributeValue())
                {
                    buffer += "=\"" + reader.Value + "\"";
                }
            }

            return buffer;
        }

        public static void ReadAttributes(XmlReader reader, XmlWriter writer, bool defattr, ref StringBuilder bufappend, bool getval = false, bool getjc = false)
        {
            //Trying to keep as less instructions as possible.
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
            {
                if (reader.MoveToFirstAttribute())
                {
                    ReadAttributes(reader, writer, defattr, ref bufappend, getval);
                    reader.MoveToElement();
                }
                _crpr.Append("/>");
                return;
            }
            else if (reader.NodeType != XmlNodeType.Attribute)
            {
                MessageBox.Show("Attribute error : ReadAttributes();");
                return;
            }
            else
            {
                do
                {
                    // we need to check both XmlReader.IsDefault and XmlReader.SchemaInfo.IsDefault. 
                    // If either of these is true and defattr=false, we should not write the attribute out
                    if (defattr || !(reader.IsDefault || reader.SchemaInfo.IsDefault))
                    {
                        bufappend.Append(string.Concat(" ", reader.Name));
                        writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                        while (reader.ReadAttributeValue())
                        {
                            if (reader.NodeType == XmlNodeType.EntityReference)
                            {
                                writer.WriteEntityRef(reader.Name);
                            }
                            else
                            {
                                bufappend.Append(string.Concat("=\"", reader.Value, "\""));
                                writer.WriteString(reader.Value);
                                if (getval)
                                {
                                    sz = Convert2Int(reader.Value) / 2;
                                    getval = false;
                                }
                            }
                        }
                        writer.WriteEndAttribute();
                    }
                }
                while (reader.MoveToNextAttribute());
            }

        }

        static string attribBuf = null;
        static int readlength = 0;
        static int threshold = 15;
        static bool start = false;
        static string jc = null;
        static int sz = 10; //Default is 10
        static bool ISHIGHCHAR = false;
        static bool ISINRPR = false;
        static bool ISLASTTITLE = false;
        static string temps = null;
        static StringBuilder _crpr = new StringBuilder(350); //Not expecting the buffer size for rPr(Run Properties) to be over 512bytes
        static StringBuilder _txtbuf = new StringBuilder(300); //Not expecting texts to be over 300bytes per run node. This can be altered to fit users' needs.
        static LastNode LN = LastNode.None;
        static char[] stack = new char[4];
        static short ind_stack = 0;
        static bool end = false;
        static string accumulated = "";
        enum LastNode { Valid, Invalid, None }

        public static void Reset(XmlWriter xw, XmlReader xr, MemoryStream ms, ZipArchive zf, ZipArchive zfw)
        {
            attribBuf = null;
            readlength = 0;
            threshold = 15;
            start = false;
            jc = null;
            temps = null;
            Array.Clear(stack, 0, 4);
            ind_stack = 0;
            sz = 10;
            end = false;
            ISHIGHCHAR = false;
            ISINRPR = false;
            ISLASTTITLE = false;
            _crpr.Clear();
            _txtbuf.Clear();
            accumulated = "";
            LN = LastNode.None;
            xw.Close();
            xr.Close();
            ms.Close();
            zf.Dispose();
            zfw.Dispose();
        }
        public static unsafe bool Filter(bool initial, char* c = null, bool isHigh = false)
        {
            return jc == "right" || !(PinyinC.PinyinC.IsValid(*c) && sz <= threshold && start);
        }

        private static string Get()
        {
            StringBuilder tmp = new StringBuilder("", 4);
            for (int i = ind_stack; i <= 3; i++)
            {
                tmp.Append(stack[i]);
            }
            for (int i = 0; i < ind_stack; i++)
            {
                tmp.Append(stack[i]);
            }
            return tmp.ToString();
        }

        public static int valid_count(char* text, int index, int max_length)
        {
            int c = 0;
            for (int i = 0; i < max_length; i++)
            {
                if (PinyinC.PinyinC.IsValid(*(text + index + i)))
                    c++;
            }
            return c;
        }

        public static bool Execute(char* c, XmlWriter writer, int *index, bool custom)
        {

            //Time complexity = O(1)
            readlength = *((int*)c - 1);
            for (int i = custom?0 : 4; i < readlength; i++)
            {
                ISHIGHCHAR = char.IsHighSurrogate(*(c + i));

                if (Filter(false, c + i, ISHIGHCHAR))
                {
                    if (LN != LastNode.Invalid)
                    {
                        //attribBuf += " xml:space=\"preserve\"";
                        if (LN == LastNode.Valid)
                        {
                            writer.WriteRaw(string.Concat(custom? "<w:r>" : "</w:r><w:r>", _crpr));
                        }
                        writer.WriteRaw(string.Concat("<w:t", attribBuf, ">"));
                        LN = LastNode.Invalid;
                    }
                    stack[ind_stack++] = *(c + i);
                    ind_stack *= ind_stack < 4 ? 1 : 0;
                    _txtbuf.Append(*(c + i));
                    continue;
                }
                if (Decide(c, i, index, ISHIGHCHAR, readlength - i, custom))
                {
                    return true;
                }
                //if (*(c + i) == '隐') Console.WriteLine(*(c + i - 1));
                stack[ind_stack++] = *(c + i);
                ind_stack *= ind_stack < 4 ? 1 : 0;
                if (LN == LastNode.Invalid)
                {
                    //Expecting content to be written via WriteRaw()
                    //Change this in case it is buggy
                    //writer.WriteRaw(string.Concat(_txtbuf, "</w:t>"));
                    writer.WriteRaw(string.Concat(_txtbuf, custom? "</w:t></w:r>": "</w:t>"));
                    _txtbuf.Length = 0;
                }
                //writer.WriteRaw(string.Concat("</w:r><w:r><w:ruby><w:rubyPr><w:rubyAlign w:val=\"center\"/><w:hps w:val=\"10\"/><w:hpsRaise w:val=\"", (sz - 1)*2, "\"/><w:hpsBaseText w:val=\"", sz, 
                // "\"/><w:lid w:val=\"zh - TW\"/></w:rubyPr><w:rt><w:r><w:rPr><w:rFonts w:ascii=\"Malgun Gothic\" w:eastAsia=\"Malgun Gothic\" w:hAnsi=\"Malgun Gothic\" w:cs=\"Malgun Gothic\"/><w:sz w:val=\"10\"/></w:rPr><w:t>"
                // , PinyinC.PinyinC.C2P(*(c + i), false), "</w:t></w:r></w:rt><w:rubyBase><w:r>", _crpr,
                // "<w:t>", *(c + i), "</w:t></w:r></w:rubyBase></w:ruby>"));
                //MessageBox.Show(sz.ToString());//Malgun Gothic
                writer.WriteRaw(string.Concat(custom?"" : "</w:r>", "<w:r><w:fldChar w:fldCharType =\"begin\"/></w:r><w:r>", _crpr, "<w:instrText> " +
                    "EQ \\* jc0 \\* &quot;Font:Malgun Gothic&quot; \\* hps", 10, " \\o \\ad(\\s\\up ", sz - 1, "(",
                    ISHIGHCHAR ? PinyinC.PinyinC.C2P(*(c + i), *(c + i + 1), false, ref i, *index) + ")," + *(c + i - 1) + *(c + i) : PinyinC.PinyinC.C2P(*(c + i),
                    false, *index) + ")," + *(c + i), ")</w:instrText></w:r><w:r><w:fldChar w:fldCharType=\"end\"/>", custom? "</w:r>" : ""));
                LN = LastNode.Valid;
            }
            return false;
        }
            static bool tmp = false;

        public static void WriteNode(XmlReader reader, XmlWriter writer, bool defattr, bool normal = true)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int d = reader.NodeType == XmlNodeType.None ? -1 : reader.Depth;
            int index = 0;
            do
            {
                //yMessageBox.Show("Test4 :" + reader.Name + (reader.NodeType == XmlNodeType.EndElement).ToString());
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (!normal)
                        {
                            switch (reader.LocalName)
                            {
                                case "r":
                                    writer.WriteRaw("<w:r>");
                                    LN = LastNode.None;
                                    break;
                                case "rPr":
                                    ISINRPR = true;
                                    _crpr.Length = 0;
                                    _crpr.Append("<w:rPr>");
                                    writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                                    if (reader.IsEmptyElement)
                                    {
                                        writer.WriteEndElement();
                                    }
                                    break;
                                //break;
                                case "t":
                                    attribBuf = GetAttributes(reader, writer);
                                    reader.Read();
                                    if (!start && sz == 22)
                                    {
                                        temps += reader.Value;
                                        ISLASTTITLE = true;
                                    }
                                    else if (ISLASTTITLE)
                                    {
                                        Console.Write("Start from here? (" + temps + ") y/N:");
                                        if (Console.ReadLine() == "y")
                                        {
                                            start = true;
                                        }
                                        temps = null;
                                        ISLASTTITLE = false;
                                    }
                                    fixed (char* c = Get() + accumulated + reader.Value)
                                    {
                                        if (!Execute(c, writer, &index, false))
                                            accumulated = "";
                                            //Console.WriteLine("Test");
                                    }
                                   
                                    break;
                                default:
                                    writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                                    if (ISINRPR)
                                    {
                                        //Using reader.name here just in case the node has different prefix, don't assume all prefixes to be w!
                                        _crpr.Append(string.Concat("<", reader.Name));
                                        ReadAttributes(reader, writer, defattr, ref _crpr, reader.LocalName == "sz");
                                    }
                                    else
                                        WriteAttributes(reader, writer, defattr);
                                    if (reader.IsEmptyElement)
                                    {
                                        writer.WriteEndElement();
                                    }
                                    break;
                            }
                        }
                        else
                        {
                         //   MessageBox.Show(reader.LocalName);
                            writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                            WriteAttributes(reader, writer, defattr, reader.LocalName == "jc" && start);
                            if (reader.IsEmptyElement)
                            {
                                writer.WriteEndElement();
                            }
                        }
                        break;
                    case XmlNodeType.Text:
                        //if (reader.CanReadValueChunk)
                        //{
                        //    if (writeNodeBuffer == null)
                        //    {
                        //        writeNodeBuffer = new char[1024];
                        //    }
                        //    int read;
                        //    while ((read = reader.ReadValueChunk(writeNodeBuffer, 0, 1024)) > 0)
                        //    {
                        //        writer.WriteChars(writeNodeBuffer, 0, read);
                        //    }
                        //}
                        //else
                            writer.WriteString(reader.Value);
                        break;
                    case XmlNodeType.Whitespace:
                    case XmlNodeType.SignificantWhitespace:
                        writer.WriteWhitespace(reader.Value);
                        break;
                    case XmlNodeType.CDATA:
                        writer.WriteCData(reader.Value);
                        break;
                    case XmlNodeType.EntityReference:
                        writer.WriteEntityRef(reader.Name);
                        break;
                    case XmlNodeType.XmlDeclaration:
                    case XmlNodeType.ProcessingInstruction:
                        writer.WriteProcessingInstruction(reader.Name, reader.Value);
                        break;
                    case XmlNodeType.DocumentType:
                        writer.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
                        break;
                    case XmlNodeType.Comment:
                        writer.WriteComment(reader.Value);
                        break;
                    case XmlNodeType.EndElement:

                       
                        if (!normal)
                        {
                            if (reader.LocalName == "rPr")
                            {
                                ISINRPR = false;
                                _crpr.Append("</w:rPr>");
                            }
                            else if (reader.LocalName == "t")
                            {

                                break;
                            }
                            else if (reader.LocalName == "r")
                            {
                                if (LN == LastNode.Invalid)
                                {
                                    writer.WriteRaw(string.Concat(_txtbuf, "</w:t>"));
                                    _txtbuf.Length = 0;
                                    LN = LastNode.None;
                                }
                                writer.WriteRaw("</w:r>");
                                break;
                            }
                        }
                        writer.WriteFullEndElement();
                        break;
                }
            } while (reader.Read() && (d < reader.Depth || (d == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
            end = true;


            if (accumulated != "")
            {
                //Console.WriteLine(accumulated);
                fixed (char* c = accumulated)
                {
                    Execute(c, writer, &index, true);
                    if (LN == LastNode.Invalid)
                    {
                        //Expecting content to be written via WriteRaw()
                        //Change this in case it is buggy
                        //writer.WriteRaw(string.Concat(_txtbuf, "</w:t>"));
                        writer.WriteRaw(string.Concat(_txtbuf, "</w:t></w:r>"));
                        _txtbuf.Length = 0;
                    }
                }
                LN = LastNode.None;
                accumulated = "";
            }
            
        }

        public struct form
        {
            public char key;
            public char[] pre;
            public char[] acc;
            public int index1;
            public int index2;
            public char[,] and1;
            public char[,] and2;
            public bool extend;
        };

        public static form[] format_ = new form[100];
        static int counter = 0;
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (args.Length < 1)
            {
                Console.WriteLine("No files inputted...\nUsage: {0} Files (.docx)");
                return;
            }
            Console.WriteLine("Initializing PinyinC...");
            PinyinC.PinyinC.initializeResource();
            Console.WriteLine("PinyinC Initialized!");
            Console.WriteLine("Opening file(s)...");
            MemoryStream ms = null;
            ZipArchive zf = null;
            ZipArchive zfw = null;
            XmlReader xr = null;
            XmlWriter xw = null;
            StreamReader format = File.OpenText("D:\\Coding\\Pinyin\\bin\\Debug\\format.txt");
       
            string tmc;
            //4800kb of memory!!!!!
            while ((tmc = format.ReadLine()) != null)
            {
                if (tmc[0] == ';' || tmc.Length < 5) continue;
                int i = 2;
                //  Console.WriteLine(1);
                format_[counter].key = tmc[0];
                format_[counter].pre = new char[4];
                format_[counter].acc = new char[4];
                format_[counter].and1 = new char[4, 4];
                format_[counter].and2 = new char[4, 4];
                for (short x = 0, y1 = 0, y2 = 0; i < tmc.Length && x <= 4 && y1 < 4 && y2 < 4; i++)
                {
                    if (tmc[i - 1] == '&' || tmc[i + 1] == '&')
                    {
                        format_[counter].and1[y1, y2++] = tmc[i];
                        if (tmc[i + 1] == '|')
                        {
                            y2 = 0;
                            y1++;
                        }
                        if (tmc[++i] == ' ')
                            break;
                        continue;
                    }
                    format_[counter].pre[x++] = tmc[i];
                    if (tmc[++i] == ' ')
                        break;
                }
                i += 1;
                // Console.WriteLine(3);
                format_[counter].index1 = tmc[i] - '0';
                i += 2;
                //  Console.WriteLine(4);
                for (short x = 0, y1 = 0, y2 = 0; i < tmc.Length && x <= 4 && y1 < 4 && y2 < 4; i++)
                {
                    // Console.WriteLine("I: {0}", i);
                    if (tmc[i - 1] == '&' || tmc[i + 1] == '&')
                    {
                        format_[counter].and2[y1, y2++] = tmc[i];
                        if (tmc[i + 1] == '|')
                        {
                            y2 = 0;
                            y1++;
                        }
                        if (tmc[++i] == ' ')
                            break;
                        continue;
                    }
                    format_[counter].acc[x++] = tmc[i];
                    if (tmc[++i] == ' ')
                        break;
                }
                //   Console.WriteLine("Length : {0}, i : {1}" , tmc.Length, i);
                //if (format_[counter].key == '邪') Console.WriteLine(tmc[++i] - '0');
                format_[counter].index2 = tmc[++i] - '0';
                format_[counter].extend = tmc[tmc.Length - 1] == '\\';
              //  if (format_[counter].key == '隐') Console.WriteLine(format_[counter].pre);
               // Console.WriteLine("{0} {1} {2}", format_[counter].key, format_[counter].extend, format_[counter].pre[0]);
                counter++;
                //Console.WriteLine(6);
            }
            tmc = null;
            format.Close();
            Stopwatch sw = null;
//            return;
            for (int d = 0; d < args.Length; d++)
            {
                string tempnm = null;
                bool trig = false;
                int s = args[d].LastIndexOf('\\');
                if (s == -1) s = 0;
                for (; s < args[d].Length; s++)
                {
                    if (args[d][s] == '：')
                        trig = false;
                    else if (args[d][s] == '第')
                        trig = true;
                    if (trig)
                    {
                        tempnm += args[d][s];
                    }
                }
                File.Copy(args[d], Directory.GetCurrentDirectory() + @"\" + tempnm + "(汉语版).docx", true);
                Console.WriteLine("Loading file into memory stream...");
                ms = new MemoryStream(File.ReadAllBytes(args[d]));
                zf = new ZipArchive(ms);
                zfw = ZipFile.Open(Directory.GetCurrentDirectory() + @"\" + tempnm + "(汉语版).docx", ZipArchiveMode.Update);
                zfw.GetEntry("word/document.xml").Delete();
                xr = XmlReader.Create(zf.GetEntry("word/document.xml").Open());
                xw = XmlWriter.Create(zfw.CreateEntry("word/document.xml").Open(), new XmlWriterSettings()
                {
                    CheckCharacters = false,
                    NewLineHandling = NewLineHandling.None
                });
                //xw.Flush();
                //XmlWriter xw = XmlWriter.Create(File.Create("lol.txt"), new XmlWriterSettings()
                //{
                //    CheckCharacters = false,
                //    NewLineHandling = NewLineHandling.None
                //});
                //XmlTextWriter xw = new XmlTextWriter(Console.Out);
                //xw.Formatting = Formatting.Indented;
                //xw.Flush();
                //xw.WriteRaw("");
                Console.Write("File : " + args[d] + "\nThreshold? 15 is default. Press enter to use default or specify one :");
                int temp_thre = Convert2Int(Console.ReadLine());
                if (!(temp_thre == 0))
                    threshold = temp_thre;
                Console.WriteLine("Making changes to file...");
                sw = Stopwatch.StartNew();
                while (xr.Read())
                {
                NODE_LOOP:
                    switch (xr.NodeType)
                    {
                        case XmlNodeType.Element:
                        ELEMENT_LOOP:
                            switch (xr.LocalName)
                            {
                                case "pPr":
                                    WriteNode(xr, xw, true);
                                    if (xr.NodeType == XmlNodeType.EndElement)
                                    {
                                        goto NODE_LOOP;
                                    }
                                    goto ELEMENT_LOOP;
                                case "r":
                                    do
                                    {
                                    SKIP_:
                                        if (xr.LocalName == "r")
                                        {
                                            WriteNode(xr, xw, true, false);
                                            if (xr.NodeType == XmlNodeType.Element) goto SKIP_;
                                        }
                                    }
                                    while (!(end) && xr.Read());
                                    goto NODE_LOOP;
                            }
                            xw.WriteStartElement(xr.Prefix, xr.LocalName, xr.NamespaceURI);
                            xw.WriteAttributes(xr, true);
                            if (xr.IsEmptyElement)
                            {
                                xw.WriteEndElement();
                                break;
                            }
                            break;
                        case XmlNodeType.EndElement:
                            xw.WriteFullEndElement();
                            break;
                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                            xw.WriteProcessingInstruction(xr.Name, xr.Value);
                            break;
                        case XmlNodeType.Whitespace:
                        case XmlNodeType.SignificantWhitespace:
                            xw.WriteWhitespace(xr.Value);
                            break;
                        default:
                            MessageBox.Show("Unknown XmlNodeType : " + xr.NodeType.ToString());
                            break;
                    }
                }
                //Garbage collection happens here. 
                Reset(xw, xr, ms, zf , zfw);
            }
            Console.WriteLine("Time elapsed : {0}", sw.Elapsed.TotalMilliseconds);
            sw.Stop();
            Console.ReadLine();
        }

        //public unsafe static int RetrieveVal(string source, char f_match, char f_match2, int len_1, int start = 0)
        //{
        //    fixed (char* c = source)
        //    {
        //        for (int i = start; i < source.Length; i++)
        //        {
        //            if (char.Equals(*(c + i), f_match) && char.Equals(*(c + i + 1), f_match2) && char.Equals(*(c + i + len_1), '"'))
        //            {
        //                return Convert2Int(c, i + len_1 + 1);
        //            }
        //        }
        //    }
        //    return -1;
        //}

        //Proved to be faster than int.Parse
        public static int Convert2Int(string chr)
        {
            int y = 0;
            for (int i = 0; i < chr.Length; i++)
            {
                y = y * 10 + (chr[i] - '0');
            }
            return y;
        }

        public static int strlen(char[,] text, int index)
        {
            int c = 0;
            while (text[index, c] != '\0')
            {
                c++;
            }
            return c;
        }
        
        public static bool check(char* text, char[,] and, int index, int len)
        {
            if (len == 0) return false;
            for (int i = 1; i <= len ; i++)
            {
                if (*(text + i) != and[index, i - 1] && and[index, i - 1] != '*')
                {
                    return false;
                }
            }
            return true;
        }

        public static bool reverse_check(char* text, char[,] and, int index, int len)
        {
            if (len == 0) return false;
            for (int i = 0, x = len; i < len && x != 0; i++, x--)
            {
                if (*(text - len) != and[index, i] && and[index, i] != '*')
                    return false;
            }
            return true;
        }

        
        public unsafe static bool Decide(char* text, int target_index, int* index, bool isHigh, int endlength, bool custom)
        {
            *index = 0;
            switch (*(text + target_index))
            {
                case '不':
                    if ((char.IsHighSurrogate(*(text + target_index + 1)) ? PinyinC.PinyinC.ExtractToneNumber(*(text + target_index + 1), *(text + target_index + 2)) : PinyinC.PinyinC.ExtractToneNumber(*(text + target_index + 1))) == 4)
                    {
                        *index = 1;
                    }
                    else if (*(text + target_index + 1) == '？' || *(text + target_index + 1) == '也')
                    {
                        *index = 2;
                    }
                    break;
                default:
                    for (int i = 0; i < counter; i++)
                    {
                        // Console.WriteLine(format_[i].key);
                        if (format_[i].key == *(text + target_index))
                        {
                            // Console.WriteLine("{0}, {1},{2}", *(text + target_index - 1), *(text + target_index + 1), (format_[i].pre[0] == *(text + target_index - 1)));
                            // Console.WriteLine("{0} {1} {2} {3}.", format_[i].key, format_[i].extend, format_[i].pre[0], text[target_index - 1]);
                            for (int s = 0; s < 4; s++)
                            {
                                if (format_[i].pre[s] == '*' || (format_[i].pre[s] != '\0' && (format_[i].pre[s] == *(text + target_index - 1))) || reverse_check(text + target_index, format_[i].and1, s, strlen(format_[i].and1, s)) )
                                {
                                    *index = format_[i].index1;
                                    return false;
                                }
                                else if (format_[i].acc[s] == '*' || (format_[i].acc[s] != '\0' && (format_[i].acc[s] == *(text + target_index + 1))) || check(text + target_index, format_[i].and2, s, strlen(format_[i].and2, s)) )
                                {
                                    *index = format_[i].index2;
                                    return false;
                                }
                              //  if (format_[i].key == '般')
                                //    Console.WriteLine("Test: {0} {1} {2} {3}", *(text + target_index + 1), check(text + target_index, format_[i].and2, s, strlen(format_[i].and2, s)), format_[i].acc[s], s);
                            }
                            if (!custom && endlength < 4)
                            {
                                accumulated = Marshal.PtrToStringAuto((IntPtr)(text + target_index), endlength);
                                return true;
                            }
                            if (!format_[i].extend)
                                break;
                        }

                    }
                    break;
            }
            return false;
            /*
            switch (*(text + target_index))
            {
                case '樂':
                    if (*(text + target_index - 1) == '音' || *(text + target_index - 1) == '伎' || *(text + target_index + 1) == '器')
                    {
                        *index = 1;
                        break;
                    }
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    //Here: Check on the value of i and see if it is pointing to the end of the char array
                    //      If so set the value to accumulated.
                    break;
                case '稽':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index + 1) == '首')
                    {
                        *index = 1;
                    }

                    break;
                case '般':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index + 1) == '若' || (*(text + target_index + 1) == '泥' && *(text + target_index + 2) == '洹'))
                    {
                        *index = 1;
                    }

                    break;
                case '若':
                    if (*(text + target_index - 1) == '般')
                    {
                        *index = 1;
                    }
                    break;
                case '葉':
                    if (*(text + target_index - 1) == '迦')
                    {
                        *index = 1;
                    }
                    break;
                case '不':

                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if ((char.IsHighSurrogate(*(text + target_index + 1)) ? PinyinC.PinyinC.ExtractToneNumber(*(text + target_index + 1), *(text + target_index + 2)) : PinyinC.PinyinC.ExtractToneNumber(*(text + target_index + 1))) == 4)
                    {
                        *index = 1;
                    }
                    else if (*(text + target_index + 1) == '？')
                    {
                        *index = 2;
                    }
                    break;
                case '邪':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index + 1) == '？')
                     {
                         *index = 1;
                     }
                    break;
                case '降':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index + 1) == '魔' || *(text + target_index + 1) == '伏')
                    {
                        *index = 1;
                    }
                    break;
                case '數':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index - 1) == '數' || *(text + target_index + 1) == '數')
                    {
                        *index = 2;
                    }
                    else if (*(text + target_index - 1) == '可')
                    {
                        *index = 1;
                    }
                    break;
                case '行':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index + 1) == '樹')
                    {
                        *index = 1;
                    }
                    break;
                case '應':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    if (*(text + target_index + 1) == '供')
                    {
                        *index = 2;
                    }
                    else if (*(text + target_index + 1) == '時')
                    {
                        *index = 1;
                    }
                    break;
                case '難':
                    if (*(text + target_index - 1) == '八')
                    {
                        *index = 1;
                    }
                    break;
                case '無':
                    if (*(text + target_index - 1) == '南')
                    {
                        *index = 1;
                    }
                    break;
                case '安':
                    if (isEnd)
                    {
                        accumulated = new string(text + target_index);
                        return true;
                    }
                    break;
                case '隐':
                    if (*(text + target_index - 1) == '安')
                    {
                        *index = 1;
                    }
                    break;
                case '宿':
                    if((*(text + target_index - 3) == '二' && *(text + target_index - 1) == '八') || *(text + target_index -1) == '星')
                    {
                        *index = 2;
                    }
                    break;
                case '曲':
                    if (*(text + target_index - 1) == '諂')
                    {
                        *index = 1;
                    }
                    break;
                case '觀':
                    if (*(text + target_index - 1) == '宮' || *(text + target_index - 1) == '樓')
                    {
                        *index = 1;
                    }
                    break;
                case '怕':
                    if (*(text + target_index - 1) == '憺')
                    {
                        *index = 1;
                    }
                    break;
                case '塞':
                    if (*(text + target_index - 1) == '閉')
                    {
                        *index = 1;
                    }
                    break;
                case '校':
                    if (*(text + target_index + 1) == '飾')
                    {
                        *index = 1;
                    }
                    break;
                case '陂':
                    if (*(text + target_index + 1) == '陀')
                    {
                        *index = 1;
                    }

                    break;
                case '罷':
                    if (*(text + target_index + 1) == '極')
                    {
                        *index = 2;
                    }
                    break;
                case '惡':
                    if (*(text + target_index - 1) == '厭')
                    {
                        *index = 2;
                    }
                    break;
                case '適':
                    if (*(text + target_index + 1) == '莫')
                    {
                        *index = 2;
                    }
                    break;
                case '燋':
                    if (*(text + target_index + 1) == '然')
                    {
                        *index = 1;
                    }
                    break;
                case '泊':
                    if (*(text + target_index - 1) == '池')
                    {
                        *index = 1;
                    }
                    break;
                case '穬':
                    if (*(text + target_index - 1) == '麁')
                    {
                        *index = 1;
                    }
                    break;
                case '籠':
                    if (*(text + target_index + 1) == '繫')
                    {
                        *index = 1;
                    }
                    break;
                case '蔭':
                    if (*(text + target_index + 1) == '覆')
                    {
                        *index = 1;
                    }
                    break;
                case '重':
                    if(*(text + target_index + 1) == '啟')
                    {
                        *index = 1;
                    }
                    break;
                case '乾':
                    if (*(text + target_index - 1) == '尼')
                    {
                        *index = 1;
                    }
                    break;
     
            }
            */
        }
    }
}
