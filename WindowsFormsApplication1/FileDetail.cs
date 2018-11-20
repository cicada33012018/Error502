using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class FileDetail
    {
        string fileName;
        string fileDirectory;
        public FileDetail(string fileName,string fileDirectory)
        {
            this.fileName = fileName;
            this.fileDirectory = fileDirectory;
        }
    }
}
