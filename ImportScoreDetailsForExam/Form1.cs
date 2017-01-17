using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZY.Edu.Common;
using ZY.Edu.Entity;
using ZY.Edu.WCFServiceContract;

namespace ImportScoreDetailsForExam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            if (string.IsNullOrWhiteSpace(this.textBox1.Text.Trim()))
            {
                MessageBox.Show("请输入考试ID!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.textBox2.Text.Trim()))
            {
                MessageBox.Show("请输入查询条件!");
                return;
            }

            try
            {
                DataTable dt = new DataTable();
                //dt.Columns.Add("StudentCode");
                //dt.Columns.Add("StudentName");
                //dt.Columns.Add("TotalScore");
                //dt.Columns.Add("SchoolName");
                //dt.Columns.Add("ClassName");
                //dt.Columns.Add("ImgUrl1");
                //dt.Columns.Add("ImgUrl2");

                string strSql = "SELECT id,StudentCode,StudentName,TotalScore,SchoolName,ClassName FROM dbo.Apply_ExamUser WHERE ExamID=" + this.textBox1.Text.Trim() + " AND UploadPaper IS NOT  NULL and " + this.textBox2.Text.Trim();

                dt = DbHelperSQL.Query(strSql).Tables[0];

                dt.Columns.Add("ImgUrl1");

                dt.Columns.Add("ImgUrl2");

                this.progressBar1.Maximum = dt.Rows.Count;

                this.progressBar1.Value = 0;

                this.progressBar1.Visible = true;

                this.button1.Enabled = false;
                this.backgroundWorker1.RunWorkerAsync(dt);
            }
            catch (Exception ex)
            {
                this.button1.Enabled = true;
                this.progressBar1.Visible = false;
                this.dataGridView1.DataSource = null;
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.DataSource == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            try
            {
                string strFileName = "";
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strFileName = this.saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                DataTable dt = this.dataGridView1.DataSource as DataTable;
                NPOIForExcel.ExportDTtoExcel(dt, "学生总分以及图片", strFileName);

                MessageBox.Show("导出成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataTable dt = e.Argument as DataTable;


                int examID = int.Parse(this.textBox1.Text.Trim());

                int cardid = 0;

                Apply_Exam exam = new Apply_Exam();
                using (ServiceProxy<IExamManagerService> proxy = new ServiceProxy<IExamManagerService>())
                {
                    exam = proxy.Channel.ExamFindByID(examID);

                    if (exam != null)
                    {
                        cardid = exam.CardID;
                    }
                }

                Card_Info card = null;

                using (ServiceProxy<ICardManagerService> proxyCard = new ServiceProxy<ICardManagerService>())
                {
                    card = proxyCard.Channel.CardInfoFindById(cardid);
                    if (card != null)
                    {
                        card.LstExam = new List<Apply_Exam>();
                        card.LstExam.Add(exam);
                    }
                }

                string domian = "exampaper.zoyeo.com";//codes[10].Value;
                if (ConfigurationManager.AppSettings["QiNiuKeyDomian"] != null)
                    domian = ConfigurationManager.AppSettings["QiNiuKeyDomian"].ToString();

                List<string> listPic = null;

                int nA = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    int nExamUserID = int.Parse(dr["id"].ToString());

                    listPic = Utils.GetUserImgURL(domian, nExamUserID, card);

                    if (listPic != null && listPic.Count > 0)
                    {


                        for (int j = 0; j < 2; j++)
                        {
                            string str = listPic[j];
                            if (j == 0)
                            {
                                if (str.Equals("未上传图片"))
                                {
                                    dr["ImgUrl1"] = str;
                                }
                                else
                                {
                                    dr["ImgUrl1"] = str.Replace("?imageInfo", "");
                                }
                            }
                            else
                            {
                                if (str.Equals("未上传图片"))
                                {
                                    dr["ImgUrl2"] = str;
                                }
                                else
                                {
                                    dr["ImgUrl2"] = str.Replace("?imageInfo", "");
                                }
                            }
                        }
                    }

                  
                    this.backgroundWorker1.ReportProgress(i);
                }

                e.Result = dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.progressBar1.Visible = false;

                DataTable dt = e.Result as DataTable;
                this.dataGridView1.DataSource = dt;

                this.button1.Enabled = true;
                MessageBox.Show("查询完成！共查询到：【"+dt.Rows.Count+"】条数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  gs.id,ms.AfterShowOrderNumber,ms.Score,gs.FinalScore,u.Name,gs.GradeUser1GradeTime,");
            sb.Append(" 'http://www.yyjuan.com/GradingScore/MarkQuestion/?applyId='+CAST(gs.ExamID AS NVARCHAR(50))+'&scoreId='+ CAST(gs.id AS NVARCHAR(50)) +'&allOrderNumber='+ CAST(gs.AllOrderNumber AS NVARCHAR(50)) +'&endOrderNumber='+CAST(gs.AllOrderNumber AS NVARCHAR(50))+'&gradeTemp=2&isFirst=1' AS link");
            sb.Append(" FROM dbo.Grading_Score gs LEFT JOIN dbo.Apply_ExamScoreMarkSetting ms");
            sb.Append(" ON gs.ExamID=ms.ExamID LEFT JOIN dbo.Usy_User u ON gs.GradeUser1Id=u.id ");
            sb.Append(" WHERE gs.AllOrderNumber=ms.AfterOrderNumber AND gs.ExamID=2193 ");
           DataTable dt = DbHelperSQL.Query(sb.ToString()).Tables[0];

            this.dataGridView2.DataSource = dt;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.DataSource == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            try
            {
                string strFileName = "";
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strFileName = this.saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                DataTable dt = this.dataGridView2.DataSource as DataTable;
                NPOIForExcel.ExportDTtoExcel(dt, "学生得分以及修改链接", strFileName);

                MessageBox.Show("导出成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
