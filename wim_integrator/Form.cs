﻿using System;
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

namespace wim_integrator
{
    public partial class form : Form
    {
        public form()
        {
            InitializeComponent();
            this.listView_vol.View = View.Details;
            this.listView_vol.Columns.Add("Path");
            this.listView_vol.Columns.Add("Volume");
            this.listView_vol.Columns.Add("Name");
            this.listView_vol.Columns.Add("Platform");
            this.listView_vol.Columns.Add("Language");
            this.listView_vol.Columns.Add("Version");
        }

        private void search_wim_file(string path)
        {
            List<string> install_wim_list = new List<string>();

            install_wim_list = Directory.GetFiles(path, "install.wim", SearchOption.AllDirectories).ToList();

            this.listView_vol.BeginUpdate();
            DismApi.Initialize(DismLogLevel.LogErrors);
            for (int i = 0; i < install_wim_list.Count; i++)
            {
                DismImageInfoCollection imageInfos = DismApi.GetImageInfo(install_wim_list[i]);
                for(int j = 0; j < imageInfos.Count; j++)
                {
                    ListViewItem item_buff = new ListViewItem(install_wim_list[i]);

                    //vol
                    item_buff.SubItems.Add(imageInfos[j].ImageIndex.ToString());

                    //name
                    item_buff.SubItems.Add(imageInfos[j].ImageName);

                    //platform
                    string platform_translated;
                    switch(imageInfos[j].Architecture)
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
                }
            }
            DismApi.Shutdown();
            this.listView_vol.EndUpdate();
            this.listView_vol.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void refresh_progress_bar_step(DismProgress progress)
        {
            this.progressBar_step.Maximum = progress.Total;
            this.progressBar_step.Minimum = 0;
            this.progressBar_step.Value = progress.Current;
        }
        private void refresh_progress_bar_total (int current, int total)
        {
            this.progressBar_total.Maximum = total;
            this.progressBar_total.Minimum = 0;
            this.progressBar_total.Value = current;
        }

        private void integrate_wim_file(string path)
        {
            DismProgressCallback prog_callback = refresh_progress_bar_step;
            string wim_int_path = Environment.GetEnvironmentVariable("tmp") + "\\wim_integrator";
            Directory.CreateDirectory(wim_int_path);
            DismApi.Initialize(DismLogLevel.LogErrors);

            ProcessStartInfo dism_startinfo = new ProcessStartInfo("dism.exe");
            dism_startinfo.WindowStyle = ProcessWindowStyle.Hidden;
            dism_startinfo.CreateNoWindow = true;
            dism_startinfo.UseShellExecute = false;
            dism_startinfo.RedirectStandardOutput = true;
            dism_startinfo.RedirectStandardError = true;
            dism_startinfo.Arguments = "";

            string args;
            for (int i = 0; i < this.listView_vol.Items.Count; i++)
            {
                //TODO
                //th start
                DismApi.MountImage(this.listView_vol.Items[i].SubItems[0].Text, wim_int_path, Convert.ToInt32(this.listView_vol.Items[0].SubItems[1].Text), true, prog_callback);
                //dism /append-image
                /*
                Process dism = Process.Start(dism_startinfo);
                string std_out = dism.StandardError.ReadToEnd();
                string std_err = dism.StandardOutput.ReadToEnd();
                dism.WaitForExit();
                */
                //dism 
                DismApi.UnmountImage(wim_int_path, false, prog_callback);
                //th end

                if(true)
                {
                    this.listView_vol.Items[i].BackColor = Color.LightGreen;
                }
                else
                {
                    this.listView_vol.Items[i].BackColor = Color.Red;
                }
                refresh_progress_bar_total(i+1, this.listView_vol.Items.Count);
            }
            
            

            DismApi.Shutdown();
            Directory.Delete(wim_int_path, true);
        }

        private void button_search_folder_sel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_sel_dialog = new System.Windows.Forms.FolderBrowserDialog();
            //folder_sel_dialog.Description = "Select the folder";
            folder_sel_dialog.ShowNewFolderButton = false;
            folder_sel_dialog.SelectedPath = this.textBox_search_folder.Text;
            if (folder_sel_dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_search_folder.Text = folder_sel_dialog.SelectedPath;
                search_wim_file(this.textBox_search_folder.Text);
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
    }
}
