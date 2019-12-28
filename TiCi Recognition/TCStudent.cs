using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiCi_Recognition
{
    public class TCStudent
    {
        public string LName, FName, Gender, ImgDir;
        public bool chkexist;
        public int Class, Code, AbsentNum, WrongSeatNum;

        public TCStudent(string _LName, string _FName, int _Class, string _Gender, string _ImgDir)
        {
            LName = _LName;
            FName = _FName;
            Class = _Class;
            Gender = _Gender;
            AbsentNum = WrongSeatNum = 0;
            chkexist = true;
            ImgDir = _ImgDir;
        }

        public TCStudent()
        {
            LName = "";
            FName = "";
            Class = 0;
            Gender = "";
            AbsentNum = WrongSeatNum = 0;
            ImgDir = "";
            chkexist = true;
        }
    }
}
