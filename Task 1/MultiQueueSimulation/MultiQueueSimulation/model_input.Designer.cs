namespace MultiQueueSimulation
{
    partial class model_input
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numSer = new System.Windows.Forms.TextBox();
            this.numStop = new System.Windows.Forms.TextBox();
            this.ntx_btn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.time_number = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.testcase = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NumberOfServers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "StoppingNumber";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "StoppingCriteria";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "SelectionMethod";
            // 
            // numSer
            // 
            this.numSer.Location = new System.Drawing.Point(205, 49);
            this.numSer.Name = "numSer";
            this.numSer.Size = new System.Drawing.Size(141, 20);
            this.numSer.TabIndex = 4;
            this.numSer.Text = "2";
            // 
            // numStop
            // 
            this.numStop.Location = new System.Drawing.Point(205, 105);
            this.numStop.Name = "numStop";
            this.numStop.Size = new System.Drawing.Size(141, 20);
            this.numStop.TabIndex = 5;
            this.numStop.Text = "100";
            // 
            // ntx_btn
            // 
            this.ntx_btn.Location = new System.Drawing.Point(205, 353);
            this.ntx_btn.Name = "ntx_btn";
            this.ntx_btn.Size = new System.Drawing.Size(96, 45);
            this.ntx_btn.TabIndex = 8;
            this.ntx_btn.Text = "Next";
            this.ntx_btn.UseVisualStyleBackColor = true;
            this.ntx_btn.Click += new System.EventHandler(this.ntx_btn_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(228, 164);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(228, 214);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(92, 21);
            this.comboBox2.TabIndex = 10;
            // 
            // time_number
            // 
            this.time_number.Location = new System.Drawing.Point(205, 273);
            this.time_number.Name = "time_number";
            this.time_number.Size = new System.Drawing.Size(141, 20);
            this.time_number.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Simulation end time";
            // 
            // testcase
            // 
            this.testcase.Location = new System.Drawing.Point(97, 326);
            this.testcase.Name = "testcase";
            this.testcase.Size = new System.Drawing.Size(30, 20);
            this.testcase.TabIndex = 13;
            // 
            // model_input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 477);
            this.Controls.Add(this.testcase);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.time_number);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ntx_btn);
            this.Controls.Add(this.numStop);
            this.Controls.Add(this.numSer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "model_input";
            this.Text = "input_form";
            this.Load += new System.EventHandler(this.model_input_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numSer;
        private System.Windows.Forms.TextBox numStop;
        private System.Windows.Forms.Button ntx_btn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox time_number;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox testcase;
    }
}