using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportScoreDetailsForExam.Model
{
   public class ChapterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int TextbookId { get; set; }

        public int OrderNumber { get; set; }
    }
}
