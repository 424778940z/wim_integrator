namespace wim_integrator
{
    partial class form
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_vol = new System.Windows.Forms.ListView();
            this.textBox_search_folder = new System.Windows.Forms.TextBox();
            this.label_search_folder = new System.Windows.Forms.Label();
            this.button_search_folder_sel = new System.Windows.Forms.Button();
            this.progressBar_integration = new System.Windows.Forms.ProgressBar();
            this.button_integrate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_vol
            // 
            this.listView_vol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_vol.Location = new System.Drawing.Point(12, 41);
            this.listView_vol.Name = "listView_vol";
            this.listView_vol.Size = new System.Drawing.Size(680, 381);
            this.listView_vol.TabIndex = 0;
            this.listView_vol.UseCompatibleStateImageBehavior = false;
            // 
            // textBox_search_folder
            // 
            this.textBox_search_folder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_search_folder.Location = new System.Drawing.Point(101, 14);
            this.textBox_search_folder.Name = "textBox_search_folder";
            this.textBox_search_folder.Size = new System.Drawing.Size(512, 21);
            this.textBox_search_folder.TabIndex = 1;
            // 
            // label_search_folder
            // 
            this.label_search_folder.AutoSize = true;
            this.label_search_folder.Location = new System.Drawing.Point(12, 17);
            this.label_search_folder.Name = "label_search_folder";
            this.label_search_folder.Size = new System.Drawing.Size(83, 12);
            this.label_search_folder.TabIndex = 2;
            this.label_search_folder.Text = "Search Folder";
            // 
            // button_search_folder_sel
            // 
            this.button_search_folder_sel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_search_folder_sel.Location = new System.Drawing.Point(619, 12);
            this.button_search_folder_sel.Name = "button_search_folder_sel";
            this.button_search_folder_sel.Size = new System.Drawing.Size(73, 23);
            this.button_search_folder_sel.TabIndex = 3;
            this.button_search_folder_sel.Text = "Select";
            this.button_search_folder_sel.UseVisualStyleBackColor = true;
            this.button_search_folder_sel.Click += new System.EventHandler(this.button_search_folder_sel_Click);
            // 
            // progressBar_integration
            // 
            this.progressBar_integration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar_integration.Location = new System.Drawing.Point(12, 429);
            this.progressBar_integration.Name = "progressBar_integration";
            this.progressBar_integration.Size = new System.Drawing.Size(601, 23);
            this.progressBar_integration.TabIndex = 4;
            // 
            // button_integrate
            // 
            this.button_integrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_integrate.Location = new System.Drawing.Point(619, 429);
            this.button_integrate.Name = "button_integrate";
            this.button_integrate.Size = new System.Drawing.Size(73, 23);
            this.button_integrate.TabIndex = 5;
            this.button_integrate.Text = "Integrate";
            this.button_integrate.UseVisualStyleBackColor = true;
            this.button_integrate.Click += new System.EventHandler(this.button_integrate_Click);
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.button_integrate);
            this.Controls.Add(this.progressBar_integration);
            this.Controls.Add(this.button_search_folder_sel);
            this.Controls.Add(this.label_search_folder);
            this.Controls.Add(this.textBox_search_folder);
            this.Controls.Add(this.listView_vol);
            this.Name = "form";
            this.Text = "WIM Integrator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_vol;
        private System.Windows.Forms.TextBox textBox_search_folder;
        private System.Windows.Forms.Label label_search_folder;
        private System.Windows.Forms.Button button_search_folder_sel;
        private System.Windows.Forms.ProgressBar progressBar_integration;
        private System.Windows.Forms.Button button_integrate;
    }
}

