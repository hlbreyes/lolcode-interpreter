using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace cmsc124_lolterpreter
{
    public partial class MainWindow : Form
    {
        Hashtable variable_list = new Hashtable();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();

            OpenFileDialog.Title = "Open File";
            OpenFileDialog.Filter = "LOLCode files|*.lol|Text files|*.text|Any files|*.*";
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.RestoreDirectory = true;

            if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try { CodeTextbox.Text = File.ReadAllText(OpenFileDialog.FileName); }
                catch (Exception ex) { MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message); }
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            variable_list.Clear();
            ConsoleTextbox.Clear();
            LexemeDataGrid.Rows.Clear();
            SymbolTableDataGrid.Rows.Clear();

            if (!(variable_list.ContainsKey("IT"))) //checks if IT is already in the hashmap. needed when the execute button is pressed twice
            {
                variable_list.Add("IT", "NOOB");
            }

            string[] lines = CodeTextbox.Text.Split('\n');
            int len = lines.Length;
            bool is_comment = false;
            bool is_read;
            Regex regex;

            for (int i = 0; i < len; i++)
            {
                is_read = false;

                //HAI
                regex = new Regex(@"^\s*(?<start>HAI)");
                Match match = regex.Match(lines[i]);
                if (match.Success)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["start"].Value, "Program delimiter");
                    is_read = true;
                }
                //else ConsoleTextbox.Text = "HAI opening delimiter not found."; //TODO: Edit this error messages

                //Comments
                //BTW
                regex = new Regex(@"^\s*(?<keyword>BTW)(?<comment>.*?)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Comment Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["comment"].Value, "Comment");
                    is_read = true;
                }

                /*OBTW
                regex = new Regex(@"^\s*(?<keyword>OBTW)(?<comment>.*?)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Comment Delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["comment"].Value, "Comment");
                    is_read = true;
                    is_comment = true;
                }

                //Multi-line Comments displayed per line
                regex = new Regex(@"^\s*(?<comment>.*?)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false && is_comment == true)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["comment"].Value, "Comment");
                    is_read = true;
                }

                //TLDR
                regex = new Regex(@"^\s*(?<keyword>TLDR)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false && is_comment == true)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Comment Delimiter");
                    is_read = true;
                    is_comment = false;
                }*/

                //No value (NOOB)
                regex = new Regex(@"^\s*(?<vardec>I\s+HAS\s+A)\s+(?<variable>[A-Za-z]([a-zA-z0-9_]?)+)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["vardec"].Value, "Variable Declaration");
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");

                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        ConsoleTextbox.AppendText("Variable already exists." + Environment.NewLine);
                    }
                    else
                    {
                        variable_list.Add(match.Groups["variable"].Value, "NOOB");
                        SymbolTableDataGrid.Rows.Add(match.Groups["variable"].Value, "NOOB");
                    }

                    is_read = true;
                }

                //NUMBR/NUMBAR as a value.
                regex = new Regex(@"^\s*(?<vardec>I\s+HAS\s+A)\s+(?<variable>[A-Za-z]([a-zA-z0-9_]?)+)\s+(?<varInit>ITZ)\s+(?<number>((?<numbr>-?([0-9])+)|(?<numbar>-?([0-9])+(\.[0-9]+)?)))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["vardec"].Value, "Variable Declaration");
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["varInit"].Value, "Variable Initialization");
                    LexemeDataGrid.Rows.Add(match.Groups["number"].Value, "Literal");

                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        ConsoleTextbox.AppendText("Variable already exists." + Environment.NewLine);
                    }
                    else
                    {
                        variable_list.Add(match.Groups["variable"].Value, match.Groups["number"].Value);
                        SymbolTableDataGrid.Rows.Add(match.Groups["variable"].Value, match.Groups["number"].Value);
                    }

                    is_read = true;
                }

                //YARN as a value.
                regex = new Regex(@"^\s*(?<vardec>I\s+HAS\s+A)\s+(?<variable>[A-Za-z]([a-zA-z0-9_]?)+)\s+(?<varInit>ITZ)\s+(?<yarn>""[^:]+"")(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["vardec"].Value, "Variable Declaration");
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["varInit"].Value, "Variable Initialization");
                    LexemeDataGrid.Rows.Add(match.Groups["yarn"].Value, "Literal");

                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        ConsoleTextbox.AppendText("Variable already exists." + Environment.NewLine);
                    }
                    else
                    {
                        variable_list.Add(match.Groups["variable"].Value, match.Groups["yarn"].Value);
                        SymbolTableDataGrid.Rows.Add(match.Groups["variable"].Value, match.Groups["yarn"].Value);
                    }

                    is_read = true;
                }

                //TROOF as a value.
                regex = new Regex(@"^\s*(?<vardec>I\s+HAS\s+A)\s+(?<variable>[A-Za-z]([a-zA-z0-9_]?)+)\s+(?<varInit>ITZ)\s+(?<troof>(WIN|FAIL))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["vardec"].Value, "Variable Declaration");
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["varInit"].Value, "Variable Initialization");
                    LexemeDataGrid.Rows.Add(match.Groups["troof"].Value, "Literal");

                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        ConsoleTextbox.AppendText("Variable already exists." + Environment.NewLine);
                    }
                    else
                    {
                        variable_list.Add(match.Groups["variable"].Value, match.Groups["troof"].Value);
                        SymbolTableDataGrid.Rows.Add(match.Groups["variable"].Value, match.Groups["troof"].Value);
                    }

                    is_read = true;
                }

                //Another variable as the value.
                regex = new Regex(@"^\s*(?<vardec>I\s+HAS\s+A)\s+(?<variable>[A-Za-z]([a-zA-z0-9_]?)+)\s+(?<varInit>ITZ)\s+(?<var>[A-Za-z]([a-zA-z0-9_])+)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["vardec"].Value, "Variable Declaration");
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["varInit"].Value, "Variable Initialization");
                    LexemeDataGrid.Rows.Add(match.Groups["var"].Value, "Variable Identifier");

                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        ConsoleTextbox.AppendText("Variable already exists." + Environment.NewLine);
                    }
                    else
                    {
                        if (variable_list.ContainsKey(match.Groups["var"].Value))
                        {
                            variable_list.Add(match.Groups["variable"].Value, match.Groups["number"].Value);
                            SymbolTableDataGrid.Rows.Add(match.Groups["variable"].Value, variable_list[match.Groups["var"].Value].ToString());
                        }
                        else
                        {
                            ConsoleTextbox.AppendText("Variable doesn't exist.");
                        }
                    }

                    is_read = true;
                }

                //Arithmetic operations.
                //regex = new Regex(@"^\s*(?<delimiter>(SUM|DIFF|PRODUKT|QUOSHUNT|MOD|BIGGR|SMALLR))\s+(?<divider>OF)\s+(?<xVar>[A-Za-z]([a-zA-z0-9_]?)*)\s+(?<divider2>AN)\s+(?<yVar>([A-Za-z]([a-zA-z0-9_]?)*|<yYarn>""[^:]+""|<yNumbr>-?([0-9])+|<yNumbar>-?([0-9])+(\.[0-9]+)?))(\n|\r|\r\n)?$");
                regex = new Regex(@"^\s*(?<delimiter>(SUM|DIFF|PRODUKT|QUOSHUNT|MOD|BIGGR|SMALLR))\s+(?<divider>OF)\s+((?<xVar>[A-Za-z]([a-zA-z0-9_]?)*)|(?<xYarn>""[^:]+"")|(?<xNumbar>-?([0-9])+(\.[0-9]+))|(?<xNumbr>-?([0-9])+))\s+(?<divider2>AN)\s+((?<yVar>[A-Za-z]([a-zA-z0-9_]?)*)|(?<yYarn>""[^:]+"")|(?<yNumbar>-?([0-9])+(\.[0-9]+))|(?<yNumbr>-?([0-9])+))");
                match = regex.Match(lines[i]);

                if (match.Success && is_read == false)
                {
                    String op;  //operator switch case variable
                    float xFloat = 0;
                    float yFloat = 0;
                    int xInt = 0;
                    int yInt = 0;
                    float ansFloat;
                    int ansInt;
                    bool ansBool;
                    bool isFloat = true;
                    bool xIsInt = false;
                    bool yIsInt = false;
                    bool yarnValid = true;
                    bool valid = true;
                    Regex numRegex = new Regex(@"(?<Numbar>-?[0-9]+(\.[0-9]+))|((?<Numbr>-?([0-9])+))");
                    Regex strRegex = new Regex(@"(?<Yarn>^"".*""$)");
                    Match strMatch;
                    Match numMatch;

                    LexemeDataGrid.Rows.Add(match.Groups["delimiter"].Value, "Arithmethic Operator");
                    op = match.Groups["delimiter"].Value;
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "delimiter");

                    if (match.Groups["xVar"].Success)
                    {
                        String converted;
                        LexemeDataGrid.Rows.Add(match.Groups["xVar"].Value, "Var Operand");
                        strMatch = strRegex.Match(variable_list[match.Groups["xVar"].Value].ToString());
                        if (strMatch.Groups["Yarn"].Success)
                        {
                            //remove the string delimiters before parsing
                            int length = strMatch.Groups["Yarn"].Value.Length;
                            converted = strMatch.Groups["Yarn"].Value.Substring(1, length - 2);
                        }
                        else
                        {
                            converted = variable_list[match.Groups["xVar"].Value].ToString();
                            Console.WriteLine(converted);
                        }

                        if (float.TryParse(converted, out xFloat))
                        {
                            numMatch = numRegex.Match(converted);
                            if (numMatch.Groups["Numbar"].Success)
                            {
                                xFloat = float.Parse(numMatch.Groups["Numbar"].Value.ToString(), CultureInfo.InvariantCulture);
                            }
                            else if (numMatch.Groups["Numbr"].Success)
                            {
                                xIsInt = true;
                                xInt = int.Parse(numMatch.Groups["Numbr"].Value.ToString(), CultureInfo.InvariantCulture);
                            }
                        }
                        else
                        {
                            yarnValid = false;
                        }
                    }

                    else if (match.Groups["xYarn"].Success)
                    {
                        String converted;
                        LexemeDataGrid.Rows.Add(match.Groups["xYarn"].Value, "Yarn Operand");
                        strMatch = strRegex.Match(match.Groups["xYarn"].Value);
                        if (strMatch.Groups["Yarn"].Success)
                        {
                            //remove the string delimiters before parsing
                            int length = strMatch.Groups["Yarn"].Value.Length;
                            converted = strMatch.Groups["Yarn"].Value.Substring(1, length - 2);
                        }
                        else
                        {
                            converted = match.Groups["xYarn"].Value;
                        }

                        if (float.TryParse(converted, out xFloat))
                        {
                            numMatch = numRegex.Match(converted);
                            if (numMatch.Groups["Numbar"].Success)
                            {
                                xFloat = float.Parse(numMatch.Groups["Numbar"].Value.ToString(), CultureInfo.InvariantCulture);
                            }
                            else if (numMatch.Groups["Numbr"].Success)
                            {
                                xIsInt = true;
                                xInt = int.Parse(numMatch.Groups["Numbr"].Value.ToString(), CultureInfo.InvariantCulture);
                            }
                        }
                        else
                        {
                            yarnValid = false;
                        }
                    }

                    else if (match.Groups["xNumbr"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["xNumbr"].Value, "Numbr Operand");
                        xIsInt = true;
                        xInt = int.Parse(match.Groups["xNumbr"].Value);
                    }
                    else if (match.Groups["xNumbar"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["xNumbar"].Value, "Numbar Operand");
                        xFloat = float.Parse(match.Groups["xNumbar"].Value);
                    }
                    else
                    {
                        valid = false;
                    }

                    LexemeDataGrid.Rows.Add(match.Groups["divider2"].Value, "delimiter");

                    if (match.Groups["yVar"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["yVar"].Value, "Var Operand");
                        if (variable_list.ContainsKey(match.Groups["yVar"].Value))
                        {
                            numMatch = numRegex.Match(variable_list[match.Groups["yVar"].Value].ToString());

                            //convert YARN(string) to either NUMBAR or NUMBR
                            if (numMatch.Groups["Numbar"].Success)
                            {
                                yFloat = float.Parse(variable_list[match.Groups["yVar"].Value].ToString(), CultureInfo.InvariantCulture);
                            }
                            else if (numMatch.Groups["Numbr"].Success)
                            {
                                yIsInt = true;
                                yInt = int.Parse(variable_list[match.Groups["yVar"].Value].ToString(), CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                yarnValid = false;
                            }
                        }

                    }
                    else if (match.Groups["yYarn"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["yYarn"].Value, "Yarn Operand");
                        numMatch = numRegex.Match(match.Groups["yYarn"].Value);

                        //convert YARN(string) to either NUMBAR or NUMBR
                        if (numMatch.Groups["Numbar"].Success)
                        {
                            yFloat = float.Parse(numMatch.Groups["Numbar"].Value, CultureInfo.InvariantCulture);
                        }
                        else if (numMatch.Groups["Numbr"].Success)
                        {
                            yIsInt = true;
                            yInt = int.Parse(numMatch.Groups["Numbr"].Value, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            yarnValid = false;
                        }
                    }
                    else if (match.Groups["yNumbr"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["yNumbr"].Value, "Numbr Operand");
                        yIsInt = true;
                        yInt = int.Parse(match.Groups["yNumbr"].Value);
                    }
                    else if (match.Groups["yNumbar"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["yNumbar"].Value, "Numbar Operand");
                        yFloat = float.Parse(match.Groups["yNumbar"].Value);
                    }
                    else
                    {
                        valid = false;
                    }

                    if (xIsInt == true && yIsInt == true)
                    {
                        isFloat = false;
                    }

                    if (valid && isFloat && yarnValid)  //FLOAT case
                    {
                        if (xIsInt) xFloat = (float)xInt;
                        if (yIsInt) yFloat = (float)yInt;
                        switch (op)
                        {
                            case "SUM":
                                ansFloat = xFloat + yFloat;
                                ConsoleTextbox.AppendText(ansFloat + Environment.NewLine);
                                break;
                            case "DIFF":
                                ansFloat = xFloat - yFloat;
                                ConsoleTextbox.AppendText(ansFloat + Environment.NewLine);
                                break;
                            case "PRODUKT":
                                ansFloat = xFloat * yFloat;
                                ConsoleTextbox.AppendText(ansFloat + Environment.NewLine);
                                break;
                            case "QUOSHUNT":
                                ansFloat = xFloat / yFloat;
                                ConsoleTextbox.AppendText(ansFloat + Environment.NewLine);
                                break;
                            case "MOD":
                                ansFloat = xFloat % yFloat;
                                ConsoleTextbox.AppendText(ansFloat + Environment.NewLine);
                                break;
                            case "BIGGR":
                                ansBool = xFloat > yFloat;
                                ConsoleTextbox.AppendText(ansBool + Environment.NewLine);
                                break;
                            case "SMALLR":
                                ansBool = xFloat < yFloat;
                                ConsoleTextbox.AppendText(ansBool + Environment.NewLine);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (valid && !isFloat && yarnValid)    //INT case
                    {
                        switch (op)
                        {
                            case "SUM":
                                ansInt = xInt + yInt;
                                ConsoleTextbox.AppendText(ansInt + Environment.NewLine);
                                break;
                            case "DIFF":
                                ansInt = xInt - yInt;
                                ConsoleTextbox.AppendText(ansInt + Environment.NewLine);
                                break;
                            case "PRODUKT":
                                ansInt = xInt * yInt;
                                ConsoleTextbox.AppendText(ansInt + Environment.NewLine);
                                break;
                            case "QUOSHUNT":
                                ansInt = xInt / yInt;
                                ConsoleTextbox.AppendText(ansInt + Environment.NewLine);
                                break;
                            case "MOD":
                                ansInt = xInt % yInt;
                                ConsoleTextbox.AppendText(ansInt + Environment.NewLine);
                                break;
                            case "BIGGR":
                                ansBool = xInt > yInt;
                                ConsoleTextbox.AppendText(ansBool + Environment.NewLine);
                                break;
                            case "SMALLR":
                                ansBool = xInt < yInt;
                                ConsoleTextbox.AppendText(ansBool + Environment.NewLine);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("OPERAND ERROR!");
                    }

                    is_read = true;
                }

                //Boolean.
                regex = new Regex(@"^\s*(?<delimiter>(BOTH|WON|EITHER))\s+(?<divider>OF)\s+((?<x>(WIN|FAIL))|(?<xVar>[A-Za-z]([a-zA-z0-9_]?)*))\s+(?<divider2>AN)\s+((?<y>(WIN|FAIL))|(?<yVar>[A-Za-z]([a-zA-z0-9_]?)*))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    bool x = false;
                    bool y = false;
                    LexemeDataGrid.Rows.Add(match.Groups["delimiter"].Value, "Boolean Operator");
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "Newline");

                    if (match.Groups["x"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["x"].Value, "Troof Value");
                        if (match.Groups["x"].Value.ToString() == "WIN")
                        {
                            x = true;
                        }
                        else if (match.Groups["x"].Value.ToString() == "FAIL")
                        {
                            x = false;
                        }
                    }
                    else if (match.Groups["xVar"].Success)
                    {
                        LexemeDataGrid.Rows.Add(variable_list[match.Groups["xVar"].Value], "Troof Variable");
                        if (variable_list[match.Groups["xVar"].Value].ToString() == "WIN")
                        {
                            x = true;
                        }
                        else if (variable_list[match.Groups["xVar"].Value].ToString() == "FAIL")
                        {
                            x = false;
                        }
                    }

                    LexemeDataGrid.Rows.Add(match.Groups["divider2"].Value, "delimiter");

                    if (match.Groups["y"].Success)
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["y"].Value, "Troof Value");
                        if (match.Groups["y"].Value.ToString() == "WIN")
                        {
                            y = true;
                        }
                        else if (match.Groups["y"].Value.ToString() == "FAIL")
                        {
                            y = false;
                        }
                    }
                    else if (match.Groups["yVar"].Success)
                    {
                        LexemeDataGrid.Rows.Add(variable_list[match.Groups["yVar"].Value], "Troof variable");
                        if (variable_list[match.Groups["yVar"].Value].ToString() == "WIN")
                        {
                            y = true;
                        }
                        else if (variable_list[match.Groups["yVar"].Value].ToString() == "FAIL")
                        {
                            y = false;
                        }
                    }

                    if (match.Groups["delimiter"].Success)
                    {
                        if (match.Groups["delimiter"].Value.ToString() == "BOTH")
                        {
                            if (x == true && y == true)
                            {
                                ConsoleTextbox.AppendText("true" + Environment.NewLine);
                            }
                            else
                            {
                                ConsoleTextbox.AppendText("false" + Environment.NewLine);
                            }
                        }
                        else if (match.Groups["delimiter"].Value.ToString() == "EITHER")
                        {
                            if (x == true || y == true)
                            {
                                ConsoleTextbox.AppendText("true" + Environment.NewLine);
                            }
                            else
                            {
                                ConsoleTextbox.AppendText("false" + Environment.NewLine);
                            }
                        }
                        else if (match.Groups["delimiter"].Value.ToString() == "WON")
                        {
                            if (x != y)
                            {
                                ConsoleTextbox.AppendText("true" + Environment.NewLine);
                            }
                            else
                            {
                                ConsoleTextbox.AppendText("false" + Environment.NewLine);
                            }
                        }

                    }

                    is_read = true;
                }

                //Negation (NOT).
                regex = new Regex(@"^\s*(?<delimiter>NOT)\s+((?<x>(WIN|FAIL))|(?<xVar>[A-Za-z]([a-zA-z0-9_]?)*))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    bool x = true;
                    LexemeDataGrid.Rows.Add(match.Groups["delimiter"].Value, "Not Operator");
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["x"].Value, "Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["xVar"].Value, "Variable");

                    if (match.Groups["x"].Success)
                    {
                        if (match.Groups["x"].Value.ToString() == "WIN")
                        {
                            x = true;
                        }
                        else if (match.Groups["x"].Value.ToString() == "FAIL")
                        {
                            x = false;
                        }
                    }
                    else if (match.Groups["xVar"].Success)
                    {
                        if (variable_list[match.Groups["xVar"].Value].ToString() == "WIN")
                        {
                            x = true;
                        }
                        else if (variable_list[match.Groups["xVar"].Value].ToString() == "FAIL")
                        {
                            x = false;
                        }
                    }
                    //negate
                    if (x == true)
                    {
                        x = false;
                    }
                    else
                    {
                        x = true;
                    }

                    ConsoleTextbox.AppendText(x + Environment.NewLine);
                    is_read = true;
                }

                //ANY/ALL.
                regex = new Regex(@"^\s*(?<delimiter>(ANY|ALL))\s+(?<divider>OF)\s+(?<x>(WIN|FAIL))\s+((?<divider2>AN)\s+(?<y>(WIN|FAIL))\s+)+(?<end>MKAY)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["delimiter"].Value, "Boolean Operator");
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["x"].Value, "Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["divider2"].Value, "delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["y"].Value, "Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["end"].Value, "Terminating Keyword");

                    is_read = true;
                }

                //Binary equality operation (!= / ==).
                regex = new Regex(@"^\s*(?<delimiter>(BOTH\s+SAEM|DIFFRINT))\s+((?<xTroof>(WIN|FAIL))|(?<xVar>[A-Za-z]([a-zA-z0-9_]?)*)|(?<xYarn>""[^:]+"")|(?<xNumbar>-?([0-9])+(\.[0-9]+))|(?<xNumbr>-?([0-9])+))\s+(?<divider>AN)\s+((?<yTroof>(WIN|FAIL))|(?<yVar>[A-Za-z]([a-zA-z0-9_]?)*)|(?<yYarn>""[^:]+"")|(?<yNumbar>-?([0-9])+(\.[0-9]+))|(?<yNumbr>-?([0-9])+))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    String xType = "";
                    String yType = "";
                    String xYarn = "", yYarn = "";
                    float xNumbar = 0, yNumbar = 0;
                    int xNumbr = 0, yNumbr = 0;
                    bool xTroof = false, yTroof = false;
                    LexemeDataGrid.Rows.Add(match.Groups["delimiter"].Value, "Boolean Operator");

                    //START OF x OPERAND
                    if (match.Groups["xTroof"].Success)
                    {
                        xType = "TROOF";
                        LexemeDataGrid.Rows.Add(match.Groups["xTroof"].Value, "Troof Expression");
                        if (match.Groups["xTroof"].Value == "WIN")
                        {
                            xTroof = true;
                        }
                        else
                        {
                            xTroof = false;
                        }
                    }
                    else if (match.Groups["xVar"].Success)
                    {
                        Regex varRegex = new Regex(@"((?<Troof>(WIN|FAIL))|(?<Yarn>""[^:]+"")|(?<Numbar>-?([0-9])+(\.[0-9]+))|(?<Numbr>-?([0-9])+))");
                        Match varMatch = varRegex.Match(variable_list[match.Groups["xVar"].Value].ToString());
                        if (varMatch.Groups["Troof"].Success)
                        {
                            xType = "TROOF";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Troof"].Value, "Troof Variable");
                            if (varMatch.Groups["Troof"].Value == "WIN")
                            {
                                xTroof = true;
                            }
                            else
                            {
                                xTroof = false;
                            }
                        }
                        else if (varMatch.Groups["Yarn"].Success)
                        {
                            xType = "YARN";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Yarn"].Value, "Yarn Variable");
                            xYarn = varMatch.Groups["Yarn"].Value;
                        }
                        else if (varMatch.Groups["Numbar"].Success)
                        {
                            xType = "NUMBAR";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Numbar"].Value, "Numbar Variable");
                            xNumbar = float.Parse(varMatch.Groups["Numbar"].Value, CultureInfo.InvariantCulture);
                        }
                        else if (varMatch.Groups["Numbr"].Success)
                        {
                            xType = "NUMBR";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Numbr"].Value, "Numbr Variable");
                            xNumbar = int.Parse(varMatch.Groups["Numbr"].Value, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            xType = "ERROR";
                        }
                    }
                    else if (match.Groups["xYarn"].Success)
                    {
                        xType = "YARN";
                        LexemeDataGrid.Rows.Add(match.Groups["xYarn"].Value, "Yarn Expression");
                        xYarn = match.Groups["xYarn"].Value;
                    }
                    else if (match.Groups["xNumbar"].Success)
                    {
                        xType = "NUMBAR";
                        LexemeDataGrid.Rows.Add(match.Groups["xNumbar"].Value, "Numbar Expression");
                        xNumbar = float.Parse(match.Groups["xNumbar"].Value, CultureInfo.InvariantCulture);
                    }
                    else if (match.Groups["xNumbr"].Success)
                    {
                        xType = "NUMBR";
                        LexemeDataGrid.Rows.Add(match.Groups["xNumbr"].Value, "Numbr Expression");
                        xNumbr = int.Parse(match.Groups["xNumbr"].Value, CultureInfo.InvariantCulture);
                    }

                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "delimiter");

                    //START OF y OPERAND
                    if (match.Groups["yTroof"].Success)
                    {
                        yType = "TROOF";
                        LexemeDataGrid.Rows.Add(match.Groups["yTroof"].Value, "Troof Expression");
                        if (match.Groups["yTroof"].Value == "WIN")
                        {
                            yTroof = true;
                        }
                        else
                        {
                            yTroof = false;
                        }
                    }
                    else if (match.Groups["yVar"].Success)
                    {
                        Regex varRegex = new Regex(@"((?<Troof>(WIN|FAIL))|(?<Yarn>""[^:]+"")|(?<Numbar>-?([0-9])+(\.[0-9]+))|(?<Numbr>-?([0-9])+))");
                        Match varMatch = varRegex.Match(variable_list[match.Groups["yVar"].Value].ToString());
                        if (varMatch.Groups["Troof"].Success)
                        {
                            yType = "TROOF";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Troof"].Value, "Troof Variable");
                            if (varMatch.Groups["Troof"].Value == "WIN")
                            {
                                yTroof = true;
                            }
                            else
                            {
                                yTroof = false;
                            }
                        }
                        else if (varMatch.Groups["Yarn"].Success)
                        {
                            yType = "YARN";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Yarn"].Value, "Yarn Variable");
                            yYarn = varMatch.Groups["Yarn"].Value;
                        }
                        else if (varMatch.Groups["Numbar"].Success)
                        {
                            yType = "NUMBAR";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Numbar"].Value, "Numbar Variable");
                            yNumbar = float.Parse(varMatch.Groups["Numbar"].Value, CultureInfo.InvariantCulture);
                        }
                        else if (varMatch.Groups["Numbr"].Success)
                        {
                            yType = "NUMBR";
                            LexemeDataGrid.Rows.Add(varMatch.Groups["Numbr"].Value, "Numbr Variable");
                            yNumbr = int.Parse(varMatch.Groups["Numbr"].Value, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            yType = "ERROR";
                        }
                    }
                    else if (match.Groups["yYarn"].Success)
                    {
                        yType = "YARN";
                        LexemeDataGrid.Rows.Add(match.Groups["yYarn"].Value, "Yarn Expression");
                        yYarn = match.Groups["yYarn"].Value;
                    }
                    else if (match.Groups["yNumbar"].Success)
                    {
                        yType = "NUMBAR";
                        LexemeDataGrid.Rows.Add(match.Groups["yNumbar"].Value, "Numbar Expression");
                        yNumbar = float.Parse(match.Groups["yNumbar"].Value, CultureInfo.InvariantCulture);
                    }
                    else if (match.Groups["yNumbr"].Success)
                    {
                        yType = "NUMBR";
                        LexemeDataGrid.Rows.Add(match.Groups["yNumbr"].Value, "Numbr Expression");
                        yNumbr = int.Parse(match.Groups["yNumbr"].Value, CultureInfo.InvariantCulture);
                    }

                    //Start of binary equality operation
                    if (xType == yType)
                    {
                        if (match.Groups["delimiter"].Value == "BOTH SAEM")
                        {
                            if (xType == "TROOF")
                            {
                                if (xTroof == yTroof)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                            if (xType == "YARN")
                            {
                                if (xYarn == yYarn)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                            if (xType == "NUMBAR")
                            {
                                if (xNumbar == yNumbar)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                            if (xType == "NUMBR")
                            {
                                if (xNumbr == yNumbr)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                        }

                        if (match.Groups["delimiter"].Value == "DIFFRINT")
                        {
                            if (xType == "TROOF")
                            {
                                if (xTroof != yTroof)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                            if (xType == "YARN")
                            {
                                if (xYarn != yYarn)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                            if (xType == "NUMBAR")
                            {
                                if (xNumbar != yNumbar)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                            if (xType == "NUMBR")
                            {
                                if (xNumbr != yNumbr)
                                {
                                    variable_list["IT"] = "TRUE";
                                }
                                else
                                {
                                    variable_list["IT"] = "FALSE";
                                }
                            }
                        }
                        if (variable_list["IT"].ToString() == "TRUE")
                        {
                            ConsoleTextbox.AppendText("WIN" + Environment.NewLine);
                        }
                        else if (variable_list["IT"].ToString() == "FALSE")
                        {
                            ConsoleTextbox.AppendText("FAIL" + Environment.NewLine);
                        }
                    }//end of equal-type
                    else
                    {
                        ConsoleTextbox.AppendText("FAIL(DIFFERING TYPES)" + Environment.NewLine);
                    }


                    is_read = true;
                }

                //Greater than, less than, greater/less than or equal to (>, >=, <, <=).
                regex = new Regex(@"^\s*(?<delimiter>(BOTH\s+SAEM|DIFFRNT\s+))\s+(?<x>(-?([0-9])+(\.[0-9]+)?|""[^:]+""))\s+(?<comaprison>AND\s+(BIGGR|SMALLR))\s+(?<divider>OF)\s+(?<x>(-?([0-9])+(\.[0-9]+)?|""[^:]+""))\s+(?<divider2>AN)\s+(?<y>(-?([0-9])+(\.[0-9]+)?|""[^:]+""))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["delimiter"].Value, "Comparison Operator");
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "delimiter Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["x"].Value, "Troof Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["divider2"].Value, "delimiter ");
                    LexemeDataGrid.Rows.Add(match.Groups["y"].Value, "Troof Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["end"].Value, "Terminating Keyword");

                    is_read = true;
                }

                //Concatenation.
                Regex concat = new Regex(@"^\s*(?<concat>SMOOSH)\s+((?<yarn>""[^:]+"")\s+(?<divider>AN\s+)?)+(?<end>MKAY)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["concat"].Value, "Concatination Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["yarn"].Value, "Yarn-Typed Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["end"].Value, "Terminating Keyword");
                    is_read = true;
                }

                //GIMMEH 
                regex = new Regex(@"^\s*(?<input>GIMMEH)\s+(?<variable>[A-Za-z]([a-zA-z0-9_]?)+)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["input"].Value, "Input Keyword");
                        LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");

                        /* Add a reference to Microsoft.VisualBasic!
                         * In Solution Explorer right click on References folder.
                         * Select Add Reference 
                         * Tick Microsoft.VisualBasic
                         * Click OK
                         */
                        string prompt = "Input value for " + match.Groups["variable"].Value + ":";
                        string input = Microsoft.VisualBasic.Interaction.InputBox(prompt, "GIMMEH");

                        if (variable_list.ContainsKey(match.Groups["variable"].Value))
                        {

                            //for (int j = 0; j < SymbolTableDataGrid.Rows.Count - 1; j++) <- TO BE REMOVED
                            for (int j = 0; j < SymbolTableDataGrid.Rows.Count; j++)
                            {

                                if (SymbolTableDataGrid.Rows[j].Cells[0].Value.ToString() == match.Groups["variable"].Value)
                                {
                                    //  update SymbolTableDataGrid and variable_list
                                    SymbolTableDataGrid.Rows[j].Cells[1].Value = input;
                                    variable_list[match.Groups["variable"].Value] = input;
                                }
                            }
                        }

                        is_read = true;
                    }
                    else
                    {
                        ConsoleTextbox.AppendText("Variable Doesn't Exist");
                    }
                }

                //VISIBLE
                regex = new Regex(@"^\s*(?<output>VISIBLE)\s+(?<variable>(([a-zA-Z][a-zA-Z0-9_]*?)|WIN|FAIL|\-?[1-9][0-9]*(\.[1-9][0-9]*)?|(?<opdelim>"")(?<value>.+)(?<closedelim>"")))(?<esc>\!)?(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["output"].Value, "Output Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["opdelim"].Value, "String Opening Delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["value"].Value, "Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["closedelim"].Value, "String Closing Delimiter");
                    if (match.Groups["esc"].Value == "!")
                    {
                        LexemeDataGrid.Rows.Add(match.Groups["esc"].Value, "Terminating Character");
                    }

                    if (variable_list.ContainsKey(match.Groups["variable"].Value))
                    {
                        for (int j = 0; j < SymbolTableDataGrid.Rows.Count - 1; j++)
                        {
                            if (SymbolTableDataGrid.Rows[j].Cells[0].Value.ToString() == match.Groups["variable"].Value)
                            {
                                ConsoleTextbox.AppendText(SymbolTableDataGrid.Rows[j].Cells[1].Value + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        ConsoleTextbox.AppendText(match.Groups["value"].Value.ToString() + Environment.NewLine);
                    }
                    is_read = true;
                }

                //R
                regex = new Regex(@"^\s*(?<variable>[A-Za-z]([a-zA-z0-9_])+)\s+(?<varInit>R)\s+(?<value>(?<var>([A-Za-z]([a-zA-z0-9_])+)|(?<troof>(WIN|FAIL))|(?<yarn>""[^:]+"")|(?<numbr>-?([0-9])+)|(?<numbar>-?([0-9])+(\.[0-9]+)?)))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["varInit"].Value, "Variable Assignment Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["value"].Value, "Literal");

                    String variable = match.Groups["variable"].Value;
                    String value = match.Groups["value"].Value;

                    if (variable_list.ContainsKey(variable))
                    {
                        if (variable_list.ContainsKey(value)) //<variable> R <variable>
                        {
                            variable_list[variable] = variable_list[value].ToString();
                        }
                        else //<variable> R <literal>
                        {
                            variable_list[variable] = value;
                        }

                        //for (int j = 0; j < SymbolTableDataGrid.Rows.Count - 1; j++) <- TO BE REMOVED
                        for (int j = 0; j < SymbolTableDataGrid.Rows.Count; j++)
                        {
                            if (SymbolTableDataGrid.Rows[j].Cells[0].Value.ToString() == variable)
                            {
                                SymbolTableDataGrid.Rows[j].Cells[1].Value = variable_list[variable].ToString();
                            }
                        }

                    }
                    else
                    {
                        ConsoleTextbox.AppendText("Variable Doesn't Exist");
                    }

                    is_read = true;
                }

                //O RLY
                regex = new Regex(@"^\s*(?<keyword>O\s+RLY\?)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "If-Then Keyword");

                    is_read = true;
                }

                //YA RLY
                regex = new Regex(@"^\s*(?<keyword>YA\s+RLY)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "If-Then Keyword");

                    is_read = true;
                }

                //MEBBE
                regex = new Regex(@"^\s*(?<keyword>MEBBE)\s+(?<expression>[^:]+)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "If-Then Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["expression"].Value, "Expression");

                    is_read = true;
                }

                //NO WAI
                regex = new Regex(@"^\s*(?<keyword>NO\s+WAI)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "If-Then Keyword");

                    is_read = true;
                }

                //OIC
                regex = new Regex(@"^\s*(?<keyword>OIC)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Terminating Keyword");
                    is_read = true;
                }

                //SWITCH CASE statements.
                //wtf
                regex = new Regex(@"^\s*(?<expre>(([a-zA-Z][a-zA-Z0-9_]*)|WIN|FAIL|\-?[1-9][0-9]*(\.[1-9][0-9]*)?|""(?<expre>([^:]+))"")),\s*(?<keyword>WTF\?)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["expre"].Value, "Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Case-Statement Keyword");

                    is_read = true;
                }

                //Cases
                regex = new Regex(@"^\s*(?<keyword>OMG)\s+(?<x>(-?([0-9])+(\.[0-9]+)?|""[^:]+""|FAIL))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Case-statement Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["x"].Value, "Expression");

                    is_read = true;
                }

                //gtfo/omgwtf
                regex = new Regex(@"^\s*(?<keyword>(OMGWTF|GTFO))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Case-Statement Keyword");

                    is_read = true;
                }

                //Loops.
                regex = new Regex(@"^\s*(?<keyword>IM\s+IN\s+YR)\s+(?<label>[A-Za-z]([a-zA-z0-9_])+)\s+(?<type>(UPPIN|NERFIN|NOT))\s+(?<key>YR)\s+(?<variable>[A-Za-z]([a-zA-z0-9_])+)\s+(?<cond>(TIL|WILE))\s+(?<expre>[^:]+)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Loops Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["label"].Value, "Label");
                    LexemeDataGrid.Rows.Add(match.Groups["type"].Value, "Loop Opereation");
                    LexemeDataGrid.Rows.Add(match.Groups["key"].Value, "Loop delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable, Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["cond"].Value, "Loop Condition");
                    LexemeDataGrid.Rows.Add(match.Groups["expre"].Value, "Expression");

                    is_read = true;
                }

                //Loop termination.
                regex = new Regex(@"^\s*(?<keyword>IM\s+OUTTA\s+YR)\s+(?<label>[A-Za-z]([a-zA-z0-9_])+)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Loop Exit Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["label"].Value, "Label");

                    is_read = true;
                }

                /* BONUS ***************************************************************************************************************/
                //Typecasting: MAEK
                regex = new Regex(@"^\s*(?<typecast>MAEK)\s+(?<yarn>[^:]+)\s+(?<divider>A)?\s+(?<type>(TROOF|YARN|NUMBR|NUMBAR|NOOB))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["typecast"].Value, "Typecasting Keyword");
                    LexemeDataGrid.Rows.Add(match.Groups["yarn"].Value, "Expression");
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "delimiter");
                    LexemeDataGrid.Rows.Add(match.Groups["type"].Value, "Data Type");

                    is_read = true;
                }

                //Typecasting: IS NOW A
                regex = new Regex(@"^\s*(?<variable>[A-Za-z]([a-zA-z0-9_])+)\s+((?<divider>IS\s+NOW\s+A)|(?<divider>R\s+MAEK)\s+(?<variable2>[A-Za-z]([a-zA-z0-9_])+)\s+A?)\s+(?<type>(TROOF|YARN|NUMBR|NUMBAR|NOOB))(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["variable"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["divider"].Value, "Typecasting Keywords");
                    LexemeDataGrid.Rows.Add(match.Groups["variable2"].Value, "Variable Identifier");
                    LexemeDataGrid.Rows.Add(match.Groups["type"].Value, "Variable Identifier");

                    is_read = true;
                }
                /* *********************************************************************************************************************/

                //KTHXBYE
                regex = new Regex(@"^\s*(?<keyword>KTHXBYE)(\n|\r|\r\n)?$");
                match = regex.Match(lines[i]);
                if (match.Success && is_read == false)
                {
                    LexemeDataGrid.Rows.Add(match.Groups["keyword"].Value, "Program delimiter");
                    is_read = true;
                }
                //else ConsoleTextbox.Text = "KTHXBYE closing delimiter not found."; //TODO: Edit this error messages
            }

        }

        private void CodeTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConsoleTextbox_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

/* TODO
 *  1. boolean operations (MILESTONE 3) -> CHECKED
 *      - partially DONE; no implementation of ALL OF and ANY OF yet
 *  2. if - else clause (MILESTONE 3)
 *  3. switch case (MILESTONE 3)
 *  4. concatenation (smoosh)
 *  5. <variable> R <variable> concatenation
 *  6. nested statements
 *  7. every case of comments -> DONE
 *  8. add result of operations to IT variable; currently only BOTH SAEM and DIFFRINT implements this
 *  9. every case of VISIBLE
 *  
 *  CHANGELOG:
 *  12/2/2016
 *  - Boolean, Logical, Comparison Operations
 *  - Added:
 *      Line 471-908
 *      variable_list.Add("IT", "NOOB"); //line 53
 */
