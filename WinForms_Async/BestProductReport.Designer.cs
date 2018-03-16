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
            System.Windows.Forms.ProgressBar progressBar1;
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.btnRaport = new System.Windows.Forms.Button();
            this.btnThread = new System.Windows.Forms.Button();
            this.btnThreadPool = new System.Windows.Forms.Button();
            this.btnTask = new System.Windows.Forms.Button();
            this.btnBeginExecute = new System.Windows.Forms.Button();
            this.btnAsync = new System.Windows.Forms.Button();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            progressBar1.Location = new System.Drawing.Point(12, 11);
            progressBar1.MarqueeAnimationSpeed = 50;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(900, 23);
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 4;
            // 
            // resultGrid
            // 
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.Location = new System.Drawing.Point(12, 40);
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.Size = new System.Drawing.Size(900, 407);
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
            // btnThreadPool
            // 
            this.btnThreadPool.Location = new System.Drawing.Point(201, 454);
            this.btnThreadPool.Name = "btnThreadPool";
            this.btnThreadPool.Size = new System.Drawing.Size(119, 23);
            this.btnThreadPool.TabIndex = 3;
            this.btnThreadPool.Text = "Raport z puli wątków";
            this.btnThreadPool.UseVisualStyleBackColor = true;
            this.btnThreadPool.Click += new System.EventHandler(this.btnThreadPool_Click);
            // 
            // btnTask
            // 
            this.btnTask.Location = new System.Drawing.Point(326, 454);
            this.btnTask.Name = "btnTask";
            this.btnTask.Size = new System.Drawing.Size(103, 23);
            this.btnTask.TabIndex = 5;
            this.btnTask.Text = "Raport z Taska";
            this.btnTask.UseVisualStyleBackColor = true;
            this.btnTask.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // btnBeginExecute
            // 
            this.btnBeginExecute.Location = new System.Drawing.Point(436, 454);
            this.btnBeginExecute.Name = "btnBeginExecute";
            this.btnBeginExecute.Size = new System.Drawing.Size(130, 23);
            this.btnBeginExecute.TabIndex = 6;
            this.btnBeginExecute.Text = "Begin Execute Reader";
            this.btnBeginExecute.UseVisualStyleBackColor = true;
            this.btnBeginExecute.Click += new System.EventHandler(this.btnBeginExecute_Click);
            // 
            // btnAsync
            // 
            this.btnAsync.Location = new System.Drawing.Point(573, 454);
            this.btnAsync.Name = "btnAsync";
            this.btnAsync.Size = new System.Drawing.Size(105, 23);
            this.btnAsync.TabIndex = 7;
            this.btnAsync.Text = "Async Raport";
            this.btnAsync.UseVisualStyleBackColor = true;
            this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
            // 
            // BestProductReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 488);
            this.Controls.Add(this.btnAsync);
            this.Controls.Add(this.btnBeginExecute);
            this.Controls.Add(this.btnTask);
            this.Controls.Add(progressBar1);
            this.Controls.Add(this.btnThreadPool);
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
        private System.Windows.Forms.Button btnThreadPool;
        private System.Windows.Forms.Button btnTask;
        private System.Windows.Forms.Button btnBeginExecute;
        private System.Windows.Forms.Button btnAsync;
    }
}