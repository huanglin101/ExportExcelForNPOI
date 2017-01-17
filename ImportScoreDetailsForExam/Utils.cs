using Qiniu.FileOp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZY.Edu.Entity;

namespace ImportScoreDetailsForExam
{
    class Utils
    {
        /// <summary>
        /// 获取具体学生的图片
        /// </summary>
        /// <param name="domian"></param>
        /// <param name="userid"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static List<string> GetUserImgURL(string domian, int userid, Card_Info card)
        {
            Qiniu.Conf.Config.ACCESS_KEY = "DAdzOacPUmolrT7RnB0XRVGnj0woF2haXjp0nx6x";
            Qiniu.Conf.Config.SECRET_KEY = "3LBRNtfVo0Jk1Xc3G4mvP7ThWHA2V4chw1smmsp_";

            List<string> strlst = new List<string>();

            Apply_Exam exam = card.LstExam[0];
            int intAllPageNumber = card.AllPageNumber;

            for (int i = 1; i <= intAllPageNumber; i++)
            {
                string printtime = ((DateTime)exam.AddTime).ToString("yyyyMMddHHmmssfff");

                string key = exam.id + "_" + userid + "_" + i + "_" + printtime;
                key = MD5(key);

                string url = Qiniu.RS.GetPolicy.MakeBaseUrl(domian, key);

                string strInfo = ImageInfo.MakeRequest(url);

                //ImageInfoRet infoRet = ImageInfo.Call(strInfo);

                //if (infoRet.OK)
                //{
                //    strlst.Add(strInfo);
                //}
                //else
                //{
                //    //未上传
                //    strlst.Add("未上传图片");
                //}
                 strlst.Add(strInfo);
              
            }



            return strlst;
        }

        //MD5加密
        public static string MD5(string str)
        {
            //获取要加密的字段，并转化为Byte[]数组
            byte[] data = System.Text.Encoding.Unicode
            .GetBytes(str.ToCharArray());
            //建立加密服务
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //加密Byte[]数组
            byte[] result = md5.ComputeHash(data);
            //string sResult = System.Text.Encoding.Unicode.GetString(result);

            //md5Str = "MD5普通加密：" + sResult.ToString() + "<br/>";
            //作为密码方式加密
            string EnPswdStr = System.Web.Security.FormsAuthentication.
            HashPasswordForStoringInConfigFile(str.ToString(), "MD5");

            return EnPswdStr;
        }
    }
}
