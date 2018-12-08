
using System.Globalization;
using UnityEngine;

namespace Game
{
	public class LanguageChecker : MonoBehaviour
	{
		public static CultureInfo cultureInfo;

		public bool testBuild;

		public string testingLanguage;

		void Awake()
		{
			if(testBuild)
				Debug.LogWarning("DISABLE!!!");


			if(testBuild || Application.isEditor && !string.IsNullOrEmpty(testingLanguage))
			{
				ForceTesting();
			}
			else
			{
				CheckBySystemLanguage();
			}
		}

		void ForceTesting()
		{
			Localization.language = testingLanguage;

			switch(testingLanguage)
			{
			case "Port-Br":
				cultureInfo = new CultureInfo("pt-BR");
				break;
			default:
				cultureInfo = new CultureInfo("en-US");
				break;
			}

			cultureInfo.NumberFormat.CurrencyNegativePattern = 12;
			cultureInfo.NumberFormat.NumberDecimalDigits = 0;
		}

		void CheckBySystemLanguage()
		{
			//Debug.Log("System language: " + Application.systemLanguage);
			switch(Application.systemLanguage)
			{
			case SystemLanguage.Portuguese:
				Localization.language = "Port-Br";
				cultureInfo = new CultureInfo("pt-BR");
				break;
			default:
				Localization.language = "en-US";
				cultureInfo = new CultureInfo("en-US");
				break;
			}

			cultureInfo.NumberFormat.CurrencyNegativePattern = 12;
			cultureInfo.NumberFormat.NumberDecimalDigits = 0;
		}
	}
}