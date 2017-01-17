using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportScoreDetailsForExam
{
    public partial class ExportExcel : Form
    {
        public ExportExcel()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();
            string filepath = openFileDialog1.FileName;
            txtFilePath.Text = filepath;
        }

        private void btnChapter_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = txtFilePath.Text;
                Stream stream = File.OpenRead(filepath);
                DataTable exceltable = ZY.Edu.Common.NPOIForExcel.ImportExcelToDtByFileStream(stream);

                DataTable tb = new DataTable("Dat_Chapter");
                tb.Columns.Add("Name");
                tb.Columns.Add("ParentId");
                tb.Columns.Add("Id");
                tb.Columns.Add("TextbookId");
                tb.Columns.Add("OrderNumber");
                tb.Columns.Add("TempCode");
                tb.Columns.Add("TempParentCode");
                tb.Columns.Add("TempName");
                foreach (DataRow dr in exceltable.Rows)
                {
                    DataRow currentDr = tb.NewRow();
                    string stage = dr["学段"].ToString().Trim();
                    string subject = dr["科目"].ToString().Trim();
                    int tid = GetTextBookId(stage, subject);
                    currentDr["TempCode"] = dr["章节编号"].ToString().Trim();
                    currentDr["TempName"] = dr["章节名称"].ToString().Trim();
                    currentDr["Name"] = dr["章节名称"].ToString().Trim();
                    int num = 0;
                    try
                    {
                         num = int.Parse(dr["序号"].ToString().Trim());
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    currentDr["OrderNumber"] = num;
                    currentDr["TempParentCode"] = dr["父章节编号"].ToString().Trim(); ;
                    currentDr["Id"] = 0;
                    currentDr["TextbookId"] = tid;
                    currentDr["ParentId"] = 0;
                    tb.Rows.Add(currentDr);
                }
                tb.TableName = "Dat_Chapter";
                var res = DbHelperSQL.ExcuteBulkCopyToTable(tb);
                int n = 0;
                
            }
            catch (Exception)
            {

                throw;
            }
            //int res = 0;
        }

        private void btnChapterKnowPoint_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = txtFilePath.Text;
                Stream stream = File.OpenRead(filepath);
                DataTable exceltable = ZY.Edu.Common.NPOIForExcel.ImportExcelToDtByFileStream(stream);
                int knowpointid = 2;
                int chapterid = 1311;

                DataTable tb = new DataTable("Dat_KnowledgePoint_Chapter");
                tb.Columns.Add("Id");
                tb.Columns.Add("KnowledgePointID");
                tb.Columns.Add("ChapterID");
                tb.Columns.Add("TempKnowCode");
                tb.Columns.Add("TempChapterCode");
           
                foreach (DataRow dr in exceltable.Rows)
                {
                    DataRow currentDr = tb.NewRow();
                    currentDr["TempKnowCode"] = dr["知识点编号"].ToString().Trim().Substring(1);
                    currentDr["TempChapterCode"] = dr["对应章节编号"].ToString().Trim();

                    currentDr["KnowledgePointID"] = knowpointid;
                    currentDr["ChapterID"] = chapterid;
                    currentDr["Id"] = 0;                   
                  
                    tb.Rows.Add(currentDr);
                }
                tb.TableName = "Dat_KnowledgePoint_Chapter";
                var res = DbHelperSQL.ExcuteBulkCopyToTable(tb);
                int n = 0;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public int GetTextBookId1(string stagename,string subjectname)
        {
            int tid = 0;
            try
            {
                
                SqlParameter[] paras = new SqlParameter[]
               {
                    new SqlParameter {
                        ParameterName="@stagename",
                        Value=stagename,
                        SqlDbType=SqlDbType.NVarChar,
                        Direction=ParameterDirection.Input
                    },
                     new SqlParameter {
                        ParameterName="@subjectname",
                        Value=subjectname,
                        SqlDbType=SqlDbType.NVarChar,
                        Direction=ParameterDirection.Input
                    },                     
                    new SqlParameter {
                        ParameterName="@textbookId",
                        Value=tid,
                        SqlDbType=SqlDbType.Int,
                        Direction=ParameterDirection.Output
                    },
               };
                int res = DbHelperSQL.ExecuteSql("exec [dbo].[GetTextBookId] @stagename,@subjectname,@textbookId out", paras);
            }
            catch (Exception ex)
            {

                throw;
            }
            return tid;
        }

        public int GetTextBookId(string stagename, string subjectname)
        {
            int tid = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("SELECT t.Id FROM dbo.Dat_Textbook t JOIN dbo.Dat_Stage s ON t.StageId = s.Id JOIN dbo.Dat_Subject u ON t.SubjectId = u.Id WHERE s.Name = '{0}' AND u.Name = '{1}'", stagename, subjectname);
                DataTable tb = DbHelperSQL.Query(sql.ToString()).Tables[0];
               tid=(int) tb.Rows[0][0];
            }
            catch (Exception ex)
            {

                throw;
            }
            return tid;
        }
    }
}
