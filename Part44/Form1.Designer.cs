namespace Part44
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCalculate = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnCalculateByTask = new System.Windows.Forms.Button();
            this.btnCalculateByAsync = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(3, 3);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 1;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(800, 413);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnCalculateByTask
            // 
            this.btnCalculateByTask.Location = new System.Drawing.Point(84, 3);
            this.btnCalculateByTask.Name = "btnCalculateByTask";
            this.btnCalculateByTask.Size = new System.Drawing.Size(117, 23);
            this.btnCalculateByTask.TabIndex = 3;
            this.btnCalculateByTask.Text = "Calculate by task";
            this.btnCalculateByTask.UseVisualStyleBackColor = true;
            this.btnCalculateByTask.Click += new System.EventHandler(this.BtnCalculateByTask_Click);
            // 
            // btnCalculateByAsync
            // 
            this.btnCalculateByAsync.Location = new System.Drawing.Point(207, 3);
            this.btnCalculateByAsync.Name = "btnCalculateByAsync";
            this.btnCalculateByAsync.Size = new System.Drawing.Size(131, 23);
            this.btnCalculateByAsync.TabIndex = 4;
            this.btnCalculateByAsync.Text = "Calculate by async";
            this.btnCalculateByAsync.UseVisualStyleBackColor = true;
            this.btnCalculateByAsync.Click += new System.EventHandler(this.BtnCalculateByAsync_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCalculate);
            this.flowLayoutPanel1.Controls.Add(this.btnCalculateByTask);
            this.flowLayoutPanel1.Controls.Add(this.btnCalculateByAsync);
            this.flowLayoutPanel1.Controls.Add(this.progressBar1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 419);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 31);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(344, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnCalculate;
        private RichTextBox richTextBox1;
        private Button btnCalculateByTask;
        private Button btnCalculateByAsync;
        private FlowLayoutPanel flowLayoutPanel1;
        private ProgressBar progressBar1;
    }
}