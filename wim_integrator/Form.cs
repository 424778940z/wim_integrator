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
        string version_string = "Version: 0.1";

        //for Thread, since it seems no way to pass args
        string des_wim_path;
        //int des_wim_vol = 0;
        string src_wim_path;
        int src_wim_vol = 0;

        ListView lv_ptr;

        public form()
        {
            InitializeComponent();

            //I know what I'm doing, no more stupid Microsoft call-recall endless events
            Control.CheckForIllegalCrossThreadCalls = false;

            this.Text = this.Text + " " + version_string;

            this.listView_vol.View = View.Details;
            this.listView_vol.Columns.Add("Path");
            this.listView_vol.Columns.Add("Volume");
            this.listView_vol.Columns.Add("Name");
            this.listView_vol.Columns.Add("Platform");
            this.listView_vol.Columns.Add("Language");
            this.listView_vol.Columns.Add("Version");
        }

        private void search_wim_file(string path, string keyword)
        {
            List<string> wim_file_list = new List<string>();

            wim_file_list = Directory.GetFiles(path, keyword, SearchOption.AllDirectories).ToList();

            this.listView_vol.BeginUpdate();
            DismApi.Initialize(DismLogLevel.LogErrors);
            for (int i = 0; i < wim_file_list.Count; i++)
            {
                DismImageInfoCollection imageInfos = DismApi.GetImageInfo(wim_file_list[i]);
                for(int j = 0; j < imageInfos.Count; j++)
                {
                    ListViewItem item_buff = new ListViewItem(wim_file_list[i]);

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

        private void integrate_wim()
        {
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

            string wim_int_path = Environment.GetEnvironmentVariable("tmp") + "\\wim_integrator";
            if (!Directory.Exists(wim_int_path))
            {
                Directory.CreateDirectory(wim_int_path);
            }
            string wim_int_temp_path = des_wim_path + ".temp";
            if(!Directory.Exists(wim_int_temp_path))
            {
                Directory.CreateDirectory(wim_int_temp_path);
            }

            DismProgressCallback prog_callback = refresh_progress_bar_step;
            DismApi.Initialize(DismLogLevel.LogErrors);

            //main loop
            for (int i = 0; i < lv_ptr.Items.Count; i++)
            {
                src_wim_path = path[i];
                src_wim_vol = Convert.ToInt32(vol[i]);
                string vol_name = name[i]+ "_" +
                                  platform[i] + "_" +
                                  lang[i] + "_" +
                                  ver[i];

                //mount vol
                lv_ptr.Items[i].BackColor = Color.Gold;//color
                DismApi.MountImage(src_wim_path, wim_int_path, src_wim_vol, true, prog_callback);

                //imagex
                lv_ptr.Items[i].BackColor = Color.Aqua;//color
                Process imagex = new Process();
                imagex.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                imagex.StartInfo.CreateNoWindow = true;
                imagex.StartInfo.UseShellExecute = false;
                imagex.StartInfo.RedirectStandardOutput = true;
                imagex.StartInfo.RedirectStandardError = true;

                string imagex_flag = "/compress maximum /scroll /temp" + " " + "\"" + wim_int_temp_path + "\"" + " ";
                //imagex cannot create new image by using append, must use capture, everything else is the same, stupid.
                string imagex_operation;
                if (i == 0)
                {
                    imagex_operation = "/capture" + " ";
                }
                else
                {
                    imagex_operation = "/append" + " ";
                }
                string imagex_option = "\"" + wim_int_path + "\"" + " " + "\"" + des_wim_path + "\"" + " " + "\"" + vol_name + "\"";
                imagex.StartInfo.FileName = "imagex.exe";
                imagex.StartInfo.Arguments = imagex_flag + imagex_operation + imagex_option;

                imagex.Start();

                refresh_progress_bar_step(0, 100);//reset progress bar
                string progress;
                while (!imagex.HasExited)
                {
                    progress = imagex.StandardOutput.ReadLine();
                    if(progress != null)
                    {
                        if (progress.StartsWith("["))
                        {
                            refresh_progress_bar_step(Convert.ToInt32(progress.Substring(2, 3)), 100);
                        }
                    }
                    Thread.Sleep(500);
                }
                
                //umount vol
                lv_ptr.Items[i].BackColor = Color.DodgerBlue;//color
                DismApi.UnmountImage(wim_int_path, false, prog_callback);

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
            if (Directory.Exists(wim_int_path))
            {
                Directory.Delete(wim_int_path, true);
            }
            if (Directory.Exists(wim_int_temp_path))
            {
                Directory.Delete(wim_int_temp_path, true);
            }

            //unblock buttos
            this.button_search_folder_sel.Enabled = true;
            this.button_integrate.Enabled = true;
        }

        private void integrate_wim_file(string wim_save_path)
        {
            //block buttons
            this.button_search_folder_sel.Enabled = false;
            this.button_integrate.Enabled = false;
            des_wim_path = wim_save_path;
            lv_ptr = this.listView_vol;
            Thread th = new Thread(integrate_wim);
            th.Start();
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
                search_wim_file(this.textBox_search_folder.Text, "install.wim");
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
