/*
 * Created by SharpDevelop.
 * User: Gabs
 * Date: 10/8/2020
 * Time: 12:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using SimpleExcelImport;
public class Song
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Artist { get; set; }
	public string Genre { get; set; }
	public string Duration { get; set; }
	public int ReleaseYear { get; set; }
	public string RecordLabel { get; set; }
	public int PeakChartPosition { get; set; }
}

public class Song2
{
	[ExcelImport("Title - Modified", order = 7)]
	public string Title { get; set; }
	[ExcelImport("Artist - Modified", order = 5)]
	public string Artist { get; set; }
	[ExcelImport("Duration", order = 8)]
	public string Duration { get; set; }
	[ExcelImport("Year", order = 2)]
	public string ReleaseYear { get; set; }
	[ExcelImport("Original Record Label & Catalog No.", order = 9)]
	public string RecordLabel { get; set; }
	[ExcelImport("Peak Chart Position", order = 3)]
	public string PeakChartPosition { get; set; }
}