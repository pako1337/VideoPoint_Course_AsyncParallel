namespace WinForms_Async
{
    partial class BestProductReport
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
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.btnRaport = new System.Windows.Forms.Button();
            this.btnThread = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // resultGrid
            // 
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.Location = new System.Drawing.Point(12, 12);
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.Size = new System.Drawing.Size(900, 435);
            this.resultGrid.TabIndex = 0;
            // 
            // btnRaport
            // 
            this.btnRaport.Location = new System.Drawing.Point(13, 453);
            this.btnRaport.Name = "btnRaport";
            this.btnRaport.Size = new System.Drawing.Size(75, 23);
            this.btnRaport.TabIndex = 1;
            this.btnRaport.Text = "Raport";
            this.btnRaport.UseVisualStyleBackColor = true;
            this.btnRaport.Click += new System.EventHandler(this.btnRaport_Click);
            // 
            // btnThread
            // 
            this.btnThread.Location = new System.Drawing.Point(95, 454);
            this.btnThread.Name = "btnThread";
            this.btnThread.Size = new System.Drawing.Size(100, 23);
            this.btnThread.TabIndex = 2;
            this.btnThread.Text = "Raport w wątku";
            this.btnThread.UseVisualStyleBackColor = true;
            this.btnThread.Click += new System.EventHandler(this.btnThread_Click);
            // 
            // BestProductReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 488);
            this.Controls.Add(this.btnThread);
            this.Controls.Add(this.btnRaport);
            this.Controls.Add(this.resultGrid);
            this.Name = "BestProductReport";
            this.Text = "Najlepsze produkty";
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.Button btnRaport;
        private System.Windows.Forms.Button btnThread;
    }
}