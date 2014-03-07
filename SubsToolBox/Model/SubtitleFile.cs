using System.Collections.Generic;
using System.Linq;

namespace SubsToolBox.Model
{
    public class SubtitleFile
    {
        #region Members

        public List<Subtitle> Subtitles { get; set; }
        public string FileName { get; set; }
        public string FileDirectory { get; set; }

        #endregion

        #region Constructor

        public SubtitleFile()
        {
            this.Subtitles = new List<Subtitle>();
        }

        #endregion

        #region Public Members
        
        /// <summary>
        /// Reorder the subtitles list by setting the first element to given id
        /// </summary>
        /// <param name="startIndex">First element to be</param>
        public void StartSubtitleListFromId(int startIndex){
            this.Subtitles.RemoveAll(s => s.Id < startIndex);

            int i = 1;
            foreach (Subtitle s in this.Subtitles.OrderBy(s => s.Id).ToList())
            {
                s.Id = i;
                i++;
            }
        }

        #endregion
    }
}
