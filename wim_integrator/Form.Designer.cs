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
            this.progressBar_step = new System.Windows.Forms.ProgressBar();
            this.button_integrate = new System.Windows.Forms.Button();
            this.progressBar_total = new System.Windows.Forms.ProgressBar();
            this.label_mnt_point = new System.Windows.Forms.Label();
            this.textBox_mnt_point = new System.Windows.Forms.TextBox();
            this.textBox_tmp_folder = new System.Windows.Forms.TextBox();
            this.label_tmp_folder = new System.Windows.Forms.Label();
            this.button_mount_point_sel = new System.Windows.Forms.Button();
            this.button_tmp_folder_sel = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label_search_rule = new System.Windows.Forms.Label();
            this.textBox_search_rule = new System.Windows.Forms.TextBox();
            this.label_comp_lv = new System.Windows.Forms.Label();
            this.comboBox_comp_lv = new System.Windows.Forms.ComboBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_vol
            // 
            this.listView_vol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_vol.Location = new System.Drawing.Point(14, 148);
            this.listView_vol.Name = "listView_vol";
            this.listView_vol.Size = new System.Drawing.Size(680, 259);
            this.listView_vol.TabIndex = 0;
            this.listView_vol.UseCompatibleStateImageBehavior = false;
            // 
            // textBox_search_folder
            // 
            this.textBox_search_folder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_search_folder.Location = new System.Drawing.Point(133, 14);
            this.textBox_search_folder.Name = "textBox_search_folder";
            this.textBox_search_folder.Size = new System.Drawing.Size(480, 21);
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
            // progressBar_step
            // 
            this.progressBar_step.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_step.Location = new System.Drawing.Point(12, 413);
            this.progressBar_step.Name = "progressBar_step";
            this.progressBar_step.Size = new System.Drawing.Size(601, 11);
            this.progressBar_step.TabIndex = 4;
            // 
            // button_integrate
            // 
            this.button_integrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_integrate.Location = new System.Drawing.Point(619, 413);
            this.button_integrate.Name = "button_integrate";
            this.button_integrate.Size = new System.Drawing.Size(73, 23);
            this.button_integrate.TabIndex = 5;
            this.button_integrate.Text = "Integrate";
            this.button_integrate.UseVisualStyleBackColor = true;
            this.button_integrate.Click += new System.EventHandler(this.button_integrate_Click);
            // 
            // progressBar_total
            // 
            this.progressBar_total.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_total.Location = new System.Drawing.Point(12, 425);
            this.progressBar_total.Name = "progressBar_total";
            this.progressBar_total.Size = new System.Drawing.Size(601, 11);
            this.progressBar_total.TabIndex = 4;
            // 
            // label_mnt_point
            // 
            this.label_mnt_point.AutoSize = true;
            this.label_mnt_point.Location = new System.Drawing.Point(12, 45);
            this.label_mnt_point.Name = "label_mnt_point";
            this.label_mnt_point.Size = new System.Drawing.Size(71, 12);
            this.label_mnt_point.TabIndex = 2;
            this.label_mnt_point.Text = "Mount Point";
            // 
            // textBox_mnt_point
            // 
            this.textBox_mnt_point.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_mnt_point.Location = new System.Drawing.Point(133, 41);
            this.textBox_mnt_point.Name = "textBox_mnt_point";
            this.textBox_mnt_point.Size = new System.Drawing.Size(480, 21);
            this.textBox_mnt_point.TabIndex = 1;
            // 
            // textBox_tmp_folder
            // 
            this.textBox_tmp_folder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_tmp_folder.Location = new System.Drawing.Point(133, 68);
            this.textBox_tmp_folder.Name = "textBox_tmp_folder";
            this.textBox_tmp_folder.Size = new System.Drawing.Size(480, 21);
            this.textBox_tmp_folder.TabIndex = 1;
            // 
            // label_tmp_folder
            // 
            this.label_tmp_folder.AutoSize = true;
            this.label_tmp_folder.Location = new System.Drawing.Point(12, 72);
            this.label_tmp_folder.Name = "label_tmp_folder";
            this.label_tmp_folder.Size = new System.Drawing.Size(101, 12);
            this.label_tmp_folder.TabIndex = 2;
            this.label_tmp_folder.Text = "Temporary Folder";
            // 
            // button_mount_point_sel
            // 
            this.button_mount_point_sel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_mount_point_sel.Location = new System.Drawing.Point(619, 39);
            this.button_mount_point_sel.Name = "button_mount_point_sel";
            this.button_mount_point_sel.Size = new System.Drawing.Size(73, 23);
            this.button_mount_point_sel.TabIndex = 3;
            this.button_mount_point_sel.Text = "Select";
            this.button_mount_point_sel.UseVisualStyleBackColor = true;
            this.button_mount_point_sel.Click += new System.EventHandler(this.button_mount_point_sel_Click);
            // 
            // button_tmp_folder_sel
            // 
            this.button_tmp_folder_sel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_tmp_folder_sel.Location = new System.Drawing.Point(619, 66);
            this.button_tmp_folder_sel.Name = "button_tmp_folder_sel";
            this.button_tmp_folder_sel.Size = new System.Drawing.Size(73, 23);
            this.button_tmp_folder_sel.TabIndex = 3;
            this.button_tmp_folder_sel.Text = "Select";
            this.button_tmp_folder_sel.UseVisualStyleBackColor = true;
            this.button_tmp_folder_sel.Click += new System.EventHandler(this.button_tmp_folder_sel_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 439);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(704, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // label_search_rule
            // 
            this.label_search_rule.AutoSize = true;
            this.label_search_rule.Location = new System.Drawing.Point(12, 98);
            this.label_search_rule.Name = "label_search_rule";
            this.label_search_rule.Size = new System.Drawing.Size(71, 12);
            this.label_search_rule.TabIndex = 2;
            this.label_search_rule.Text = "Search Rule";
            // 
            // textBox_search_rule
            // 
            this.textBox_search_rule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_search_rule.Location = new System.Drawing.Point(133, 95);
            this.textBox_search_rule.Name = "textBox_search_rule";
            this.textBox_search_rule.Size = new System.Drawing.Size(480, 21);
            this.textBox_search_rule.TabIndex = 1;
            // 
            // label_comp_lv
            // 
            this.label_comp_lv.AutoSize = true;
            this.label_comp_lv.Location = new System.Drawing.Point(12, 125);
            this.label_comp_lv.Name = "label_comp_lv";
            this.label_comp_lv.Size = new System.Drawing.Size(101, 12);
            this.label_comp_lv.TabIndex = 2;
            this.label_comp_lv.Text = "Compresson Level";
            // 
            // comboBox_comp_lv
            // 
            this.comboBox_comp_lv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_comp_lv.FormattingEnabled = true;
            this.comboBox_comp_lv.Location = new System.Drawing.Point(133, 122);
            this.comboBox_comp_lv.Name = "comboBox_comp_lv";
            this.comboBox_comp_lv.Size = new System.Drawing.Size(480, 20);
            this.comboBox_comp_lv.TabIndex = 7;
            this.comboBox_comp_lv.SelectedIndexChanged += new System.EventHandler(this.comboBox_comp_lv_SelectedIndexChanged);
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.comboBox_comp_lv);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.button_integrate);
            this.Controls.Add(this.progressBar_total);
            this.Controls.Add(this.progressBar_step);
            this.Controls.Add(this.button_tmp_folder_sel);
            this.Controls.Add(this.button_mount_point_sel);
            this.Controls.Add(this.button_search_folder_sel);
            this.Controls.Add(this.label_comp_lv);
            this.Controls.Add(this.label_search_rule);
            this.Controls.Add(this.label_tmp_folder);
            this.Controls.Add(this.label_mnt_point);
            this.Controls.Add(this.label_search_folder);
            this.Controls.Add(this.textBox_search_rule);
            this.Controls.Add(this.textBox_tmp_folder);
            this.Controls.Add(this.textBox_mnt_point);
            this.Controls.Add(this.textBox_search_folder);
            this.Controls.Add(this.listView_vol);
            this.MinimumSize = new System.Drawing.Size(720, 500);
            this.Name = "form";
            this.Text = "WIM Integrator";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_vol;
        private System.Windows.Forms.TextBox textBox_search_folder;
        private System.Windows.Forms.Label label_search_folder;
        private System.Windows.Forms.Button button_search_folder_sel;
        private System.Windows.Forms.ProgressBar progressBar_step;
        private System.Windows.Forms.Button button_integrate;
        private System.Windows.Forms.ProgressBar progressBar_total;
        private System.Windows.Forms.Label label_mnt_point;
        private System.Windows.Forms.TextBox textBox_mnt_point;
        private System.Windows.Forms.TextBox textBox_tmp_folder;
        private System.Windows.Forms.Label label_tmp_folder;
        private System.Windows.Forms.Button button_mount_point_sel;
        private System.Windows.Forms.Button button_tmp_folder_sel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Label label_search_rule;
        private System.Windows.Forms.TextBox textBox_search_rule;
        private System.Windows.Forms.Label label_comp_lv;
        private System.Windows.Forms.ComboBox comboBox_comp_lv;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}

