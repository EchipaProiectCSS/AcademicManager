namespace InputOutputManagement
{
    partial class InsertStudentClass
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.combPromoted = new System.Windows.Forms.ComboBox();
            this.insertClass = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.textStudentId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class Name";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(118, 22);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(159, 20);
            this.txtClassName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Promoted";
            // 
            // combPromoted
            // 
            this.combPromoted.FormattingEnabled = true;
            this.combPromoted.Location = new System.Drawing.Point(118, 72);
            this.combPromoted.Name = "combPromoted";
            this.combPromoted.Size = new System.Drawing.Size(159, 21);
            this.combPromoted.TabIndex = 3;
            // 
            // insertClass
            // 
            this.insertClass.Location = new System.Drawing.Point(32, 162);
            this.insertClass.Name = "insertClass";
            this.insertClass.Size = new System.Drawing.Size(136, 23);
            this.insertClass.TabIndex = 4;
            this.insertClass.Text = "Insert Class";
            this.insertClass.UseVisualStyleBackColor = true;
            this.insertClass.Click += new System.EventHandler(this.insertClass_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(215, 162);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(117, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Student Id";
            // 
            // textStudentId
            // 
            this.textStudentId.Location = new System.Drawing.Point(118, 111);
            this.textStudentId.Name = "textStudentId";
            this.textStudentId.Size = new System.Drawing.Size(100, 20);
            this.textStudentId.TabIndex = 7;
            // 
            // InsertStudentClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 236);
            this.Controls.Add(this.textStudentId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.insertClass);
            this.Controls.Add(this.combPromoted);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label1);
            this.Name = "InsertStudentClass";
            this.Text = "InsertStudentClass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combPromoted;
        private System.Windows.Forms.Button insertClass;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textStudentId;
    }
}