/* Football result browser that reads results from a JSON-file and displays them on a webpage. Date-based search-function implemented.
 * @author: Roope Kivioja
 * @date: 14.04.2016
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;

namespace Tulospalvelu
{
    public partial class Tulospalvelu : System.Web.UI.Page
    {
        private static List<RootObject> matches;
        private static List<RootObject> filteredMatches;

        /// <summary>
        /// Just calls the function that is needed to load the results from the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMatchFile();
        }

        /// <summary>
        /// Loads the results from a file.
        /// </summary>
        private void LoadMatchFile()
        {
            //string filePath = Path.GetFullPath("matches.json"); 
            //TODO: KORJAA
            string filePath = "C:\\Users\\Roope\\Documents\\Koulutus\\AdafyOhjt\\Tulospalvelu\\Tulospalvelu\\matches.json";
            
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                matches = JsonConvert.DeserializeObject<List<RootObject>>(json);
            }

            PrintAllMatches(matches, TableAllMatches);
        }

        /// <summary>
        /// Prints the full match list to given table.
        /// </summary>
        /// <param name="input">List of RootObjects to print.</param>
        /// <param name="target">The table that will hold the printed matches.</param>
        private void PrintAllMatches(List<RootObject> input, Table target)
        {
            foreach (RootObject match in input)
            {
                TableRow MatchRow = new TableRow();
                target.Rows.Add(MatchRow);

                TableCell DateCell = new TableCell();
                DateTime starttime = ConvertToDateTime(match.MatchDate.ToString());
                DateCell.Text = starttime.ToString();
                MatchRow.Cells.Add(DateCell);
                
                TableCell HomeTeamCell = new TableCell();
                HomeTeamCell.Text = match.HomeTeam.Name.ToString();
                MatchRow.Cells.Add(HomeTeamCell);
                TableCell HomeGoalsCell = new TableCell();
                HomeGoalsCell.Text = match.HomeGoals.ToString();
                MatchRow.Cells.Add(HomeGoalsCell);

                TableCell SeparatorCell = new TableCell();
                SeparatorCell.Text = "-";
                MatchRow.Cells.Add(SeparatorCell);

                TableCell AwayGoalsCell = new TableCell();
                AwayGoalsCell.Text = match.AwayGoals.ToString();
                MatchRow.Cells.Add(AwayGoalsCell);
                TableCell AwayTeamCell = new TableCell();
                AwayTeamCell.Text = match.AwayTeam.Name.ToString();
                MatchRow.Cells.Add(AwayTeamCell);
            }
        }

        /// <summary>
        /// The click-event for the search. Searches through the results and prints them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, System.EventArgs e)
        {
            string after = TextBoxStartDay.Text.ToString();
            DateTime afterdate;
            try
            {
                afterdate = ConvertToDateTime(after);
            }
            catch
            {
                afterdate = new DateTime(2015, 4, 11, 10, 00, 00);
            }

            string before = TextBoxEndDay.Text.ToString();
            DateTime beforedate;
            try
            {
                beforedate = ConvertToDateTime(before);
            }
            catch
            {
                beforedate = new DateTime(2015, 10, 26, 10, 00, 00);
            }

            var beforeDateX = matches.Where(i => ConvertToDateTime(i.MatchDate) >= afterdate && ConvertToDateTime(i.MatchDate) <= beforedate);

            foreach (RootObject filteredmatch in beforeDateX)
            {
                TableRow MatchRow = new TableRow();
                TableResults.Rows.Add(MatchRow);

                TableCell DateCell = new TableCell();
                DateTime starttime = ConvertToDateTime(filteredmatch.MatchDate.ToString());
                DateCell.Text = starttime.ToString();
                MatchRow.Cells.Add(DateCell);

                TableCell HomeTeamCell = new TableCell();
                HomeTeamCell.Text = filteredmatch.HomeTeam.Name.ToString();
                MatchRow.Cells.Add(HomeTeamCell);
                TableCell HomeGoalsCell = new TableCell();
                HomeGoalsCell.Text = filteredmatch.HomeGoals.ToString();
                MatchRow.Cells.Add(HomeGoalsCell);

                TableCell SeparatorCell = new TableCell();
                SeparatorCell.Text = "-";
                MatchRow.Cells.Add(SeparatorCell);

                TableCell AwayGoalsCell = new TableCell();
                AwayGoalsCell.Text = filteredmatch.AwayGoals.ToString();
                MatchRow.Cells.Add(AwayGoalsCell);
                TableCell AwayTeamCell = new TableCell();
                AwayTeamCell.Text = filteredmatch.AwayTeam.Name.ToString();
                MatchRow.Cells.Add(AwayTeamCell);
            }
        }

        /// <summary>
        /// Converts the date and time strings from the file into a proper DateTime-format.
        /// </summary>
        /// <param name="filedate">The date and time string that is being converted.</param>
        /// <returns>A DateTime-object.</returns>
        private static DateTime ConvertToDateTime(string filedate)
        {
            DateTime converted = Convert.ToDateTime(filedate);

            return converted;

        }

        /// <summary>
        /// Basic object for holding JSON-data.
        /// </summary>
        public class RootObject
        {
            public int Id { get; set; }
            public object Round { get; set; }
            public int RoundNumber { get; set; }
            public string MatchDate { get; set; }
            public HomeTeam HomeTeam { get; set; }
            public AwayTeam AwayTeam { get; set; }
            public int HomeGoals { get; set; }
            public int AwayGoals { get; set; }
            public int Status { get; set; }
            public int PlayedMinutes { get; set; }
            public object SecondHalfStarted { get; set; }
            public string GameStarted { get; set; }
            public List<object> MatchEvents { get; set; }
            public List<object> PeriodResults { get; set; }
            public bool OnlyResultAvailable { get; set; }
            public int Season { get; set; }
            public string Country { get; set; }
            public string League { get; set; }
        }

        /// <summary>
        /// Basic object for holding JSON-data.
        /// </summary>
        public class HomeTeam
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string FullName { get; set; }
            public object Logo { get; set; }
            public string LogoUrl { get; set; }
            public int Ranking { get; set; }
            public string Message { get; set; }
        }

        /// <summary>
        /// Basic object for holding JSON-data.
        /// </summary>
        public class AwayTeam
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string FullName { get; set; }
            public object Logo { get; set; }
            public string LogoUrl { get; set; }
            public int Ranking { get; set; }
            public string Message { get; set; }
        }
    }
}