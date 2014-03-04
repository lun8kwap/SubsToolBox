using SubsToolBox.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Model.Entities.Concrete
{
    public class Subtitle : ISubtitle
    {
        #region Members

        public int Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Text { get; set; }

        #endregion
    }
}
