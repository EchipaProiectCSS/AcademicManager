namespace InputOutputManagement
{
    partial class Form1
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
            this.combPromoted = new System.Windows.Forms.ComboBox();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.combStatus = new System.Windows.Forms.ComboBox();
            this.combYear = new System.Windows.Forms.ComboBox();
            this.Firstname = new System.Windows.Forms.Label();
            this.Lastname = new System.Windows.Forms.Label();
            this.Promoted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInsertStudent = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.insertStudentClass = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnInsertStatus = new System.Windows.Forms.Button();
            this.lblClass = new System.Windows.Forms.Label();
            this.txtClass = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // combPromoted
            // 
            this.combPromoted.FormattingEnabled = true;
            this.combPromoted.Location = new System.Drawing.Point(383, 18);
            this.combPromoted.Name = "combPromoted";
            this.combPromoted.Size = new System.Drawing.Size(121, 21);
            this.combPromoted.TabIndex = 1;
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(100, 23);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(157, 20);
            this.txtFirstname.TabIndex = 2;
            this.txtFirstname.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(100, 61);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(157, 20);
            this.txtLastname.TabIndex = 3;
            // 
            // combStatus
            // 
            this.combStatus.FormattingEnabled = true;
            this.combStatus.Location = new System.Drawing.Point(660, 68);
            this.combStatus.Name = "combStatus";
            this.combStatus.Size = new System.Drawing.Size(121, 21);
            this.combStatus.TabIndex = 4;
            // 
            // combYear
            // 
            this.combYear.FormattingEnabled = true;
            this.combYear.Location = new System.Drawing.Point(660, 23);
            this.combYear.Name = "combYear";
            this.combYear.Size = new System.Drawing.Size(121, 21);
            this.combYear.TabIndex = 5;
            // 
            // Firstname
            // 
            this.Firstname.AutoSize = true;
            this.Firstname.Location = new System.Drawing.Point(27, 30);
            this.Firstname.Name = "Firstname";
            this.Firstname.Size = new System.Drawing.Size(57, 13);
            this.Firstname.TabIndex = 6;
            this.Firstname.Text = "First Name";
            this.Firstname.Click += new System.EventHandler(this.label1_Click);
            // 
            // Lastname
            // 
            this.Lastname.AutoSize = true;
            this.Lastname.Location = new System.Drawing.Point(26, 68);
            this.Lastname.Name = "Lastname";
            this.Lastname.Size = new System.Drawing.Size(58, 13);
            this.Lastname.TabIndex = 7;
            this.Lastname.Text = "Last Name";
            // 
            // Promoted
            // 
            this.Promoted.AutoSize = true;
            this.Promoted.Location = new System.Drawing.Point(313, 26);
            this.Promoted.Name = "Promoted";
            this.Promoted.Size = new System.Drawing.Size(52, 13);
            this.Promoted.TabIndex = 8;
            this.Promoted.Text = "Promoted";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(542, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Budget / Fee payer";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(612, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Year";
            // 
            // btnInsertStudent
            // 
            this.btnInsertStudent.Location = new System.Drawing.Point(30, 170);
            this.btnInsertStudent.Name = "btnInsertStudent";
            this.btnInsertStudent.Size = new System.Drawing.Size(113, 23);
            this.btnInsertStudent.TabIndex = 11;
            this.btnInsertStudent.Text = "Insert Student";
            this.btnInsertStudent.UseVisualStyleBackColor = true;
            this.btnInsertStudent.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(30, 102);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(113, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(29, 212);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(825, 194);
            this.dataGridView2.TabIndex = 14;
            // 
            // insertStudentClass
            // 
            this.insertStudentClass.Location = new System.Drawing.Point(159, 170);
            this.insertStudentClass.Name = "insertStudentClass";
            this.insertStudentClass.Size = new System.Drawing.Size(126, 23);
            this.insertStudentClass.TabIndex = 15;
            this.insertStudentClass.Text = "Insert Student Class";
            this.insertStudentClass.UseVisualStyleBackColor = true;
            this.insertStudentClass.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(767, 170);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(87, 23);
            this.btnExport.TabIndex = 17;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // btnInsertStatus
            // 
            this.btnInsertStatus.Location = new System.Drawing.Point(305, 170);
            this.btnInsertStatus.Name = "btnInsertStatus";
            this.btnInsertStatus.Size = new System.Drawing.Size(129, 23);
            this.btnInsertStatus.TabIndex = 18;
            this.btnInsertStatus.Text = "Import Student Status";
            this.btnInsertStatus.UseVisualStyleBackColor = true;
            this.btnInsertStatus.Click += new System.EventHandler(this.btnInsertStatus_Click);
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(333, 71);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(32, 13);
            this.lblClass.TabIndex = 19;
            this.lblClass.Text = "Class";
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(383, 68);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(121, 20);
            this.txtClass.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 418);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.btnInsertStatus);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnInsertStudent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Promoted);
            this.Controls.Add(this.Lastname);
            this.Controls.Add(this.Firstname);
            this.Controls.Add(this.combYear);
            this.Controls.Add(this.combStatus);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.txtFirstname);
            this.Controls.Add(this.combPromoted);
            this.Controls.Add(this.insertStudentClass);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn StudentName;
        private System.Windows.Forms.ComboBox combPromoted;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.ComboBox combStatus;
        private System.Windows.Forms.ComboBox combYear;
        private System.Windows.Forms.Label Firstname;
        private System.Windows.Forms.Label Lastname;
        private System.Windows.Forms.Label Promoted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInsertStudent;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button insertStudentClass;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnInsertStatus;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.TextBox txtClass;
    }
}

