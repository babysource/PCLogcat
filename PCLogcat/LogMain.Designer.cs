namespace PCLogcat
{
    partial class LogMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.droidplay != null)
                {
                    this.closeListen();
                }
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogMain));
            this.logcatBtn = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.TableLayoutPanel();
            this.writeLog = new System.Windows.Forms.RichTextBox();
            this.lockBtn = new System.Windows.Forms.Button();
            this.queryTxt = new System.Windows.Forms.TextBox();
            this.queryBtn = new System.Windows.Forms.Button();
            this.writeTimer = new System.Windows.Forms.Timer(this.components);
            this.gridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // logcatBtn
            // 
            this.logcatBtn.BackColor = System.Drawing.Color.Green;
            this.gridPanel.SetColumnSpan(this.logcatBtn, 3);
            this.logcatBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logcatBtn.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logcatBtn.ForeColor = System.Drawing.Color.White;
            this.logcatBtn.Location = new System.Drawing.Point(3, 3);
            this.logcatBtn.Name = "logcatBtn";
            this.logcatBtn.Size = new System.Drawing.Size(978, 59);
            this.logcatBtn.TabIndex = 0;
            this.logcatBtn.Text = "开 始 监 听";
            this.logcatBtn.UseVisualStyleBackColor = false;
            this.logcatBtn.Click += new System.EventHandler(this.logcatBtn_Click);
            // 
            // gridPanel
            // 
            this.gridPanel.ColumnCount = 3;
            this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.gridPanel.Controls.Add(this.logcatBtn, 0, 0);
            this.gridPanel.Controls.Add(this.writeLog, 0, 1);
            this.gridPanel.Controls.Add(this.lockBtn, 2, 2);
            this.gridPanel.Controls.Add(this.queryTxt, 0, 2);
            this.gridPanel.Controls.Add(this.queryBtn, 1, 2);
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPanel.Location = new System.Drawing.Point(0, 0);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.RowCount = 3;
            this.gridPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.gridPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.gridPanel.Size = new System.Drawing.Size(984, 662);
            this.gridPanel.TabIndex = 3;
            // 
            // writeLog
            // 
            this.writeLog.BackColor = System.Drawing.Color.Black;
            this.gridPanel.SetColumnSpan(this.writeLog, 3);
            this.writeLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.writeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeLog.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeLog.ForeColor = System.Drawing.Color.White;
            this.writeLog.Location = new System.Drawing.Point(3, 68);
            this.writeLog.Name = "writeLog";
            this.writeLog.ReadOnly = true;
            this.writeLog.Size = new System.Drawing.Size(978, 555);
            this.writeLog.TabIndex = 4;
            this.writeLog.Text = "";
            this.writeLog.WordWrap = false;
            this.writeLog.GotFocus += new System.EventHandler(this.writeLog_Focus);
            // 
            // lockBtn
            // 
            this.lockBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lockBtn.Enabled = false;
            this.lockBtn.Image = global::PCLogcat.Properties.Resources.unlock;
            this.lockBtn.Location = new System.Drawing.Point(951, 629);
            this.lockBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.lockBtn.Name = "lockBtn";
            this.lockBtn.Size = new System.Drawing.Size(30, 27);
            this.lockBtn.TabIndex = 5;
            this.lockBtn.UseVisualStyleBackColor = true;
            this.lockBtn.Click += new System.EventHandler(this.lockBtn_Click);
            // 
            // queryTxt
            // 
            this.queryTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.queryTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryTxt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.queryTxt.Location = new System.Drawing.Point(3, 631);
            this.queryTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 6);
            this.queryTxt.Name = "queryTxt";
            this.queryTxt.Size = new System.Drawing.Size(822, 23);
            this.queryTxt.TabIndex = 2;
            // 
            // queryBtn
            // 
            this.queryBtn.BackColor = System.Drawing.Color.Yellow;
            this.queryBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.queryBtn.ForeColor = System.Drawing.Color.Black;
            this.queryBtn.Location = new System.Drawing.Point(828, 629);
            this.queryBtn.Margin = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(120, 27);
            this.queryBtn.TabIndex = 3;
            this.queryBtn.Text = "开 始 查 找";
            this.queryBtn.UseVisualStyleBackColor = false;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // writeTimer
            // 
            this.writeTimer.Interval = 300;
            this.writeTimer.Tick += new System.EventHandler(this.writeTimer_Tick);
            // 
            // LogMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.gridPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCLogcat";
            this.Load += new System.EventHandler(this.LogMain_Load);
            this.gridPanel.ResumeLayout(false);
            this.gridPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button logcatBtn;
        private System.Windows.Forms.TableLayoutPanel gridPanel;
        private System.Windows.Forms.TextBox queryTxt;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.RichTextBox writeLog;
        private System.Windows.Forms.Button lockBtn;
        private System.Windows.Forms.Timer writeTimer;
    }
}

