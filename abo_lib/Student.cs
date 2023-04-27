using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abo_lib
{
    [Serializable]
    public class Student
    {
        public string id { get; set; }
        public string hoten { get; set; }
        public DateTime ngaysinh { get; set; }
        public string que { get; set; }
        public float diem { get; set; }

    }
}
