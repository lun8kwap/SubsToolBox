using SubsToolBox.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Business.Abstract
{
    public interface IConversionManager
    {
        /// <summary>
        /// Getting the ratio between 
        /// </summary>
        /// <returns></returns>
        double GetRatio();

        /// <summary>
        /// Getting the gap between 2 subtitles
        /// </summary>
        /// <param name="firstSub"></param>
        /// <param name="secondSub"></param>
        /// <returns></returns>
        TimeSpan GetTimeGap(ISubtitle firstSub, ISubtitle secondSub);
        
    }
}
