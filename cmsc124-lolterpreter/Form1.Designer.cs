namespace cmsc124_lolterpreter
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.LexemeDataGrid = new System.Windows.Forms.DataGridView();
            this.LexemeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdentifierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsoleLabel = new System.Windows.Forms.Label();
            this.CodeLabel = new System.Windows.Forms.Label();
            this.LexemesLabel = new System.Windows.Forms.Label();
            this.SymbolTableLabel = new System.Windows.Forms.Label();
            this.LoadFileButton = new System.Windows.Forms.Button();
            this.SymbolTableDataGrid = new System.Windows.Forms.DataGridView();
            this.SymbolTableIdentifierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolTableValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeTextbox = new System.Windows.Forms.RichTextBox();
            this.ConsoleTextbox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.LexemeDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolTableDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(12, 389);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(860, 45);
            this.ExecuteButton.TabIndex = 3;
            this.ExecuteButton.Text = "EXECUTE";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // LexemeDataGrid
            // 
            this.LexemeDataGrid.AllowUserToAddRows = false;
            this.LexemeDataGrid.AllowUserToDeleteRows = false;
            this.LexemeDataGrid.AllowUserToResizeColumns = false;
            this.LexemeDataGrid.AllowUserToResizeRows = false;
            this.LexemeDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.LexemeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LexemeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LexemeColumn,
            this.IdentifierColumn});
            this.LexemeDataGrid.Location = new System.Drawing.Point(374, 48);
            this.LexemeDataGrid.Name = "LexemeDataGrid";
            this.LexemeDataGrid.Size = new System.Drawing.Size(245, 325);
            this.LexemeDataGrid.TabIndex = 4;
            // 
            // LexemeColumn
            // 
            this.LexemeColumn.HeaderText = "LEXEME";
            this.LexemeColumn.Name = "LexemeColumn";
            this.LexemeColumn.ReadOnly = true;
            // 
            // IdentifierColumn
            // 
            this.IdentifierColumn.HeaderText = "IDENTIFIER";
            this.IdentifierColumn.Name = "IdentifierColumn";
            this.IdentifierColumn.ReadOnly = true;
            // 
            // ConsoleLabel
            // 
            this.ConsoleLabel.AutoSize = true;
            this.ConsoleLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleLabel.Location = new System.Drawing.Point(8, 446);
            this.ConsoleLabel.Name = "ConsoleLabel";
            this.ConsoleLabel.Size = new System.Drawing.Size(72, 19);
            this.ConsoleLabel.TabIndex = 6;
            this.ConsoleLabel.Text = "CONSOLE";
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeLabel.Location = new System.Drawing.Point(8, 17);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(45, 19);
            this.CodeLabel.TabIndex = 7;
            this.CodeLabel.Text = "CODE";
            // 
            // LexemesLabel
            // 
            this.LexemesLabel.AutoSize = true;
            this.LexemesLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LexemesLabel.Location = new System.Drawing.Point(370, 17);
            this.LexemesLabel.Name = "LexemesLabel";
            this.LexemesLabel.Size = new System.Drawing.Size(72, 19);
            this.LexemesLabel.TabIndex = 8;
            this.LexemesLabel.Text = "LEXEMES";
            // 
            // SymbolTableLabel
            // 
            this.SymbolTableLabel.AutoSize = true;
            this.SymbolTableLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SymbolTableLabel.Location = new System.Drawing.Point(628, 17);
            this.SymbolTableLabel.Name = "SymbolTableLabel";
            this.SymbolTableLabel.Size = new System.Drawing.Size(117, 19);
            this.SymbolTableLabel.TabIndex = 9;
            this.SymbolTableLabel.Text = "SYMBOL TABLE";
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.Location = new System.Drawing.Point(87, 12);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(250, 30);
            this.LoadFileButton.TabIndex = 2;
            this.LoadFileButton.Text = "LOAD FILE";
            this.LoadFileButton.UseVisualStyleBackColor = true;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // SymbolTableDataGrid
            // 
            this.SymbolTableDataGrid.AllowUserToAddRows = false;
            this.SymbolTableDataGrid.AllowUserToDeleteRows = false;
            this.SymbolTableDataGrid.AllowUserToResizeColumns = false;
            this.SymbolTableDataGrid.AllowUserToResizeRows = false;
            this.SymbolTableDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.SymbolTableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SymbolTableDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SymbolTableIdentifierColumn,
            this.SymbolTableValueColumn});
            this.SymbolTableDataGrid.Location = new System.Drawing.Point(627, 48);
            this.SymbolTableDataGrid.Name = "SymbolTableDataGrid";
            this.SymbolTableDataGrid.Size = new System.Drawing.Size(245, 325);
            this.SymbolTableDataGrid.TabIndex = 11;
            // 
            // SymbolTableIdentifierColumn
            // 
            this.SymbolTableIdentifierColumn.HeaderText = "IDENTIFIER";
            this.SymbolTableIdentifierColumn.Name = "SymbolTableIdentifierColumn";
            this.SymbolTableIdentifierColumn.ReadOnly = true;
            // 
            // SymbolTableValueColumn
            // 
            this.SymbolTableValueColumn.HeaderText = "VALUE";
            this.SymbolTableValueColumn.Name = "SymbolTableValueColumn";
            this.SymbolTableValueColumn.ReadOnly = true;
            // 
            // CodeTextbox
            // 
            this.CodeTextbox.AcceptsTab = true;
            this.CodeTextbox.EnableAutoDragDrop = true;
            this.CodeTextbox.Font = new System.Drawing.Font("Hack", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeTextbox.Location = new System.Drawing.Point(12, 48);
            this.CodeTextbox.Name = "CodeTextbox";
            this.CodeTextbox.Size = new System.Drawing.Size(325, 325);
            this.CodeTextbox.TabIndex = 12;
            this.CodeTextbox.Text = "";
            // 
            // ConsoleTextbox
            // 
            this.ConsoleTextbox.AcceptsTab = true;
            this.ConsoleTextbox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ConsoleTextbox.Font = new System.Drawing.Font("Hack", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleTextbox.ForeColor = System.Drawing.SystemColors.Window;
            this.ConsoleTextbox.Location = new System.Drawing.Point(12, 468);
            this.ConsoleTextbox.Name = "ConsoleTextbox";
            this.ConsoleTextbox.ReadOnly = true;
            this.ConsoleTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.ConsoleTextbox.Size = new System.Drawing.Size(860, 81);
            this.ConsoleTextbox.TabIndex = 13;
            this.ConsoleTextbox.Text = "";
            this.ConsoleTextbox.TextChanged += new System.EventHandler(this.ConsoleTextbox_TextChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.ConsoleTextbox);
            this.Controls.Add(this.CodeTextbox);
            this.Controls.Add(this.SymbolTableDataGrid);
            this.Controls.Add(this.SymbolTableLabel);
            this.Controls.Add(this.LexemesLabel);
            this.Controls.Add(this.CodeLabel);
            this.Controls.Add(this.ConsoleLabel);
            this.Controls.Add(this.LexemeDataGrid);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.LoadFileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOLCode Interpreter";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LexemeDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolTableDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.DataGridView LexemeDataGrid;
        private System.Windows.Forms.Label ConsoleLabel;
        private System.Windows.Forms.Label CodeLabel;
        private System.Windows.Forms.Label LexemesLabel;
        private System.Windows.Forms.Label SymbolTableLabel;
        private System.Windows.Forms.Button LoadFileButton;
        private System.Windows.Forms.DataGridView SymbolTableDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LexemeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdentifierColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolTableIdentifierColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolTableValueColumn;
        private System.Windows.Forms.RichTextBox CodeTextbox;
        private System.Windows.Forms.RichTextBox ConsoleTextbox;
    }
}

