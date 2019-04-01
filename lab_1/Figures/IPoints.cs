using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    public interface IPoints
    {
        List<MyPoint> GetPoints();
        void SetPoints(List<MyPoint> myPoints);
        
    }
}
