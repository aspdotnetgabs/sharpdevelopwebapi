/*
 * Created by SharpDevelop.
 * User: Gabs
 * Date: 10/8/2020
 * Time: 12:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.ComponentModel.DataAnnotations.Schema;
[Table("SongsAPI")]
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

	public string Title { get; set; }

	public string Artist { get; set; }

	public string Duration { get; set; }

	public string ReleaseYear { get; set; }

	public string RecordLabel { get; set; }

	public string PeakChartPosition { get; set; }
}