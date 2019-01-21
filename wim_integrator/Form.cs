using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;
using Microsoft.Dism;
using System.Threading;

namespace wim_integrator
{
    public partial class form : Form
    {
        //version
        string version_string = "Version: 0.83";

        //for Thread, since it seems no way to pass args
        //search_wim
        string search_path;
        string search_keyword;
        //integrate_wim
        string src_wim_path;
        int src_wim_vol = 0;
        string des_wim_path;
        //int des_wim_vol = 0;

        string mount_point;
        string tmp_folder;
        string comp_lv;

        ListView lv_ptr;

        public form()
        {
            InitializeComponent();

            //I know what I'm doing, no more stupid Microsoft call-recall endless events
            Control.CheckForIllegalCrossThreadCalls = false;

            //title
            this.Text = this.Text + " " + version_string;

            //options init
            mount_point = Environment.GetEnvironmentVariable("tmp") + "\\wim_integrator_mnt_point";
            this.textBox_mnt_point.Text = mount_point;

            this.comboBox_comp_lv.Items.Add("Maximum");
            this.comboBox_comp_lv.Items.Add("Fast");
            this.comboBox_comp_lv.Items.Add("None");
            this.comboBox_comp_lv.SelectedIndex = 0;
            this.comboBox_comp_lv.DropDownStyle =  ComboBoxStyle.DropDownList;

            this.textBox_search_rule.Text = "*.wim";
            
            //lv init
            this.listView_vol.View = View.Details;
            this.listView_vol.Columns.Add("Path");
            this.listView_vol.Columns.Add("Volume");
            this.listView_vol.Columns.Add("Name");
            this.listView_vol.Columns.Add("Platform");
            this.listView_vol.Columns.Add("Language");
            this.listView_vol.Columns.Add("Version");
            this.listView_vol.ContextMenuStrip = listview_contextmenu;

            refresh_status_label("Status", "Ready");
        }

