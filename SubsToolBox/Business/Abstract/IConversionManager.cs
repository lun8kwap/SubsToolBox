using SubsToolBox.Model.Entities;
using System;

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
        TimeSpan GetTimeGap(Subtitle firstSub, Subtitle secondSub);
        
    }
}
