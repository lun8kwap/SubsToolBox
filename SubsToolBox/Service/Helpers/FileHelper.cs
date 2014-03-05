using SubsToolBox.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubsToolBox.Service.Helpers
{
    public class FileHelper
    {
        #region Private Methods

        /// <summary>
        /// Get list of subtitles from subtitle path
        /// </summary>
        /// <param name="subtitleFilePath">File path to .srt</param>
        /// <returns>List of subtitles (only)</returns>
        private List<Subtitle> GetSubtitlesListFromFilePath(string subtitleFilePath)
        {
            // Check UTF8
            //StreamReader sr = new StreamReader(subtitleFilePath, Encoding.Default, true);
            //if ((sr.CurrentEncoding).CodePage != Encoding.UTF8.CodePage)
            //{
            //    throw new Exception("File is not in UTF8 format");
            //}

            List<string> lines = File.ReadAllLines(subtitleFilePath, Encoding.Default).ToList();

            List<Subtitle> subtitlesList = new List<Subtitle>();
            List<string> currentSubtitleLines = new List<string>();

            for(int i=0; i<lines.Count; i++)
            {
                if ((lines[i].Trim().Length == 0) && (i > 0))
                {
                    subtitlesList.Add(GetSubtitleFromLines(currentSubtitleLines));
                    currentSubtitleLines = new List<string>();
                }
                else
                {
                    currentSubtitleLines.Add(lines[i]);
                }
            }

            // Adding last one if last line is not a blank one
            if (currentSubtitleLines.Count > 0)
            {
                subtitlesList.Add(GetSubtitleFromLines(currentSubtitleLines));
            }

            return subtitlesList;
        }

        /// <summary>
        /// Return one subtitle from a group of lines from file
        /// </summary>
        /// <param name="subtitleLines">Array of file lines representing one subtitle</param>
        /// <returns>One subtitle</returns>
        private Subtitle GetSubtitleFromLines(List<string> subtitleLines)
        {
            Subtitle subtitle = new Subtitle();

            // Id
            int id;
            if (!int.TryParse(subtitleLines[0], out id))
            {
                throw new Exception("Can't get subtitle id.");
            }
            else
            {
                subtitle.Id = id;
            }

            // Start & End
            TimeSpan[] times = GetStartAndEndTimeFromFileLine(subtitleLines[1]);
            subtitle.Start = times[0];
            subtitle.End = times[1];

            // Text
            subtitle.Text.AddRange(subtitleLines.GetRange(2, subtitleLines.Count - 2));

            return subtitle;
        }

        /// <summary>
        /// Get an array of two items representing the start timecode and end timecode of the subtitle
        /// </summary>
        /// <param name="fileLine">Line from subtitle file</param>
        /// <returns>Array of 2 TimeSpan</returns>
        private TimeSpan[] GetStartAndEndTimeFromFileLine(string fileLine)
        {
            TimeSpan[] res = new TimeSpan[2];

            string[] times = Regex.Split(fileLine, "-->");
            res[0] = TimeSpan.Parse(times[0].Trim());
            res[1] = TimeSpan.Parse(times[1].Trim());

            return res;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get List of subtitles from srt file
        /// </summary>
        /// <param name="subtitleFilePath">File path to .srt</param>
        /// <returns>SubtitleFile containing list of subtitles (and more)</returns>
        public SubtitleFile GenerateSubtitleFile(string subtitleFilePath){
            SubtitleFile outputFile = new SubtitleFile();
            outputFile.FileDirectory = Path.GetDirectoryName(subtitleFilePath);
            outputFile.FileName = Path.GetFileName(subtitleFilePath);
            outputFile.Subtitles = GetSubtitlesListFromFilePath(subtitleFilePath);

            return outputFile;
        }

        #endregion

    }
}
