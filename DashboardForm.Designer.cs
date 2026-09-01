namespace SonicWallManager
{
    partial class DashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblIp = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.gridInterfaces = new System.Windows.Forms.DataGridView();
            this.btnLoadInterfaces = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Tab1Network = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridSvrIps = new System.Windows.Forms.DataGridView();
            this.btnLoadSvrIp = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNonSscConfig = new System.Windows.Forms.Button();
            this.btnSscConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridInterfaces)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Tab1Network.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSvrIps)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTitle.Location = new System.Drawing.Point(13, 20);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(359, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SonicWall Manager";
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIp.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblIp.Location = new System.Drawing.Point(611, 31);
            this.lblIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(148, 27);
            this.lblIp.TabIndex = 1;
            this.lblIp.Text = "Sonicwall IP:-";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.Black;
            this.btnLogout.Location = new System.Drawing.Point(1170, 20);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(122, 54);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // gridInterfaces
            // 
            this.gridInterfaces.AllowUserToAddRows = false;
            this.gridInterfaces.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridInterfaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInterfaces.Location = new System.Drawing.Point(34, 145);
            this.gridInterfaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridInterfaces.Name = "gridInterfaces";
            this.gridInterfaces.ReadOnly = true;
            this.gridInterfaces.RowHeadersWidth = 62;
            this.gridInterfaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridInterfaces.Size = new System.Drawing.Size(1227, 309);
            this.gridInterfaces.TabIndex = 0;
            this.gridInterfaces.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridInterfaces_CellContentClick);
            // 
            // btnLoadInterfaces
            // 
            this.btnLoadInterfaces.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoadInterfaces.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadInterfaces.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLoadInterfaces.Location = new System.Drawing.Point(34, 34);
            this.btnLoadInterfaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadInterfaces.Name = "btnLoadInterfaces";
            this.btnLoadInterfaces.Size = new System.Drawing.Size(223, 62);
            this.btnLoadInterfaces.TabIndex = 1;
            this.btnLoadInterfaces.Text = "Load Network";
            this.btnLoadInterfaces.UseVisualStyleBackColor = false;
            this.btnLoadInterfaces.Click += new System.EventHandler(this.btnLoadInterfaces_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(78, 685);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(21, 20);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "...";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.Tab1Network);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(81, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 7);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1305, 572);
            this.tabControl1.TabIndex = 9;
            // 
            // Tab1Network
            // 
            this.Tab1Network.Controls.Add(this.btnSscConfig);
            this.Tab1Network.Controls.Add(this.btnNonSscConfig);
            this.Tab1Network.Controls.Add(this.gridInterfaces);
            this.Tab1Network.Controls.Add(this.btnLoadInterfaces);
            this.Tab1Network.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Tab1Network.Location = new System.Drawing.Point(34, 4);
            this.Tab1Network.Name = "Tab1Network";
            this.Tab1Network.Padding = new System.Windows.Forms.Padding(3);
            this.Tab1Network.Size = new System.Drawing.Size(1267, 564);
            this.Tab1Network.TabIndex = 0;
            this.Tab1Network.Text = "Network";
            this.Tab1Network.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridSvrIps);
            this.tabPage2.Controls.Add(this.btnLoadSvrIp);
            this.tabPage2.Location = new System.Drawing.Point(34, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1267, 564);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridSvrIps
            // 
            this.gridSvrIps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSvrIps.Location = new System.Drawing.Point(27, 143);
            this.gridSvrIps.Name = "gridSvrIps";
            this.gridSvrIps.RowHeadersWidth = 62;
            this.gridSvrIps.RowTemplate.Height = 28;
            this.gridSvrIps.Size = new System.Drawing.Size(887, 314);
            this.gridSvrIps.TabIndex = 1;
            this.gridSvrIps.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSvrIps_CellContentClick);
            // 
            // btnLoadSvrIp
            // 
            this.btnLoadSvrIp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoadSvrIp.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSvrIp.Location = new System.Drawing.Point(32, 37);
            this.btnLoadSvrIp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadSvrIp.Name = "btnLoadSvrIp";
            this.btnLoadSvrIp.Size = new System.Drawing.Size(223, 62);
            this.btnLoadSvrIp.TabIndex = 0;
            this.btnLoadSvrIp.Text = "Load Object";
            this.btnLoadSvrIp.UseVisualStyleBackColor = false;
            this.btnLoadSvrIp.Click += new System.EventHandler(this.btnLoadSvrIp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.lblIp);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1305, 90);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1305, 572);
            this.panel2.TabIndex = 11;
            // 
            // btnNonSscConfig
            // 
            this.btnNonSscConfig.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNonSscConfig.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNonSscConfig.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNonSscConfig.Location = new System.Drawing.Point(470, 34);
            this.btnNonSscConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNonSscConfig.Name = "btnNonSscConfig";
            this.btnNonSscConfig.Size = new System.Drawing.Size(223, 62);
            this.btnNonSscConfig.TabIndex = 2;
            this.btnNonSscConfig.Text = "Enable";
            this.btnNonSscConfig.UseVisualStyleBackColor = false;
            this.btnNonSscConfig.Click += new System.EventHandler(this.btnNonSscConfig_Click);
            // 
            // btnSscConfig
            // 
            this.btnSscConfig.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSscConfig.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSscConfig.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSscConfig.Location = new System.Drawing.Point(800, 34);
            this.btnSscConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSscConfig.Name = "btnSscConfig";
            this.btnSscConfig.Size = new System.Drawing.Size(223, 62);
            this.btnSscConfig.TabIndex = 3;
            this.btnSscConfig.Text = "DIsable";
            this.btnSscConfig.UseVisualStyleBackColor = false;
            this.btnSscConfig.Click += new System.EventHandler(this.btnSscConfig_Click);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 662);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridInterfaces)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Tab1Network.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSvrIps)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataGridView gridInterfaces;
        private System.Windows.Forms.Button btnLoadInterfaces;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Tab1Network;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLoadSvrIp;
        private System.Windows.Forms.DataGridView gridSvrIps;
        private System.Windows.Forms.Button btnSscConfig;
        private System.Windows.Forms.Button btnNonSscConfig;
    }

}