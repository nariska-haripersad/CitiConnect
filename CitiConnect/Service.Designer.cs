namespace CitiConnect
{
    partial class Service
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelServiceRequests = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxLayout = new System.Windows.Forms.ComboBox();
            this.panelProvinces = new System.Windows.Forms.Panel();
            this.treeViewServiceRequests = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.panelProvinces.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtSearch.Location = new System.Drawing.Point(403, 56);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 36);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            // 
            // lblItemCount
            // 
            this.lblItemCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblItemCount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblItemCount.Location = new System.Drawing.Point(1035, 171);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(150, 25);
            this.lblItemCount.TabIndex = 8;
            this.lblItemCount.Text = "Items: 0";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSearch.Image = global::CitiConnect.Properties.Resources.search1;
            this.btnSearch.Location = new System.Drawing.Point(709, 59);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(42, 36);
            this.btnSearch.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label1.Location = new System.Drawing.Point(314, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 10;
            this.label1.Text = "Search:";
            // 
            // panelServiceRequests
            // 
            this.panelServiceRequests.AutoScroll = true;
            this.panelServiceRequests.Location = new System.Drawing.Point(25, 199);
            this.panelServiceRequests.Name = "panelServiceRequests";
            this.panelServiceRequests.Size = new System.Drawing.Size(1160, 690);
            this.panelServiceRequests.TabIndex = 0;
            // 
            // comboBoxLayout
            // 
            this.comboBoxLayout.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLayout.FormattingEnabled = true;
            this.comboBoxLayout.Location = new System.Drawing.Point(40, 165);
            this.comboBoxLayout.Name = "comboBoxLayout";
            this.comboBoxLayout.Size = new System.Drawing.Size(121, 31);
            this.comboBoxLayout.TabIndex = 11;
            this.comboBoxLayout.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayout_SelectedIndexChanged_1);
            // 
            // panelProvinces
            // 
            this.panelProvinces.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProvinces.Controls.Add(this.treeViewServiceRequests);
            this.panelProvinces.Location = new System.Drawing.Point(1212, 116);
            this.panelProvinces.Name = "panelProvinces";
            this.panelProvinces.Size = new System.Drawing.Size(578, 683);
            this.panelProvinces.TabIndex = 12;
            // 
            // treeViewServiceRequests
            // 
            this.treeViewServiceRequests.Location = new System.Drawing.Point(241, 16);
            this.treeViewServiceRequests.Name = "treeViewServiceRequests";
            this.treeViewServiceRequests.Size = new System.Drawing.Size(321, 648);
            this.treeViewServiceRequests.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.label2.Location = new System.Drawing.Point(1168, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(607, 60);
            this.label2.TabIndex = 13;
            this.label2.Text = "            Want to know what\'s happening in your area?\r\nChoose your location to " +
    "see service requests made near you: ";
            // 
            // Service
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelProvinces);
            this.Controls.Add(this.comboBoxLayout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelServiceRequests);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.txtSearch);
            this.Name = "Service";
            this.Text = "Service Request Status";
            this.panelProvinces.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel panelServiceRequests;
        private System.Windows.Forms.ComboBox comboBoxLayout;
        private System.Windows.Forms.Panel panelProvinces;
        private System.Windows.Forms.TreeView treeViewServiceRequests;
        private System.Windows.Forms.Label label2;
    }
}