using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blogimage {
    public partial class frmMain : Form {

        // this should be configurable on runtime?!
        const string PYTHON_PATH = @"C:\Python27\python.exe";
        // do not change these as they reflect the www directory tree
        const string SCRIPTS_FOLDER = @"..\";
        const string BLOG_FOLDER = @"..\..\pages\02.blog\";

        const string PARSE_OK = "ok";
        const string PARSE_MISSING = "missing";
        const string PARSE_EXCESS = "excess";
        const string PARSE_OUTDATE = "outdated";

        const int REPORT_PROCESS_INFO = 0;
        const int REPORT_PROCESS_OUTPUT = 1;

        private Dictionary<string, DirectoryInfo> blogposts;        
        private string script_path, blog_path;

        public frmMain() {
            this.blogposts = new Dictionary<string, DirectoryInfo>();
            string exepath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            this.script_path = Path.Combine(exepath, SCRIPTS_FOLDER);
            this.blog_path = Path.Combine(exepath, BLOG_FOLDER);

            InitializeComponent();            
        }

        // list missing images and old images
        private bool mustProcessPost(DirectoryInfo post, out List<string> messages) {
            messages = new List<string>();
            List<string> valid_images = new List<string>();

            FileInfo[] images = post.GetFiles();
            FileInfo[] resimages = post.GetFiles("res/");
            // filter only png and jpg
            images = Array.FindAll(images, f => f.Name.ToLower().EndsWith(".jpg") || f.Name.ToLower().EndsWith(".png"));
            resimages = Array.FindAll(resimages, f => f.Name.ToLower().EndsWith(".jpg") || f.Name.ToLower().EndsWith(".png"));


            // convert to string only arrays
            List<string> images_names = new List<string>();
            List<string> resimages_names = new List<string>();
            foreach (FileInfo fi in images) {
                images_names.Add(fi.Name.ToLower());
            }
            foreach (FileInfo fi in resimages) {
                resimages_names.Add(fi.Name.ToLower());
            }

            // get same
            var intersect = resimages_names.Intersect(images_names);

            if (images.Length != resimages.Length) {
                // log missing
                foreach (string resimage in resimages_names) {
                    if (!intersect.Contains(resimage)) {
                        messages.Add(String.Format("{0}:{1}", PARSE_MISSING, resimage));
                    }
                }
                // log excess
                foreach (string image in images_names) {
                    if (!intersect.Contains(image)) {
                        messages.Add(String.Format("{0}:{1}", PARSE_EXCESS, image));
                    }
                }

                valid_images.AddRange(intersect);
            }

            // check dates of intersect
            // lengths match -> compare write time
            foreach(string intpost in intersect ) {
                // find in images and resimages      
                FileInfo fi_image = images[images_names.IndexOf(intpost)];
                FileInfo fi_resimage = resimages[resimages_names.IndexOf(intpost)];
                if (fi_resimage.LastWriteTime > fi_image.LastWriteTime) {
                    messages.Add(String.Format("{0}:{1}", PARSE_OUTDATE, fi_resimage.Name));
                } else {
                    valid_images.Add(fi_image.Name);
                }
            }            

            // add valid images
            foreach(string image in valid_images) {
                messages.Add(String.Format("{0}:{1}", PARSE_OK, image));
            }

            return (messages.Count - valid_images.Count) > 0;
        }

        private void UpdateBlogFolderListing() {
            UseWaitCursor = true;
            DirectoryInfo di = new DirectoryInfo(this.blog_path);

            DirectoryInfo[] posts = di.GetDirectories();
            // add to list
            this.blogposts.Clear();
            foreach (DirectoryInfo post in posts) {
                this.blogposts.Add(post.Name, post);
            }

            // 1) sort by modified property            
            Array.Sort(
                posts,
                delegate (DirectoryInfo x, DirectoryInfo y) {
                    // get write time of blog_item.md files                    
                    
                    // i dont check for multiple md files or missing ones!
                    FileInfo x_md = x.GetFiles("blog_item.md")[0];
                    FileInfo y_md = y.GetFiles("blog_item.md")[0];
                                        
                    return x_md.LastWriteTime.CompareTo(y_md.LastWriteTime);
            });
            // sort descending
            Array.Reverse(posts);

            // 2) select all folder where:
            // res/images are newer then /images
            // size of res/images != /images
            List<DirectoryInfo> post2check = new List<DirectoryInfo>();
            List<DirectoryInfo> postnot2check = new List<DirectoryInfo>();
            foreach (DirectoryInfo post in posts) {
                List<string> messages;
                if(mustProcessPost(post, out messages)) {
                    post2check.Add(post);
                } else {
                    postnot2check.Add(post);
                }
            }

            // add to listbox
            this.clbblogposts.Items.Clear();
            foreach (DirectoryInfo post in post2check) {
                clbblogposts.Items.Add(post.Name, true);
            }
            foreach (DirectoryInfo post in postnot2check) {
                clbblogposts.Items.Add(post.Name, false);
            }
            // set index
            if(this.clbblogposts.Items.Count > 0 ) {
                this.clbblogposts.SelectedIndex = 0;
            }

            // checkbox all
            this.cball.ThreeState = true;            
            this.cball.CheckState = CheckState.Indeterminate;
            this.cball.Enabled = this.clbblogposts.Items.Count > 0;

            // reset process ui
            this.lbprocess.Text = "";
            this.pbprocess.Value = 0;
            UseWaitCursor = false;
        }

        private void Form1_Load(object sender, EventArgs e) {
            // check if valid folder (contains blog.md)
            DirectoryInfo di = new DirectoryInfo(this.blog_path);
            if( di.GetFiles("blog.md").Length != 1 ) {
                this.Close();
                return;
            }
            UpdateBlogFolderListing();
        }

        private bool processPost(string post, out string output) {
            string postpath = Path.Combine(this.blog_path, post);            
            // remove old image files            
            DirectoryInfo dipost = new DirectoryInfo(
                postpath
            );
            FileInfo[] images = dipost.GetFiles();            
            // filter only png and jpg
            images = Array.FindAll(images, f => f.Name.EndsWith(".jpg") || f.Name.EndsWith(".png"));
            foreach(FileInfo fiimg in images) {
                fiimg.Delete();
            }

            // run img2web
            Process p = new Process();            
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = PYTHON_PATH;
            p.StartInfo.WorkingDirectory = this.script_path;
            p.StartInfo.Arguments = String.Format(@"img2web\img2web.py {0}", postpath);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return p.ExitCode == 0;
        }

        private void button1_Click(object sender, EventArgs e) {            
            freezeWindowInputs();
            lbprocessout.Items.Clear();
            this.pbprocess.Maximum = this.clbblogposts.CheckedItems.Count;
            this.pythonProcess.RunWorkerAsync();                        
        }

        private void clbblogposts_SelectedIndexChanged(object sender, EventArgs e) {
            int idx = this.clbblogposts.SelectedIndex;
            if (idx > -1) {
                Dictionary<string, Color> details_typemap = new Dictionary<string, Color>();
                details_typemap.Add(PARSE_OK, Color.LightGreen);
                details_typemap.Add(PARSE_MISSING, Color.IndianRed);
                details_typemap.Add(PARSE_EXCESS, Color.OrangeRed);
                details_typemap.Add(PARSE_OUTDATE, Color.LightSalmon);

                lvDetails.Items.Clear();             
                // get post string
                string post = (string)this.clbblogposts.Items[idx];
                // get post directory
                DirectoryInfo postdir = this.blogposts[post];
                List<string> messages;
                this.mustProcessPost(postdir, out messages);
                foreach(string msg in messages) {                    
                    foreach (KeyValuePair<string, Color> dtm in details_typemap) {
                        string header = String.Format("{0}:", dtm.Key);
                        if (msg.StartsWith(header)) {
                            string fname = msg.Substring(header.Length);
                            ListViewItem lvitm = new ListViewItem(fname);
                            lvitm.BackColor = dtm.Value;
                            lvitm.SubItems.Add(dtm.Key);                            
                            lvDetails.Items.Add(lvitm);
                            break;
                        }
                    }                    
                }
                lvDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e) {
            freezeWindowInputs();
            UpdateBlogFolderListing();
            unfreezeWindowInputs();            
        }
        

        private void pythonProcess_DoWork(object sender, DoWorkEventArgs e) {
            // run img2web for all checked items
            for (var i=0;i< this.clbblogposts.CheckedItems.Count;++i) {

                string post = (string)this.clbblogposts.CheckedItems[i];
                Tuple<int, string> info = new Tuple<int, string>(i + 1, post);
                pythonProcess.ReportProgress(REPORT_PROCESS_INFO, info);


                string output;                
                if ( !processPost(post, out output) ) {
                    i = this.clbblogposts.CheckedItems.Count-1;
                }
                string[] state = new string[2] { post, output };
                pythonProcess.ReportProgress(REPORT_PROCESS_OUTPUT, output);
            }
        }

        private void pythonProcess_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if( e.ProgressPercentage == REPORT_PROCESS_INFO ) {
                Tuple<int, string> info = (Tuple<int, string>)e.UserState;
                this.lbprocess.Text = info.Item2;
                this.pbprocess.Value = info.Item1;
            } else {
                string output = (string)e.UserState;
                // add output seperator
                lbprocessout.Items.Add("==================");
                // split newlines
                foreach (string line in Regex.Split(output, "\n")) {
                    lbprocessout.Items.Add(line);
                }
            }            
        }

        void freezeWindowInputs() {
            this.clbblogposts.Enabled = false;
            this.btnprocess.Enabled = false;
            this.btnrefresh.Enabled = false;         
        }

        void unfreezeWindowInputs() {
            this.clbblogposts.Enabled = true;
            this.btnprocess.Enabled = true;
            this.btnrefresh.Enabled = true;
        }

        private void pythonProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            UpdateBlogFolderListing();
            unfreezeWindowInputs();
        }

        private void cball_Click(object sender, EventArgs e) {
            this.cball.ThreeState = false;

            if (this.cball.CheckState == CheckState.Checked) {                
                for (var i = 0; i < this.clbblogposts.Items.Count; i++) {
                    this.clbblogposts.SetItemChecked(i, true);
                }
            } else {                
                for (var i = 0; i < this.clbblogposts.Items.Count; i++) {
                    this.clbblogposts.SetItemChecked(i, false);
                }
            }
        }
    }
}
