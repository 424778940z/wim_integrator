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

            DismProgressCallback prog_callback = refresh_progress_bar_step;

            ProcessStartInfo proc_startinfo = new ProcessStartInfo();
            proc_startinfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc_startinfo.CreateNoWindow = true;
            proc_startinfo.UseShellExecute = false;
            proc_startinfo.RedirectStandardOutput = true;
            proc_startinfo.RedirectStandardError = true;

            string wim_int_path = Environment.GetEnvironmentVariable("tmp") + "\\wim_integrator";
            Directory.CreateDirectory(wim_int_path);
            DismApi.Initialize(DismLogLevel.LogErrors);

            for (int i = 0; i < path.Count; i++)
            {
                src_wim_path = path[i];
                src_wim_vol = Convert.ToInt32(vol[i]);
                string vol_name = name[i]+ "_" +
                                  platform[i] + "_" +
                                  lang[i] + "_" +
                                  ver[i];

                //mount vol
                DismApi.MountImage(src_wim_path, wim_int_path, src_wim_vol, true, prog_callback);

                //imagex
                string imagex_flag = "/compress maximum /scroll" + " ";
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
                proc_startinfo.FileName = "imagex.exe";
                proc_startinfo.Arguments = imagex_flag + imagex_operation + imagex_option;
                
                Process imagex = new Process();
                imagex.StartInfo = proc_startinfo;
                imagex.Start();

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
                DismApi.UnmountImage(wim_int_path, false, prog_callback);

                if (imagex.ExitCode == 0)
                {
                    lv_ptr.Items[i].BackColor = Color.LightGreen;
                }
                else
                {
                    lv_ptr.Items[i].BackColor = Color.Red;
                }
                refresh_progress_bar_total(i + 1, path.Count);
            }

            DismApi.Shutdown();
            Directory.Delete(wim_int_path, true);
        }

        private void integrate_wim_file(string wim_save_path)
        {
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
