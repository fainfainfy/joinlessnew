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


namespace FyColocation
{
    public class FileDdatasouce:InDatasouce
    {
    
  public  List<string> inputdata(string file)
        {
            string[] Lines;
            Lines = File.ReadAllLines(file);
            int k=0;
          //  MessageBox.Show(Lines.Length+"");
            List<string> h=new List<string>();
            while (k < Lines.Length)
            {
                h.Add(Lines[k]);
                k++;
            }
          //  foreach (var str in Lines)
          //  {
         //       h.Add(str);
         //   }
            return h;
        }

  public  List<string> inputconfig(string file)
   {
       string[] Lines;
       Lines = File.ReadAllLines(file);
       int k = 0;
       List<string> h = new List<string>();
       while (Lines[k] != "")
       {
           h.Add(Lines[k]);
       }
       return h;
   }
     
    }
}
