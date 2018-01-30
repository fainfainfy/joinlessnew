using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyColocation
{
   public  class Instance
    {
       public int type;
       public double x;
       public double y;

       public double getM(double x1, double y1)
       {
           return (x - x1) * (x - x1) + (y - y1) * (y - y1);
       }

       public int getx(int d)
        {
            return (int)x/d;
        }
       public int gety(int d)
        {
            return (int)y / d;
        }
    }


}
