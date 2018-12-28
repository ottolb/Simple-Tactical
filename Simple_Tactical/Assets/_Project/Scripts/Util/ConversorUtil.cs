using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Game
{
	public class ConversorUtil
	{
		public static string DecodeEncodedNonAsciiCharacters(string value)
		{
			return System.Text.RegularExpressions.Regex.Replace(
				value,
				@"\\u(?<Value>[a-zA-Z0-9]{4})",
				m =>
				{
					return ((char)int.Parse(m.Groups["Value"].Value, System.Globalization.NumberStyles.HexNumber)).ToString();
				});
		}

		public static string formatTime(float p_time)
		{
			int s = ((int)p_time) % 60;
			int m = (int)(p_time / 60);
			return digit(m) + ":" + digit(s);
		}

		public static string formatTimeMiliSeconds(float p_time)
		{
			int s = ((int)p_time) % 60;
			int ms = ((int)((p_time - s) * 100f)) % 100;
			int m = (int)(p_time / 60);
			return digit(m) + ":" + digit(s) + ":" + digit(ms);
		}

		public static string digit(int p_num)
		{
			return p_num < 10 ? "0" + p_num : p_num + "";
		}

		/// <summary>
		/// Convert seconds to hh:mm:ss format string.
		/// 'places' tell how many time units will be used.
		/// </summary>
		/// <param name="p_seconds"></param>
		/// <param name="p_format_places"></param>
		/// <returns></returns>
		static public string SecondsToTime(float p_seconds, int p_format_places = 2, bool p_use_ms = false, string p_separator = ":")
		{
			int nms = p_use_ms ? (Mathf.FloorToInt(p_seconds * 100f) % 100) : 0;
			int ns = Mathf.FloorToInt(p_seconds) % 60;
			int nm = (p_format_places >= 2) ? Mathf.FloorToInt(p_seconds / 60f) % 60 : 0;
			int nh = (p_format_places >= 3) ? Mathf.FloorToInt(p_seconds / 60f / 60f) % 24 : 0;
			int nd = (p_format_places >= 4) ? Mathf.FloorToInt(p_seconds / 60f / 60f / 24f) : 0;
			string res = "";
			string sep = p_separator;
			if(p_use_ms)
				res = sep + nms.ToString("00");
			res = ns.ToString("00") + res;
			if(p_format_places >= 2)
				res = nm.ToString("00") + sep + res;
			if(p_format_places >= 3)
				res = nh.ToString("00") + sep + res;
			if(p_format_places >= 4)
				res = nd.ToString("00") + sep + res;
			return res;
		}

		/// <summary>
		/// Convert seconds to a compact time string
		/// </summary>
		static public string SecondsToCompactTime(float p_seconds, string p_separator = "")
		{
			int ns = Mathf.FloorToInt(p_seconds) % 60;
			int nm = Mathf.FloorToInt(p_seconds / 60f) % 60;
			int nh = Mathf.FloorToInt(p_seconds / 60f / 60f) % 24;
			int nd = Mathf.FloorToInt(p_seconds / 60f / 60f / 24f);
			string res = "";

			if(nd > 0)
			{
				res = nd.ToString("0" + p_separator + "d");
				//+sep + res;
			}
			else if(nh > 0)
			{
				res = nh.ToString("0" + p_separator + "h");
				//+sep + res;
			}
			else if(nm > 0)
			{
				res = nm.ToString("0" + p_separator + "m");
				//+sep + res;
			}
			else
				res = ns.ToString("0" + p_separator + "s");
			return res;
		}

		public static DateTime FromUnixTime(string dateStr)
		{
			double unixTime = Convert.ToDouble(dateStr);
			return FromUnixTime(unixTime);
		}

		public static DateTime FromUnixTime(double unixTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(unixTime);
		}

		public static string FormatDateTime(DateTime aux, string p_format)
		{
			return aux.ToString(p_format);
		}

		public static string FormatDateTime(string p_dateStr, string p_format)
		{
			DateTime aux = DateTime.Parse(p_dateStr);
			return aux.ToString(p_format);
		}

		/*public static string FormatDateTimeServer(string p_dateStr, string p_format)
		{
			DateTime aux = GetDateTimeServer(p_dateStr);
			return aux.ToString(p_format);
		}*/

		public static DateTime GetDateTime(string p_dateStr)
		{
			return DateTime.Parse(p_dateStr);
		}

		public static DateTime GetDateTimeUTC(string p_dateStr)
		{
			TimeSpan ts = DateTime.Now - DateTime.UtcNow;
			//DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(GetDateTime(p_dateStr), TimeZoneInfo.Local);
			DateTime dt = GetDateTime(p_dateStr).Add(ts);
			return dt;
		}

		/*public static DateTime GetDateTimeServer(string p_dateStr)
		{
			DateTime aux = GetDateTime(p_dateStr);
			aux = TimeZoneInfo.ConvertTime(aux, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"), TimeZoneInfo.Local);
			return aux;
		}*/

		public static TimeSpan GetDuration(string p_dateStr)
		{
			return TimeSpan.Parse(p_dateStr);
		}

		public static bool IsTheSameDay(DateTime date1, DateTime date2)
		{
			return (date1.Year == date2.Year && date1.DayOfYear == date2.DayOfYear);
		}

		public static bool IsPast(DateTime date1, DateTime date2)
		{
			return date1 > date2;
		}

		public static string GetToday()
		{
			//string date = DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss");
			string date = DateTime.Now.ToString(@"yyyy-MM-dd" + " 00:00:00");
			//Debug.LogWarning("Date: " + date);
			return date;
		}

		public static string GetTodayUTC()
		{
			//string date = DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss");
			string date = DateTime.UtcNow.ToString(@"yyyy-MM-dd" + " 00:00:00");
			//Debug.LogWarning("Date: " + date);
			return date;
		}

		/// <summary>
		/// Generates a concatenaed date hash for '.Now' variable.
		/// </summary>
		/// <returns></returns>
		static public string DateHash()
		{
			return DateHash(System.DateTime.Now);
		}

		/// <summary>
		/// Generates a concatenaed date hash.
		/// </summary>
		/// <param name="p_date"></param>
		/// <returns></returns>
		static public string DateHash(System.DateTime p_date)
		{
			string prefix = "";
			prefix += p_date.Year.ToString("0000");
			prefix += p_date.Month.ToString("00");
			prefix += p_date.Day.ToString("00");
			prefix += p_date.Hour.ToString("00");
			prefix += p_date.Minute.ToString("00");
			prefix += p_date.Second.ToString("00");
			return prefix;
		}

		/// <summary>
		/// Returns the ordinal suffix.
		/// </summary>
		/// <param name="p_number"></param>
		/// <returns></returns>
		static public string Ordinal(int p_number)
		{
			int n = p_number % 4;
			switch(n)
			{
			case 0:
				return "th";
			case 1:
				return "st";
			case 2:
				return "nd";
			case 3:
				return "rd";
			}
			return "th";
		}


		public static float ClampAngle(float angle, float min, float max)
		{
			if(angle < -360)
				angle += 360;
			if(angle > 360)
				angle -= 360;

			// Hacked to get it working with the Euler angles. Could be better I guess...
			if(angle > 360 + min)
			{
				//angle = 180 -angle;
				return angle;
			}
			if(angle > 180)
			{
				angle = -angle;
			}
			return Mathf.Clamp(angle, min, max);

		}

		public static bool TimePassed(DateTime oldDate, float minutes)
		{
			DateTime start = DateTime.Now;
			if(start.Subtract(oldDate) >= TimeSpan.FromMinutes(minutes))
			{
				//X minutes were passed from start
				return true;
			}
			return false;
		}

		public static void SaveTimeNow(string timeKey)
		{
			DateTime start = System.DateTime.Now;
			long time = start.ToFileTimeUtc();
			PlayerPrefs.SetString(timeKey, time.ToString());
			//Debug.Log(time);
		}

		public static string ConvertToCurrency(int p_value, CultureInfo p_culture)
		{
			return p_value.ToString("C", p_culture);
		}

		public static string ConvertToCurrency(int p_value)
		{
			return p_value.ToString("C", CultureInfo.CurrentCulture);
		}

		public static string ConvertToCurrency(float p_value, CultureInfo p_culture)
		{
			return p_value.ToString("C", p_culture);
		}

		public static string ConvertToNumber(int p_value, CultureInfo p_culture)
		{
			return p_value.ToString("N", p_culture);
		}

		public static string ToMoney(double p_money)
		{
			p_money = Math.Abs(p_money);
			int digitK = Mathf.FloorToInt((float)(p_money / 1000));
			int digitM = Mathf.FloorToInt((float)(p_money / 1000000));
			int digitB = Mathf.FloorToInt((float)(p_money / 1000000000));
			int digitT = Mathf.FloorToInt((float)(p_money / 1000000000000));
			string res = "";

			if(digitT > 0)
			{
				res = p_money.ToString("0,,,,.#T", CultureInfo.InvariantCulture);
			}
			else if(digitB > 0)
			{
				res = p_money.ToString("0,,,.#B", CultureInfo.InvariantCulture);
			}
			else if(digitM > 0)
			{
				res = p_money.ToString("0,,.#M", CultureInfo.InvariantCulture);
			}
			else if(digitK > 0)
			{
				res = p_money.ToString("0,.##K", CultureInfo.InvariantCulture);
			}
			else
				res = p_money.ToString("0");

			return res;
		}

		public static double GetNumber(string p_value)
		{
			double result = 0;
			double.TryParse(p_value, NumberStyles.Float, CultureInfo.CurrentCulture, out result);
			return result;
		}

		public static string FormatDistance(float p_distance)
		{
			string fd = "";
			//RegionInfo region = new RegionInfo(p_culture.LCID);
			int digitK;
			//Debug.Log("IsMetric: " + region.IsMetric);

			digitK = Mathf.FloorToInt((p_distance / 1000));
			if(digitK > 0)
			{
				fd = p_distance.ToString("0,.#KM");
			}
			else {
				fd = p_distance.ToString("0M");
			}
			return fd;
		}

		public static int UnixTimeStampUTC()
		{
			int unixTimeStamp;
			DateTime currentTime = DateTime.Now;
			DateTime zuluTime = currentTime.ToUniversalTime();
			DateTime unixEpoch = new DateTime(1970, 1, 1);
			unixTimeStamp = (Int32)(zuluTime.Subtract(unixEpoch)).TotalSeconds;
			return unixTimeStamp;
		}

		public static List<T> Randomize<T>(List<T> list)
		{
			List<T> randomizedList = new List<T>();
			System.Random rnd = new System.Random();
			while(list.Count > 0)
			{
				int index = rnd.Next(0, list.Count); //pick a random item from the master list
				randomizedList.Add(list[index]); //place it at the end of the randomized list
				list.RemoveAt(index);
			}
			return randomizedList;
		}

		public static string GetRandomName()
		{
			TextAsset textAsset = Resources.Load<TextAsset>("names");
			string text = textAsset.text;

			string[] names = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			text = names[UnityEngine.Random.Range(0, names.Length)];
			return text;

		}

		public static string[] GetRandomPhrases(string p_language)
		{
			TextAsset textAsset = Resources.Load<TextAsset>("phrases_" + p_language);
			if(textAsset == null)
				return null;
			string text = textAsset.text;
			if(text == null)
				return null;

			string[] phrases = text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);

			return phrases;

		}

		public static float CheckAspect(Texture2D p_texture)
		{
			float w, h;

			w = p_texture.width;
			h = p_texture.height;
			//Debug.Log("Image " + w + "  " + h);
			return w / h;
		}
	}
}