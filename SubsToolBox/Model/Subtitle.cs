using System;
using System.Collections.Generic;

namespace SubsToolBox.Model
{
    public class Subtitle
    {
        #region Members

        public int Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public List<string> Text { get; set; }

        #endregion

        #region Constructor

        public Subtitle()
        {
            this.Text = new List<string>();
        }

        #endregion
    }
}