        private void button_search_folder_sel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_sel_dialog = new System.Windows.Forms.FolderBrowserDialog();
            folder_sel_dialog.Description = "Select folder contains WIM files, I will search all sub folders.";
            folder_sel_dialog.ShowNewFolderButton = false;
            folder_sel_dialog.SelectedPath = this.textBox_search_folder.Text;
            if (folder_sel_dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_search_folder.Text = folder_sel_dialog.SelectedPath;
                search_wim_file(this.textBox_search_folder.Text, this.textBox_search_rule.Text);
            }
        }
        private void button_mount_point_sel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_sel_dialog = new System.Windows.Forms.FolderBrowserDialog();
            folder_sel_dialog.Description = "Note: this folder will be DELETED after integration!";
            folder_sel_dialog.ShowNewFolderButton = true;
            folder_sel_dialog.SelectedPath = this.textBox_search_folder.Text;
            if (folder_sel_dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_mnt_point.Text = folder_sel_dialog.SelectedPath;
            }
        }
        private void button_tmp_folder_sel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_sel_dialog = new System.Windows.Forms.FolderBrowserDialog();
            folder_sel_dialog.Description = "Note: this folder will be DELETED after integration!";
            folder_sel_dialog.ShowNewFolderButton = true;
            folder_sel_dialog.SelectedPath = this.textBox_search_folder.Text;
            if (folder_sel_dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_tmp_folder.Text = folder_sel_dialog.SelectedPath;
            }
        }
        private void button_integrate_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file_dialog = new System.Windows.Forms.SaveFileDialog();
            //file_dialog.Title = "Save ingrated wim file";
            save_file_dialog.Filter = "wim file (*.wim)|*.wim";
            if(save_file_dialog.ShowDialog() == DialogResult.OK)
            {
                integrate_wim_file(save_file_dialog.FileName);
            }
        }
        private void comboBox_comp_lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            comp_lv = this.comboBox_comp_lv.SelectedItem.ToString();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_vol.SelectedItems.Count > 0)
            {
                foreach (ListViewItem i in listView_vol.SelectedItems)
                {
                    listView_vol.Items.Remove(i);
                }
            }
        }

        private void refresh_status_label(string from, string text)
        {
            this.toolStripStatusLabel.Text = "[" + from + "]" + " " + text;
        }

        private void refresh_progress_bar_step(DismProgress dism_progress)
        {
            this.progressBar_step.Maximum = dism_progress.Total;
            this.progressBar_step.Minimum = 0;
            this.progressBar_step.Value = dism_progress.Current;
        }

        private void refresh_progress_bar_step(int current, int total)
        {
            this.progressBar_step.Maximum = total;
            this.progressBar_step.Minimum = 0;
            this.progressBar_step.Value = current;
        }

        private void refresh_progress_bar_total(int current, int total)
        {
            this.progressBar_total.Maximum = total;
            this.progressBar_total.Minimum = 0;
            this.progressBar_total.Value = current;
        }

        private void en_dis_able_everything(bool is_enable)
        {
            //this.Enabled = is_enable;
            
            this.textBox_search_folder.Enabled = is_enable;
            this.textBox_mnt_point.Enabled = is_enable;
            this.textBox_tmp_folder.Enabled = is_enable;
            this.textBox_search_rule.Enabled = is_enable;

            this.comboBox_comp_lv.Enabled = is_enable;

            this.button_search_folder_sel.Enabled = is_enable;
            this.button_mount_point_sel.Enabled = is_enable;
            this.button_tmp_folder_sel.Enabled = is_enable;
            this.button_integrate.Enabled = is_enable;        }
        
        private void search_wim()
        {
            //block buttons
            en_dis_able_everything(false);
            //clear progress bar
            refresh_progress_bar_step(0, 100);
            refresh_progress_bar_total(0, 100);
            //status bar
            refresh_status_label("Status", "Busy");

            List<string> wim_file_list = new List<string>();

            wim_file_list = Directory.GetFiles(search_path, search_keyword, SearchOption.AllDirectories).ToList();

            this.listView_vol.BeginUpdate();
            DismApi.Initialize(DismLogLevel.LogErrors);
            this.listView_vol.Items.Clear();
            for (int i = 0; i < wim_file_list.Count; i++)
            {
                DismImageInfoCollection imageInfos = DismApi.GetImageInfo(wim_file_list[i]);
                for (int j = 0; j < imageInfos.Count; j++)
                {
                    ListViewItem item_buff = new ListViewItem(wim_file_list[i]);

                    //vol
                    item_buff.SubItems.Add(imageInfos[j].ImageIndex.ToString());

                    //name
                    item_buff.SubItems.Add(imageInfos[j].ImageName);

                    //platform
                    string platform_translated;
                    switch (imageInfos[j].Architecture)
                    {
                        case DismProcessorArchitecture.None:
                            platform_translated = "None";
                            break;
                        case DismProcessorArchitecture.Intel:
                            platform_translated = "x86";
                            break;
                        case DismProcessorArchitecture.AMD64:
                            platform_translated = "amd64";
                            break;
                        case DismProcessorArchitecture.IA64:
                            platform_translated = "ia64";
                            break;
                        case DismProcessorArchitecture.Neutral:
                            platform_translated = "neutral";
                            break;
                        default:
                            platform_translated = "unknow";
                            break;
                    }
                    item_buff.SubItems.Add(platform_translated);

                    //lang
                    item_buff.SubItems.Add(imageInfos[j].DefaultLanguage.Name);

                    //version
                    item_buff.SubItems.Add(imageInfos[j].ProductVersion.ToString());

                    this.listView_vol.Items.Add(item_buff);

                    //progress bar for total index in single file
                    refresh_progress_bar_step(j + 1, imageInfos.Count);
                }
                //progress bar for total file
                refresh_progress_bar_total(i + 1, wim_file_list.Count);
            }
            DismApi.Shutdown();
            this.listView_vol.EndUpdate();
            this.listView_vol.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //unblock buttos
            en_dis_able_everything(true);
            refresh_status_label("Status", "Ready");
        }

        private void search_wim_file(string search_path, string search_keyword)
        {
            this.search_path = search_path;
            this.search_keyword = search_keyword;
            Thread th = new Thread(search_wim);
            th.Start();
        }

        private void integrate_wim()
        {
            //block buttons
            en_dis_able_everything(false);
            //clear progress bar
            refresh_progress_bar_total(0, 100);
            //status bar
            refresh_status_label("Status", "Busy");

            //not necessary, but looks more clear
            List<string> path = new List<string>();
            List<string> vol = new List<string>();
            List<string> name = new List<string>();
            List<string> platform = new List<string>();
            List<string> lang = new List<string>();
            List<string> ver = new List<string>();
            for (int i = 0; i < lv_ptr.Items.Count; i++)
            {
                path.Add(lv_ptr.Items[i].SubItems[0].Text);
                vol.Add(lv_ptr.Items[i].SubItems[1].Text);
                name.Add(lv_ptr.Items[i].SubItems[2].Text);
                platform.Add(lv_ptr.Items[i].SubItems[3].Text);
                lang.Add(lv_ptr.Items[i].SubItems[4].Text);
                ver.Add(lv_ptr.Items[i].SubItems[5].Text);
            }

            if (this.textBox_mnt_point.Text.Length == 0)
            {
                mount_point = Environment.GetEnvironmentVariable("tmp") + "\\wim_integrator_mnt_point";
            }
            else
            {
                mount_point = this.textBox_mnt_point.Text;
            }
            if (this.textBox_tmp_folder.Text.Length == 0)
            {
                tmp_folder = des_wim_path + ".temp";
            }
            else
            {
                tmp_folder = this.textBox_tmp_folder.Text;
            }

            if (!Directory.Exists(mount_point))
            {
                Directory.CreateDirectory(mount_point);
            }

            if (!Directory.Exists(tmp_folder))
            {
                Directory.CreateDirectory(tmp_folder);
            }

            DismProgressCallback prog_callback = refresh_progress_bar_step;
            DismApi.Initialize(DismLogLevel.LogErrors);

            //main loop
            for (int i = 0; i < lv_ptr.Items.Count; i++)
            {
                src_wim_path = path[i];
                src_wim_vol = Convert.ToInt32(vol[i]);
                string vol_name = name[i] + "_" +
                                  platform[i] + "_" +
                                  lang[i] + "_" +
                                  ver[i];

                //mount vol

                //clear progress bar
                refresh_progress_bar_step(0, 100);
                //status bar
                refresh_status_label("DISM", "Mounting");

                lv_ptr.Items[i].BackColor = Color.Gold;//color
                DismApi.MountImage(src_wim_path, mount_point, src_wim_vol, true, prog_callback);

                //imagex
                lv_ptr.Items[i].BackColor = Color.Aqua;//color
                Process imagex = new Process();
                imagex.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                imagex.StartInfo.CreateNoWindow = true;
                imagex.StartInfo.UseShellExecute = false;
                imagex.StartInfo.RedirectStandardOutput = true;
                imagex.StartInfo.RedirectStandardError = true;

                string imagex_flag = "/scroll /compress" + " " + comp_lv + " " + "/temp" + " " + "\"" + tmp_folder + "\"" + " ";
                //imagex cannot create new image by using append, must use capture insted, everything else is the same, stupid.
                string imagex_operation;
                if (i == 0)
                {
                    imagex_operation = "/capture" + " ";
                }
                else
                {
                    imagex_operation = "/append" + " ";
                }
                string imagex_option = "\"" + mount_point + "\"" + " " + "\"" + des_wim_path + "\"" + " " + "\"" + vol_name + "\"";
                imagex.StartInfo.FileName = "imagex.exe";
                imagex.StartInfo.Arguments = imagex_flag + imagex_operation + imagex_option;

                imagex.Start();

                //clear progress bar
                refresh_progress_bar_step(0, 100);
                string progress;
                while (!imagex.HasExited)
                {
                    progress = imagex.StandardOutput.ReadLine();
                    if (progress != null)
                    {
                        if (progress.StartsWith("[") && !progress.Contains("ERROR"))
                        {
                            refresh_progress_bar_step(Convert.ToInt32(progress.Substring(2, 3)), 100);
                        }
                        //status bar
                        refresh_status_label("IMAGEX", progress);
                    }
                    //avoid load cpu
                    //since there is no any ReadReady() kind of stuff, I have to use this dirty and easy way
                    Thread.Sleep(10);
                }

                //umount vol

                //clear progress bar
                refresh_progress_bar_step(0, 100);
                //status bar
                refresh_status_label("DISM", "Unmounting");

                lv_ptr.Items[i].BackColor = Color.DodgerBlue;//color
                DismApi.UnmountImage(mount_point, false, prog_callback);

                if (imagex.ExitCode == 0)//color
                {
                    lv_ptr.Items[i].BackColor = Color.Lime;
                }
                else
                {
                    lv_ptr.Items[i].BackColor = Color.Red;
                }

                refresh_progress_bar_total(i + 1, lv_ptr.Items.Count);
            }

            DismApi.Shutdown();
            if (Directory.Exists(mount_point))
            {
                Directory.Delete(mount_point, true);
            }
            if (Directory.Exists(tmp_folder))
            {
                Directory.Delete(tmp_folder, true);
            }

            //unblock buttos
            en_dis_able_everything(true);
            refresh_status_label("Status", "Ready");
        }

        private void integrate_wim_file(string wim_save_path)
        {
            des_wim_path = wim_save_path;
            lv_ptr = this.listView_vol;
            Thread th = new Thread(integrate_wim);
            th.Start();
        }

    }
}
