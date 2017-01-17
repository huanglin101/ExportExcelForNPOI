namespace ImportScoreDetailsForExam
{
    partial class ExportExcel
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
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnChapter = new System.Windows.Forms.Button();
            this.btnChapterKnowPoint = new System.Windows.Forms.Button();
            this.btnTextBookSerise = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(445, 29);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(52, 31);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(361, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnChapter
            // 
            this.btnChapter.Location = new System.Drawing.Point(86, 116);
            this.btnChapter.Name = "btnChapter";
            this.btnChapter.Size = new System.Drawing.Size(75, 23);
            this.btnChapter.TabIndex = 2;
            this.btnChapter.Text = "导入章节";
            this.btnChapter.UseVisualStyleBackColor = true;
            this.btnChapter.Click += new System.EventHandler(this.btnChapter_Click);
            // 
            // btnChapterKnowPoint
            // 
            this.btnChapterKnowPoint.Location = new System.Drawing.Point(214, 116);
            this.btnChapterKnowPoint.Name = "btnChapterKnowPoint";
            this.btnChapterKnowPoint.Size = new System.Drawing.Size(75, 23);
            this.btnChapterKnowPoint.TabIndex = 3;
            this.btnChapterKnowPoint.Text = "章节知识点";
            this.btnChapterKnowPoint.UseVisualStyleBackColor = true;
            this.btnChapterKnowPoint.Click += new System.EventHandler(this.btnChapterKnowPoint_Click);
            // 
            // btnTextBookSerise
            // 
            this.btnTextBookSerise.Location = new System.Drawing.Point(347, 116);
            this.btnTextBookSerise.Name = "btnTextBookSerise";
            this.btnTextBookSerise.Size = new System.Drawing.Size(75, 23);
            this.btnTextBookSerise.TabIndex = 4;
            this.btnTextBookSerise.Text = "教材";
            this.btnTextBookSerise.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ExportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 262);
            this.Controls.Add(this.btnTextBookSerise);
            this.Controls.Add(this.btnChapterKnowPoint);
            this.Controls.Add(this.btnChapter);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "ExportExcel";
            this.Text = "ExportExcel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnChapter;
        private System.Windows.Forms.Button btnChapterKnowPoint;
        private System.Windows.Forms.Button btnTextBookSerise;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}